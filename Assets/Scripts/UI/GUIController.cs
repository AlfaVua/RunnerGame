using TMPro;
using UnityEngine;

namespace UI
{
    public class GUIController : BaseUICanvas
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI coinsText;

        public void UpdateScore(float score)
        {
            scoreText.text = "Score: " + Mathf.Floor(score);
        }

        public void UpdateCoins(int coins)
        {
            coinsText.text = coins.ToString();
        }

        protected override void OnShow()
        {
            base.OnShow();
            scoreText.text = "0";
            coinsText.text = "0";
        }
    }
}