using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/*
Handles updating information regarding the Menu item, e.g. its icon, etc.
*/
public class InventoryMenuItem : MonoBehaviour
{
    public string Name;
    public Image Icon;

    private InventoryItem itemData;
    private InventoryMenuManager menuManager;

    public void AssignValues(InventoryItem item,InventoryMenuManager menuManagerRef){
        Name = item.BrickName;
        Icon.sprite = item.BrickIcon;
        itemData = item;
        menuManager = menuManagerRef;
    }

    public void ItemWasSelected(){
        menuManager.MenuItemWasClicked(itemData);
    }
}
