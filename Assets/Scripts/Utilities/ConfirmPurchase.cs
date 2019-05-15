using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmPurchase : MonoBehaviour
{
    public BuyItem buyItem;
    
    public void Exit()
    {
        Destroy(gameObject);
    }

    public void Purchase()
    {
        buyItem.Confirmed();
        Exit();
    }
}
