using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] float initialSpeedWorld = 1f;
    public float speedWorld;
    public bool dead = false;
    public bool startGame = false;
    int points = 0;


    [Header("UI")]
    [SerializeField] TextMeshProUGUI pointText;
    public GameObject tuturialImage;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    private void Awake()
    {
        if(instance == null)    instance = this;
    }

    private void Start()
    {
        speedWorld = initialSpeedWorld;

        int highScore = PlayerPrefs.GetInt("highScore", 0);
        if(highScore < points)
        {
            highScore = points;
            PlayerPrefs.SetInt("highScore", highScore);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        dead = true;
        speedWorld = 0f;
        UpdateUI();
        gameOverPanel.SetActive(true);
    }

    public void NewGame()
    {
        points = 0;
        pointText.text = points.ToString();
        dead = false;
        startGame = false;
        speedWorld = initialSpeedWorld;
        gameOverPanel.SetActive(false);
        pointText.gameObject.SetActive(true);

        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach(Obstacle obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        BirdController player = FindObjectOfType<BirdController>();
        player.transform.position = player.startPosition;
        player.transform.rotation = Quaternion.Euler(0,0,0);
    }
    public void AddPoint()
    {
        points++;
        pointText.text = points.ToString();
    }
    public int GetPoint()
    {
        return points;
    }

    void UpdateUI()
    {
        int highScore = PlayerPrefs.GetInt("highScore", 0);
        if(highScore<points)
        {
            highScore = points;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        pointText.gameObject.SetActive(false);
        scoreText.text = points.ToString();
        bestScoreText.text = highScore.ToString();

    }
}
