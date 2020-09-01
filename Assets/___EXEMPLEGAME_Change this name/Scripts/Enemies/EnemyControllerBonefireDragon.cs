using System.Collections;
using FrancoisSauce.changeName.Scripts.Player;
using FrancoisSauce.Scripts.FSEvents.SO;
using FrancoisSauce.Scripts.FSUtils.FSGlobalVariables;
using UnityEngine;

public class EnemyControllerBonefireDragon : MonoBehaviour
{
    private bool isDashing = false;
    private bool isBlocked = false;

    private float dashTimer = 0f;
    [SerializeField] private FSGlobalFloatSO dashCooldown = null;

    [SerializeField] private Rigidbody myRigidbody = null;
    [SerializeField] private Animator myAnimator = null;
    private Transform myTransform;

    [SerializeField] private Transform playerTransform = null;
    private static readonly int Dashing = Animator.StringToHash("Dashing");
    private static readonly int Victory = Animator.StringToHash("Victory");

    [SerializeField] private FSIntEventSO onPlayerHitByEnemy = null;
    private static readonly int Dead = Animator.StringToHash("Dead");

    private void Awake()
    {
        myTransform = transform;
    }

    [SerializeField] private AudioSource eatingSound = null;
    [SerializeField] private AudioSource dashSound = null;
    [SerializeField] private AudioSource idleSound = null;
    [SerializeField] private AudioSource preparingSound = null;
    [SerializeField] private AudioSource breakingWallSound = null;

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.layer)
        {
            //Player layer
            case 8:
                eatingSound.Play();
                
                if (other.gameObject.GetComponent<PlayerController>().isInvincible) break;
                onPlayerHitByEnemy.Invoke(1);
                break;
            //KillableEnemy layer
            case 11:
                eatingSound.Play();
                
                var otherAnimator = other.gameObject.GetComponent<Animator>();
                
                if (otherAnimator) if (!otherAnimator.GetBool(Dead)) otherAnimator.SetBool(Dead, true);
                Destroy(other.gameObject, 1.5f);
                break;
            //BreakableWall layer
            case 10:
                breakingWallSound.Play();
                
                Destroy(other.gameObject);
                break;
        }
    }

    public void OnUpdateGameStarted()
    {
        if (!idleSound.isPlaying) idleSound.Play();
        if (isBlocked) return;
        
        if (isDashing)
        {
            if (myRigidbody.velocity.magnitude > .5f) return;
            myAnimator.SetBool(Dashing, false);
            isDashing = false;
            dashTimer = 0f;
            return;
        }

        LookForPlayer();
        
        dashTimer += Time.deltaTime;
        if (dashTimer >= dashCooldown.value) StartCoroutine(Dash());
    }

    private void LookForPlayer()
    {
        var lookPos = playerTransform.position - myTransform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        myTransform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
    }

    private IEnumerator Dash()
    {
        preparingSound.Play();
        myAnimator.SetBool(Dashing, true);
        isDashing = true;
        isBlocked = true;
        
        yield return new WaitForSecondsRealtime(1.5f);
        dashSound.Play();
        yield return new WaitForSecondsRealtime(.5f);

        preparingSound.Stop();
        myRigidbody.AddForce(myTransform.forward.normalized * 1000);
        
        yield return new WaitForSecondsRealtime(.5f);
        isBlocked = false;
    }
}
