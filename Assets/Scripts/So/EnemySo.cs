using System;
using System.Collections.Generic;
using System.Linq;
using Core.StateMachine.Interface;
using UnityEngine;
using UnityEngine.Serialization;

namespace So
{
    public enum ActionType // use enum for now, in future, implement serialization of interface
    {
        Idle,
        Patrol,
        Attack,
        Chase
    }
    
    [CreateAssetMenu(fileName = "EnemySo", menuName = "So/EnemySo", order = 0)]
    public class EnemySo : EntitySo
    {
        public List<StateActionAnimationId> animationIds = new();
        
        public StateActionAnimationId GetAnimationIdFromActionType(ActionType actionType)
        {
            return animationIds.FirstOrDefault(x => x.actionType == actionType);
        }
        
    }
    
    [Serializable]
    public class StateActionAnimationId
    {
        public string name = "Action";
        public ActionType actionType;
        public float animationValue;
    }
 
}