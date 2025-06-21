using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaCanister : MonoBehaviour
{
    bool isTaking;

    public Transform point;
    public TeaTaker teaTaker;

    private void Update()
    {
            if(isTaking)
            {
                teaTaker.InsTea();
                SystemManager.instance.step1Finished=true;
            }        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="chabo")
            isTaking = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "chabo")
            isTaking = false;
    }
}
