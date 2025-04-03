using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    
    public TextMeshProUGUI scoreText;

    void Update()
    {
        scoreText.text = "Score: " + _Gamemanager.Instance.score.ToString();
    }
}
