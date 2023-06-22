using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Replay
{
    public List<double> states;
    public double reward;

    public Replay(double ballx, double bally,double ballz, double r)
    {
        states = new List<double>();
        states.Add(ballx);
         //ball z position of platfor
        states.Add(bally);//ball velocitys iz z direction
        states.Add(ballz);
       
        reward = r;
    }
}

public class Brain : MonoBehaviour
{

    
    
    //public static int counter = 0;
    
    public float force;//topun atýlýþ hýzý
    // Use this for initialization
    private List<Vector3> pinPositions;//dubalarýn baþlangýç konumlarýný taip etmek 
    private List<Quaternion> pinRotations;// quaternion 3 vektor degerinin çakýþma ihtimaller için kullanýlýr 
    private Vector3 ballPosition;
    



    ANN ann;

    public GameObject ball;
    float reward = 0.0f;
    List<Replay> replayMemory = new List<Replay>();

    int mCapacity = 10000; //memory capacity

    float discount = 0.99f;//gelecekteki ödülleri satýn alýrken ne kadar indirim yapacaðýmýzdýr
    float exploreRate = 100.0f;//chance of picking random action
    float maxExploreRate = 100.0f;
    float minExploreRate = 00.01f;
    float exploreDecay = 0.0001f;

    Vector3 ballStartPos;
    int failCount = 0;
    float ballfast = 30.0f;

    float timer = 0;
    float maxBalanceTime = 0;

    
    Rigidbody rb;
    public bool dropped = false;
    void Start()
    {
        ann = new ANN(3, 2, 1, 6, 0.2f);
        ballStartPos = ball.transform.position;
        Time.timeScale = 5.0f;
        rb = this.GetComponent<Rigidbody>();

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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "dropped")
        {
            dropped = true;
        }
    }
    void Update()
        {
            // if (this.transform.position.y == -3.317538) {
            bowlingball.counter = pin1.count1 + pin2.count2 + pin3.count3 + pin4.count4 + pin5.count5 + pin6.count6;
        }
    void FixedUpdate()
    {

        timer += Time.deltaTime;
        List<double> states = new List<double>();//rotx ballz velx= inputs
        List<double> qs = new List<double>();//q values
        states.Add(this.transform.position.x);
        states.Add(this.transform.position.y);
        states.Add(this.transform.position.z);
        
       
        

        qs = SoftMax(ann.CalcOutput(states));
        double maxQ = qs.Max();

        int maxQIndex = qs.ToList().IndexOf(maxQ);
        exploreRate = Mathf.Clamp(exploreRate - exploreDecay, minExploreRate, maxExploreRate);


        if (maxQIndex == 0)
        {
            rb.AddForce(Vector3.forward * ballfast * (float)qs[maxQIndex]);
        }
        else if (maxQIndex == 1)
        {
            rb.AddForce(Vector3.right * ballfast * (float)qs[maxQIndex]);
        }
        else if (maxQIndex == 2)
        {
            rb.AddForce(Vector3.left * ballfast * (float)qs[maxQIndex]);
        }


        if (bowlingball.counter!=6)
        {
            reward = -1.0f;

        }
        else if (dropped)
        {
            reward = -1.0f;
        }
        else
        {
            reward = 0.1f;
        }

        Replay lastMemory = new Replay(ball.transform.position.x,
                                     ball.transform.position.y,
                                     ball.transform.position.z,                            
                                     reward);

        if (replayMemory.Count > mCapacity)
        {
            replayMemory.RemoveAt(0);
        }

        replayMemory.Add(lastMemory);


        if (timer>10.0f)
        {
            for (int i = replayMemory.Count - 1; i >= 0; i--)
            {
                List<double> toutputsOld = new List<double>();//what our q values with the current memory
                List<double> toutputsNew = new List<double>();
                toutputsOld = SoftMax(ann.CalcOutput(replayMemory[i].states));
                //toutputsOld = ann.CalcOutput(replayMemory[i].states);
                double maxQold = toutputsOld.Max();
                int action = toutputsOld.ToList().IndexOf(maxQold);

                double feedback;
                if (i == replayMemory.Count - 1 || replayMemory[i].reward == -1)
                {
                    //if we are last memory on the list  there is no nxt memory to get any maximum values
                    feedback = replayMemory[i].reward;
                }
                else
                {
                    toutputsNew = SoftMax(ann.CalcOutput(replayMemory[i + 1].states));
                    //toutputsNew = ann.CalcOutput(replayMemory[i + 1].states);
                    maxQ = toutputsNew.Max();
                    feedback = (replayMemory[i].reward +
                        discount * maxQ);
                }

                toutputsOld[action] = feedback;
                ann.Train(replayMemory[i].states, toutputsOld);
            }
            if (timer > maxBalanceTime)
            {
                maxBalanceTime = timer;
                
            }
            timer = 0;
            Debug.Log(bowlingball.counter);
           /* pin1.count1 = 0;
            pin2.count2 = 0;
            pin3.count3 = 0;
            pin4.count4 = 0;
            pin5.count5 = 0;
            pin6.count6 = 0;
            bowlingball.counter = 0;*/
            dropped = false;
            ResetBall();
            this.transform.rotation = Quaternion.identity;
            //ball.GetComponent<state>().dropped = false;
            replayMemory.Clear();
            failCount++;
        }

    }

    void ResetBall()
    {
        
        var pins = GameObject.FindGameObjectsWithTag("Pin");
        pin1.count1 = 0;
        pin2.count2 = 0;
        pin3.count3 = 0;
        pin4.count4 = 0;
        pin5.count5 = 0;
        pin6.count6 = 0;
        bowlingball.counter = 0;
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

        }


    }

    List<double> SoftMax(List<double> values)//outputs values on the list 
    {
        //softmax is used quite often as a calculation on the output layer of neural nets
        double max = values.Max();

        float scale = 0.0f;
        for (int i = 0; i < values.Count; ++i)
        {
            scale += Mathf.Exp((float)(values[i] - max));
            //how far each value in the array is away from the maximum value that's in the array
        }
        List<double> result = new List<double>();
        for (int i = 0; i < values.Count; i++)
        {
            result.Add(Mathf.Exp((float)(values[i] - max)) / scale);
        }
        return result;

    }
    
}
