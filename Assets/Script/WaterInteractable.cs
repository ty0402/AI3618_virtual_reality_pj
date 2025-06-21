using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInteractable : MonoBehaviour
{
    public bool IsWatered;
    public string targetName;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.gameObject.transform.root.gameObject.name);
        if(other.gameObject.transform.root.GetComponent<WaterInteractor>().ItemName==targetName)
            IsWatered = true;
        else
            IsWatered=false;
    }
}
