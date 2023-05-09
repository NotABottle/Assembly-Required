using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory Menu/Inventory", order = 1)]
public class Inventory : ScriptableObject
{
    public InventoryItem[] InventoryItems;
}
