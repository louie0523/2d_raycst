using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public GameObject objectA;
    public GameObject objectB;
    public GameObject door;
    public GameObject heart;
    public AudioClip activationSound;
    public float activationDistance = 1f;
    private bool hasActivated = false;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!hasActivated)
        {
            float distance = Vector3.Distance(objectA.transform.position, objectB.transform.position);
            if (distance <= activationDistance)
            {
                ActivateObjects();
                hasActivated = true; // 한번 발동후 다시는 발동안함
            }
        }
    }

    void ActivateObjects()
    {
        door.SetActive(true);
        heart.SetActive(true);

        Vector3 midpoint = (objectA.transform.position + objectB.transform.position) / 2;
        heart.transform.position = midpoint;

        objectA.SetActive(false);
        objectB.SetActive(false);

        if (activationSound != null && audioSource != null)
        {
            Debug.Log("소리 잘남");
            audioSource.PlayOneShot(activationSound);
        }
        else
        {
            Debug.Log("오디오소스 못찾음");
        }

    }
}
