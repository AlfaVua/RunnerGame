using UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GUIController guiController;
    public void ShowGameUI()
    {
        guiController.gameObject.SetActive(true);
    }

    public void ShowGameOverUI()
    {
        guiController.gameObject.SetActive(false);
    }

    public void UpdateScore(float score)
    {
        guiController.UpdateScore(score);
    }
}
