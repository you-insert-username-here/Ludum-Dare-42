using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour 
{
    Quaternion rotation;
    public GameObject playerCharacter;

    private void Awake()
    {
        rotation = transform.rotation;
    }

    private void Start()
    {
        playerCharacter = GameObject.Find("Player");
    }

    private void Update()
    {
        this.transform.position = playerCharacter.transform.position;
    }

    private void LateUpdate()
    {
        transform.rotation = rotation;
    }
}