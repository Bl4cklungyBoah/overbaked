using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

[CreateAssetMenu()]
public class DoughingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int doughProgressMax;
}
