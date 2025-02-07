using UnityEngine;
using System.Collections;

public class Dissolve : MonoBehaviour
{
    #region Variables

    [Header("Object Properties")]
    [SerializeField] private Material dissolveMaterial;

    [Header("Dissolve Properties")]
    [SerializeField] private float dissolveSpeed = 0.5f;

    //Material
    private Material _originalMaterial;

    //Dissolve Properties
    private Renderer _rend;
    private MaterialPropertyBlock _mpb;
    private bool _isDissolving = false;
    private float _dissolveAmount = 0f;

    #endregion

    #region Built-In Methods

    private void Awake()
    {
        _rend = GetComponent<Renderer>();
        _originalMaterial = _rend.material;
        _mpb = new MaterialPropertyBlock();
        _rend.GetPropertyBlock(_mpb);
        _mpb.SetFloat("_DissolveEffect", 0f);
        _rend.SetPropertyBlock(_mpb);
    }

    #endregion

    #region Dissolve Effcet

    public void DissolvingObject()
    {
        if (!_isDissolving)
        {
            StartCoroutine(DissolveEffect());
        }
    }

    private IEnumerator DissolveEffect()
    {
        _isDissolving = true;
        _rend.material = dissolveMaterial;

        while (_dissolveAmount < 1f)
        {
            _dissolveAmount += Time.deltaTime * dissolveSpeed;

            _rend.GetPropertyBlock(_mpb);
            _mpb.SetFloat("_DissolveEffect", _dissolveAmount);
            _rend.SetPropertyBlock(_mpb);

            yield return null;
        }

        //gameObject.SetActive(false);
    }

    public void AppearingObject()
    {
        //gameObject.SetActive(true);

        if (!_isDissolving)
        {
            StartCoroutine(Appear());
        }
    }

    private IEnumerator Appear()
    {
        _isDissolving = true;
        _rend.material = dissolveMaterial;
        _dissolveAmount = 1f;

        while (_dissolveAmount > 0f)
        {
            _dissolveAmount -= Time.deltaTime * dissolveSpeed;

            _rend.GetPropertyBlock(_mpb);
            Debug.Log("ee");
            _mpb.SetFloat("_DissolveEffect", _dissolveAmount);
            _rend.SetPropertyBlock(_mpb);

            yield return null;
        }

        _rend.material = _originalMaterial;
        _isDissolving = false;
    }

    #endregion
}
