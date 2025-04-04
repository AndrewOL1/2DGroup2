using Inventory;
using UnityEngine;

namespace Player
{
    /*
     * Store all const varibles for the player
     */
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerConfiguration : ScriptableObject
    {
        
        [Header("Interaction Values")]
        public GameObject interactionObject;
        [Header("lastCheckpoint")]
        public Vector3 lastCheckpoint;
        [Header("ItemSlots")]
        public InventoryItem[] itemSlots;
    }
}
