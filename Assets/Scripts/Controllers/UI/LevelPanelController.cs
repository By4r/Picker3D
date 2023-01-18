using System.Collections.Generic;
using Data.ValueObjects;
using DG.Tweening;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Controllers.UI
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<TextMeshProUGUI> levelTexts = new List<TextMeshProUGUI>();
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
        }

        private void UnSubscribeEvents()
        {
            UISignals.Instance.onSetNewLevelValue -= OnSetNewLevelValue;
            UISignals.Instance.onSetStageColor -= OnSetStageColor;
            UISignals.Instance.onResetStageColor -= OnResetStageColor;
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


        // private void OnResetStageColor()
        // {
        //     foreach (var VARIABLE in stageImages)
        //     {
        //         stageImages[Image].DOColor(Color.white, .05f).SetEase(Ease.Linear);
        //     }
        //     
        //     
        // }
        
        private void OnResetStageColor()
        {
            for (int i = 0; i < stageImages.Count; i++)
            {
                stageImages[i].color = Color.white;
            }
        }

    }
}