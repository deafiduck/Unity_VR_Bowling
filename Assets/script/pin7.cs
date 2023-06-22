using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin7 : MonoBehaviour
{
    public static int count7 = 0;
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

            count7 = 1;
            m_MyAudioSource.Play();
            isAudioPlayed = true;

        }
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            count7 = 0;
        }
        if (count7 != 0)
        {
            
        }
    }
}
