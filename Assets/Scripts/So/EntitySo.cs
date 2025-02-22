using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace So
{
    [CreateAssetMenu(fileName = "EntitySo", menuName = "So/EntitySo", order = 0)]
    public class EntitySo : ScriptableObject
    {
        public string entityName;
        public string entityDescription;
        
        public int entityMaxHealth;
        public int entityMaxHunger;
        
        public int entitySpeed;
        
        public float hungerLossRate = 1f;
        public int starvationDamage = 1; // Damage per second when hunger is 0

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