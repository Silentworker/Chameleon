using Assets.Script.Consts;
using Assets.Script.PlayGround.Shot;
using Assets.Script.Score;
using UnityEngine;
using Zenject;

namespace Assets.Script.PlayGround.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [Inject]
        private IScoreManager scoreManager;

        [Inject]
        private IShotFactory shotFactory;

        private float _health;
        private RectTransform _rectTransform;
        private Rigidbody2D _rigidbody;
        private float _startProgressXPosition;

        public float MaxHealth;
        public float Speed;
        public Animator Animator;

        [Header("Progress")]
        public RectTransform ProgressTransform;
        public float ProgessMaxWidth;


        public int PoolIndex { get; set; }

        public bool Dead
        {
            get { return _health <= 0; }
        }

        public void Init()
        {
            Animator.Play(0);
            _rigidbody.gravityScale = 0;
            _health = MaxHealth;
            ShowProgress();
            _startProgressXPosition = ProgressTransform.anchoredPosition.x;
        }

        public void Clear()
        {
            Init();
        }

        void OnEneble()
        {
            Init();
        }

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rigidbody = GetComponent<Rigidbody2D>();
            Init();
        }

        void Update()
        {
            _rectTransform.Translate(Vector2.left * Speed * Time.deltaTime);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == Tag.Shot && !Dead)
            {
                ApplyDamage(other.GetComponent<Shot.Shot>().Damage);
                shotFactory.ReturnShotToPool(other.gameObject);
            }
        }

        private void ApplyDamage(float damage)
        {
            if (Dead) return;

            _health -= damage;
            _health = _health < 0 ? 0 : _health;
            ShowProgress();
            Debug.LogFormat("damage: {0}", damage);

            if (!Dead) return;

            Animator.Stop();
            _rigidbody.gravityScale = 1;

            scoreManager.AddScore(Consts.Score.KillEnemy);
        }

        public void ShowProgress()
        {
            float width = ProgessMaxWidth * _health / MaxHealth;
            ProgressTransform.sizeDelta = new Vector2(width, ProgressTransform.sizeDelta.y);
            ProgressTransform.anchoredPosition = new Vector2(_startProgressXPosition + (ProgessMaxWidth - width) / 2, ProgressTransform.anchoredPosition.y);
        }
    }
}