using UnityEngine;

namespace DropDown
{
    public class DropdownSelector : MonoBehaviour
    {
        [SerializeField] private string selectedValue;
        public string SelectedValue => selectedValue;
    }
}
