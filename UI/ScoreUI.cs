using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    #region Singleton
    static private ScoreUI s_Instance;
    #endregion

    public TextMeshProUGUI m_ScoreText;
    public TextMeshProUGUI m_HiScoreText;

    private int m_CurrentScore = 0;

    private void Awake() 
    {
        s_Instance = this;
    }

    private void Start()
    {
        m_HiScoreText.text = PlayerPrefs.GetInt("hiScore").ToString();
    }

    public static void GainScore(int amount)
    {
        s_Instance.UpdateScoreUI(amount);
    }

    public static void SaveScore()
    {
        s_Instance.Save();
    }

    public void Save()
    {
        if(PlayerPrefs.GetInt("hiScore") < m_CurrentScore)
        {
            PlayerPrefs.SetInt("hiScore",m_CurrentScore);
        }
    }

    public void UpdateScoreUI(int amount)
    {
        m_CurrentScore += amount;
        m_ScoreText.text = m_CurrentScore.ToString();
    }
}
