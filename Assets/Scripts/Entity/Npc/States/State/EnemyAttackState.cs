using System.Collections;
using System.Threading.Tasks;
using Core.StateMachine.Base;
using Entity.Npc.States.Abstract;
using Entity.OnWorldItem.EnemyWeapons;
using Entity.Player;
using So;
using UnityEngine;

namespace Entity.Npc.States.State
{
    public class EnemyAttackState : EnemyState
    {
        private Coroutine _cooldownCoroutine;
        

        public EnemyAttackState(StateControllerBase stateController) : base(stateController)
        {
        }

        public override void Enter()
        {
            SetFloat(ActionType.Attack);

            if (_cooldownCoroutine == null)
            {
                NavMeshAgent.SetDestination(EnemyBase.transform.position);
                _cooldownCoroutine = EnemyBase.StartCoroutine(UseSpecialAbility());
            }
        }
        private IEnumerator UseSpecialAbility()
        {
            var distance = PlayerDetectorController.GetPlayerDistance();

            if (distance < EnemySo.shortRangeAttack)
                PerformAoeAttack();
            else
                FireProjectile();
            
            yield return new WaitForSeconds(EnemySo.abilityCooldown);
            
            EnemyStateController.SetState<EnemyChaseState>();
            _cooldownCoroutine = null;
        }

        private void FireProjectile()
        {
            Debug.Log("Enemy fires a projectile!");
            
            GameObject projectile = Object.Instantiate(EnemySo.longRangeAttackPrefab, EnemyBase.transform.position,
                Quaternion.identity);
            
            PlayFx(FxSoEnum.FireShoot, projectile.transform,projectile.transform);
            
            projectile.GetComponent<ShooterArrow>().SetDamage(EnemySo.arrowDamage);
            projectile.GetComponent<Rigidbody2D>().velocity =
                (PlayerDetectorController.PlayerPosition - EnemyBase.transform.position).normalized * EnemySo.projectileSpeed;
        }

        private void PerformAoeAttack()
        {
            PlayFx(FxSoEnum.FireAoe, EnemyBase.transform, EnemyBase.transform,true);

            Debug.Log("Enemy performs an AOE attack!");
            Collider2D[] hitPlayers = new Collider2D[10];
            Physics2D.OverlapCircleNonAlloc(EnemyBase.transform.position, 3f, hitPlayers);
            foreach (Collider2D hit in hitPlayers)
            {
                if(hit == null )
                    continue;
                
                if (hit.CompareTag("Player"))
                {
                    hit.GetComponent<PlayerBase>()?.GetHit(EnemySo.aoeAttackDamage);
                }            
            }
        }
        
        
       
    }
}