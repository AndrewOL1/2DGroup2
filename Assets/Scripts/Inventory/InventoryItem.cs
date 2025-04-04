using UnityEngine;

namespace Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        public int ID;
        public string Name;
        
        private void OnMouseDown()
        {
            InventoryManager.Instance.AddItemToInventory(this);//might need to set it to send its prefab so i can be spawned again
            Destroy(gameObject);
        }
    }
}
