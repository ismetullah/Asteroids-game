using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (GameManager._instance == null)
            {
                _instance = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
            if (GameManager._instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return GameManager._instance;
        }
    }

    private enum GAMESTATE
    {
        MENU,
        PLAYING,
        GAMEOVER,
        PAUSE
    }

    public Text scoreTxt;
    public HorizontalLayoutGroup livesGroup;
    public GameObject lifePrefab;
    public List<GameObject> asteroids;
    public GameObject shipPrefab;
    public GameObject menuScreen;
    public GameObject inGameScreen;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;


    public int lifeCount = 3;

    private GameObject ship;
    private int score;
    private List<GameObject> livesList;
    private GAMESTATE gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GAMESTATE.MENU;
        SetUIState(gameState);

        SpawnAsteroids(8);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (gameState == GAMESTATE.PLAYING || gameState == GAMESTATE.PAUSE)
                PauseGame();
        }
    }

    private void SetScore(int score)
    {
        scoreTxt.text = string.Format("{0:D6}", score);
    }

    private void SetUIState(GAMESTATE state)
    {
        menuScreen.SetActive(state == GAMESTATE.MENU);
        inGameScreen.SetActive(state == GAMESTATE.PLAYING);
        pauseScreen.SetActive(state == GAMESTATE.PAUSE);
        gameOverScreen.SetActive(state == GAMESTATE.GAMEOVER);
    }

    private void AddLife()
    {
        RectTransform parent = livesGroup.GetComponent<RectTransform>();
        GameObject l = Instantiate(lifePrefab);
        l.GetComponent<RectTransform>().SetParent(parent);
        livesList.Add(l);
    }

    private void SpawnAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {

            Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            Instantiate(asteroids[Random.Range(0, asteroids.Count)], randomPositionOnScreen, Quaternion.identity);
        }
    }

    private void DeleteAllAsteroids()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in asteroids)
        {
            Destroy(asteroid);
        }
    }

    public void Die()
    {
        if (lifeCount == 1)
        {
            gameState = GAMESTATE.GAMEOVER;
            SetUIState(gameState);
        }
        lifeCount--;
        Destroy(ship);
        if (gameState != GAMESTATE.GAMEOVER)
            ship = Instantiate(shipPrefab);
        Destroy(livesList[lifeCount]);
        livesList.RemoveAt(lifeCount);
    }

    public void AddScore(int score)
    {
        int temp = this.score;
        this.score += score;
        if ((temp / 10000) < (this.score / 10000))
        {
            AddLife();
        }
        SetScore(this.score);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        gameState = GAMESTATE.PLAYING;
        SetUIState(gameState);

        DeleteAllAsteroids();

        ship = Instantiate(shipPrefab);
        score = 0;
        SetScore(score);

        livesList = new List<GameObject>(lifeCount);

        for (int i = 0; i < lifeCount; i++)
        {
            AddLife();
        }

        SpawnAsteroids(6);
    }

    public void PauseGame()
    {
        if (gameState != GAMESTATE.PAUSE)
        {
            gameState = GAMESTATE.PAUSE;
            SetUIState(gameState);
        }
        else
        {
            gameState = GAMESTATE.PLAYING;
            SetUIState(gameState);
        }
        Time.timeScale = (gameState == GAMESTATE.PAUSE) ? 0 : 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
