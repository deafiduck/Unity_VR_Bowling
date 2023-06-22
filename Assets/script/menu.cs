using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class menu : MonoBehaviour
{
 
    

    public void quit()
    {
        Application.Quit();

    }
   public void scene1()
    {
      //  SceneManager.LoadScene(1);

    }
    public void bowling()
    {
        SceneManager.LoadScene(1);

    }
    public void basketbol()
    {
        SceneManager.LoadScene(3);

    }
    public void atýþSimülasyonu()
    {
        SceneManager.LoadScene(2);

    }
    public void okçuluk()
    {
        SceneManager.LoadScene(5);

    }
}
