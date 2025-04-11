using SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Grid
{
   public class UIPlacementOverride : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
   {

      public void OnPointerEnter(PointerEventData eventData)
      {
         SceneManager.Instance.overUI = true;
      }

      public void OnPointerExit(PointerEventData eventData)
      {
         SceneManager.Instance.overUI = false;
      }
   }
}
