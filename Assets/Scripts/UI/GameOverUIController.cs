using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIController : BaseUICanvas
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button closeGameButton;
    
    private void OnEnable()
    {
        closeGameButton.onClick.AddListener(CloseGame);
        newGameButton.onClick.AddListener(StartNewGame);
    }

    private void OnDisable()
    {
        closeGameButton.onClick.RemoveListener(CloseGame);
        newGameButton.onClick.RemoveListener(StartNewGame);
    }

    private static void CloseGame()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
#endif
        Application.Quit();
    }

    private static void StartNewGame()
    {
        GlobalEvents.CallEvent(EventNames.StartNewGame);
    }
}