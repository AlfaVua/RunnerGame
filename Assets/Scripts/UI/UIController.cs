using UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GUIController guiController;
    [SerializeField] private GameOverUIController gameOverController;
    
    public void ShowGameUI()
    {
        guiController.Show();
        gameOverController.Hide();
    }

    public void ShowGameOverUI()
    {
        guiController.Hide();
        gameOverController.Show();
    }

    public void UpdateScore(float score)
    {
        guiController.UpdateScore(score);
    }

    public void UpdateGUICoins(int coinAmount)
    {
        guiController.UpdateCoins(coinAmount);
    }

    public void UpdateGameOverView(float score, float maxScore, int totalCoins)
    {
        gameOverController.UpdateView(score, maxScore, totalCoins);
    }
}
