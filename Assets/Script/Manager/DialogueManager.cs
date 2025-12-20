using Ink.Runtime;
using System.Collections; // Coroutine 사용을 위해 추가
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem; // PlayerInput 제어를 위해 추가
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("UI 연결")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;

    private Story currentStory;
    private bool isDialoguePlaying = false;
    private float dialogueStartTime;

    // [추가 1] 플레이어의 입력을 제어하기 위한 변수
    private PlayerInput playerInput;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // 씬에 있는 PlayerInput 컴포넌트를 찾아서 기억해둠
        playerInput = FindObjectOfType<PlayerInput>();
    }

    public void StartDialogue(TextAsset inkJSON, Sprite portrait, string npcName)
    {
        // [추가 2] 대화 시작하면 플레이어 움직임/상호작용 잠금!
        if (playerInput != null)
        {
            playerInput.enabled = false;
            // 주의: PlayerController가 아니라 PlayerInput을 꺼야 'OnInteract' 신호 자체가 차단됩니다.
        }

        currentStory = new Story(inkJSON.text);
        portraitImage.sprite = portrait;

        int currentScore = DataManager.instance.GetFriendship(npcName);
        if (currentStory.variablesState.Contains("friendship"))
        {
            currentStory.variablesState["friendship"] = currentScore;
        }

        dialoguePanel.SetActive(true);
        isDialoguePlaying = true;
        dialogueStartTime = Time.time;

        ContinueStory();
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isDialoguePlaying = false;
        dialoguePanel.SetActive(false);

        // [추가 3] 바로 켜지 말고, 0.2초만 기다렸다가 켬 (F키 연타 방지)
        StartCoroutine(EnablePlayerInputAfterDelay());
    }

    // [추가 4] 쿨타임 코루틴
    IEnumerator EnablePlayerInputAfterDelay()
    {
        // F키를 눌러서 대화창을 닫는 그 순간의 입력이
        // PlayerController에게 전달되지 않도록 프레임을 뜀
        yield return new WaitForSeconds(0.2f);

        if (playerInput != null)
        {
            playerInput.enabled = true; // 이제 다시 움직이고 말 걸 수 있음
        }
    }

    void Update()
    {
        if (!isDialoguePlaying) return;

        // 대화 시작 직후 쿨타임
        if (Time.time - dialogueStartTime < 0.2f) return;

        // 입력 받기
        if ((Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) ||
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)) // F키
        {
            ContinueStory();
        }
    }
}