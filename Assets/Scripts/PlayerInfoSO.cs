using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfoSO", menuName = "PlayerInfoSO")]
public class PlayerInfoSO : ScriptableObject
{
    public int totalCollectAmt;
    public int dieAmt;


    public void AddDieAmt()
    {
        dieAmt++;
    }
    public void AddCollectible()
    {
        totalCollectAmt ++;
        Debug.Log("PM");
    }
    public void SetupPlayerInfo()
    {
        totalCollectAmt = 0;
        dieAmt = 0;
    }
}
