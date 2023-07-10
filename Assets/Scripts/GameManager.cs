using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject platformSpawner;
    public GameObject gamePlayUI;
    public bool gameStarted;
    public GameObject menuUI;


    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    int score = 0;
    int bestScore;

    AudioSource audioSource;
    public AudioClip[] gameMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
        bestScoreText.text = "Best Score : " + bestScore;
    }

    void Update()
    {
        if (!gameStarted)
        {
            if (Input.GetMouseButton(0))
            {
                GameStart();
            }
        }
    }

    public void GameStart()
    {
        gameStarted = true;
        platformSpawner.SetActive(true);

        menuUI.SetActive(false);
        gamePlayUI.SetActive(true);

        StartCoroutine("UpdateScore");

        
        audioSource.PlayOneShot(gameMusic[1]);
    }

    public void GameOver()
    {
        platformSpawner.SetActive(false);
        StopCoroutine("UpdateScore");
        SaveBestScore();
        Invoke("ReloadLevel", 1f);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator UpdateScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            score++;

            scoreText.text = score.ToString();
        }
    }

    public void IncrementScore()
    {
        score += 2;
        scoreText.text = score.ToString();

        audioSource.PlayOneShot(gameMusic[1]);
    }

    public void NitroScore()
    {
        score += 4;
        scoreText.text = score.ToString();

    }

    void SaveBestScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            if (score > PlayerPrefs.GetInt("BestScore"))
            {
                PlayerPrefs.SetInt("BestScore", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", score);
        }

    }
}
