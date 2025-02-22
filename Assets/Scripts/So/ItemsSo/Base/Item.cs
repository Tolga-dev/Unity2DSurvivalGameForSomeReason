using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace So.ItemsSo.Base
{
    public enum ItemTypes { Resource, Consumable, Weapon, Armor,Undefined }

    [CreateAssetMenu(fileName = "Item", menuName = "So/Item", order = 0)]
    public class Item : ScriptableObject
    {
        [SerializeField]
        protected string displayName = string.Empty;
        
        [SerializeField][TextArea]
        protected string description = string.Empty;
        
        [SerializeField]
        protected Sprite icon = null;
        
        [SerializeField]
        protected int maxStackAbleAmount;

        public GameObject onWorldItemPrefab;
        
        public string DisplayName => displayName;
        public Sprite Icon => icon;
        public int MaxStackAbleAmount => maxStackAbleAmount;

        public UnityEvent onUseEvent;
        
        public virtual ItemTypes GetItemType()
        {
            return ItemTypes.Undefined;
        }

        public virtual string GetFullDescription()
        {
            return description;
        }
        public bool CanStackAble()
        {
            return MaxStackAbleAmount != 1;
        }
    }
}