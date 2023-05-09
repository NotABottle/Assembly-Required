using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Page
{
        public const int MaximumNumberOfItemsPerPage = 7;

        public List<InventoryItem> listOfItems;
        public Transform pageTransform;

        private int numberOfItemsInPage;

        public Page(){
            listOfItems = new List<InventoryItem>();
            numberOfItemsInPage = 0;
        }

        public void AddItemToPage(InventoryItem item){
            numberOfItemsInPage++;
            listOfItems.Add(item);
        }

        public bool IsFull(){
            if(numberOfItemsInPage >= MaximumNumberOfItemsPerPage){
                return true;
            }
            return false;
        }
    
}
