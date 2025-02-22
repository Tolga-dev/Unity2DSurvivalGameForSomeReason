using Entity.Base;
using Entity.Controllers.Player;
using Manager;
using So;
using UI.PopUps;
using UnityEngine;

namespace Entity.Player
{
	public class PlayerBase : EntityBase
	{
		[Header("Controllers")] 
		public InputController inputController;
		public MovementController movementController;
		public PickUpController pickUpController;
		protected override void Start()
		{
			base.Start();
			StartCoroutine(HungerDepletion());
		}

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
			pickUpController.Update();
		}
		private void FixedUpdate()
		{
			movementController.Update();
		}
		protected override void EntityDeadAction()
		{
			gameManager.fxManager.PlayFx(FxSoEnum.PlayerDeathFx, transform, null,true);
		}

		protected override int CheckForEffects(int damage)
		{
			var totalProtection = inGameUIPopUp.totalArmorProtection;

			if (damage > totalProtection)
			{
				return Mathf.Max(damage - totalProtection, 0);
			}

			float minProtectionFactor = 0.05f; 
			float maxProtectionFactor = 0.5f;  

			float protectionFactor = 
				Mathf.Lerp(minProtectionFactor, maxProtectionFactor,
					Mathf.InverseLerp(0, 1000, totalProtection));
			
			protectionFactor += Random.Range(-0.1f, 0.1f); 
			protectionFactor = Mathf.Clamp(protectionFactor, minProtectionFactor, maxProtectionFactor);

			return Mathf.Max(Mathf.CeilToInt(damage * protectionFactor), 1);
		}

	}
}
