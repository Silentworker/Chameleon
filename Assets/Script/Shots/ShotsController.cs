using UnityEngine;

namespace Assets.Script.Shots
{
    public class ShotsController : MonoBehaviour, IShotsController
    {
        public GameObject TouchSensor;

        [Header("Prefabs")]
        public GameObject SimpleShotPrefab;
        public GameObject AdvancedShotPrefab;
        public GameObject MegaShotPrefab;
    }
}
