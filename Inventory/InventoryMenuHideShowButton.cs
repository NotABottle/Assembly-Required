using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuHideShowButton : MonoBehaviour
{
    public Animator menuAnimator;

    private bool IsInventoryVisible = true;

    public void Update(){
        if(Input.GetKeyDown(KeyCode.Tab)) SwapInventoryVisibilityState();
    }

    public void SwapInventoryVisibilityState(){
        if(IsInventoryVisible){
            //Hide inventory
            IsInventoryVisible = false;
            //Play Hide Animation
            menuAnimator.SetTrigger("Hide Inventory");
        }else{
            //Show inventory
            IsInventoryVisible = true;
            //Play Show Animation
            menuAnimator.SetTrigger("Show Inventory");
        }
    }
}
