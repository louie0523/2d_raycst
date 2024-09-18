using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    public GameObject player;   
    public AudioClip soundClip;  
    public float activationDistance = 1f; 
    private AudioSource audioSource;  // AudioSource 컴포넌트
    private bool hasActivated = false; 

    void Start()
    {
        // AudioSource 컴포넌트가 없으면 추가
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // 거리가 1 이하이고 아직 발동되지 않았으면
        if (distance <= activationDistance && !hasActivated)
        {
            // 사운드 재생
            if (soundClip != null)
            {
                audioSource.PlayOneShot(soundClip);
            }

            // 오브젝트를 보이지 않게 설정 
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }

            // 발동 완료
            hasActivated = true;
        }
    }
}
