using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuPageTurner : MonoBehaviour
{
    private InventoryMenuInitializer initializer;
    private List<Page> listOfPages;

    private int currentActivePage;
    private int numberOfPages;

    private void Awake(){
        initializer = GetComponent<InventoryMenuInitializer>();
    }

    private void UpdatePageCount(){
        listOfPages = initializer.listOfPages;
        numberOfPages = listOfPages.Count;
    }

    public void CycleRight(){
        UpdatePageCount();
        HideCurrentPage(currentActivePage);

        currentActivePage++;
        if(currentActivePage >= numberOfPages) currentActivePage = 0;

        ShowTargetPage(currentActivePage);
    }

    public void CycleLeft(){
        UpdatePageCount();
        HideCurrentPage(currentActivePage);

        currentActivePage--;
        if(currentActivePage <= -1) currentActivePage = numberOfPages-1;

        ShowTargetPage(currentActivePage);
    }

    private void ShowTargetPage(int pageIndex){
        listOfPages[currentActivePage].pageTransform.gameObject.SetActive(true);
    }

    private void HideCurrentPage(int pageIndex){
        listOfPages[currentActivePage].pageTransform.gameObject.SetActive(false);
    }
}
