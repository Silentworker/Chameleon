using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Script.PlayGround.Enemy
{
    public class EnemyFactory : MonoBehaviour, IEnemyFactory
    {
        [Inject]
        private DiContainer container;

        public GameObject[] Prefabs;
        public float SpawnDelay;

        public float TopPositionYLimit;
        public float BottomPositionYLimit;

        private float _lastSpawnTime;
        private Dictionary<int, List<GameObject>> _poolDictionary = new Dictionary<int, List<GameObject>>();

        void Start()
        {
            _lastSpawnTime = Time.time;

            for (int i = 0; i < Prefabs.Length; i++)
            {
                _poolDictionary[i] = new List<GameObject>();
            }
        }

        void Update()
        {
            if (Time.time < _lastSpawnTime + SpawnDelay) return;

            _lastSpawnTime = Time.time;

            int index = Random.Range(0, Prefabs.Length);

            GameObject prefab = Prefabs[index];

            List<GameObject> pool = _poolDictionary[index];


            GameObject enemy;
            if (pool.Count > 0)
            {
                enemy = pool.First();
                pool.Remove(enemy);
            }
            else
            {
                enemy = container.InstantiatePrefab(prefab);
                enemy.GetComponent<Enemy>().PoolIndex = index;
            }

            enemy.SetActive(true);
            RectTransform enemyTransform = enemy.GetComponent<RectTransform>();

            enemyTransform.SetParent(gameObject.transform, false);
            enemy.SetActive(true);

            float XPosition = gameObject.GetComponent<RectTransform>().rect.width / 2 + enemyTransform.sizeDelta.x;
            float YPosition = Random.Range(BottomPositionYLimit, TopPositionYLimit);
            enemyTransform.anchoredPosition = new Vector2(XPosition, YPosition);
            Debug.LogFormat("POsition: {0}  {1}", XPosition, YPosition);
        }

        public void ReturnToPool(GameObject enemy)
        {
            enemy.SetActive(false);
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent.Clear();
            List<GameObject> pool = _poolDictionary[enemyComponent.PoolIndex];
            pool.Add(enemy);
        }
    }
}