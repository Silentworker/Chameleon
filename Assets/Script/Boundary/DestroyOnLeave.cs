using System.Runtime.InteropServices;
using Assets.Script.PlayGround.Shot;
using UnityEngine;
using Zenject;

namespace Assets.Script.Boundary
{
    public class DestroyOnLeave : MonoBehaviour
    {
        [Inject]
        private IShotFactory shotFactory;

        void OnTriggerExit(Collider other)
        {
            if (other.tag == "Shot")
            {
                shotFactory.ReturnShotToPool(other.gameObject);
            }

        }
    }
}
