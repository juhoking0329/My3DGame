using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MySample
{
    /// <summary>
    /// CardSO 데이터를 읽어 와서 카드 그리기
    /// </summary>
    public class DrawCard : MonoBehaviour
    {
        #region Variables
        [Header("Card Data")]
        [SerializeField] private CardSO cardData; // 카드 데이터 ScriptableObject

        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI nameText;        // 카드 이름 텍스트
        [SerializeField] private TextMeshProUGUI descriptionText; // 카드 설명 텍스트
        [SerializeField] private TextMeshProUGUI manaText;        // 마나 코스트 텍스트
        [SerializeField] private TextMeshProUGUI atkText;         // 공격력 텍스트
        [SerializeField] private TextMeshProUGUI hpText;          // 체력 텍스트
        [SerializeField] private Image artImage;                  // 카드 일러스트 이미지
        #endregion

        #region Unity Event Methods
        // 객체가 활성화되거나 시작할 때 카드 데이터를 UI에 갱신합니다.
        private void Start()
        {
            // 카드 데이터를 읽어서 UI를 업데이트합니다.
            UpdateCardUI();
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// 카드 데이터를 UI 요소들에 적용하여 카드 그래픽을 갱신합니다.
        /// </summary>
        public void UpdateCardUI()
        {
            // 카드 데이터가 유효한지 검사합니다.
            if (cardData == null)
            {
                Debug.LogWarning("Card Data가 할당되지 않았습니다.");
                return;
            }

            // 각 UI 텍스트 컴포넌트가 존재할 경우 카드 데이터 값을 할당합니다.
            if (nameText != null)
            {
                nameText.text = cardData.name;
            }

            if (descriptionText != null)
            {
                // CardSO에 정의된 desctiption 필드를 사용합니다 (오타 반영).
                descriptionText.text = cardData.desctiption;
            }

            if (manaText != null)
            {
                manaText.text = cardData.mana.ToString();
            }

            if (atkText != null)
            {
                atkText.text = cardData.attack.ToString();
            }

            if (hpText != null)
            {
                hpText.text = cardData.health.ToString();
            }

            // 이미지 컴포넌트 및 카드 일러스트 이미지가 있을 경우 이미지를 할당합니다.
            if (artImage != null && cardData.artImage != null)
            {
                artImage.sprite = cardData.artImage;
            }
        }

        /// <summary>
        /// 새로운 카드 데이터를 스크립트에 전달하고 UI를 새로고침합니다.
        /// </summary>
        /// <param name="newCardData">새로 그릴 카드 데이터</param>
        public void SetCardData(CardSO newCardData)
        {
            cardData = newCardData;
            UpdateCardUI();
        }
        #endregion
    }
}