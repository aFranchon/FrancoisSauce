using System;
using System.Collections;
using DG.Tweening;
using FrancoisSauce.changeName.Scripts.Player;
using FrancoisSauce.Scripts.FSEvents.SO;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{
    [SerializeField] private FSIntEventSO onPlayerHitByEnemy = null;
    [SerializeField] private int damage = 1;

    [SerializeField] private ParticleSystem myParticleSystem = null;
    [SerializeField] private Renderer myRenderer = null;

    [SerializeField] private AudioSource hitSound = null;
    [SerializeField] private AudioSource flyingSound = null;

    private void OnEnable()
    {
        flyingSound.Play();
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) //player layer
        {
            if (!other.gameObject.GetComponent<PlayerController>().isInvincible)
                onPlayerHitByEnemy.Invoke(damage);
        }

        hitSound.Play();
        
        transform.DOKill();
        
        myParticleSystem.gameObject.SetActive(true);
        myRenderer.enabled = false;
        
        yield return new WaitForSeconds(1f);
        myParticleSystem.gameObject.SetActive(false);
        gameObject.SetActive(false);
        myRenderer.enabled = true;
    }

    private void OnDisable()
    {
        flyingSound.Stop();
        transform.DOKill();
    }
}
