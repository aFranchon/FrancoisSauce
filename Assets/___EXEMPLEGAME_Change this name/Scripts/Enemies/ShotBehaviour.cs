using System;
using DG.Tweening;
using FrancoisSauce.changeName.Scripts.Player;
using FrancoisSauce.Scripts.FSEvents.SO;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{
    [SerializeField] private FSIntEventSO onPlayerHitByEnemy = null;
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) //player layer
        {
            if (!other.gameObject.GetComponent<PlayerController>().isInvincible)
                onPlayerHitByEnemy.Invoke(damage);
        }

        transform.DOKill();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
