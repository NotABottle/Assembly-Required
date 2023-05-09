using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
Delegates tasks to other scripts on how the inventory should behave
e.g. telling the initializer when to create the menu elements
*/
public class InventoryMenuManager : MonoBehaviour
{
    public event Action<InventoryItem> OnMenuItemWasClicked;

    private void Awake()
    {
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        var inventoryMenuInitializer = GetComponent<InventoryMenuInitializer>();
        if (inventoryMenuInitializer != null) inventoryMenuInitializer.InitializeInventoryMenu();
    }

    public void MenuItemWasClicked(InventoryItem item){
        OnMenuItemWasClicked?.Invoke(item);
    }

    public void StartListeningForClick(Action<InventoryItem> listener){
        OnMenuItemWasClicked += listener;
    }

    public void StopListeningForClick(Action<InventoryItem> listener){
        OnMenuItemWasClicked -= listener;
    }
}
