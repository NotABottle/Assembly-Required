using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Takes in inventory SO and then unpacks it spawning the various ui elements
and filling them in with their appropriate information, icons, quanity, etc.
*/
public class InventoryMenuInitializer : MonoBehaviour
{
    public Inventory inventory;
    public GameObject PagePrefab;
    public Transform PageParent;
    public InventoryMenuItem inventoryMenuItemPrefab;

    public List<Page> listOfPages;
    private InventoryMenuManager menuManager;

    private void Awake(){
        menuManager = GetComponent<InventoryMenuManager>();
    }

    public void InitializeInventoryMenu()
    {
        CreateNeededPages();
        AddItemsToPages();

        SpawnPageUIObjects();
        SpawnItemUIObjects();

        HideAllPagesExceptFirst();
    }

    private void HideAllPagesExceptFirst()
    {
        for (int i = 0; i < listOfPages.Count; i++)
        {
            if (i != 0) listOfPages[i].pageTransform.gameObject.SetActive(false);
        }
    }

    private void SpawnItemUIObjects()
    {
        foreach (Page p in listOfPages)
        {
            foreach (InventoryItem i in p.listOfItems)
            {
                var itemParent = p.pageTransform;
                var itemObject = Instantiate(inventoryMenuItemPrefab, itemParent);
                itemObject.GetComponent<InventoryMenuItem>().AssignValues(i, menuManager);
            }
        }
    }

    private void SpawnPageUIObjects()
    {
        foreach (Page p in listOfPages)
        {
            var pageObject = Instantiate(PagePrefab, PageParent);
            p.pageTransform = pageObject.transform;
        }
    }

    private void AddItemsToPages()
    {
        var ItemsToBeSortedIntoPages = inventory.InventoryItems;
        int pageIndex = 0;
        foreach (InventoryItem item in ItemsToBeSortedIntoPages)
        {
            var pageToBeFilled = listOfPages[pageIndex];
            if (pageToBeFilled.IsFull())
            {
                pageIndex++;
                pageToBeFilled = listOfPages[pageIndex];
            }

            pageToBeFilled.AddItemToPage(item);
        }
    }

    private void CreateNeededPages()
    {
        var numberOfItems = inventory.InventoryItems.Length;
        var numberOfPages = Mathf.CeilToInt((float)numberOfItems / (float)Page.MaximumNumberOfItemsPerPage);
        for (int i = 0; i < numberOfPages; i++)
        {
            var newPage = new Page();
            listOfPages.Add(newPage);
        }
    }
}
