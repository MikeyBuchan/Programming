using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour 
{
    public bool canDrop = false;


    public virtual void DropItem(GameObject resourceItem, int price)
    {
        Instantiate(resourceItem, new Vector3(transform.position.x, transform.position.y - transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}
