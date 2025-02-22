using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace So
{
    public enum FxSoEnum
    {
        FireShoot,
        FireWalk,
        FireAoe,
        EnemyDeathFx,
        PlayerDeathFx,
        BloodFx,
    }
    
    [CreateAssetMenu(fileName = "FxSo", menuName = "So/FxSo", order = 0)]
    public class FxSo : ScriptableObject
    {
        public GameObject fxPrefab;
        public List<FxSoData> fxSoData = new List<FxSoData>();
        
        public GameObject GetFxPrefab(FxSoEnum fxSoEnum)
        {
            return (from fxData in fxSoData where fxData.fxSoEnum == fxSoEnum select fxData.fxPrefab).FirstOrDefault();
        }
    }
    
    [Serializable]
    public class FxSoData
    {
        public FxSoEnum fxSoEnum;
        public GameObject fxPrefab;
    }
}