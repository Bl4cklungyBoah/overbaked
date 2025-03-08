using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoughCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField] private DoughingRecipeSO[] doughingRecipeSOArray;

    private int doughProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // counter has no object
            if (player.HasKitchenObject())
            {
                // player has object
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player carreis object from recipe
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    doughProgress = 0;

                    DoughingRecipeSO doughingRecipeSO = GetDoughingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)doughProgress / doughingRecipeSO.doughProgressMax
                    }) ;
                }

                // player drops object on counter
            }
        }
        else
        {
            //counter has object
            if (!player.HasKitchenObject())
            {
                // player does not have object
                GetKitchenObject().SetKitchenObjectParent(player);
                // player pick up object
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            // there is a KitchenObject here AND its an input from a recipe          
            doughProgress++;

            DoughingRecipeSO doughingRecipeSO = GetDoughingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)doughProgress / doughingRecipeSO.doughProgressMax
            });

            if(doughProgress >= doughingRecipeSO.doughProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }

            

        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        DoughingRecipeSO doughingRecipeSO = GetDoughingRecipeSOWithInput(inputKitchenObjectSO);
        if (doughingRecipeSO != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        DoughingRecipeSO doughingRecipeSO = GetDoughingRecipeSOWithInput(inputKitchenObjectSO);
        if (doughingRecipeSO != null)
        {
            return doughingRecipeSO.output;
        }
        else
        {
            return null;
        }


    }

    private DoughingRecipeSO GetDoughingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (DoughingRecipeSO doughingRecipeSO in doughingRecipeSOArray)
        {
            if (doughingRecipeSO.input == inputKitchenObjectSO)
            {
                return doughingRecipeSO;
            }
        }
        return null;
    }
}
