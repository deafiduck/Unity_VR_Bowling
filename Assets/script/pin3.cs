using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin3 : MonoBehaviour
{

    public static int count3 = 0;
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

            count3 = 1;
            m_MyAudioSource.Play();
           isAudioPlayed = true;

        }
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            count3 = 0;
        }
       
    }
}
