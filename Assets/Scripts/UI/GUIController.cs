using TMPro;
using UnityEngine;

namespace UI
{
    public class GUIController : BaseUICanvas
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        public void UpdateScore(float score)
        {
            scoreText.text = "Score: " + Mathf.Floor(score);
        }
    }
}