using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin2 : MonoBehaviour
{

    public static int count2 = 0;
    [SerializeField]
    AudioSource m_MyAudioSource;
    bool isAudioPlayed = false;
    private void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "cube")
        {
            count2 = 1;
            m_MyAudioSource.Play();
            isAudioPlayed = true;

        }
    }
    void Update()
    {

        if (m_MyAudioSource == null )
        {
            m_MyAudioSource = GetComponent<AudioSource>();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            count2 = 0;
        }
    }
}
