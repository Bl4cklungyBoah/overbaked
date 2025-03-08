using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlCounter : BaseCounter
{
    public event EventHandler OnBowlSpawned;
    public event EventHandler OnBowlRemoved;

    [SerializeField] private KitchenObjectSO bowlKitchenObjectSO;

    private float spawnBowlTimer;
    private float spawnBowlTimerMax = 4f;
    private int bowlsSpawnedAmount;
    private int bowlsSpawnedAmountMax = 3;

    private void Update()
    {
        spawnBowlTimer += Time.deltaTime;
        if(spawnBowlTimer > spawnBowlTimerMax)
        {
            spawnBowlTimer = 0f;

            if(bowlsSpawnedAmount < bowlsSpawnedAmountMax)
            {
                bowlsSpawnedAmount++;

                OnBowlSpawned?.Invoke(this, EventArgs.Empty);
            }

        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            // player carries nothing
            if(bowlsSpawnedAmount > 0)
            {
                //at least one bowl spawned
                bowlsSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(bowlKitchenObjectSO, player);

                OnBowlRemoved.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
