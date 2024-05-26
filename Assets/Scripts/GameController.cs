using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private UIController uiController;
    [SerializeField] private List<Transform> movementLines;

    [SerializeField] private UserData userData;

    private int _coinAmount = 0;
    private float _score;

    private void Start()
    {
        player.Init(movementLines);
        StartNewGame();
        GlobalEvents.AddAction(EventNames.GameOver, OnGameOver);
        GlobalEvents.AddAction(EventNames.StartNewGame, StartNewGame);
        GlobalEvents.AddAction(EventNames.CoinTaken, OnCoinPickup);
    }

    private void ResetLocal()
    {
        _score = _coinAmount = 0;
        player.transform.position = new Vector3();
    }

    private void StartNewGame(object data = null)
    {
        ResetLocal();
        levelGenerator.enabled = true;
        levelGenerator.StartGenerator();
        player.gameObject.SetActive(true);
        uiController.ShowGameUI();
    }

    private void OnGameOver(object data)
    {
        player.gameObject.SetActive(false);
        levelGenerator.enabled = false;
        userData.maxScore = Mathf.Max(userData.maxScore, _score);
        userData.totalCoins += _coinAmount;
        ShowGameOverUI();
    }

    private void Update()
    {
        _score += levelGenerator.ShiftingSpeed * .1f * Time.deltaTime;
        uiController.UpdateScore(_score);
    }

    private void OnCoinPickup(object data)
    {
        uiController.UpdateGUICoins(++_coinAmount);
    }

    private void ShowGameOverUI()
    {
        uiController.UpdateGameOverView(_score, userData.maxScore, userData.totalCoins);
        uiController.ShowGameOverUI();
    }
}
