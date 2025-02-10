using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class BlowTorchScript : MonoBehaviour
{
    private bool activated;
    [SerializeField] private VisualEffect blowtorchFlame;
    [SerializeField] private TopDiskScript topDiskScript;

    
    void Start()
    {
         blowtorchFlame.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void ActivateBlowTorch()
    {
        activated = true;
        Debug.Log("Debut");
        blowtorchFlame.Play();
    }

    public void DesactivateBlowTorch()
    {
        activated = false;
        blowtorchFlame.Stop();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
        {
            Unscrew(other);

        }
    }

    private void OnTriggerStay(Collider other) // a changer si le jau lag
    {
        if (activated)
        {
            Unscrew(other);

        }

    }

    private void Unscrew(Collider screw)
    {
        screw.GetComponent<Renderer>().enabled = false;
        topDiskScript.CheckIfOpen();
    }
}
