using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ConsumableObject")]
public class ConsumableItems : ItemObject
{
    public bool used;

    private void Awake()
    {
        type = ItemType.Consumable;
    }
}
