using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        private InventoryItem[] _items;  
        [SerializeField] 
        [Tooltip("The UI Item Widget")]private GameObject itemWidget;
        private InventoryItem _item;
        [SerializeField] private GameObject inventoryBackgroundRow1,inventoryBackgroundRow2;
        [SerializeField] private int inventorySize;
        private Dictionary<int, string> _itemDictionary = new Dictionary<int, string>();
        private int _inventoryCount;    
        [SerializeField]
        List<InventoryWidgetItem> inventoryWidgetItems = new List<InventoryWidgetItem>();

        public static InventoryManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (Instance != this)
                Destroy(gameObject);
        }

        public void AddItemToInventory(InventoryItem item)
        {
            if (_inventoryCount >= inventorySize) return;
            _itemDictionary.Add(item.ID, item.name);
            GameObject temp;
            if(_inventoryCount<4)
            {
               
                temp = Instantiate(itemWidget, inventoryBackgroundRow1.transform, true);
            }
            else
            {
                temp = Instantiate(itemWidget, inventoryBackgroundRow2.transform, true);
            }

            UpdateWidget(item, temp);
            temp.GetComponent<InventoryWidget>().id = item.ID;
            _inventoryCount++;
        }
        //need a check when loading scene to unload pickup if we are holding it
        private void UpdateWidget(InventoryItem item,GameObject temp)
        {
            //maybe display name on hover over
            //update icon
            var index = 0;
            for (; index < inventoryWidgetItems.Count; index++)
            {
                var iWi = inventoryWidgetItems[index];
                if (item.ID == iWi.id)
                    temp.GetComponent<Image>().sprite = iWi.sprite;
            }
            temp.GetComponent<InventoryWidget>().size=item.Size;
        }
        public void RemoveItemFromInventory(int id )
        {
            int count = -1;
            foreach (var item in _itemDictionary)
            {
                count++;
                if (item.Key == id)
                {
                    _itemDictionary.Remove(item.Key);
                    if (count < 3)
                        Destroy(inventoryBackgroundRow1.transform.GetChild(count).gameObject);
                    else if (count < 8)
                        Destroy(inventoryBackgroundRow2.transform.GetChild(count-4).gameObject);
                    return;
                }
            }
        }

        public bool CheckIfInInventory(int id)
        {
            return _itemDictionary.ContainsKey(id);
        }

        public void UseItem(int id)
        {
            PlayerController.Instance.SetActiveItem(id);
        }

        [Serializable]
        public struct InventoryWidgetItem
        {
            public int id;
            public string name;
            public Sprite sprite;
        }
    }
}
