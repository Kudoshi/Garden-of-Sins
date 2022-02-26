using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Attached to warp hole
/// </summary>
public class LevelWarp : MonoBehaviour
{
    public EndScreen endScreen;
    private int currentLevel;

    private void Start()
    {
        //Updates the level so on the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        currentLevel = currentScene.buildIndex;

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.root.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            endScreen.EndLevel();
        }
    }
}
