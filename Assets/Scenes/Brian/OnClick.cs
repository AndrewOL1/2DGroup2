using UnityEngine;
using UnityEngine.InputSystem;
using Inventory;

public class MouseClickSound : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = InventoryManager.Instance.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        
        //audioSource.PlayOneShot(clickSound);
    }
    public void PlaySound()
    {
        _audioSource.PlayOneShot(clickSound);
    }
}
