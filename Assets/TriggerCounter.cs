using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCounter : MonoBehaviour
{
    public int targetCount=5;
    public string tagName;
    public int taskIndex;
    public bool isFinish;
    int count;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name==tagName&&Level1Manager.instance.TaskIndex==taskIndex)
        {
            count++;
            if(count>=targetCount)
            {
                isFinish = true;
            }
        }
    }
}
