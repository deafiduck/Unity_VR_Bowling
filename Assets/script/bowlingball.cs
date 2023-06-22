using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class bowlingball : MonoBehaviour
{
    [SerializeField] Text scoreText;
    AudioSource m_MyAudioSource;
    bool isAudioPlayed = false;
    private AudioClip audioClip;
    //bir dakika boyunca ResetPinse basýldýðýnda i++ olabilir . 1 dakika sonra i lere kadar foreach ile diziyi yazýdr
   
    int[] score=new int[30];
    int i; 
    Drop dropScript;

    public static InputFeatureUsage<bool> primaryTouch;
    public static InputFeatureUsage<bool> secondaryTouch;

    [SerializeField] Text score_1;
    [SerializeField] Text score_2;
    [SerializeField] Text score_3;
    [SerializeField] Text score_4;
    [SerializeField] Text score_5;
    [SerializeField] Text score_6;

   
    int counter1 = 0;
    public static int counter = 0;
    int sira1 = 0;
    int sira2 = 0;
    int max = 6;
    public float force;//topun atýlýþ hýzý
    // Use this for initialization
    private List<Vector3> pinPositions;//dubalarýn baþlangýç konumlarýný taip etmek 
    private List<Quaternion> pinRotations;// quaternion 3 vektor degerinin çakýþma ihtimaller için kullanýlýr 
    private Vector3 ballPosition;
    // public Text strike;
    //public Text space;


    [SerializeField]
    private InputActionReference m_ResetPin, m_ResetBall;


    
    void Start()
    {
        
        

        m_MyAudioSource = GetComponent<AudioSource>();
        audioClip = m_MyAudioSource.clip;
        var pins = GameObject.FindGameObjectsWithTag("Pin");
        pinPositions = new List<Vector3>();
        pinRotations = new List<Quaternion>();
        foreach (var pin in pins)
        {
            pinPositions.Add(pin.transform.position);
            pinRotations.Add(pin.transform.rotation);
        }

        ballPosition = GameObject.FindGameObjectWithTag("Ball").transform.position;
    }

    


   
    
       private void OnTriggerEnter(Collider other)
        {
        Debug.Log(" gamzedeyim deva bulmam");
        if (other.tag =="ballTrigger") {
            int i = 0;
            for (i = 0; i < 6; i++)
            {

               
                    if (i % 2 == 0)
                    {
                        if (counter == 10)//strike
                        {
                            score[i] = counter;
                            // score[i] = score[i + 1] + score[i + 2] + score[i];
                            StartCoroutine(WaitForSecondsCoroutine(8));
                            ResetBall();
                            i++;
                            score[i] = 0;
                        }
                        StartCoroutine(WaitForSecondsCoroutine(8));
                        score[i] = counter;


                    }
                    else
                    {
                        if (counter == 10)//spare
                        {
                            score[i] = counter;
                            //score[i] = score[i + 1] + score[i];
                            StartCoroutine(WaitForSecondsCoroutine(8));
                            ResetPins();

                        }
                        StartCoroutine(WaitForSecondsCoroutine(8));
                        score[i] = counter;
                    }
                



            }
        }


    
        
    }
    private IEnumerator WaitForSecondsCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    /* public void IncreaseScore()
     {

         scoreText.text = "Score" + counter;

     }*/

    /*Siz mesela W tuþuna basýldýðýnda gidecek olan bir araba yaptýnýz diyelim. 
      Siz bu arabaya velocity ile 200 birimlik bir kuvvet uyguladýnýz diyelim.
      W tuþuna bastýðýnýz anda araba birden 200 birim ileri ok gibi fýrlayacak 
      ve elinizi tuþtan çektiðiniz anda ayarladýðýnýz direnç 
      (Drag, Türkçesi ile hava sürtünmesi de denebilir)
      etkisiyle yavaþlayýp duracak.
      Mantýken bu çok saçma bir durum.
      Bu örnekte AddForce kullanýrsak, W tuþuna bastýðýmýz anda
      araba 0'dan 200'e birden deðil de yavaþ yavaþ ivmelenerek
      veyahut hýzlanarak çýkacak ve siz elinizi W'den çekince de
     ayný oranda ve Drag deðerine baðlý olarak yavaþlayýp duracak.
     Ýþte kýsaca en basit örnekle böyle anlatýlabilir aradaki farklarý.
     
      
    Velocity> Anlýk tepkiler
    AddForce> Yavaþ yavaþ hýzlanarak uygulanan kuvvetler.

    addforce türkçesi kuvvet ekle
    addvelocity türkçesi hýz ekle
     */


    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "zemin")
        {
            
            m_MyAudioSource.Play();
            isAudioPlayed = true;
            
        }
    }
    void Update()
    {
       

        score_1.text = score[0].ToString();
        score_2.text = score[1].ToString();
        score_3.text = score[2].ToString();
        score_4.text = score[3].ToString();
        score_5.text = score[4].ToString();
        score_6.text = score[5].ToString();

        
                /* float XValue = X.action.ReadValue<float>();
                 handAnimator.SetFloat("primaryButton", XValue);


                 float YValue = Y.action.ReadValue<float>();
                 handAnimator.SetFloat("primaryButton", YValue);*/

                // if (this.transform.position.y == -3.317538) {
                counter = pin1.count1 + pin2.count2 + pin3.count3 + pin4.count4 + pin5.count5 + pin6.count6+ pin7.count7+pin8.count8+pin9.count9+pin10.count10;
               // counter = counter * 2;


        if (Input.GetKeyUp(KeyCode.W))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, force * 5));
        }

        /*if (Input.GetKeyUp(KeyCode.Space))
         {
             GetComponent<Rigidbody>().AddForce(new Vector3(0, force*2, 0));//yavaþlatýyo
         }*/
        if (Input.GetKeyUp(KeyCode.D))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(2, 0, 0), ForceMode.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0), ForceMode.Impulse);
        }


        if (m_ResetBall.action.IsPressed())
        {
            ResetBall();
            Debug.Log("test ball");
        }

        if (m_ResetPin.action.IsPressed())
        {
            ResetPins();
            Debug.Log("test pin");
        }


        if (Input.GetKeyUp(KeyCode.R))
        {
            //counter += counter;
            Debug.Log(counter);


            counter = 0;

            Debug.Log(counter);
            var pins = GameObject.FindGameObjectsWithTag("Pin");

            /*for (int i = 0; i < pins.Length; i++)
            {
                //collision.gameObject.transform.parent.gameObject.tag;
                var pinPhysics = pins[i].GetComponent<Rigidbody>();


                pinPhysics.velocity = Vector3.zero;
                pinPhysics.position = pinPositions[i];
                pinPhysics.rotation = pinRotations[i];
                pinPhysics.velocity = Vector3.zero;
                pinPhysics.angularVelocity = Vector3.zero;

                var ball = GameObject.FindGameObjectWithTag("Ball");
                ball.transform.position = ballPosition;
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            }*/
            //kullanici1[sira1] = counter;


            /*if (sira1 = 11)
            {
                sira1 = 0;

            }
            */
        }

        
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }



        if (Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log("max deger");
            Debug.Log(max);
        }
        if (counter == 6)
        {

        }
        if (counter >= 6)
        {
            counter = 6;
        }
    }

    
    public void ResetPins()
    {
       //dropScript.lobut();
        
        //dropScript.yenileme();
       
        // Debug.Log(counter);
        var pins = GameObject.FindGameObjectsWithTag("Pin");

        for (int i = 0; i < pins.Length; i++)
        {
            //collision.gameObject.transform.parent.gameObject.tag;
            var pinPhysics = pins[i].GetComponent<Rigidbody>();


            pinPhysics.velocity = Vector3.zero;
            pinPhysics.position = pinPositions[i];
            pinPhysics.rotation = pinRotations[i];
            pinPhysics.velocity = Vector3.zero;
            pinPhysics.angularVelocity = Vector3.zero;

            var ball = GameObject.FindGameObjectWithTag("Ball");
            ball.transform.position = ballPosition;
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pin1.count1 = 0;
            pin2.count2 = 0;
            pin3.count3 = 0;
            pin4.count4 = 0;
            pin5.count5 = 0;
            pin6.count6 = 0;
            pin6.count6 = 0;
            pin7.count7 = 0;
            pin8.count8 = 0;
            pin9.count9 = 0;
            pin10.count10 = 0;
            scoreText.text = "Score:" + counter;
            
        }
    }

    public void ResetBall()
    {
           // Debug.Log(counter);
            var ball = GameObject.FindGameObjectWithTag("Ball");
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            ball.transform.position = ballPosition;
      
        score[i]=counter;
        i++;
        //counter1 += counter;
       /* pin1.count1 = 0;
        pin2.count2 = 0;
        pin3.count3 = 0;
        pin4.count4 = 0;
        pin5.count5 = 0;
        pin6.count6 = 0;
        pin6.count6 = 0;
        pin7.count7 = 0;
        pin8.count8 = 0;
        pin9.count9 = 0;
        pin10.count10 = 0;*/
        scoreText.text = "Score:"+counter;
            //Debug.Log(counter1);
        
    }


}