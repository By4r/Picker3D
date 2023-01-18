using System;
using Enums;
using Extensions;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        // #region Singleton
        //
        // public static CoreGameSignals Instance;
        //
        // private void Awake()
        // {
        //     if (Instance != null && Instance != this)
        //     {
        //         Debug.LogWarning("this: " + this.GetInstanceID());
        //         Debug.LogWarning("Singleton: " + Instance.GetInstanceID());
        //         Destroy(gameObject);
        //         return;
        //     }
        //
        //     Instance = this;
        // }
        //
        // #endregion

        public UnityAction<GameStates> onChangeGameState = delegate { };
        public UnityAction<int> onLevelInitialize = delegate { };
        public UnityAction<int> onLevelContinue = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
        public UnityAction onContinue = delegate { };
        public UnityAction<int> onLevelValueChanged = delegate{ };
        public Func<int> onGetLevelID = delegate { return 0; };
        public UnityAction onCollectableCollected = delegate {  };
        public UnityAction<int> onPoolRequiredAmountChanged = delegate { };
        public UnityAction onDecreaseMana = delegate {  };
        public UnityAction onOverBar = delegate {  };

        public UnityAction onStageAreaEntered = delegate { };
        public UnityAction<byte> onStageAreaSuccessful = delegate { };
    }
}