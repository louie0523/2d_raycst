using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gm2 : MonoBehaviour
{
    public GameObject player;             // 플레이어 오브젝트
    public GameObject haveKeyObject;      // HaveKey 오브젝트
    public GameObject doorlock;   // 비활성화할 Door 오브젝트
    public GameObject underdoor;     // 활성화할 Door 오브젝트
    public GameObject lockObject;         // 비활성화할 Lock 오브젝트
    public GameObject keyObject;          // 활성화할 Key 오브젝트
    public GameObject blackScreen;        // Black Screen UI
    public GameObject loveObject;         // Love 오브젝트
    public GameObject girl;               // Girl 오브젝트
    public GameObject man;                // Man 오브젝트
    public AudioClip activationSound;
    public AudioClip cleaarSound;// 활성화 시 재생될 소리
    public float activationDistance = 1f; // 플레이어와 HaveKey 오브젝트 사이의 감지 거리

    private bool hasActivated = false;    // 한 번만 발동되도록 체크
    private AudioSource audioSource;      // AudioSource 컴포넌트

    void Start()
    {
        // AudioSource 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // haveKeyObject와 player가 설정되어 있고, 아직 발동되지 않았으면
        if (haveKeyObject != null && player != null && !hasActivated)
        {
            // 플레이어와 HaveKey 오브젝트 간의 거리 계산
            float distance = Vector3.Distance(player.transform.position, haveKeyObject.transform.position);
            if (distance <= activationDistance)
            {
                // Door와 Lock 오브젝트 비활성화
                if (doorlock != null)
                {
                    doorlock.SetActive(false);
                }
                if (lockObject != null)
                {
                    lockObject.SetActive(false);
                }

                // Key 오브젝트 활성화
                if (keyObject != null)
                {
                    keyObject.SetActive(true);
                }

                // Open_Door 함수 호출
                Open_Door();

                // 발동 상태로 변경 (한 번만 발동되도록)
                hasActivated = true;
            }
        }
    }

    void Open_Door()
    {
        // Black Screen UI 활성화
        if (blackScreen != null)
        {
            blackScreen.SetActive(true);
        }

        // Love 오브젝트 활성화
        if (loveObject != null)
        {
            loveObject.SetActive(true);
        }

        // Girl과 Man 오브젝트 비활성화
        if (girl != null)
        {
            girl.SetActive(false);
        }
        if (man != null)
        {
            man.SetActive(false);
        }

        // 소리 재생
        if (activationSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(activationSound);
        }

        // 4초 후에 다시 Black Screen 비활성화하고 두 번째 Door 활성화
        StartCoroutine(ClaerSnece());
    }

    IEnumerator ClaerSnece()
    {
        // 4초 대기
        yield return new WaitForSeconds(4f);

        // Black Screen UI 비활성화
        if (blackScreen != null)
        {
            blackScreen.SetActive(false);
        }

        // 두 번째 Door 오브젝트 활성화
        if (underdoor != null)
        {
            underdoor.SetActive(true);
        }
        audioSource.PlayOneShot(cleaarSound);
    }
}
