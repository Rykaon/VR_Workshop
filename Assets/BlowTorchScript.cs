using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class BlowTorchScript : MonoBehaviour
{
    private bool activated;
    [SerializeField] private VisualEffect blowtorchFlame;
    [SerializeField] private TopDiskScript topDiskScript;
    public GameManager GM;
    
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
        if (GM.firstDisk.locked)
        {
            screw.GetComponent<Renderer>().enabled = false;
            topDiskScript.CheckIfOpen();
        }
        
    }
}
