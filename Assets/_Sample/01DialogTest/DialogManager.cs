using UnityEngine;
using System.Collections.Generic; 

namespace MySample
{
    /// <summary>
    /// 대화를 관리하는 클래스
    /// 리소스 폴더에 있는 Dialog.xml 파일 읽어 "List<Dialog>"
    /// 대화 인덱스로 현재 대화 구성하기 - "Queue<Dialog>"
    /// 현재 대화에서 대화를 하나씩 꺼내서 보여준다
    /// </summary>
    public class DialogManager : MonoBehaviour
    {
        public List<Dialog> dialogList = new List<Dialog>();
        public Queue<Dialog> currentDialogQueue = new Queue<Dialog>();

        private void Awake()
        {
            LoadDialogs();
        }

        /// <summary>
        /// 리소스 폴더에 있는 Dialog.xml 파일 읽어 "List<Dialog>"로 저장
        /// </summary>
        public void LoadDialogs()
        {
            // Resources/Dialog.xml 텍스트 에셋 로드 (만약 Resources/Dialog 폴더 안에 있다면 "Dialog/Dialog"로 변경)
            TextAsset xmlAsset = Resources.Load<TextAsset>("Dialog");
            if (xmlAsset != null)
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Dialog>), new System.Xml.Serialization.XmlRootAttribute("ArrayOfDialog"));
                using (System.IO.StringReader reader = new System.IO.StringReader(xmlAsset.text))
                {
                    dialogList = (List<Dialog>)serializer.Deserialize(reader);
                }
            }
            else
            {
                Debug.LogError("Resources 폴더에서 Dialog.xml 파일을 찾을 수 없습니다.");
            }
        }

        /// <summary>
        /// 대화 인덱스로 현재 대화 구성하기 - "Queue<Dialog>"
        /// </summary>
        public void SetupDialog(int dialogIndex)
        {
            currentDialogQueue.Clear();
            foreach (var dialog in dialogList)
            {
                if (dialog.number == dialogIndex)
                {
                    currentDialogQueue.Enqueue(dialog);
                }
            }
        }

        [Header("UI Reference")]
        public DrawDialog drawDialog;

        /// <summary>
        /// 현재 대화에서 대화를 하나씩 꺼내서 반환
        /// </summary>
        public Dialog GetNextDialog()
        {
            if (currentDialogQueue.Count > 0)
            {
                return currentDialogQueue.Dequeue();
            }
            
            return null; // 대화 종료
        }

        private void Update()
        {
            // 신규 Input System을 사용하여 키보드 0번 또는 키패드 0번 키 입력 감지
            if (UnityEngine.InputSystem.Keyboard.current != null)
            {
                if (UnityEngine.InputSystem.Keyboard.current.digit0Key.wasPressedThisFrame || 
                    UnityEngine.InputSystem.Keyboard.current.numpad0Key.wasPressedThisFrame)
                {
                    StartDialog(0);
                }
            }
        }

        /// <summary>
        /// 특정 인덱스의 대화를 시작
        /// </summary>
        public void StartDialog(int index)
        {
            SetupDialog(index);
            ShowNextDialog();
        }

        /// <summary>
        /// 큐에서 다음 대화를 꺼내와 UI에 그려줍니다.
        /// (Next 버튼 등에서 호출하기 좋음)
        /// </summary>
        public void ShowNextDialog()
        {
            Dialog nextDialog = GetNextDialog();
            
            if (nextDialog != null)
            {
                if (drawDialog != null)
                {
                    // 큐에 대화가 더 남아있거나, XML 상에서 다음으로 넘어갈 next 인덱스가 있다면 다음 대화가 있는 것으로 간주
                    bool hasNext = (currentDialogQueue.Count > 0) || (nextDialog.next != -1);
                    drawDialog.Draw(nextDialog, hasNext);
                }
                else
                {
                    Debug.LogWarning("DrawDialog 컴포넌트가 연결되지 않았습니다.");
                }
            }
        }
    }
}

/*
0번 버튼을 누르면 0번 인덱스의 대화를 보여주는 함수 구현해줘

*/