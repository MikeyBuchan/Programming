using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour 
{
    private GameObject cameraa;

	void Start () 
	{
        cameraa = GameObject.FindGameObjectWithTag("MainCamera");
	}
	

	void Update () 
	{
        transform.LookAt(cameraa.transform);
	}
}
