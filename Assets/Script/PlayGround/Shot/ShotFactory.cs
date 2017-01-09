using System.Collections.Generic;
using System.Linq;
using Assets.Script.Chameleon;
using Assets.Script.Consts;
using UnityEngine;
using Zenject;

namespace Assets.Script.PlayGround.Shot
{
    public class ShotFactory : MonoBehaviour
    {
        [Inject]
        private DiContainer container;
        [Inject]
        private IChameleonController chameleonController;

        public float ShotDelay;
        public float ShotSpeed;

        public RectTransform Folder;
        public RectTransform MouthPoint;

        [Header("Prefabs")]
        public GameObject SimpleShotPrefab;
        public GameObject AdvancedShotPrefab;
        public GameObject MegaShotPrefab;

        private List<GameObject> _simpleShotPool = new List<GameObject>();
        private List<GameObject> _anvancedShotPool = new List<GameObject>();
        private List<GameObject> _megaShotPool = new List<GameObject>();

        private int _lasTouchID;
        private float _lastShotTime;
        private float _touchStartTime;
        private float _lastClickTime;
        private ShotProgress _progress;

        public void ReturnShotToPool(GameObject shot)
        {

        }

        void Start()
        {
            _progress = GetComponent<ShotProgress>();
        }

        void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN

            if (Input.GetMouseButtonDown(0))
            {
                _lastClickTime = Time.time;
            }
            else if (Input.GetMouseButton(0))
            {
                ShowProgress(Time.time - _lastClickTime);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _progress.HideProgress();
                if (Time.time > _lastShotTime + ShotDelay)
                {
                    Vector3 mousePosition = Input.mousePosition;
                    SpawnShot(Time.time - _lastClickTime, new Vector2(mousePosition.x, mousePosition.y));
                    _lastShotTime = Time.time;
                }
            }
#endif

#if (UNITY_IPHONE || UNITY_ANDROID)
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    if (_lasTouchID != touch.fingerId)
                    {
                        _lasTouchID = touch.fingerId;
                        _lastShotTime = Time.time;
                    }
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    if (_lasTouchID == touch.fingerId)
                    {
                        //SpawnShot(Time.time - _lastShotTime, position);   //todo 
                        _lastShotTime = Time.time;
                    }
                }
                else
                {
                    ShowProgress(Time.time - _lastShotTime);
                }
            }
#endif
        }

        private void SpawnShot(float progress, Vector2 position)
        {
            Vector2 odds = position - MouthPoint.anchoredPosition;
            float angle = Mathf.Atan2(odds.y, odds.x) * Mathf.Rad2Deg;
            angle = angle < 0 ? 0 : angle > 90 ? 90 : angle;

            Debug.LogFormat("SHOT: {0}, position {1} {2}. angle {3}", progress, position.x, position.y, angle);


            GameObject prefab = null;
            List<GameObject> pool = null;
            byte shotType = ShotType.Simple;
            float damage = 0;
            float maxAdvanceDamage = Damage.AdvancedMaxFactor * Damage.BaseShot;
            float maxMegaDamage = Damage.MegaMaxFactor * Damage.BaseShot;

            if (progress < Duration.ShotBecameAdvanced)
            {
                prefab = SimpleShotPrefab;
                pool = _simpleShotPool;
                shotType = ShotType.Simple;
                damage = Damage.BaseShot;
            }
            else if (progress < Duration.ShotBecameMega)
            {
                prefab = AdvancedShotPrefab;
                pool = _anvancedShotPool;
                shotType = ShotType.Advanced;

                //float advancedProgress =

                damage = Damage.BaseShot;
            }
            else if (progress <= Duration.ShotTouchLimit)
            {
                prefab = MegaShotPrefab;
                pool = _megaShotPool;
                shotType = ShotType.Mega;

                damage = Damage.BaseShot;
            }

            GameObject shot;

            if (pool.Count > 0)
            {
                shot = pool.First();
                pool.Remove(shot);
            }
            else
            {
                shot = container.InstantiatePrefab(prefab);
            }

            RectTransform shotTransform = shot.GetComponent<RectTransform>();
            shotTransform.SetParent(Folder, false);
            shotTransform.position = MouthPoint.position;
            shotTransform.rotation = Quaternion.Euler(0, 0, angle);

            Shot shotComponent = shot.GetComponent<Shot>();
            shotComponent.Damage = damage;
            shotComponent.Type = shotType;

            chameleonController.Attack(angle);
        }

        private void ShowProgress(float progress)
        {
            if (progress < Duration.ShotTouchLimit)
            {
                _progress.ShowProgress(progress);
            }
            else
            {
                _progress.HideProgress();
            }
        }
    }
}
