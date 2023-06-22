using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour
{
   
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "door")
        {
            Application.Quit();
            Debug.Log("snsn");
        }
       
    }

    void Update()
    {
        
    }
}
