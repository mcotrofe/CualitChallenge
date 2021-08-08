using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<AIBrain>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
