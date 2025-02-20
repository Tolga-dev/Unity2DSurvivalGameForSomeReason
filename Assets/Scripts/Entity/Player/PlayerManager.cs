using Entity.Controllers.Player;
using Manager.Base;
using UnityEngine;

namespace Entity.Player
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
