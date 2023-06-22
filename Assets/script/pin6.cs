using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin6 : MonoBehaviour
{
    public static int count6 = 0;
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

            count6 = 1;
            m_MyAudioSource.Play();
            isAudioPlayed = true;

        }
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            count6 = 0;
        }
        if (count6 != 0)
        {
            
        }
    }
}
