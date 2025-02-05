using UnityEngine;

public class VFXManager : MonoBehaviour
{
    #region Variables

    [Header("ScreenEffects")]
    [SerializeField] private ParticleSystem particlesObjectsX;
    [SerializeField] private ParticleSystem particlesObjectsT, particlesObjectsL;

    //Singleton.
    private static VFXManager _instance;

    #endregion

    #region Properties

    //Singleton.
    public static VFXManager Instance => _instance;

    #endregion

    #region Built-In Methods

    private void Awake()
    {
        // Singleton
        if (_instance) Destroy(this);
        _instance = this;
    }

    #endregion

    #region Screen Effects

    public void ScreenEffects()
    {
        particlesObjectsX.Play();
        particlesObjectsT.Play();
        particlesObjectsL.Play();
    }

    #endregion
}
