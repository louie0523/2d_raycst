using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM3 : MonoBehaviour
{
    public GameObject objectA;     
    public GameObject objectB;      
    public GameObject EndingUI;
    public GameObject blackScreen;
    public AudioClip activationSound;
    public AudioClip ClearSound;
    public float activationDistance = 2f;  
    private bool hasActivated = false;     // 한 번이라도 활성화되면 더 이상 발동하지 않음

    private AudioSource audioSource; // AudioSource 컴포넌트

    void Awake()
    {
        // AudioSource 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!hasActivated)  // 아직 활성화되지 않았다면
        {
            float distance = Vector3.Distance(objectA.transform.position, objectB.transform.position);
            if (distance <= activationDistance)
            {
                StartCoroutine(GAC2()); // ActivateObjects를 코루틴으로 호출
                hasActivated = true;  // 한 번 활성화되면 다시는 발동하지 않게 설정
            }
        }
    }

    IEnumerator GAC2()
    {
        blackScreen.SetActive(true);
        if (activationSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(activationSound); // 소리 재생
        }
        yield return new WaitForSeconds(3f);
        EndingUI.SetActive(true);
        audioSource.PlayOneShot(ClearSound);
        Time.timeScale = 0f;
    }
}
