using System.Collections.Generic;
using Data.ValueObjects;
using DG.Tweening;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEditorInternal;

namespace Controllers.UI
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<TextMeshProUGUI> levelTexts = new List<TextMeshProUGUI>();
        [SerializeField] private TextMeshProUGUI gemText = new TextMeshProUGUI();
        [Space] [SerializeField] private List<Image> stageImages = new List<Image>();

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onSetNewLevelValue += OnSetNewLevelValue;
            UISignals.Instance.onSetStageColor += OnSetStageColor;
            UISignals.Instance.onResetStageColor += OnResetStageColor;
            UISignals.Instance.onSetGem += OnSetGem;

        }

        private void UnSubscribeEvents()
        {
            UISignals.Instance.onSetNewLevelValue -= OnSetNewLevelValue;
            UISignals.Instance.onSetStageColor -= OnSetStageColor;
            UISignals.Instance.onResetStageColor -= OnResetStageColor;
            UISignals.Instance.onSetGem -= OnSetGem;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        private void OnSetNewLevelValue(int levelValue)
        {
            if (levelValue <= 0) levelValue = 1;

            levelTexts[0].text = levelValue.ToString();
            var value = ++levelValue;
            levelTexts[1].text = value.ToString();
        }

        [Button("OnSetStageColor")]
        private void OnSetStageColor(int stageValue)
        {
            stageImages[stageValue].DOColor(Color.red, .35f).SetEase(Ease.Linear);
        }

        private void OnSetGem(int amountGem)
        {
            int gem = amountGem;
            gemText.text = "Gem : "+ gem.ToString();
        }
        
        private void OnResetStageColor()
        {
            for (int i = 0; i < stageImages.Count; i++)
            {
                stageImages[i].color = Color.white;
            }
        }

    }
}