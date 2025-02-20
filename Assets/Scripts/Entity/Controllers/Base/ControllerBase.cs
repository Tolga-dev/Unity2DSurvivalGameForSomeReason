using System;
using Manager.Base;

namespace Entity.Controllers.Base
{
    [Serializable]
    public class ControllerBase
    {
        protected virtual ManagerBase ManagerBase { get; private set; }
        
        public virtual void Start(ManagerBase managerBase)
        {
            ManagerBase = managerBase;
        }
        public virtual void Update()
        {
            
        }
    }
}