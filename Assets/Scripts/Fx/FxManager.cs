using System.Collections;
using Manager.Base;
using So;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fx
{
    public class FxManager : ManagerBase
    {
        public FxSo fxSo;

        public GameObject PlayFx(FxSoEnum fxSoEnum, Transform parentForPosition, Transform parent = null, bool durationDestroy = false)
        {
            var fxParticle = fxSo.GetFxPrefab(fxSoEnum);
            if (fxParticle == null) return null;

            parent = parent == null ? transform : parentForPosition;
            
            var fxPrefab = Instantiate(fxSo.fxPrefab, parentForPosition.transform.position, Quaternion.identity, parent);
            var createdFxParticle = Instantiate(fxParticle, fxPrefab.transform.position, Quaternion.identity, fxPrefab.transform);

            var particle = createdFxParticle.GetComponentInChildren<ParticleSystem>();
            particle.Play();

            var duration= particle.main.duration + particle.main.startLifetimeMultiplier;

            if (durationDestroy)
                Destroy(fxPrefab, duration);
    
            return fxPrefab;
        }

    }
}