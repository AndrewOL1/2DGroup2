using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement
{
    [CreateAssetMenu(fileName = "SceneList", menuName = "Data/Scenelist")]
    public class SceneList : ScriptableObject
    {
        public  List<string> scenes = new List<string>();

    }
}
