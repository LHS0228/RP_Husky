using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance; // 싱글톤

    [Header("UI 연결")]
    public GameObject dialoguePanel; // 대화창 패널
    public TextMeshProUGUI dialogueText; // 대사 텍스트
    public Image portraitImage; // 초상화

    private Story currentStory; // 현재 실행 중인 Ink 스토리 엔진
    private bool isDialoguePlaying = false;

    void Awake() { instance = this; }

    // 1. 대화 시작 (NPC가 호출)
    public void StartDialogue(TextAsset inkJSON, Sprite portrait)
    {
        currentStory = new Story(inkJSON.text); // Ink 엔진 로드
        portraitImage.sprite = portrait; // 초상화 세팅

        dialoguePanel.SetActive(true);
        isDialoguePlaying = true;

        ContinueStory();
    }

    // 2. 다음 줄 읽기 (클릭할 때마다 호출)
    public void ContinueStory()
    {
        // 더 읽을 내용이 있으면
        if (currentStory.canContinue)
        {
            // 한 줄 읽어서 UI에 뿌리기
            dialogueText.text = currentStory.Continue();
        }
        // 내용이 끝났으면
        else
        {
            EndDialogue();
        }
    }

    // 3. 대화 종료
    void EndDialogue()
    {
        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);
    }

    // 화면 클릭 처리
    void Update()
    {
        if (isDialoguePlaying && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }
}
