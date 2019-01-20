using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBuilder : MonoBehaviour
{
    public Node node;

    public int newNumberToTree;
    public bool addNumber;
    public int beginNumber;

	void Start () 
	{
        node = new Node(beginNumber);
	}
	

	void Update () 
	{
		if (addNumber == true)
        {
            node.AddNumber(newNumberToTree);
            addNumber = false;
        }
	}
}
