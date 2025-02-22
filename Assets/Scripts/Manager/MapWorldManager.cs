using System;
using System.Collections.Generic;
using Entity.OnWorldItem;
using Entity.OnWorldItem.Base;
using Manager.Base;
using So.ItemsSo.Base;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Manager
{
    public class MapWorldManager : ManagerBase
    {
        public GameManager gameManager;

        public List<Transform> spawnPoints = new List<Transform>();
        
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
        public void SpawnFromItem(Item currentItem,Transform spawnPoint = null, int amount = 0, Vector3? spawnPointParent = null)
        {
            var spawnedItemBase = CreateNewItemInstance(currentItem,spawnPoint, spawnPointParent);
            
            amount = amount == 0 ? Random.Range(1, currentItem.MaxStackAbleAmount) : amount;
            spawnedItemBase.SetItemFromData(currentItem, amount, InventoryManager);
        }
        
        private WorldItemBase CreateNewItemInstance(Item currentItem,Transform spawnPoint, Vector3? spawnPointParent = null)
        {
            Vector3 spawnPointParentVector3;
            
            if (spawnPointParent == null)
            {
                if (spawnPoint == null)
                    spawnPoint = gameManager.playerBase.transform;
                
                spawnPointParentVector3 = spawnPoint.position;
            }
            else
            {
                spawnPointParentVector3 = (Vector3)spawnPointParent;
            }
            
            var spawnedItemBase = Instantiate(currentItem.onWorldItemPrefab, spawnPointParentVector3, Quaternion.identity, spawnPoint)
                .GetComponent<WorldItemBase>();
            
            if(spawnPoint != null)
                spawnedItemBase.transform.localPosition = Vector3.zero;
            spawnedItemBase.transform.localScale = Vector3.one * 0.4f;
            
            spawnedItemBase.transform.SetParent(null);
            
            return spawnedItemBase;
        }

        private InventoryManager InventoryManager => gameManager.inventoryManager;
    }
}