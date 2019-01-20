using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int number;

    public Node smaller;

    public Node bigger;

	public Node (int num)
    {
        number = num;
    }

    public void AddNumber(int num)
    {
        if (num == number)
        {
            Debug.Log("Can't do cant");
        }
        else if (num < number)
        {
            if (smaller == null)
            {
                smaller = new Node(num);
            }
            else
            {
                smaller.AddNumber(num);
            }

            Debug.Log("To the left(Smaller)");
        }
        else if (num > number)
        {
            if (bigger == null)
            {
                bigger = new Node(num);
            }
            else
            {
                bigger.AddNumber(num);
            }

            Debug.Log("To the right(Bigger)");
        }
    }
}
