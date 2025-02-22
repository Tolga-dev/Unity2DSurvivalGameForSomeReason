using System;
using Entity.Npc.Enemy;
using Entity.Npc.States.State;
using UnityEngine;

namespace Entity.Controllers.Npc.Enemy
{
    public class PlayerDetectorController : MonoBehaviour
    {
        private Transform _playerTransform;
        public EnemyBase enemyBase;
    
        public CircleCollider2D idleCollider;  
        public CircleCollider2D chaseCollider;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerTransform = other.transform;

                if (idleCollider.enabled)
                {
                    enemyBase.EnemyStateController.SetState<EnemyChaseState>(); 
                }
            }
        }
        
        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerTransform = other.transform;

                if (chaseCollider.enabled)
                {
                    enemyBase.EnemyStateController.SetState<EnemyIdleState>();
                }                
            }
        }
        
        public void SetIdleCollider(bool b)
        {
            idleCollider.enabled = b;
            chaseCollider.enabled = !b;
        }
        
        public bool IsPlayerClose(float distance)
        {
            return _playerTransform != null && IsNpcClose(_playerTransform, distance);
        }

        private bool IsNpcClose(Transform playerTransform, float distance)
        {
            return Vector3.Distance(enemyBase.transform.position, playerTransform.position) < distance;
        }
        public float GetPlayerDistance()
        {
            return Vector3.Distance(enemyBase.transform.position, _playerTransform.position);
        }
        
        public Vector3 PlayerPosition => _playerTransform == null ? enemyBase.transform.position : _playerTransform.position;
    }

}