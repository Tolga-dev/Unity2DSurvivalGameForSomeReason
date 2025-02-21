using Entity.Controllers.Player;
using Entity.OnWorldItem;
using Manager;
using Manager.Base;
using So.ItemsSo.Base;
using TMPro;
using UI.PopUps;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entity.Player
{
	public class PlayerBase : ManagerBase
	{
		[Header("Controllers")] 
		public InputController inputController;
		public MovementController movementController;
		public AnimationController animationController;
		public PickUpController pickUpController;

		public GameManager gameManager;
		public InGameUIPopUp inGameUIPopUp;
		
		protected override void Awake()
		{
			inputController.Start(this);
			movementController.Start(this);
			animationController.Start(this);
			pickUpController.Start(this);
			
			inGameUIPopUp.SetUI();
		}

		private void Update()
		{
			inputController.Update(); 
			animationController.Update();
			pickUpController.Update();
		}
		private void FixedUpdate()
		{
			movementController.Update();
		}
	}
}
