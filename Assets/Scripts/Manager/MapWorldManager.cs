using System;
using System.Collections.Generic;
using Entity.OnWorldItem;
using Manager.Base;
using So.ItemsSo.Base;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Manager
{
    public class MapWorldManager : ManagerBase
    {
        public GameManager gameManager;

        public List<Transform> spawnPoints = new List<Transform>();
        public GameObject onWorldItemPrefab;
        
        protected override void Awake()
        {
        }

        private void Start()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                var currentItem = InventoryManager.items[Random.Range(0, InventoryManager.items.Count)];
                SpawnFromItem(currentItem,spawnPoint);
                // spawnedItemBase.SetItemFromData(currentItem, 40); // to test it with big amount
            }
        }
        public void SpawnFromItem(Item currentItem,Transform spawnPoint = null, int amount = 0)
        {
            var spawnedItemBase = CreateNewItemInstance(spawnPoint);
            
            amount = amount == 0 ? Random.Range(1, currentItem.MaxStackAbleAmount) : amount;
            spawnedItemBase.SetItemFromData(currentItem, amount);
        }
        private OnWorldItemBase CreateNewItemInstance(Transform spawnPoint)
        {
            if (spawnPoint == null)
                spawnPoint = gameManager.playerBase.transform;
            
            var spawnedItemBase = Instantiate(onWorldItemPrefab, spawnPoint.position, Quaternion.identity, spawnPoint)
                .GetComponent<OnWorldItemBase>();
            
            spawnedItemBase.transform.localPosition = Vector3.zero;
            spawnedItemBase.transform.localScale = Vector3.one * 0.4f;
            
            spawnedItemBase.transform.SetParent(null);
            
            return spawnedItemBase;
        }

        private InventoryManager InventoryManager => gameManager.inventoryManager;
    }
}