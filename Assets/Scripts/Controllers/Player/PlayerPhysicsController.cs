using UnityEngine;
using Signals;
using Managers;
using Controllers.Pool;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
    
        [SerializeField] private PlayerManager manager;
        [SerializeField] private LevelManager  levelManager;

        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;

        #endregion
        
        #region Private Variables
        
        [ShowInInspector] private bool _isOver;
        
        #endregion
        
        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("StageArea"))
            {

                manager.ForceCommand.Execute();
                CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();

                DOVirtual.DelayedCall(3, () =>
                {
                    var result = other.transform.parent.GetComponentInChildren<PoolController>()
                        .TakeStageResult(manager.StageID);
                    if (result)
                    {
                        CoreGameSignals.Instance.onStageAreaSuccessful?.Invoke(manager.StageID);
                        UISignals.Instance.onSetStageColor?.Invoke(manager.StageID);
                        InputSignals.Instance.onEnableInput?.Invoke();
                        manager.StageID++;
                    }
                    else CoreGameSignals.Instance.onLevelFailed?.Invoke();
                });
                return;
            }
            if (other.CompareTag("Finish"))
            {
                //levelManager.levelID++;
                CoreGameSignals.Instance.onContinue?.Invoke();
                UISignals.Instance.onResetStageColor?.Invoke();
                CoreGameSignals.Instance.onDecreaseMana?.Invoke();
                //UISignals.Instance.onSetNewLevelValue?.Invoke(levelManager.levelID);
            }
            if (other.CompareTag("Collectable"))
            {
                CoreGameSignals.Instance.onCollectableCollected?.Invoke();
            }
            if (other.CompareTag("Gem"))
            {   
                Debug.Log("COREGAMESINYALI ONOVERBAR");
                
                CoreGameSignals.Instance.onOverBar?.Invoke();
            }
            if (other.CompareTag("Boost"))
            {
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            }
            
        }
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var transform1 = manager.transform;
            var position = transform1.position;
            Gizmos.DrawSphere(new Vector3(position.x, position.y - 1.2f, position.z + 1f), 1.65f);
        }

        public void OnReset()
        {
            
        }
    }
}