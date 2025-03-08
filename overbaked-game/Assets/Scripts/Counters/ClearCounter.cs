using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{


    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            // counter has no object
            if(player.HasKitchenObject())
            {
                //player has object
                player.GetKitchenObject().SetKitchenObjectParent(this);
                //player drops object on counter
            }
        }
        else
        {
            //counter has object
            if(player.HasKitchenObject())
            {
                //player has object
                if(player.GetKitchenObject().TryGetBowl(out BowlKitchenObject bowlKitchenObject))
                {
                    //player has a bowl
                    if (bowlKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //player does not have a bowl but something else
                    if(GetKitchenObject().TryGetBowl(out bowlKitchenObject))
                    {
                        // Counter has a bowl
                        if(bowlKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                //player does not have object
                GetKitchenObject().SetKitchenObjectParent(player);
                //player picks up object
                
            }
        }
    }
}
