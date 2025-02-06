using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GodModeScript : MonoBehaviour
{
    public GameManager GM;
    public InputActionProperty input;

    
    void Start()
    {
        input.action.started += ctx => GM.LaunchSecondPhase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
