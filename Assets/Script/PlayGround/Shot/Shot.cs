using UnityEngine;

namespace Assets.Script.PlayGround.Shot
{
    public class Shot : MonoBehaviour
    {
        public float Speed;

        public float Damage { get; set; }
        public byte Type { get; set; }

        private RectTransform _rectTransform;

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        void Update()
        {
            _rectTransform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
    }
}