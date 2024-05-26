using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIController : BaseUICanvas
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button closeGameButton;
    [SerializeField] private TextMeshProUGUI totalCoins;
    [SerializeField] private TextMeshProUGUI maxScore;
    [SerializeField] private TextMeshProUGUI reachedScore;
    
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

    public void UpdateView(float score, float maxScore, int totalCoins)
    {
        this.totalCoins.text = totalCoins.ToString();
        this.maxScore.text = Mathf.FloorToInt(maxScore).ToString();
        reachedScore.text = Mathf.FloorToInt(score).ToString();
    }
}