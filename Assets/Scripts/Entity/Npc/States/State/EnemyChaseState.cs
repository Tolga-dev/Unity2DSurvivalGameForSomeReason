using System.Collections;
using Core.StateMachine.Base;
using Entity.Npc.States.Abstract;
using So;
using UnityEngine;
using UnityEngine.Rendering;

namespace Entity.Npc.States.State
{
    public class EnemyChaseState : EnemyState
    {
        private float _lastEffectTime = 0f;  // Tracks when the last effect was spawned

        private Coroutine _speedBoostCoroutine;
        public EnemyChaseState(StateControllerBase stateController) : base(stateController)
        {
        }

        public override void Enter()
        {
            SetFloat(ActionType.Chase);
            PlayerDetectorController.SetIdleCollider(false);
            NavMeshAgent.speed = EnemySo.chaseSpeed; // set from so 
        }

        public override void Update()
        {
            
            if (PlayerDetectorController.IsPlayerClose(EnemySo.longRangeAttack))
                EnemyBase.EnemyStateController.SetToAttackState();
            else
                NavMeshAgent.SetDestination(PlayerDetectorController.PlayerPosition);

            CheckForSpeedBoost();
            CreateWalkEffect();
            
            DrawRange(EnemySo.longRangeAttack);
        }

        private void CreateWalkEffect()
        {
            if (Time.time >= _lastEffectTime + EnemySo.walkStepCoolDown)
            {
                PlayFx(FxSoEnum.FireShoot, EnemyBase.transform,null, true);
                _lastEffectTime = Time.time; 
            }
        }

        private void CheckForSpeedBoost()
        {
            if (_speedBoostCoroutine == null)
            {
                _speedBoostCoroutine = EnemyBase.StartCoroutine(SpeedBoost());
            }
        }
        
        public override void Exit()
        {
            if (_speedBoostCoroutine != null)
            {
                EnemyBase.StopCoroutine(_speedBoostCoroutine);
                _speedBoostCoroutine = null;
            }
            
            PlayerDetectorController.SetIdleCollider(true);
            NavMeshAgent.speed = EnemySo.entitySpeed;
        }
        private IEnumerator SpeedBoost()
        {
            if (Random.value > 0.5f)
                NavMeshAgent.speed =  EnemySo.boostSpeed;
            
            yield return new WaitForSeconds(EnemySo.speedBoostDuration);

            NavMeshAgent.speed = EnemySo.entitySpeed;
            _speedBoostCoroutine = null;
        }
        
        private void DrawRange(float range)
        {
            Vector3 start = EnemyBase.transform.position;
            Vector3 direction = (PlayerDetectorController.PlayerPosition - start).normalized;
            Vector3 end = start + direction * range;
            Debug.DrawLine(start, end, Color.red); 
        }
    }
}