using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoughCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // counter has no object
            if (player.HasKitchenObject())
            {
                //player has object
                player.GetKitchenObject().SetKitchenObjectParent(this);
                //player drops object on counter
            }
        }
        else
        {
            //counter has object
            if (!player.HasKitchenObject())
            {
                //player does not have object
                GetKitchenObject().SetKitchenObjectParent(player);
                //player pick up object
            }
        }
    }
}
