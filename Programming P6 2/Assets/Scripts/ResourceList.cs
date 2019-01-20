using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceList : MonoBehaviour 
{
    public GameObject resourceCollection;
    public List<GameObject> resources;

	void Start () 
	{
        //Putting all resources in a list
        for (int i = 0; i < resourceCollection.transform.childCount; i++)
        {
            resources.Add(resourceCollection.transform.GetChild(i).gameObject);
        }
    }
	

	void Update () 
	{
		
	}
}
