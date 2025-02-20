using Controllers.Player;
using Manager.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Manager
{
	public class PlayerManager : ManagerBase
	{
		[Header("Controllers")] 
		public InputController inputController;
		public MovementController movementController;
		public AnimationController animationController;
		
		protected override void Awake()
		{
			inputController.Start(this);
			movementController.Start(this);
			animationController.Start(this);
		}

		private void Update()
		{
			inputController.Update(); 
			animationController.Update();
		}
		private void FixedUpdate()
		{
			movementController.Update();
		}
		
	}
}
