using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MySample
{
    /// <summary>
    /// 대화창 그리기를 관리하는 클래스
    /// 매개 변수로 들어온 Dialog 데이터를 UI에 적용하기
    /// </summary>
    public class DrawDialog : MonoBehaviour
    {
        #region Variables
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI sentenceText;
        public GameObject npcImage;
        public GameObject nextButton;
        
        [Header("Settings")]
        public float typingSpeed = 0.05f;
        #endregion

        /// <summary>
        /// 매개 변수로 들어온 Dialog 데이터를 UI에 적용합니다.
        /// </summary>
        public void Draw(Dialog dialog, bool hasNext = false)
        {
            if (dialog == null) return;

            // 이름 텍스트 갱신
            if (nameText != null)
                nameText.text = dialog.name;

            // 대화 캐릭터 인덱스가 0이면 NPC 이미지 비활성화, 0이 아니면 활성화 및 스프라이트 변경
            if (npcImage != null)
            {
                if (dialog.character == 0)
                {
                    npcImage.SetActive(false);
                }
                else
                {
                    npcImage.SetActive(true);

                    // Resources/Npc/npc01, npc02 등의 이름 규칙으로 스프라이트 로드
                    string spriteName = string.Format("Npc/npc{0:D2}", dialog.character);
                    Sprite loadedSprite = Resources.Load<Sprite>(spriteName);
                    
                    if (loadedSprite != null)
                    {
                        Image img = npcImage.GetComponent<Image>();
                        if (img != null)
                        {
                            img.sprite = loadedSprite;
                        }
                        else
                        {
                            Debug.LogWarning("npcImage 게임 오브젝트에 Image 컴포넌트가 없습니다.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"리소스 폴더에서 스프라이트를 찾을 수 없습니다: {spriteName}");
                    }
                }
            }

            // 이전 타이핑 연출 중지
            StopAllCoroutines();

            // 타이핑 연출 코루틴 시작
            StartCoroutine(TypeSentence(dialog.sentence, hasNext));
        }

        /// <summary>
        /// 텍스트를 한 글자씩 출력하는 타이핑 코루틴
        /// </summary>
        private IEnumerator TypeSentence(string sentence, bool hasNext)
        {
            // 타이핑 연출하는 동안 nextButton 비활성화
            if (nextButton != null)
                nextButton.SetActive(false);

            sentenceText.text = "";

            // 타이핑 효과
            foreach (char letter in sentence.ToCharArray())
            {
                sentenceText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            // 타이핑이 끝난 후, 다음 대화가 있으면 nextButton 활성화
            if (nextButton != null)
            {
                nextButton.SetActive(hasNext);
            }
        }
    }
}