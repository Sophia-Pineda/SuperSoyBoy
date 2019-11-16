using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float rotationsPerMinute = 640.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        transform.Rotate(0, 0, rotationsPerMinute * Time.deltaTime, Space.Self);  // Roll (z axis rotation)
    }
}
