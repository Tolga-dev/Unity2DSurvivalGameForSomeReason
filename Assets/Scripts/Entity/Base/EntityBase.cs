using System.Collections;
using Entity.Controllers.Base;
using Manager;
using Manager.Base;
using So;
using TMPro;
using UI.PopUps;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Entity.Base
{
    public class EntityBase : ManagerBase
    {
        [Header("Entity")]
        public GameManager gameManager;
        public EntitySo entitySo;
        
        public AnimationController animationController;

        [Header("UI")] 
        public InGameUIPopUp inGameUIPopUp;
        public Transform healthLoseIndicator;
        
        public Image healthBar;
        public Image hungerBar;
        
        private int _health;
        private int _hunger;
        
        protected virtual void Start()
        {
            _health = entitySo.entityMaxHealth;
            _hunger = entitySo.entityMaxHunger;

            UpdateBars();
            animationController.Start(this);
        }

        public void GetHit(int damage)
        {
            damage = CheckForEffects(damage);
            _health = Mathf.Max(_health - damage, 0);
            UpdateBars();
            CheckDeath();
            SpawnHealthLoseIndicator(damage);
            gameManager.fxManager.PlayFx(FxSoEnum.BloodFx, transform, null,true);
        }

        protected virtual int CheckForEffects(int damage)
        {
            return damage;
        }

        public void ConsumeFood(int foodValue)
        {
            _hunger = Mathf.Min(_hunger + foodValue, entitySo.entityMaxHunger);
            UpdateBars();
        }

        public void ConsumeHealth(int healthValue)
        {
            _health = Mathf.Min(_health + healthValue, entitySo.entityMaxHealth);
            UpdateBars();
        }

        public IEnumerator HungerDepletion()
        {
            while (_health > 0)
            {
                yield return new WaitForSeconds(1f);

                if (_hunger > 0)
                {
                    _hunger = Mathf.Max(_hunger - (int)entitySo.hungerLossRate, 0);
                }
                else
                {
                    _health = Mathf.Max(_health - entitySo.starvationDamage, 0);
                }

                UpdateBars();
                CheckDeath();
            }
        }

        private void CheckDeath()
        {
            if (_health <= 0)
            {
                EntityDeadAction();
                Destroy(gameObject);
                StopAllCoroutines();
            }
        }

        protected virtual void EntityDeadAction()
        {
            Debug.Log("Entity has died.");
        }
        private void UpdateBars()
        {
            if (healthBar != null)
                healthBar.fillAmount = (float)_health / entitySo.entityMaxHealth;

            if (hungerBar != null)
                hungerBar.fillAmount = (float)_hunger / entitySo.entityMaxHunger;
        }

        private void SpawnHealthLoseIndicator(int damageAmount)
        {
            var healthLoseIndicatorPrefab = inGameUIPopUp.healthLoseIndicatorPrefab;
            Canvas indicator = Instantiate(healthLoseIndicatorPrefab, healthLoseIndicator.position, Quaternion.identity, healthLoseIndicator);
            indicator.transform.localPosition = Vector3.zero;
    
            TextMeshProUGUI textComponent = indicator.GetComponentInChildren<TextMeshProUGUI>();
            textComponent.text = "-" + damageAmount;
            
            StartCoroutine(AnimateHealthLoseIndicator(indicator.transform));
        }

        private IEnumerator AnimateHealthLoseIndicator(Transform indicatorTransform)
        {
            float elapsedTime = 0f;
            Vector3 startPosition = indicatorTransform.localPosition;

            var duration = inGameUIPopUp.duration;
            var moveSpeed = inGameUIPopUp.moveSpeed;
            
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                indicatorTransform.localPosition = startPosition + Vector3.up * (moveSpeed * elapsedTime);
                yield return null;
            }

            Destroy(indicatorTransform.gameObject);
        }

        
    }
}