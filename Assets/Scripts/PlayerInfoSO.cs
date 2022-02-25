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
    public void AddCollectible(int amt)
    {
        totalCollectAmt += amt;
    }
    public void ResetCollectible()
    {
        totalCollectAmt = 0;
    }
}
