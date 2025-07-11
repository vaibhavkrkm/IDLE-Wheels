using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Text highscoreText;
    public AudioSource mainMusic;
    public AudioSource clickSound;

    [HideInInspector] public int highscore;

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore", 0);    // loading highscore
        highscoreText.text = "<i>Highscore: " + highscore.ToString() + "</i>";

        GameObject.Find("Logo").GetComponent<Animator>().speed = 0.4f;    // modifying logo animator speed
    }

    public void PlayButtonClicked()
    {
        clickSound.Play();
        SceneManager.LoadScene("Game");
    }

    public void QuitButtonClicked()
    {
        clickSound.Play();
        Application.Quit();
    }

    public void MoreGamesButtonClicked()
    {
        clickSound.Play();
        Application.OpenURL("https://lets-connect-team.itch.io/");
    }
}
