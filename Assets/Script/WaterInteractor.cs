using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInteractor : MonoBehaviour
{
    private string itemName;
    Vector3 pos;
    Quaternion quaternion;
    public string ItemName
    {
        get
        {
            return itemName;
        }
    }

    public bool isGrab;
    public bool isTaking;

    public bool isWatered;

    public Transform waterDetectPoint;
    public GameObject waterVFX;

    public bool canWater;
    void Start()
    {
        quaternion = transform.rotation;
        pos = transform.position;
        itemName=gameObject.name;
    }

    public void Grab()
    {
        isGrab = true;
        StopAllCoroutines();
    }

    public void UnGrab()
    {
        isGrab = false;
        StartCoroutine(ResetPos());
    }

    private void Update()
    {
        if (isGrab&&canWater)
        {
            Ray ray = new Ray(waterDetectPoint.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.transform.root != this.transform)
            {
                Debug.Log(hit.collider.gameObject.name);
                waterVFX.SetActive(true);
            }
            else
                waterVFX.SetActive(false);
        }
        else
            waterVFX.SetActive(false);
    }
    IEnumerator ResetPos()
    {
        yield return new WaitForSeconds(3f);
        transform.position = pos;
        transform.rotation=quaternion;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
