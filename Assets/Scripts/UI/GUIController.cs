using TMPro;
using UnityEngine;

namespace UI
{
    public class GUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        public void UpdateScore(float score)
        {
            scoreText.text = "Score: " + Mathf.Floor(score);
        }
    }
}