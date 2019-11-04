using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// File Name: DeathPlaneController.cs
/// Author: Kevin Yuayan
/// Last Modified by: Kevin Yuayan
/// Date Last Modified: Nov. 3, 2019
/// Description: Controller for the DeathPlane Object
/// Revision History:
/// </summary>
[System.Serializable]
public class DeathPlaneController : MonoBehaviour
{
    public Transform activeCheckpoint;
    public GameObject player;
    public GameObject gameController;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = activeCheckpoint.position;
            gameController.GetComponent<GameController>().Lives -= 1;
        }
    }
}
