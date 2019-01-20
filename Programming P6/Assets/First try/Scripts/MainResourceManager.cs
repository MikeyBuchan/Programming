using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainResourceManager : MonoBehaviour 
{
    public GameObject resourceCollection;
    public List<GameObject> resourceList;

    void Start () 
	{
        resourceCollection = GameObject.FindGameObjectWithTag("Resources");

        //Putting all resources in a list
        for (int i = 0; i < resourceCollection.transform.childCount; i++)
        {
            resourceList.Add(resourceCollection.transform.GetChild(i).gameObject);
        }
    }
}
