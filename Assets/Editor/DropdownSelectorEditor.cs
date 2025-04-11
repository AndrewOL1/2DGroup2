using DropDown;
using SceneManagement;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Editor
{
    [CustomEditor(typeof(SceneTransition))]
    public class DropdownSelectorEditor : UnityEditor.Editor
    {
        private List<string> _sceneOptions;
        private string[] _sceneOptionsArray;// Dropdown options
        private int _selectedIndex = 0;

        private SceneTransition _myTarget;
        //sceneList = AssetDatabase.LoadAssetAtPath<SceneList>("Assets/Scripts/SceneManagement/SceneList.asset");
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            _myTarget = (SceneTransition)target;
            _sceneOptions = _myTarget.sceneList.scenes;
            
            // Find the current index of the stored value
            if (_myTarget.SelectedScene == null)
                _selectedIndex = 0;
            else
            {
                _selectedIndex=_sceneOptions.IndexOf(_myTarget.SelectedScene);
            }

            // Display the dropdown
            _sceneOptionsArray=_sceneOptions.ToArray();
            int newIndex = EditorGUILayout.Popup("Select Scene", _selectedIndex, _sceneOptionsArray);
            // Save the selected option

            if (newIndex != _selectedIndex)
            {
                _selectedIndex = newIndex;
                _myTarget.GetType().GetField("selectedScene",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .SetValue(_myTarget, _sceneOptions[_selectedIndex]);
            }

            // Apply changes
            if (GUI.changed)
            {
                EditorUtility.SetDirty(_myTarget);
            }
        }
    }
}
