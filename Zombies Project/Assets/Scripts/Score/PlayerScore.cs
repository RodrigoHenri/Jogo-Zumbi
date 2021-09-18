using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerScore : MonoBehaviour
{
    [Header("Player Score")]
    [SerializeField] private int ActualScore = 0;

    [Header("Score Text")]
    [SerializeField] private TextMeshProUGUI Text_PlayerActualScore;

    [Header("Game Over Settings")]
    [SerializeField] private int HighScoreRegistered;
    [SerializeField] private TextMeshProUGUI Text_MyHighScore;
    [SerializeField] private TextMeshProUGUI GameOver_text_ActualScore;

    void Start()
    {
        HighScoreRegistered = PlayerPrefs.GetInt("HighScore");
    }

    public void SavePointsWhenDied()
    {
        if (ActualScore > HighScoreRegistered)
        {
            HighScoreRegistered = ActualScore;
            PlayerPrefs.SetInt("HighScore", ActualScore);
        }

        GameOver_text_ActualScore.text = "Sua Pontuação: " + ActualScore;

        Text_MyHighScore.text = "Seu Record: " + HighScoreRegistered;
    }

    public void UpdateActualScore(int PointsToIncrement)
    {
        ActualScore = ActualScore + PointsToIncrement;
        Text_PlayerActualScore.text = "Pontuação: " + ActualScore;
    }

    public void QuitAndSaveHighScore()
    {
        if (ActualScore > HighScoreRegistered)
        {
            PlayerPrefs.SetInt("HighScore", ActualScore);
        }

    }
}
