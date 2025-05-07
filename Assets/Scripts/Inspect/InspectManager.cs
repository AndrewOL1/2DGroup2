using System.Collections;
using Controllers;
using Input;
using Inventory;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Inspect
{
    public class InspectManager : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        private bool _inspectIsActive=false;
        [SerializeField]
        private PostProcessLayer postProcessLayer;
        [SerializeField]
        private InputReader inputReader;
        public bool inspectInput = false;

        public bool delayedDialogue=false;
        public static InspectManager Instance { get; private set; }

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

        private void Start()
        {
            inputReader.InspectEvent += HandleInspect;
        }

        private void HandleInspect()
        {
            if (inspectInput)return;
            inspectInput = true;
            StartCoroutine(Delay());
        }

        // Update is called once per frame
        public void InspectObjectInInventory(Sprite sprite,Vector2 rectTransform)
        {
            image.gameObject.SetActive(true);
            RectTransform temp =image.gameObject.GetComponent<RectTransform>();
            temp.sizeDelta = rectTransform;
            image.sprite = sprite;
            _inspectIsActive = true;
            postProcessLayer.enabled = true;
        }
        public void InspectObjectInInventory(Sprite sprite)
        {
            image.gameObject.SetActive(true);
            image.sprite = sprite;
            _inspectIsActive = true;
            postProcessLayer.enabled = true;
        }
        public void InspectAndStoreInInventory(GameObject obj)
        {
            image.sprite=obj.GetComponent<SpriteRenderer>().sprite;
            image.gameObject.SetActive(true);
            _inspectIsActive = true;
            postProcessLayer.enabled = true;
            InventoryManager.Instance.AddItemToInventory(obj.GetComponent<InventoryItem>());
            delayedDialogue = true;
        }
        public void InspectAndStoreInInventory(GameObject obj,Vector2 rectTransform)
        {
            image.sprite = obj.GetComponent<SpriteRenderer>().sprite;
            RectTransform temp =image.gameObject.GetComponent<RectTransform>();
            temp.sizeDelta = rectTransform;
            image.gameObject.SetActive(true);
            postProcessLayer.enabled = true;
            _inspectIsActive = true;
            //call to invent to store it
            InventoryManager.Instance.AddItemToInventory(obj.GetComponent<InventoryItem>());
            delayedDialogue = true;
        }

        public void TurnOffInspect()
        {
            _inspectIsActive = false;
            postProcessLayer.enabled = false;
        }

        public bool GetIsInspectActive()
        {
            return _inspectIsActive;
        }
    
        private IEnumerator Delay()
        {
                yield return new WaitForSeconds(0.1f);
                inspectInput = false;
        }
    }
}
