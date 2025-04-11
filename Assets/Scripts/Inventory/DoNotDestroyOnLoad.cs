using UnityEngine;

namespace Inventory
{
    public class DoNotDestroyOnLoad : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
