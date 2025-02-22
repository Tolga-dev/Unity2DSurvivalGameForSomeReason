using Entity.Npc.Enemy;
using Entity.OnWorldItem.Base;
using So.ItemsSo.ConsumableItems;
using UnityEngine;

namespace Entity.OnWorldItem.WeaponOnWorld
{
    public class ShootWorldItemBase : UsableWorldItemBase
    {
        [SerializeField] private float speed;
        private Vector2 _direction;
        public void Initialize(Vector2 direction)   
        {
            speed = ((DamagingConsumable)currentItem).speed;
            _direction = direction.normalized;
        }
        public override void UseItem()
        {
        }
        private void Update()
        {
            transform.position += (Vector3)_direction * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(onActionCollider.enabled == false)
                return;
            
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyBase>().GetHit(((DamagingConsumable)currentItem).effectAmount);
                Destroy(gameObject);
            }
            else if (other.CompareTag("Obstacle"))
            {
                speed = 0;   
                Destroy(gameObject, 2);
            }
        }
    }
}