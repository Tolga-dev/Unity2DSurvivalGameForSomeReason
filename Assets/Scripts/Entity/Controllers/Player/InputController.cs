using System;
using Entity.Controllers.Base;
using UnityEngine;

namespace Entity.Controllers.Player
{
    [Serializable]
    public class InputController : ControllerBase
    {
        private Vector2 _input = Vector2.zero;

        public override void Update()
        {
            _input = GetInput();
        }
        
        private Vector2 GetInput()
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        
        public Vector2 MovementInput => _input;


    }
}