using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public Animation lobutDevrilme;
   // public Animation lobut_yenileme;
   
    /*public void yenileme()
    {
        lobut_yenileme.Play();
        StartCoroutine(WaitForSecondsCoroutine(2));
    }*/
    
    public void lobut()
    {
        lobutDevrilme.Play();
        StartCoroutine(WaitForSecondsCoroutine(5));
    }

    private IEnumerator WaitForSecondsCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}