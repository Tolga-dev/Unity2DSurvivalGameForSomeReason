using System;
using Entity.Controllers.Base;
using Entity.Player;
using Manager;
using Manager.Base;
using UnityEngine;

namespace Entity.Controllers.Player
{
    [Serializable]
    public class AnimationController : ControllerBase
    {
        private MovementController _movementController;
        [SerializeField] private SpriteRenderer playerSpriteRenderer;

        private Color _upColor = Color.blue;
        private Color _downColor = Color.green;
        private Color _leftColor = Color.yellow;
        private Color _rightColor = Color.red;
        
        public override void Start(ManagerBase playerBase)
        {
            base.Start(playerBase);
            _movementController = PlayerManager.movementController;
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


        public PlayerManager PlayerManager => (PlayerManager)ManagerBase;
    }
}
