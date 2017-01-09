using Assets.Script.Consts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.PlayGround.Shot
{
    public class ShotProgress : MonoBehaviour
    {
        public Image ProgressImage;
        public RectTransform ProgressTransform;
        public float ProgressMaxWidth;

        private float _startXPosition;

        void Start()
        {
            _startXPosition = ProgressTransform.anchoredPosition.x - ProgressMaxWidth / 2;
            HideProgress();
        }

        public void ShowProgress(float progress)
        {
            float width = ProgressMaxWidth * progress / Duration.ShotTouchLimit;
            ProgressTransform.sizeDelta = new Vector2(width, ProgressTransform.sizeDelta.y);
            ProgressTransform.anchoredPosition = new Vector2(_startXPosition + width / 2, ProgressTransform.anchoredPosition.y);

            ProgressImage.color = progress < Duration.ShotBecameAdvanced ? Color.green
                : progress < Duration.ShotBecameMega ? Color.yellow
                : Color.red;
        }


        public void HideProgress()
        {
            ProgressTransform.sizeDelta = new Vector2(0, ProgressTransform.sizeDelta.y);
            ProgressTransform.anchoredPosition = new Vector2(_startXPosition, ProgressTransform.anchoredPosition.y);
        }
    }
}
