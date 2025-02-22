using Entity.Npc.Enemy;
using Entity.OnWorldItem.Base;
using So.ItemsSo.WeaponsItem.Base;
using System.Collections;
using UnityEngine;

namespace Entity.OnWorldItem.WeaponOnWorld
{

    public class WeaponWorldItemBase : UsableWorldItemBase
    {
        private void Start()
        {
            StartPosition = transform.localPosition; // Save initial position
        }

        public override void UseItem()
        {
            if (CurrentCoroutine == null)
            {
                onActionCollider.enabled = true;
                CurrentCoroutine = StartCoroutine(MoveWeapon());
            }
        }

        private IEnumerator MoveWeapon()
        {
            yield return MoveTo(StartPosition + Vector3.right * 2, ((WeaponSo)currentItem).fireRate);
            yield return MoveTo(StartPosition, ((WeaponSo)currentItem).fireRate);

            OnUseItemEvent();
            
            onActionCollider.enabled = false;
            CurrentCoroutine = null;
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                var enemyComp = other.gameObject.GetComponent<EnemyBase>();
                var weapon = ((WeaponSo)currentItem);
                enemyComp.GetHit(weapon.dps);
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        protected override void SetActionFromData()
        {
            
        }
    }
}