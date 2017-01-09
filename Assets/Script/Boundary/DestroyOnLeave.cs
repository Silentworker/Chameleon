using UnityEngine;

namespace Assets.Script
{
    public class DestroyOnLeave : MonoBehaviour
    {
        void OnTriggerExit(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}
