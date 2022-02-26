using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Functions as a controller for player info as well
/// </summary>
public class EndScreen : MonoBehaviour
{
    public GameObject display;
    public TextMeshProUGUI collectibleCounter;
    public TextMeshProUGUI levelTimer;
    public PlayerInfoSO playerInfoSO;
    public LevelSO levelSO;
    private float timer = 0.0f;
    private int totalCollectible;
    private void Awake()
    {
        display.SetActive(false);
        playerInfoSO.SetupPlayerInfo();
    }
    private void Start()
    {
        totalCollectible = FindObjectsOfType<CollectibleTrigger>().Length;
    }
    public void EndLevel()
    {
        SoundRepoSO.PlayOneShotSound(gameObject, "Victory");
        collectibleCounter.text = playerInfoSO.totalCollectAmt.ToString() + " / " + totalCollectible;

        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");

        string keyName = "BestTime_" + levelSO.GetCurrentLevel().ToString();
        float bestTime = PlayerPrefs.GetFloat(keyName);

        string newScore = "";
        if (timer < bestTime)
        {
            PlayerPrefs.SetFloat(keyName, timer);
            bestTime = timer;
            newScore = "\nNEW TIME";
        }
        string bestMinutes = Mathf.Floor(bestTime / 60).ToString("00");
        string bestSeconds = (bestTime % 60).ToString("00");

        levelTimer.text = "Time: " + minutes + ":"+seconds+" / " + bestMinutes + ":" + bestSeconds + newScore;

        display.SetActive(true);
    }


    public void Button_Restart()
    {
        levelSO.RestartLevel();
    }

    public void Button_NextLevel()
    {
        //Updates the level so on the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        int currentLevel = currentScene.buildIndex;
        levelSO.GoNextLevel(currentLevel);
    }
    public void Button_Hover()
    {
        SoundRepoSO.PlayOneShotSound(gameObject, "ButtonHover");
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
}
