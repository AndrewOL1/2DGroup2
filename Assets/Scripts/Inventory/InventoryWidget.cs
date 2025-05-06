using System;
using System.Collections;
using Inspect;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryWidget : MonoBehaviour, IPointerClickHandler
    {
        public int id;
        private Sprite _sprite;
        private bool _delay;
        public Vector2 size;

        private void Start()
        {
            _sprite = GetComponent<Image>().sprite;
        }

        public void UseItem()
        {
            InventoryManager.Instance.UseItem(id);
        }
        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.1f);
            _delay = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!InspectManager.Instance.inspectInput || _delay) return;
            InspectManager.Instance.InspectObjectInInventory(_sprite,size);
            _delay = true;
            StartCoroutine(Delay());
        }
    }
}
