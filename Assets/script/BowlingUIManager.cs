using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BowlingUIManager : MonoBehaviour
{

    [SerializeField] Text ScoreText;
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreaseScore()
    {
        ScoreText.text = "Score: " + score;
    }
}
