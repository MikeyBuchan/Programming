using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWood : Resource 
{
    public GameObject Wood;
    public int price;


    void Update () 
	{
        if (canDrop == true)
        {
            DropItem(Wood, price);
        }
	}

    public override void DropItem(GameObject resourceItem, int price)
    {
        base.DropItem(resourceItem, price);
    }
}
