using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GeneratorObject levelGenerator;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private UIController uiController;
    [SerializeField] private List<Transform> movementLines;

    private void Start()
    {
        player.Init(movementLines);
        StartGame();
    }

    public void StartGame()
    {
        levelGenerator.enabled = true;
        levelGenerator.StartGenerator();
        player.gameObject.SetActive(true);
        uiController.ShowGameUI();
    }
}
