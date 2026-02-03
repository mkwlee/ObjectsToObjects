using UnityEngine;
using TMPro;
using DG.Tweening;
namespace SpaceGame
{
    public class TextUi : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private GameObject button;

        [SerializeField] private float shakeDuration = 5f;
        [SerializeField] private float shakeStrength = 5f;
        [SerializeField] private int shakeVibrato = 0;

        void Start()
        {
            StartPositionShake();
            StartRotationShake();
        }

        void StartPositionShake()
        {
            text.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, 1, false).OnComplete(StartPositionShake);
        }

        void StartRotationShake()
        {
            text.transform.DOShakeRotation(shakeDuration, shakeStrength, shakeVibrato, 1, false).OnComplete(StartRotationShake);
        }

        public void TextSelected()
        {
            text.color = new Color32(184, 184, 184, 255);
            if (button != null)
            {
                button.transform.DOScale(0.7f, 0.5f).SetEase(Ease.OutBack);
            }
            
        }

        public void TextUnselected()
        {
            text.color = new Color32(121, 180, 183, 255);
            if (button != null)
            {
                button.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
            }
            
        }
    }
}