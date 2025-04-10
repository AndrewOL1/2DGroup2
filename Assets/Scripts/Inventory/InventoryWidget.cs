using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryWidget : MonoBehaviour
    {
        public int id;

        public void UseItem()
        {
            InventoryManager.Instance.UseItem(id);
        }

    }
}
