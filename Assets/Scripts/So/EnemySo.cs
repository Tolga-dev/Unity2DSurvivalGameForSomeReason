using System.Collections.Generic;
using System.Linq;
using So.ItemsSo.Base;
using UnityEngine;
using UnityEngine.Serialization;

namespace So
{
    public enum ActionType // use enum for now, in future, implement serialization of interface
    {
        Idle,
        Patrol,
        Attack,
        Chase,
        Walk
    }
    
    [CreateAssetMenu(fileName = "EnemySo", menuName = "So/EnemySo", order = 0)]
    public class EnemySo : EntitySo
    {
        [Header("Chase Values")]
        public float chaseSpeed;
        public float boostSpeed;
        public float speedBoostDuration = 0.5f;
        
        [Header("Animation")]
        public float walkStepCoolDown;

        [Header("Attack Values")]
        public int aoeAttackDamage = 10;
        public float abilityCooldown = 1f;
        public List<Item> canBeDropItems = new List<Item>();

        [Header("Attack Range Values")]
        public float longRangeAttack;
        public float shortRangeAttack;
        public GameObject longRangeAttackPrefab;
        public float projectileSpeed;
        public int arrowDamage = 5;

        [Header("Patrol Values")]
        public float patrolRadius = 10f;
        public float minDistanceToTarget = 0.5f;
        public float toFindNewTargetWaitTime = 1f;

        public List<Item> GetDropItems()
        {
            int itemCount = Random.Range(2, canBeDropItems.Count + 1); 
            return canBeDropItems.OrderBy(x => Random.value).Take(itemCount).ToList(); 
        }

    }

}