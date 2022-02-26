using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public LevelSO levelSO;
    public void ButtonHover()
    {
        SoundRepoSO.PlayOneShotSound(gameObject, "ButtonHover");
    }

    public void StartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int currentLevel = currentScene.buildIndex;
        levelSO.GoNextLevel(currentLevel);
    }
}
