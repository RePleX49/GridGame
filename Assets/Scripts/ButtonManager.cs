using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Text Highscore;

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void Start()
    {
        Highscore.text = "Gold Collected: " + GridManagerScript.Instance.TotalScore;
    }
}
