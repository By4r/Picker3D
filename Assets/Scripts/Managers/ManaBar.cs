using Controllers.Player;
using UnityEngine;
using UnityEngine.UI;
using Signals;
using Controllers.Pool;

namespace Managers
{
    public class ManaBar : MonoBehaviour
    {
        [SerializeField] private PlayerPhysicsController playerPhysicsController;

        [SerializeField] private PoolController poolController;
        [SerializeField] private int _requiredAmount;

        public static ManaBar instance;

        #region BarVariables

        private int MANA_MAX = 50;
        private float manaAmount = 0;
        private float manaRegenAmount = 25f;
        private float barMaskWidth;
        private RectTransform barMaskRectTransform;
        private RectTransform edgeRectTransform;
        private RawImage barRawImage;
        private bool isStarted = false;
        private bool isDecrease;
        private bool isOverBar;

        #endregion


        public static ManaBar Instance { get; private set; }

        private void Awake()
        {
            isDecrease = false;

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            barMaskRectTransform = transform.Find("barMask").GetComponent<RectTransform>();
            barRawImage = transform.Find("barMask").Find("bar").GetComponent<RawImage>();
            edgeRectTransform = transform.Find("edge").GetComponent<RectTransform>();

            barMaskWidth = barMaskRectTransform.sizeDelta.x;
        }


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onCollectableCollected += OnCollectableCollected;
            CoreGameSignals.Instance.onPoolRequiredAmountChanged += OnPoolRequiredAmountChanged;
            CoreGameSignals.Instance.onDecreaseMana += OnDecreaseMana;
            CoreGameSignals.Instance.onOverBar += OnOverBar;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onCollectableCollected -= OnCollectableCollected;
            CoreGameSignals.Instance.onPoolRequiredAmountChanged -= OnPoolRequiredAmountChanged;
            CoreGameSignals.Instance.onDecreaseMana -= OnDecreaseMana;
            CoreGameSignals.Instance.onOverBar -= OnOverBar;
            //isStarted = false;
        }

        private void Update()
        {
            ManaBarWork();

            Debug.Log("MANA MAX DEGERI: " + MANA_MAX);
            Debug.Log("mana Amount: " + manaAmount);
        }

        private void ManaBarWork()
        {
            manaAmount = Mathf.Clamp(manaAmount, 0f, MANA_MAX);
            Rect uvRect = barRawImage.uvRect;

            barRawImage.uvRect = uvRect;

            Vector2 barMaskSizeDelta = barMaskRectTransform.sizeDelta;
            barMaskSizeDelta.x = GetManaNormalized() * barMaskWidth;
            barMaskRectTransform.sizeDelta = barMaskSizeDelta;

            edgeRectTransform.anchoredPosition = new Vector2(GetManaNormalized() * barMaskWidth, 0);


            edgeRectTransform.gameObject.SetActive(GetManaNormalized() < 1f);

            if (!isDecrease)
            {
                return;
            }
            else
            {
                ManaAmount -= manaRegenAmount * Time.deltaTime;
                uvRect.x -= .08f * Time.deltaTime;
            }
        }

        private void OnDecreaseMana()
        {
            isDecrease = true;
        }

        private void OnCollectableCollected()
        {
            manaAmount += manaRegenAmount * Time.deltaTime;
        }

        private void OnOverBar()
        {
            if (manaAmount <= 0)
            {
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            }
            else
            {
                return;
            }
        }


        private void OnPoolRequiredAmountChanged(int requiredAmount)
        {
            _requiredAmount = requiredAmount;
        }

        private float GetManaNormalized()
        {
            return manaAmount / MANA_MAX;
        }

        public float ManaAmount
        {
            get { return manaAmount; }
            set { manaAmount = value; }
        }
    }
}