using System;
using System.Collections;
using System.Security.Cryptography;
using FrancoisSauce.Scripts.FSSingleton;
using FrancoisSauce.Scripts.FSUtils.FSGlobalVariables;
using UnityEngine;

public class EnemyControllerBonefire : MonoBehaviour
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

    private void Awake()
    {
        myTransform = transform;
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.layer)
        {
            //Player layer
            case 8:
                myAnimator.SetBool(Victory, true);
                GameLogic.Instance.OnLose();
                break;
            //BreakableWall layer
            case 10:
                Destroy(other.gameObject);
                break;
        }
    }

    //TODO enemy always look the direction he's moving

    public void OnUpdateGameStarted()
    {
        if (isBlocked) return;
        
        if (isDashing)
        {
            if (myRigidbody.velocity.magnitude > .01f) return;
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
        myAnimator.SetBool(Dashing, true);
        isDashing = true;
        isBlocked = true;
        
        yield return new WaitForSecondsRealtime(2f);

        myRigidbody.AddForce(myTransform.forward * 1000);
        
        yield return new WaitForSecondsRealtime(.5f);
        isBlocked = false;
    }
}
