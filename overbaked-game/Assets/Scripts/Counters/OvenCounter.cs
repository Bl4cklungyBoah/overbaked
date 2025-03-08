using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DoughCounter;

public class OvenCounter : BaseCounter, IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    private enum State
    {
        Idle,
        Baking,
        Baked,
        Burned,
    }

    [SerializeField] private BakingRecipeSO[] bakingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float bakingTimer;
    private BakingRecipeSO bakingRecipeSO;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;


    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if(HasKitchenObject())
        {
            switch(state)
            {
                case State.Idle:

                    break;
                case State.Baking:
                    bakingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = bakingTimer / bakingRecipeSO.bakingTimerMax
                    });

                    if (bakingTimer > bakingRecipeSO.bakingTimerMax)
                    {
                        //Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(bakingRecipeSO.output, this);
                        
                        state = State.Baked;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    }
                    break;
                case State.Baked:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        //Burned
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned; 

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:

                    break;

            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // counter has no object
            if (player.HasKitchenObject())
            {
                // player has object
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player carries object from recipe
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    bakingRecipeSO = GetBakingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Baking;
                    bakingTimer = 0f;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = bakingTimer / bakingRecipeSO.bakingTimerMax
                    });
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
                // player picks up object
                state = State.Idle;
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        BakingRecipeSO bakingRecipeSO = GetBakingRecipeSOWithInput(inputKitchenObjectSO);
        if (bakingRecipeSO != null)
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
        BakingRecipeSO bakingRecipeSO = GetBakingRecipeSOWithInput(inputKitchenObjectSO);
        if (bakingRecipeSO != null)
        {
            return bakingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private BakingRecipeSO GetBakingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BakingRecipeSO bakingRecipeSO in bakingRecipeSOArray)
        {
            if (bakingRecipeSO.input == inputKitchenObjectSO)
            {
                return bakingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
