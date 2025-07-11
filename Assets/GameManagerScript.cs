using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [Range(1, 2)] public float globalSpeed;
    public GameObject car;
    public GameObject starGroup;
    public Transform carPositionsTransform;
    public int minCarSpawnRate;
    public int maxCarSpawnRate;
    public int starSpawnRate;
    public Text scoreText;
    public GameObject pauseUI;
    public GameObject gameoverUI;
    public Text gameoverScoreText;
    public Text gameoverHighscoreText;
    public Transform scoreUITransform;

    List<GameObject> carPositionsList = new List<GameObject>();
    float carSpawnTimer = 0;
    int carSpawnRate;
    float starSpawnTimer = 0;
    float scoreRate = 0.125f;
    float scoreTimer = 0;
    int score;
    int highscore;
    bool isPaused = false;

    // sounds
    public AudioSource hornSound;
    public AudioSource starSound;
    public AudioSource clickSound;
    public AudioSource drivingSound;
    public AudioSource crashSound;

    private void Start()
    {
        // loading highscore
        highscore = PlayerPrefs.GetInt("Highscore", 0);

        for (int i=0; i<carPositionsTransform.childCount; i++)
        {
            carPositionsList.Add(carPositionsTransform.GetChild(i).gameObject);
        }

        carSpawnRate = Random.Range(minCarSpawnRate, maxCarSpawnRate);
    }

    private void Update()
    {
        // managing timers

        // SCORE
        if (scoreTimer < scoreRate)
        {
            scoreTimer += Time.deltaTime;
        }
        else
        {
            // increase score
            UpdateScore(1);
            scoreTimer = 0;
        }

        // CARS
        if (carSpawnTimer < (carSpawnRate / globalSpeed))
        {
            carSpawnTimer += Time.deltaTime;
        }
        else
        {
            // spawn the car
            Instantiate(car, GetRandomCarPosition(), car.transform.rotation);
            carSpawnTimer = 0;
            carSpawnRate = Random.Range(minCarSpawnRate, maxCarSpawnRate);
        }

        // STARS
        if (starSpawnTimer < (starSpawnRate / globalSpeed))
        {
            starSpawnTimer += Time.deltaTime;
        }
        else
        {
            // spawn the star group
            Instantiate(starGroup, GetRandomCarPosition(), starGroup.transform.rotation);
            starSpawnTimer = 0;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GamePause();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!hornSound.isPlaying)
                hornSound.Play();
        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();
        globalSpeed = Mathf.Min(globalSpeed + 0.001f, 2);
    }

    public void PlayStarSound()
    {
        starSound.Play();
    }

    // function to get random position to spawn a new car
    Vector3 GetRandomCarPosition()
    {
        return carPositionsList[Random.Range(0, carPositionsList.Count)].transform.position;
    }

    public void GamePause()
    {
        clickSound.Play();
        if (isPaused)
        {
            drivingSound.Play();
            // unpause the game if already paused
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
        else
        {
            // pause the game
            drivingSound.Pause();
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }

        isPaused = !isPaused;
    }

    public void Gameover()
    {
        crashSound.Play();
        // saving highscore if needed
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore);
        }

        Time.timeScale = 0;
        gameoverUI.SetActive(true);
        gameoverScoreText.text = "You scored: " + score.ToString() + "!";
        gameoverHighscoreText.text = "Your Best: " + highscore.ToString() + "!";
    }

    public void QuitButtonClicked()
    {
        clickSound.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ReplayButtonClicked()
    {
        clickSound.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
