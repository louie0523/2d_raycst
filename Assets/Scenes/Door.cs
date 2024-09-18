using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject targetObject; 
    public AudioClip collisionSound; 
    public string nextSceneName;     
    public float activationDistance = 1f; 
    private AudioSource audioSource; // AudioSource 컴포넌트
    private bool hasActivated = false; 

    void Awake()
    {
        // AudioSource 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("오디오 소스 못찾음.");
        }
    }

    void Update()
    {
        if (!hasActivated)
        {
            float distance = Vector3.Distance(transform.position, targetObject.transform.position);
            if (distance <= activationDistance)
            {
                Activate();
                hasActivated = true; // 한번더 안되게 체크
            }
        }
    }

    void Activate()
    {
        if (collisionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collisionSound); // 
        }
        else
        {
            Debug.LogError("X");
        }

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName); // 씬 이동
        }
    }
}
