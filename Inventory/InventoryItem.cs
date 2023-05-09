using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Brick Item", menuName = "Inventory Menu/Inventory Item", order = 2)]
public class InventoryItem : ScriptableObject
{
    public string BrickName;
    public Sprite BrickIcon;
    public Brick BrickPrefab;

}
