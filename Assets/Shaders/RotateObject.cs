using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float oscillationSpeed;
    [SerializeField] private float oscillationHeight;

    private float initialY;
    // Update is called once per frame

    private void Start()
    {
        initialY = transform.position.y;
    }
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);


        float newY = initialY + Mathf.PingPong(Time.time * oscillationSpeed, oscillationHeight);

        // Applique la nouvelle position à l'objet
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
