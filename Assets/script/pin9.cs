using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin9 : MonoBehaviour
{
    public static int count9 = 0;
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

            count9 = 1;
            m_MyAudioSource.Play();
            isAudioPlayed = true;

        }
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            count9 = 0;
        }
        if (count9 != 0)
        {
            
        }
    }
}
