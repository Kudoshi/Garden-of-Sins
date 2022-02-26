using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles any other player related stuff
/// 
/// Currently handling
/// - Skills
/// - Player die
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject boxPf;
    [SerializeField] private GameObject controllerCursorPf;
    [SerializeField] private PlayerInfoSO playerInfoSO;


    [Header("Box")]
    [SerializeField] private float boxRange;
    [SerializeField] private Vector2 rangeSpawn; // Range where the cursor should start spawning
    [SerializeField] private float cdDuration; // Range where the cursor should start spawning


    private Vector2 cubePlacement;
    private bool cubeCursorOn = true;
    private GameObject cubeCursor = null;
    private GameObject boxPlaced;
    private float lastBoxTime = 0;
    private BoxCursorControl cursorControl;


    /////////////////////////////////
    /////     CUBE PLACEMENT
    ///////////////////////////////

    private void OnDrawGizmosSelected()
    {
            
            Gizmos.DrawSphere(cubePlacement, 1);
    }

    /// <summary>
    /// Inputs from Player Input Controller
    /// Sets the cube placement position using the input
    /// 
    /// Gets the input and plus it with the transform position
    /// Then sets the cubeplacement
    /// </summary>
    /// <param name="rightStick"></param>
    public void CubePlacement(Vector2 rightStick)
    {
        Vector2 target = transform.position + new Vector3(rightStick.x * boxRange, rightStick.y * boxRange);
        cubePlacement = target;
    }

    public void CubePlacementMouse(Vector3 mousePos)
    {
        mousePos = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        float distance = Vector3.Distance(mousePos, transform.position);
        if (distance > boxRange)
        {
            Vector3 direction = mousePos - transform.position;
            direction *= boxRange / distance;
            cubePlacement = transform.position + direction;
        }
        else
        {
            cubePlacement = mousePos;
        }

    }


    /// <summary>
    /// Determines if cube is within the spawnRange. If yes turn cubecursor off. If no then turn it on
    /// Then spawn the cube if not created yet then update position. If cubecursor off. Destroy it
    /// </summary>
    public void SpawnCubeCursor()
    {
        // Determine if should spawn cube cursor
        //If within spawnRange's x
        if (cubePlacement.x < (transform.position.x + rangeSpawn.x) && cubePlacement.x > (transform.position.x - rangeSpawn.x))
        {

            if (cubePlacement.y < (transform.position.y + rangeSpawn.y) && cubePlacement.y > (transform.position.y - rangeSpawn.y))
            {
                cubeCursorOn = false;
            }
            else
            {
                cubeCursorOn = true;
            }
        }
        else
        {
            cubeCursorOn = true;
        }

        //Spawns the cube and update its position if cube cursor is on
        //If not destroy it
        if (cubeCursorOn)
        {
            //Check cd
            // If still cd
            //  Spawn red one
            // Else spawn blue

            //Check whether spawn and whether is the right one
            //Spawns new cube
            if (cubeCursor == null) // If no cube is spawn
            {
                cubeCursor = Instantiate(controllerCursorPf, cubePlacement, boxPf.transform.rotation);
                cursorControl = cubeCursor.GetComponent<BoxCursorControl>();
            }

            cursorControl.CursorActive(CheckBoxCDDuration());
            cubeCursor.transform.position = cubePlacement;
        }
        else if (!cubeCursorOn)
        {
            if (cubeCursor != null)
            {
                Destroy(cubeCursor);
                cubeCursor = null;
                cursorControl = null;
            }
        }
    }

    private void Update()
    {
        SpawnCubeCursor();
        
    }

    public void Die()
    {
        playerInfoSO.AddDieAmt();
        SoundRepoSO.PlayOneShotSound(gameObject, "Death");
        Event.TriggerPlayerDie(transform);
    }

    /// <summary>
    /// Spawns the cube
    /// It checks
    /// - cubeCursor bool
    /// - boxPlace GameObj
    /// </summary>
    public void PlaceCube()
    {
        if (cubeCursor && boxPlaced == null && CheckBoxCDDuration())
        {
            boxPlaced = Instantiate(boxPf, cubePlacement, boxPf.transform.rotation);
            SoundRepoSO.PlayOneShotSound(gameObject,"BoxSpawn");
            lastBoxTime = Time.time; 
        }
        else if (boxPlaced != null)
        {
            Destroy(boxPlaced);
            SoundRepoSO.PlayOneShotSound(gameObject, "BoxDestroy");

            boxPlaced = null;
        }
        else
        {
            SoundRepoSO.PlayOneShotSound(gameObject, "BoxSpawnFail");

        }
    }

    /// <summary>
    /// Checks if the box cd duration has finished
    /// Returns:
    /// True - CD finished
    /// False - CD not finished
    /// </summary>
    /// <returns></returns>
    private bool CheckBoxCDDuration()
    {
        if (boxPlaced == null && ((Time.time - lastBoxTime) > cdDuration || lastBoxTime == 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddCollectible()
    {
        playerInfoSO.AddCollectible();
    }
}
