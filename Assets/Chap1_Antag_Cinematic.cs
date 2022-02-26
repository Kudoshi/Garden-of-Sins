using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Cinematic hardcode scripts for chapter 1 antagonist
/// </summary>
public class Chap1_Antag_Cinematic : MonoBehaviour
{
    public Transform pointsToMove;
    public float speedMove;

    public DialogueTrigger dialogue1;
    public DialogueTrigger dialogue2;

    public GameObject orb1;
    public GameObject orb2;
    public GameObject orb3;
    public GameObject tutorial_dialogue;
    public GameObject tutorial_trigger;
    public float cinematicWaitTime;
    public GameObject lightWall;

    private int talkNumber = 1;
    private InteractionManager playerInteractor;
    private bool canFlyAway;

    private void Awake()
    {
        dialogue1.enabled = true;
        dialogue2.enabled = false;
    }

    private void Start()
    {
        orb1.SetActive(false);
        orb2.SetActive(false);
        orb3.SetActive(false);
        tutorial_dialogue.SetActive(false);
        tutorial_trigger.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            playerInteractor = collision.transform.root.GetComponent<InteractionManager>();
            Invoke("CallInteract", cinematicWaitTime);
        }
    }
    private void CallInteract()
    {
        playerInteractor.Interact();
        if (talkNumber == 1)
            tutorial_dialogue.SetActive(true);
    }
    public void EndDialogue(Dialogue dialogue)
    {
        if (talkNumber == 1) //Player talk
        {
            GetComponent<BoxCollider2D>().enabled = false;
            orb1.SetActive(true);
            orb2.SetActive(true);
            orb3.SetActive(true);
            tutorial_dialogue.SetActive(false);
            tutorial_trigger.SetActive(true);
            Destroy(dialogue1);
            talkNumber++;
        }
        else if (talkNumber == 2) //Orb taken
        {
            tutorial_trigger.SetActive(false);
            orb1.SetActive(false);
            orb2.SetActive(false);
            orb3.SetActive(false);
            GetComponent<BoxCollider2D>().enabled = true;
            talkNumber++;

        }
        else if (talkNumber == 3) //Player talk 2 finish
        {
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(AnimateFlyAway());
            Destroy(lightWall);
        }

        
    }

    IEnumerator AnimateFlyAway()
    {
        yield return new WaitForSeconds(.5f);
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        yield return new WaitForSeconds(0.5f);
        canFlyAway = true;
    }

    private void Update()
    {
        if (canFlyAway)
        {
            Vector3 pointToTravel = new Vector3(pointsToMove.position.x, pointsToMove.position.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, pointToTravel, speedMove * Time.deltaTime);
        }
        
    }
}
