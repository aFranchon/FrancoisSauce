using System;
using System.Collections;
using System.Collections.Generic;
using FrancoisSauce.Scripts.FSUtils.FSGlobalVariables;
using UnityEngine;
using UnityEngine.UI;

//TODO Comment this file

namespace FrancoisSauce.Scripts.GameScene.Playing
{
    public class HeartUI : MonoBehaviour
    {
        [SerializeField] private List<GameObject> life = new List<GameObject>();
        [SerializeField] private FSGlobalIntSO playerLife = null;

        private void Awake()
        {
            SetLife();
        }

        private void OnEnable()
        {
            SetLife();
        }

        private void SetLife()
        {
            for (var i = 0; i < life.Count; i++)
            {
                life[i].SetActive(i < playerLife.value ? true : false);
            }
        }
        
        public void OnPlayerHitByEnemy(int damageTaken)
        {
            SetLife();
        }
    }
}