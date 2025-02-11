using System.Collections;
using System.Collections.Generic;
using Meta.XR.ImmersiveDebugger;
using UnityEngine;

public class RangeCassetScript : MonoBehaviour
{
    public GameManager GM;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("bien");

        if (other.gameObject.layer == 8)
        {
            if(GM.firstDisk.repaired)
            {
                GM.firstDisk.installed = true;
                GM.LaunchSecondPhase();

                Debug.Log("TADADADA DA DA  DA.DADA");
            }
                
        }
    }
}
