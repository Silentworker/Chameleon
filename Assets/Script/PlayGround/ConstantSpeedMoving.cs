using UnityEngine;

namespace Assets.Script.PlayGround
{
    public class ConstantSpeedMoving : MonoBehaviour
    {
        public float Speed;
        public float RepeatPartSize;

        private bool _motion;
        private RectTransform _rectTransform;
        private float _startXPosition;

        void Start()
        {
            _rectTransform = gameObject.GetComponent<RectTransform>();
            _startXPosition = _rectTransform.anchoredPosition.x;
            _motion = true;
        }

        void Update()
        {
            if (!_motion) return;

            _rectTransform.Translate(Vector2.left * Speed * Time.deltaTime);

            if (_rectTransform.anchoredPosition.x < _startXPosition - RepeatPartSize)
            {
                _rectTransform.anchoredPosition = new Vector3(_startXPosition, _rectTransform.anchoredPosition.y);
            }
        }

        public void Move()
        {
            _motion = true;
        }

        public void Stop()
        {
            _motion = false;
        }
    }
}
