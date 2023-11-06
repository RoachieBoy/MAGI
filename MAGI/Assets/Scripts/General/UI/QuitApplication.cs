using UnityEngine;
using UnityEngine.UI;

namespace General.UI
{
    public class QuitApplication : MonoBehaviour
    {
        private Button _quitButton;
    
        private void Awake()
        {
            _quitButton = gameObject.GetComponent<Button>();
            
            _quitButton.onClick.AddListener(Quit);
        }
    
        private static void Quit()
        {
            Application.Quit();
        }
    
        private void OnDestroy()
        {
            _quitButton.onClick.RemoveAllListeners();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Quit();
            }
        }
    }
}
