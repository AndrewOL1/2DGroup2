using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        private InventoryItem[] _items; 
        [SerializeField] private GameObject itemWidget;
        [SerializeField] private InventoryItem item;
        [SerializeField] private GameObject inventoryBackground;
        [SerializeField] private int inventorySize;
        private int _inventoryCount;
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
            GameObject temp = Instantiate(itemWidget, inventoryBackground.transform, true);
            UpdateWidget(item, temp);
            _inventoryCount++;
        }

        private void UpdateWidget(InventoryItem item,GameObject temp)
        {
            temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Name;//maybe display name on hover over
            //update icon
        }
        public void RemoveItemFromInventory(InventoryItem item)
        {
            //find item index delete Inventory item script then delete the widget with corresponding index
        }
    }
}
