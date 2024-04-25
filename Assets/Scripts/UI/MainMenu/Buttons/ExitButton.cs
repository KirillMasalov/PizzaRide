using General;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu.Buttons
{
    public class ExitButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(GameController.Instance.QuitGame);
        }
        
        private void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
}