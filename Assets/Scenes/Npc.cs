using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    public GameObject npcUI;          // UI를 담는 GameObject
    public Text npcText;              // UI의 Text 컴포넌트
    public float mindDistance;
    public Transform player;
    public float typingSpeed = 0.05f; // 글자 출력 속도
    [TextArea]
    public string[] dialogues;        // 여러 대사를 담을 배열
    private int currentDialogueIndex = 0;
    private bool isTyping = false;    // 현재 텍스트가 타이핑 중인지 체크
    public bool isYou = false;
    public Text youText; 

    private void Awake()
    {
        npcUI.SetActive(false);  // 처음에는 UI를 비활성화
        npcText.text = "";       // 처음에 텍스트를 빈 상태로 설정
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < mindDistance)
        {
            if (!npcUI.activeSelf)  // UI가 처음 활성화될 때
            {
                npcUI.SetActive(true);
                StartCoroutine(TypeText());  // 텍스트 출력 코루틴 시작
            }

            if (Input.GetMouseButtonDown(0) && !isTyping)  // 좌클릭으로 대사 넘김
            {
                NextDialogue();
            }
        }
        else
        {
            if (npcUI.activeSelf)  // UI가 비활성화될 때 텍스트 초기화
            {
                npcUI.SetActive(false);
                StopCoroutine(TypeText());  // 코루틴 중지
                npcText.text = "";          // 텍스트 초기화
                currentDialogueIndex = 0;   // 처음 대사로 돌아감
            }
        }

        if(currentDialogueIndex == 3 && !isYou)
        {
            isYou = true;
            youText.gameObject.SetActive(true);
        }
    }

    IEnumerator TypeText()
    {
        isTyping = true;  // 타이핑 중 상태
        npcText.text = "";
        foreach (char letter in dialogues[currentDialogueIndex].ToCharArray())
        {
            npcText.text += letter;  // 한 글자씩 추가
            yield return new WaitForSeconds(typingSpeed);  // 지정된 속도로 글자 출력
        }
        isTyping = false;  // 타이핑 완료 상태
    }

    void NextDialogue()
    {
        currentDialogueIndex++;  // 다음 대사로 이동

        if (currentDialogueIndex >= dialogues.Length)  // 마지막 대사에 도달하면 처음으로 돌아감
        {
            currentDialogueIndex = 0;
        }

        StartCoroutine(TypeText());  // 새로운 대사 출력 시작
    }
}
