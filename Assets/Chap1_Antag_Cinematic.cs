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

    public DialogueEventListener dialogue1;
    public DialogueEventListener dialogue2;

    public GameObject orb1;
    public GameObject orb2;
    public GameObject orb3;
    public float cinematicWaitTime;

    private int talkNumber = 1;
    private InteractionManager playerInteractor;
    private bool canFlyAway;

    private void Awake()
    {
        dialogue1.enabled = true;
        dialogue2.enabled = false;
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
    }
    public void EndDialogue(Dialogue dialogue)
    {
        if (talkNumber == 1) //Player talk
        {
            GetComponent<BoxCollider2D>().enabled = false;
            orb1.SetActive(true);
            orb2.SetActive(true);
            orb3.SetActive(true);
            talkNumber++;
        }
        if (talkNumber == 2) //Orb taken
        {
            
            dialogue1.enabled = false;
            dialogue2.enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
        }
        if (talkNumber == 3)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(AnimateFlyAway());
        }

        
    }

    IEnumerator AnimateFlyAway()
    {
        yield return null;
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
