using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float force;//topun at�l�� h�z�
    // Use this for initialization
    private List<Vector3> pinPositions;//dubalar�n ba�lang�� konumlar�n� taip etmek 
    private List<Quaternion> pinRotations;// quaternion 3 vektor degerinin �ak��ma ihtimaller i�in kullan�l�r 
    private Vector3 ballPosition;
    // Start is called before the first frame update
    void Start()
    {
      

    ballPosition = GameObject.FindGameObjectWithTag("Ball").transform.position;
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
