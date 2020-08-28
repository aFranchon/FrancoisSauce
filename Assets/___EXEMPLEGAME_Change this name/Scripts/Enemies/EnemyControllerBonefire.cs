using System;
using System.Collections;
using FrancoisSauce.Scripts.FSUtils.FSGlobalVariables;
using UnityEngine;

public class EnemyControllerBonefire : MonoBehaviour
{
    private bool isDashing = false;
    private bool isBlocked = false;

    private float dashTimer = 0f;
    [SerializeField] private FSGlobalFloatSO dashCooldown = null;

    [SerializeField] private Rigidbody myRigidbody = null;
    private Transform myTransform;

    [SerializeField] private Transform playerTransform = null;

    private void Awake()
    {
        myTransform = transform;
    }

    public void OnUpdateGameStarted()
    {
        if (isBlocked) return;
        
        if (isDashing)
        {
            print(myRigidbody.velocity.magnitude);
            if (myRigidbody.velocity.magnitude > .01f) return;
            print("Stop dash");
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
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        myTransform.LookAt(playerTransform);
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        isBlocked = true;
        
        yield return new WaitForSecondsRealtime(.5f);

        isBlocked = false;
        myRigidbody.AddForce(myTransform.forward * 1000);
    }
}
