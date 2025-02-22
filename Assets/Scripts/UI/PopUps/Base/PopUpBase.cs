using UnityEngine;

namespace UI.PopUps.Base
{
    public class PopUpBase : MonoBehaviour
    {
        public virtual void OnStartShow()
        {
            Debug.Log("OnStartShow " + GetType());
            
        }
        
        public virtual void OnStartHidden()
        {
            Debug.Log("OnStartHidden " + GetType());
            
        }

        public virtual bool IsActive()
        {
            return gameObject.activeSelf;
        }
    }
}