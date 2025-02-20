using UnityEngine;

namespace So
{
    [CreateAssetMenu(fileName = "EntitySo", menuName = "So/EntitySo", order = 0)]
    public class EntitySo : ScriptableObject
    {
        public string entityName;
        public string entityDescription;
        public int entityHealth;
        public int entitySpeed;
    }
}