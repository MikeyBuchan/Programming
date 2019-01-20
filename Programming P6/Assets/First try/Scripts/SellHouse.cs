using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellHouse : MonoBehaviour 
{
    public float timeBeforeCanSell;
    public bool canSell;

    [Header("Sell Amount")]
    public int woodAmount;
    public int stoneAmount;

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "AI Guy")
        {
            string carrying = c.gameObject.GetComponent<AI>().carrying;

            if (carrying == "Wood")
            {
                c.gameObject.GetComponent<AI>().money += woodAmount;
                c.gameObject.GetComponent<AI>().moneyText.text = "Money: " + c.gameObject.GetComponent<AI>().money.ToString();
                c.gameObject.GetComponent<AI>().carrying = null;

                canSell = false;
            }
            else if (carrying == "Rock")
            {
                c.gameObject.GetComponent<AI>().money += stoneAmount;
                c.gameObject.GetComponent<AI>().moneyText.text = "Money: " + c.gameObject.GetComponent<AI>().money.ToString();
                c.gameObject.GetComponent<AI>().carrying = null;

                canSell = false;
            }
            else
            {
                
            }
        }
    }

    public IEnumerator WaitBeforeCanSell()
    {
        yield return new WaitForSeconds(timeBeforeCanSell);
        canSell = true;
    }
}
