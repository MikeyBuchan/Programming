using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropStone : Resource 
{
    public GameObject stone;
    public int price;


    void Update ()
    {
        if (canDrop == true)
        {
            DropItem(stone, price);
        }
    }

    public override void DropItem(GameObject resourceItem, int price)
    {
        base.DropItem(resourceItem, price);
    }
}
