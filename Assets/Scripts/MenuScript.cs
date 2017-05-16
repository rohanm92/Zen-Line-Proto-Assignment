using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
    public Text highScoreText;

    // Use this for initialization
    void Start () {
        highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
    }
    public void Play() {
        SceneManager.LoadScene("PlayScreen");
    }
}