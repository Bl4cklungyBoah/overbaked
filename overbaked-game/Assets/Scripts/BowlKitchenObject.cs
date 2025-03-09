using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    } 


    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    [SerializeField] private KitchenObjectSO bowlCompleteSO;
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //not a valid ingerdient
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            if (IsBowlFilled())
            {
                //if the bowl is filled
                
                DestroySelf();
                SpawnKitchenObject(bowlCompleteSO, this.GetKitchenObjectParent());
            }
            return true;
        }
    }

    private bool IsBowlFilled()
    {
        HashSet<KitchenObjectSO> currentIngredients = new HashSet<KitchenObjectSO>(kitchenObjectSOList);
        HashSet<KitchenObjectSO> requiredIngredients = new HashSet<KitchenObjectSO>(validKitchenObjectSOList);

        if(currentIngredients.SetEquals(requiredIngredients))
        {
            
            Debug.Log("all ingredients aquired");
            return true;
        }
        else
        {
            Debug.Log("not all ingredients aquired");
            return false;
        }
    }

    
}
