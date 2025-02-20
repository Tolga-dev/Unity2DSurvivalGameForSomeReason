using System;
using Entity.Npc.Enemy;
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
                    enemyBase.PlayerDetectedByIdleCollider();    
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
                    enemyBase.PlayerLostByChaseCollider();   
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
            return IsNpcClose(_playerTransform, distance);
        }

        public bool IsNpcClose(Transform playerTransform, float distance)
        {
            return Vector3.Distance(enemyBase.transform.position, playerTransform.position) < distance;
        }
        public Vector3 PlayerPosition => _playerTransform == null ? enemyBase.transform.position : _playerTransform.position;
    }

}