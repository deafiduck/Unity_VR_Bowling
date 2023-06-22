using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class state : MonoBehaviour
{
    public bool dropped = false;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "dropped")
        {
            dropped = true;
        }
    }
}
