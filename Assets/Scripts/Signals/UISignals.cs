using Extensions;
using Unity.VisualScripting;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<int> onSetNewLevelValue = delegate { };
        public UnityAction<int> onSetStageColor = delegate { };
        public UnityAction onResetStageColor = delegate { };
        public UnityAction<int> onSetGem = delegate {  };
    }
}