using System;
using Entity.Controllers.Base;
using Entity.Player;
using Manager;
using Manager.Base;
using So;
using UnityEngine;

namespace Entity.Controllers.Player
{
    [Flags]
    public enum MovementDirection
    {
        None = 0,
        Up = 1 << 0,    // 0001
        Down = 1 << 1,  // 0010
        Left = 1 << 2,  // 0100
        Right = 1 << 3  // 1000
    }
    
    [Serializable]
    public class MovementController : ControllerBase
    {
        private PlayerBase _playerBase;
        
        [SerializeField] private float smoothDampParameter = 0.2f;
        [SerializeField] private float velocityConstant = 0.2f;

        private Rigidbody2D _rb;
        private Vector2 _velocity = Vector2.zero;
        private float _maxSpeed;
        private MovementDirection _direction;
        public override void Start(ManagerBase playerBase)
        {
            base.Start(playerBase);
            _playerBase = (PlayerBase)playerBase;

            _rb = playerBase.GetComponent<Rigidbody2D>();
            InputController = _playerBase.inputController;

            _maxSpeed = Time.fixedDeltaTime * velocityConstant;
        }

        public override void Update()
        {
            Move();
            SetDirection();
        }

        private void SetDirection()
        {
            _direction = GetDirection();
        }
        private void Move()
        {
            var x = InputController.MovementInput.x * _maxSpeed;
            var y = InputController.MovementInput.y * _maxSpeed;
            Vector2 targetVelocity = new Vector2(x, y);

            _rb.velocity = Vector2.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, smoothDampParameter);

            if (CurrentVelocity > MaxVelocity) // change this part later - unnecessary check in long term
            {
                _rb.velocity = _rb.velocity.normalized * _maxSpeed;
            }
        }

        public MovementDirection GetDirection()
        {
            if (CurrentVelocity < 0.01f)
            {
                _playerBase.animationController.SetFloat(ActionType.Idle);
                return MovementDirection.None;
            }

            var normalizedVelocity = _rb.velocity.normalized;
            var direction = MovementDirection.None;

            switch (normalizedVelocity.x)
            {
                case > 0.1f:
                    direction |= MovementDirection.Right;
                    break;
                case < -0.1f:
                    direction |= MovementDirection.Left;
                    break;
            }

            switch (normalizedVelocity.y)
            {
                case > 0.1f:
                    direction |= MovementDirection.Up;
                    break;
                case < -0.1f:
                    direction |= MovementDirection.Down;
                    break;
            }
            _playerBase.animationController.SetFloat(ActionType.Walk);

            return direction;
        }

        public float CurrentVelocity => _rb.velocity.magnitude;
        public float MaxVelocity => _maxSpeed;
        public MovementDirection CurrentDirection => _direction;
        public InputController InputController { get; set; } 
        
        public bool IsUp => ((_direction & MovementDirection.Up) != 0);
        public bool IsDown => ((_direction & MovementDirection.Down) != 0);
        public bool IsRight => ((_direction & MovementDirection.Right) != 0);
        public bool IsLeft => ((_direction & MovementDirection.Left) != 0);
    }

}