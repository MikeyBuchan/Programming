﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum Harvester { Idle, CollectResource, SellResource }

public class AI : MonoBehaviour 
{
    public GameObject resourceManager;
    public GameObject startLocObject;
    public Transform startLoc;
    public float colliderRange;
    public string carrying;
    public GameObject sellHouse;

    //this bool makes sure the "AI Guy" doesn't change target every frame
    public bool chooseResource = false;

    [Header("Enum")]
    public Harvester doing;

    [Header("NavMesh")]
    public NavMeshAgent agent;
    public Transform target;
    public GameObject targetObject;


	void Start () 
	{
        GameObject g = Instantiate(startLocObject, transform.position, Quaternion.identity);
        startLoc = g.transform;

        agent = GetComponent<NavMeshAgent>();

        resourceManager = GameObject.FindGameObjectWithTag("ResourceManager");
        sellHouse = GameObject.FindGameObjectWithTag("Sellhouse");
	}
	

	void Update () 
	{
        //These if statements check which state the "AI Guy" is in
		if (doing == Harvester.Idle)
        {
            Idle();
        }
        if (doing == Harvester.CollectResource)
        {
            CollectResource();
        }
        if (doing == Harvester.SellResource)
        {
            SellResource();
        }
	}

    public void Idle()
    {
        List<GameObject> resources = resourceManager.GetComponent<ResourceList>().resources;

        //checks if there are resource in the scene, if there are the "AI Guy" will go to the next state
        if (resources.Count != 0)
        {
            chooseResource = true;
            doing = Harvester.CollectResource;
            print("doing = Harvester.CollectResource");
        }
        //if there aren't any resources in the scene the "AI Guy" will go back to its starting location
        else if (resources.Count == 0)
        {
            agent.SetDestination(startLoc.position);
            print("Reset to start postion");
        }
    }

    public void CollectResource()
    {
        List<GameObject> resources = resourceManager.GetComponent<ResourceList>().resources;
        Collider[] selfRange = Physics.OverlapSphere(transform.position, colliderRange);


        //if this if statement is true the "AI Guy" will choose a random resource to go to
        if (chooseResource == true)
        {
            target = resources[Random.Range(0, resources.Count)].transform;
            targetObject = target.gameObject;
            agent.SetDestination(target.position);
            chooseResource = false;
            print("Random resource chosen");
        }

        //Checks if the object is still in the scene and if it is not it will choose another random object
        if (resources.Count != 0)
        {
            if (target == null)
            {
                target = resources[Random.Range(0, resources.Count)].transform;
                targetObject = target.gameObject;
                agent.SetDestination(target.position);
                print("Couldn't get to chosen resource. Changed resource target");
            }
        }
        else if(resources.Count == 0)
        {
            doing = Harvester.Idle;
        }

        //Checks if the "AI Guy" collides with the targetObject
        for (int i = 0; i < selfRange.Length; i++)
        {
            if (selfRange[i].gameObject == targetObject)
            {
                carrying = targetObject.tag;
                resources.Remove(selfRange[i].gameObject);
                Destroy(selfRange[i].gameObject);

                doing = Harvester.SellResource;
                print("Harvested resource target. Going to next state: doing = Harvester.SellResource");
            }
        }
    }

    public void SellResource()
    {
        target = sellHouse.transform;
        agent.SetDestination(target.position);

        Collider[] selfRange = Physics.OverlapSphere(transform.position, colliderRange);

        for (int i = 0; i < selfRange.Length; i++)
        {
            if (selfRange[i].gameObject.tag == "Sellhouse")
            {
                doing = Harvester.Idle;
                print("Sold resource. doing = Harvester.Idle");
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, colliderRange);
    }
}
