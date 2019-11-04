using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// File Name: BackgroundController.cs
/// Author: Kevin Yuayan
/// Last Modified by: Kevin Yuayan
/// Date Last Modified: Nov. 3, 2019
/// Description: Controls background to give a parallax effect
/// Revision History:
/// </summary>
public class BackgroundController : MonoBehaviour
{
    private float length, startPosition;
    public GameObject camera;
    public float parallexEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (camera.transform.position.x * (1 - parallexEffect));
        float distance = (camera.transform.position.x * parallexEffect);

        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        if (temp > startPosition + length)
        {
            startPosition += length;
        }

        else if (temp < startPosition - length)
        {
            startPosition -= length;
        }
    }
}
