using Entity.Player;
using UnityEngine;

namespace Entity.OnWorldItem.Border
{
    public class Border : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D hit)
        {
            Destroy(hit.gameObject);
        }
    }
}