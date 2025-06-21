using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaTaker : MonoBehaviour
{
    public Transform tea;

    bool isGrab;
    bool isTaking;

    public Transform point1;
    public Transform point2;
    public void Grab()
    {
        isGrab = true;
    }

    public void UnGrab()
    {
        isGrab = false;
    }
    private void Update()
    {
        //if (isGrab)
        {
            //Ray ray1 = new Ray(point1.position, Vector3.down);
            //Ray ray2 = new Ray(point2.position, Vector3.down);
            //RaycastHit hit1;
            //RaycastHit hit2;
            if (/*Physics.Raycast(ray1, out hit1) && hit1.collider.gameObject.transform.root.gameObject.name == "主泡器" &&*/ isTaking&&SystemManager.instance.currentStep==6)
            {

                SystemManager.instance.step6Finished = true;
            }
            //if (Physics.Raycast(ray2, out hit2) && hit2.collider.gameObject.transform.root.gameObject.name == "主泡器" && isTaking && SystemManager.instance.currentStep == 6)
            //{

            //    SystemManager.instance.step6Finished = true;
            //}
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "chabo")
            isTaking = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "chabo")
            isTaking = false;
    }

    public void InsTea()
    {
        tea.gameObject.SetActive(true);
    }
}
