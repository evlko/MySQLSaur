using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Dependencies")] 
    public ObjectsPool cactusPool;

    [Header("Ground")] 
    public Transform initialGroundPosition;
    public Transform ground;
    public float groundBorder;

    [Header("Difficulty")] 
    public float cactusInitialSpeed;
    public float cactusInitialSpawnRate;
    public float cactusMaxSpeed;
    public float cactusMinSpawnRate;
    public float cactusChangeSpeed;
    public float cactusChangeSpawnRate;
    public float increaseDifficultyRate;

    [Header("User Interface")] 
    public TextMeshProUGUI scoreText;
    public Transform deathPanel;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI maxHighscoreText;
    public string initialCurrentScoreText;
    public string initialHighscoreText;
    public string initialMaxHighscoreText;
    public string newHighscoreLabelText;
    public Color normalTextColor;
    public Color newHighscoreTextColor;

    private float cactusCurrentSpeed;
    private float cactusCurrentSpawnRate;
    private int currentScore;

    private void Awake()
    {
        cactusCurrentSpeed = cactusInitialSpeed;
        cactusCurrentSpawnRate = cactusInitialSpawnRate;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(SpawnCactus());
        StartCoroutine(IncreaseDifficulty());
    }

    private void FixedUpdate()
    {
        currentScore += 1;
        scoreText.text = currentScore.ToString();

        ground.transform.position -= new Vector3(cactusCurrentSpeed / 2, 0, 0);
        if (ground.transform.position.x < groundBorder)
        {
            ground.transform.position = initialGroundPosition.transform.position;
        }
    }

    private IEnumerator SpawnCactus()
    {
        Cactus cactus = cactusPool.GetEntityFromPool().GetComponent<Cactus>();
        if (cactus != null)
        {
            cactus.Speed = cactusCurrentSpeed;
        }

        yield return new WaitForSeconds(cactusCurrentSpawnRate);

        StartCoroutine(SpawnCactus());
    }

    private IEnumerator IncreaseDifficulty()
    {
        yield return new WaitForSeconds(increaseDifficultyRate);

        cactusCurrentSpeed += cactusChangeSpeed;
        if (cactusCurrentSpeed > cactusMaxSpeed)
        {
            cactusCurrentSpeed = cactusMaxSpeed;
        }

        cactusCurrentSpawnRate -= cactusChangeSpawnRate;
        if (cactusCurrentSpawnRate < cactusMinSpawnRate)
        {
            cactusCurrentSpawnRate = cactusMinSpawnRate;
        }

        StartCoroutine(IncreaseDifficulty());
    }

    public void Fail()
    {
        int t = 0;
        Time.timeScale = 0f;
        deathPanel.gameObject.SetActive(true);

        if (GameManager.instance.highscore < currentScore)
        {
            GameManager.instance.highscore = currentScore;
            t = 1;
            if (GameManager.instance.maxhighscore < currentScore)
            {
                GameManager.instance.maxhighscore = currentScore;
                t = 2;
            }
        }

        ShowScores(t);
    }

    private void ShowScores(int s)
    {
        List<TextMeshProUGUI> scoreTextsGUIs = new List<TextMeshProUGUI>
            { currentScoreText, maxHighscoreText, highscoreText };
        List<string> scoreTexts = new List<string>
        {
            initialCurrentScoreText + currentScore.ToString(),
            initialMaxHighscoreText + GameManager.instance.maxhighscore.ToString(), 
            initialHighscoreText + GameManager.instance.highscore.ToString(),
        };

        for (var i = scoreTextsGUIs.Count - 1; i >= 0; i--)
        {
            scoreTextsGUIs[i].text = scoreTexts[i];
            scoreTextsGUIs[i].color = normalTextColor;
            if (s <= 0) continue;
            scoreTextsGUIs[i].color = newHighscoreTextColor;
            scoreTextsGUIs[i].text += newHighscoreLabelText;
            s -= 1;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}