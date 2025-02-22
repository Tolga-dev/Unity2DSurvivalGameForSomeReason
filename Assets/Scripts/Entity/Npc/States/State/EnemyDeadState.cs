using System.Collections;
using System.Threading.Tasks;
using Core.StateMachine.Base;
using Entity.Npc.States.Abstract;
using Entity.OnWorldItem.EnemyWeapons;
using Entity.Player;
using Manager;
using So;
using UnityEngine;

namespace Entity.Npc.States.State
{
    public class EnemyDeadState : EnemyState
    {
        private Coroutine _cooldownCoroutine;

        public EnemyDeadState(StateControllerBase stateController) : base(stateController)
        {
        }

        public override void Enter()
        {
            PlayFx(FxSoEnum.EnemyDeathFx, EnemyBase.transform, null,true);
            DropItem();
            GameManager.StartCoroutine(InstantiateEnemyAfterDelay(5f));
        }
        private void DropItem()
        {
            var dropItems = EnemySo.GetDropItems();
            foreach (var dropItem in dropItems)
            {
                GameManager.mapWorldGenerator.SpawnFromItem(dropItem,spawnPointParent:EnemyBase.transform.position);
            }
        }
        private IEnumerator InstantiateEnemyAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Object.Instantiate(GameManager.enemyPrefab, Vector3.zero, Quaternion.identity);
        }
        
        public GameManager GameManager => EnemyBase.gameManager;

    }
}