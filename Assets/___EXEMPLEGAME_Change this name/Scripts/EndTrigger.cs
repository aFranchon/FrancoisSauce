using System;
using System.Collections;
using System.Collections.Generic;
using FrancoisSauce.Scripts.FSSingleton;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public bool win = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) //player layer
        {
            if (win)
                GameLogic.Instance.OnWin();
            else
                GameLogic.Instance.OnLose();
        } 
    }
}
