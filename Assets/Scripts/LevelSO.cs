using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loads the next scene
/// </summary>
[CreateAssetMenu(fileName = "LevelSO", menuName = "Level SO")]
public class LevelSO : ScriptableObject
{
    private int currentLevel;


    /// <summary>
    /// Called from main menu and stages
    /// </summary>
    /// <param name="currentLevel"></param>
    public void GoNextLevel(int currentLevel)
    {
        this.currentLevel = currentLevel++;
        SceneManager.LoadScene(sceneBuildIndex: this.currentLevel);
    }
    public void GoMainMenu()
    {
        this.currentLevel = 0;
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
    public void RestartLevel() //Make sure there is level warp else it will have problem with restarting
    {
        SceneManager.LoadScene(sceneBuildIndex: this.currentLevel);
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
