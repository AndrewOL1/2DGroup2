using UnityEngine;

namespace Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        public int ID;
        public string Name;
        
        private void OnMouseDown()
        {
            //check player state and decide to interact or pick up
            InventoryManager.Instance.AddItemToInventory(this);//might need to set it to send its prefab so i can be spawned again
            Destroy(gameObject);//maybe turn off visual and the collider
        }
    }
}
