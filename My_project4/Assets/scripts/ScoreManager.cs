using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // Score initial
    public TMP_Text scoreText; // Texte UI en TextMeshPro

    private void Start()
    {
        UpdateScoreDisplay(); // Afficher le score initial
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
        else
        {
            Debug.LogError("ScoreText (TextMeshPro) n'est pas assigné dans l'inspecteur !");
        }
    }
}
