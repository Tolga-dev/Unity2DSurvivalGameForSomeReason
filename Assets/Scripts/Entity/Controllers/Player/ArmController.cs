using System;
using Entity.Controllers.Base;
using UnityEngine;

namespace Entity.Controllers.Player
{
    [Serializable]
    public class ArmController : ControllerBase
    {
        public Transform shouldTransform;
        public Camera camera;
        
        public override void Update()
        {
            RotateTowardsMouse();
        }
        
        private void RotateTowardsMouse()
        {
            Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector3 direction = (mousePosition - shouldTransform.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            shouldTransform.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        }
        
    }
}