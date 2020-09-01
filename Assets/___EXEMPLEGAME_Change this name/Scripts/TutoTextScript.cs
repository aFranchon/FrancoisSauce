using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoTextScript : MonoBehaviour
{
    public GameObject text;

    private void Awake()
    {
        text.SetActive(false);
    }

    public void OnClickedStartButton()
    {
        text.SetActive(true);
    }

    public void OnWin()
    {
        text.SetActive(false);
    }
}
