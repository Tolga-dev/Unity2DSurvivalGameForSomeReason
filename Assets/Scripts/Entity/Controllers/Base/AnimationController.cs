using System;
using Entity.Base;
using So;
using UnityEngine;

namespace Entity.Controllers.Base
{
    [Serializable]
    public class AnimationController : ControllerBase
    {
        public Animator animator;

        public void SetFloat(ActionType actionType)
        {
            var id = EnemyBase.entitySo.GetAnimationIdFromActionType(actionType);
            animator.SetFloat(id.name, id.animationValue);
        }
        public EntityBase EnemyBase => (EntityBase)ManagerBase;
    }
    
    // This is the original code for color animation without animation controller. for every direction it has different color.
    // I changed to usual animator controller. but if u want u can you this class
    
    /*public class ColorAnimationController :ControllerBase
    {
        private MovementController _movementController;
        private PlayerBase _playerBase;

        [SerializeField] private SpriteRenderer playerSpriteRenderer;

        private Color _upColor = Color.blue;
        private Color _downColor = Color.green;
        private Color _leftColor = Color.yellow;
        private Color _rightColor = Color.red;
        
        public override void Start(ManagerBase playerBase)
        {
            base.Start(playerBase);
            _playerBase = (PlayerBase) playerBase;
            _movementController = _playerBase.movementController;
        }

        public override void Update()
        {
            AnimateColor();
        }

        private void AnimateColor()
        {
            var speedRatio = Mathf.Clamp01(_movementController.CurrentVelocity / _movementController.MaxVelocity);
            var max = GetBaseColor(_movementController.CurrentDirection);
            
            playerSpriteRenderer.color = Color.Lerp(Color.white, max, speedRatio);
        }

        private Color GetBaseColor(MovementDirection direction)
        {
            if (direction == MovementDirection.None)
            {
                return Color.white;
            }

            Color max = Color.black; 
            int directionCount = 0;

            if (_movementController.IsUp)  
            { 
                max += _upColor; 
                directionCount++; 
            }
            if (_movementController.IsDown)  
            { 
                max += _downColor; 
                directionCount++; 
            }
            if (_movementController.IsRight)  
            { 
                max += _rightColor; 
                directionCount++; 
            }
            if (_movementController.IsLeft)  
            { 
                max += _leftColor; 
                directionCount++; 
            }
            return max / Mathf.Max(1, directionCount);
        }
    }*/
}
