using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    public float range;
    public Text moneyText;
    public int money;

    public Transform startLoc;
    public GameObject startLocation;

    public GameObject sellHouse;

    private GameObject script;

    [Header("Active Enum")]
    public Harvester doing;

    [Header("NavMesh")]
    public NavMeshAgent agent;
    public Transform target;

    [Header("Resource stuff")]
    public string carrying;
    public bool canDoAction;
    public LayerMask treeMask;


    void Start()
    {
        GameObject g = Instantiate(startLocation, transform.position, Quaternion.identity);
        startLoc = g.transform;

        //setting all variables to what its sopposed to be
        moneyText = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        sellHouse = GameObject.FindGameObjectWithTag("SellHouse");
    }


    void Update()
    {
        if (doing == Harvester.Idle)
        {
            Idleling();
        }
        else if (doing == Harvester.BreakResource)
        {
            BreakingResource();
        }
        else if (doing == Harvester.CollectResource)
        {
            CollectingResource();
        }
        else if (doing == Harvester.SellResource)
        {
            SellingWood();
        }
    }

    void Idleling()
    {
        List<GameObject> treeList = GameObject.FindGameObjectWithTag("MainResourceManager").GetComponent<MainResourceManager>().resourceList;

        //Checking if there are resources to gather
        if (treeList.Count != 0)
        {
            canDoAction = true;
            print("Going to Cutting Tree state");
            doing = Harvester.BreakResource;
        }
        else if (treeList.Count == 0)
        {
            target = startLoc;
            agent.SetDestination(target.position);
            print("All tree's are cut");
        }
    }

    void BreakingResource()
    {
        List<GameObject> resourceList = GameObject.FindGameObjectWithTag("MainResourceManager").GetComponent<MainResourceManager>().resourceList;

        Collider[] selfRange = Physics.OverlapSphere(transform.position, range, treeMask);

        //Checking if AI Guy can go to the next resource, if thats true he can go to the next resource
        if (canDoAction == true)
        {
            target = resourceList[Random.Range(0, resourceList.Count)].transform;
            agent.SetDestination(target.position);
            canDoAction = false;
        }

        if (target == null)
        {
            target = resourceList[Random.Range(0, resourceList.Count)].transform;
            agent.SetDestination(target.position);
        }

        //Part where the resource gets harvested
        if (selfRange.Length != 0)
        {
            script = selfRange[0].gameObject;
            script.GetComponent<Resource>().canDrop = true;

            resourceList.Remove(selfRange[0].gameObject);

            print("Broke Resource");
            doing = Harvester.CollectResource;
        }

        if (resourceList.Count == 0)
        {
            doing = Harvester.Idle;
        }
    }

    void CollectingResource()
    {
        //Changing target of navmesh to the resource on the ground
        target = GameObject.FindGameObjectWithTag("Wood").transform;
        agent.SetDestination(target.position);

        Collider[] selfRange = Physics.OverlapSphere(transform.position, range);

        //Checking if the AI Guy is colliding with resource
        if (selfRange[0].transform.tag == "Wood")
        {
            print("Picking Up Wood");

            carrying = selfRange[0].gameObject.tag;

            print(carrying);

            Destroy(selfRange[0].gameObject);

            doing = Harvester.SellResource;
        }

        if (selfRange[0].transform.tag == "Rock")
        {
            print("Picking Up Rock");

            carrying = selfRange[0].gameObject.tag;

            print(carrying);

            Destroy(selfRange[0].gameObject);

            doing = Harvester.SellResource;
        }
    }

    void SellingWood()
    {
        //Setting the target of the NavMesh to the sellhouse
        target = sellHouse.transform;
        agent.SetDestination(target.position);

        Collider[] selfRange = Physics.OverlapSphere(transform.position, range);

        if (selfRange[0].tag == "SellHouse")
        {
            doing = Harvester.Idle;
        }
    }

    //Drawing gizmo's for better visualisation in the scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
public enum Harvester { Idle, BreakResource, CollectResource, SellResource }