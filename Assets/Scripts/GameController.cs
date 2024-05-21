using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private UIController uiController;
    [SerializeField] private List<Transform> movementLines;

    private float _score;

    private void Start()
    {
        player.Init(movementLines);
        StartGame();
        GlobalEvents.AddAction(EventNames.GameOver, OnGameOver);
    }

    public void StartGame()
    {
        _score = 0;
        levelGenerator.enabled = true;
        levelGenerator.StartGenerator();
        player.gameObject.SetActive(true);
        uiController.ShowGameUI();
    }

    private void OnGameOver(object data)
    {
        uiController.ShowGameOverUI();
    }

    private void Update()
    {
        _score += levelGenerator.ShiftingSpeed * .1f * Time.deltaTime;
        uiController.UpdateScore(_score);
    }
}
