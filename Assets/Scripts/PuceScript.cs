using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuceScript : MonoBehaviour
{
    public int actualCounter;
    public int stepCounter;

    public List<Material> materials;

    public Renderer rendererPuce;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GrabbableTool tool) && actualCounter < stepCounter)
        {
            actualCounter++;
            rendererPuce.materials[0] = materials[actualCounter+1];
            if (actualCounter >= stepCounter)
                {
                    Rafitstole();   
                }
            }

        }

    public void Rafitstole()
    {

    }

}


