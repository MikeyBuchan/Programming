using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enumu : MonoBehaviour
{
    public enum Action { Idle, Rotate, Action };

    public Action doing;

    public NavMeshAgent agent;
    public Transform destination;

    public float rotateSpeed;


    void Update()
    {
        if (doing == Action.Idle)
        {
            agent.isStopped = true;

            StartCoroutine(WaitForNextEnum());
        }
        else if (doing == Action.Rotate)
        {
            agent.isStopped = true;
            transform.Rotate(0, 1, 0 * rotateSpeed * Time.deltaTime);

            StartCoroutine(WaitForNextEnum());
        }
        else if (doing == Action.Action)
        {
            agent.isStopped = false;
            agent.SetDestination(destination.position);

            StartCoroutine(WaitForNextEnum());
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            doing = Action.Idle;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            doing = Action.Rotate;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            doing = Action.Action;
        }
    }


    public IEnumerator WaitForNextEnum()
    {
        yield return new WaitForSeconds(Random.Range(2, 5));
        doing = (Action)Random.Range(0, 3);

        StopAllCoroutines();
    }
}
