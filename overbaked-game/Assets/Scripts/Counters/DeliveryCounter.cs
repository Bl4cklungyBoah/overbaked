using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeliveryCounter : BaseCounter
{

    [SerializeField] KitchenObjectSO targetKitchenObjectSO;

    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
            KitchenObject kitchenObject = player.GetKitchenObject();

            if(kitchenObject.GetKitchenObjectSO() == targetKitchenObjectSO)
            {
                //only accepts bowls
                Debug.Log("bread delivered");
                player.GetKitchenObject().DestroySelf();
            }
            else
            {
                Debug.Log("bread not delivered. something went wrong here.");
            }

        }
    }
}
