using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Game Settings")]
    [HideInInspector] public bool gameOver;
    public bool hardMode;

    [Header("Hazard Settings")]
    [SerializeField] private GameObject[] hazards;
    [SerializeField] private Vector3 spawnValues;
    [SerializeField] private int hazardCount;
    [SerializeField] private float spawnWait;
    [SerializeField] private float startWait;
    [SerializeField] private float waveWait;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject restartTextObj;
    [SerializeField] private GameObject gameOverTextObj;
    [SerializeField] private GameObject restartButton;

    [SerializeField] private GameObject playerObj;
    private int score;

    private void Start()
    {
        Invoke("ActivateShip", 0.4f);
        InitializeGame();
        StartCoroutine(SpawnWaves());
    }

    void ActivateShip()
    {
        playerObj.SetActive(true);
    }

    private void InitializeGame()
    {
        gameOver = false;
        score = 0;
        UpdateScore();
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                SpawnHazard();
                yield return new WaitForSeconds(spawnWait);
            }

            if (gameOver)
            {
                DisplayRestartText();

                restartButton.SetActive(true);

                break;
            }

            yield return new WaitForSeconds(waveWait);
        }
    }

    private void SpawnHazard()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        GameObject hazard = hazards[Random.Range(0, hazards.Length)];
        Instantiate(hazard, spawnPosition, spawnRotation);
    }

    private void DisplayRestartText()
    {
        StartCoroutine(FadeOutGameOverText());
    }

    private IEnumerator FadeOutGameOverText()
    {
        yield return new WaitForSeconds(1f);
        restartTextObj.SetActive(true);
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOver = true;
        StartCoroutine(DelayGameOver());
    }
    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(1f);
        gameOverTextObj.SetActive(true);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("Main");
    }
}
