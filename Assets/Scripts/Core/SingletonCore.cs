using UnityEngine;

namespace Core
{
    public class SingletonCore<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance => _instance ??= FindObjectOfType<T>();

        protected virtual void Awake()
        {
            if (_instance == null)
                _instance = this as T;
            else
                Destroy(gameObject);
        }
    }
}