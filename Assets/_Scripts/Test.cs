using UnityEngine;

public class Test : MonoBehaviour
{
    #region Variable

    //Singleton.
    private VFXManager _vfxManager;

    #endregion

    #region Built-In Methods

    private void Start()
    {
        //Singleton.
        _vfxManager = VFXManager.Instance;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _vfxManager.ScreenEffects();
        }
    }

    #endregion
}
