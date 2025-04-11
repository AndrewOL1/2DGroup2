using UnityEngine;

namespace Highlight
{
    public class Highlight : MonoBehaviour
    {
        
        private Color _startColour;
        [Header("Color")]
        [SerializeField]
        [Tooltip("It will apply this color to the SpriteRenderer on mouse over")] 
        private Color highlightColor;
        private void Start()
        {
            _startColour = GetComponent<Renderer>().material.color; 
        }
        private void OnMouseOver()
        {
            GetComponent<Renderer>().material.color = highlightColor;
        }
        private void OnMouseExit()
        {
            GetComponent<Renderer>().material.color = _startColour;
        }
        
    }
}
