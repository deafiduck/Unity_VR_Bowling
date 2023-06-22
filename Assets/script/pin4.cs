using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin4 : MonoBehaviour
{
    public static int count4 = 0;
    AudioSource m_MyAudioSource;
    bool isAudioPlayed = false;

    private void Awake()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "cube")
        {

            count4 = 1;
            m_MyAudioSource.Play();
            isAudioPlayed = true;

        }
    }
    void Update()
    {
        if (m_MyAudioSource == null)
        {
            m_MyAudioSource = GetComponent<AudioSource>();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            count4 = 0;
        }
    }
}
