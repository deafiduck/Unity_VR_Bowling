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

    int[] score = new int[30];
    int i;
    Drop dropScript;

    float currentTime = 0.0f;
    float startingTime = 50.0f;

    int[] toplam = new int[2];

    int counter1 = 0;
    public static int counter = 0;
    int sira1 = 0;
    int sira2 = 0;
    int max = 6;

    private List<Vector3> pinPositions;
    private List<Quaternion> pinRotations;
    private Vector3 ballPosition;

    [SerializeField] private InputActionReference m_ResetPin, m_ResetBall;
   
    [SerializeField] Text Total;

    int k = 0;

    void Start()
    {
        toplam[0] = 0;
        Total.text = toplam[0].ToString();
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
        if (startingTime >= 0)
        {
            startingTime -= 1 * Time.deltaTime;
            Debug.Log(startingTime);
        }
        else if (startingTime <= 0)
        {
             
            if (k > 0)
            {
                for (int a = 0; a < k; a++)
                {
                    toplam[0] += score[a];
                }
            }
            Total.text = toplam[0].ToString();
        }

        if (m_ResetBall.action.IsPressed())
        {
            ResetBall();
            Debug.Log("Reset Ball");
        }

        if (m_ResetPin.action.IsPressed())
        {
            ResetPins();
            Debug.Log("Reset Pins");
        }

      //  Debug.Log(toplam[0]);

       
        counter = pin1.count1 + pin2.count2 + pin3.count3 + pin4.count4 + pin5.count5 + pin6.count6 + pin7.count7 + pin8.count8 + pin9.count9 + pin10.count10;
    }

    public void ResetPins()
    {
        var pins = GameObject.FindGameObjectsWithTag("Pin");

        for (int i = 0; i < pins.Length; i++)
        {
            var pinPhysics = pins[i].GetComponent<Rigidbody>();
            pinPhysics.velocity = Vector3.zero;
            pinPhysics.position = pinPositions[i];
            pinPhysics.rotation = pinRotations[i];
            pinPhysics.velocity = Vector3.zero;
            pinPhysics.angularVelocity = Vector3.zero;
        }

        var ball = GameObject.FindGameObjectWithTag("Ball");
        ball.transform.position = ballPosition;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        if (k < score.Length)
        {
            score[k] = counter;
            k++;
        }

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

        // Score hesaplamasýný güncelle
        UpdateScore();
    }

    public void ResetBall()
    {
        var ball = GameObject.FindGameObjectWithTag("Ball");
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        if (k < score.Length)
        {
            score[k] = counter;
            k++;
        }

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

        ResetPins(); // Topu atarken ayný zamanda pinleri de sýfýrla

        // Score hesaplamasýný güncelle
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (startingTime <= 0 && !isAudioPlayed)
        {
            for (int a = 0; a < i; a++)
            {
                toplam[0] += score[a];
            }
            Total.text = toplam[0].ToString();
            isAudioPlayed = true;
        }
    }
}