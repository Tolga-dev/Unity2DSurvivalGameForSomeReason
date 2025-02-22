using Entity.Player;
using UnityEngine;

namespace Entity.OnWorldItem.EnemyWeapons
{
    public class ShooterArrow : MonoBehaviour
    {
        private int _damage = 5;
        public void SetDamage(int currentDamage)
        {
            _damage = currentDamage;
        }
        public void OnTriggerEnter2D(Collider2D hit)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<PlayerBase>()?.GetHit(_damage);
                Destroy(gameObject);
            }  
        }
    }
}