using System;
using System.Collections;
using System.Collections.Generic;
using FrancoisSauce.Scripts.FSUtils.FSGlobalVariables;
using UnityEngine;

namespace FrancoisSauce.changeName.Scripts.Player
{
    //TODO rename to PlayerControllerBadlands
    public class PlayerControllerJumpOnly : MonoBehaviour
    {
        [SerializeField] private FSGlobalFloatSO forwardSpeed = null;
        [SerializeField] private FSGlobalFloatSO upSpeed = null;

        [SerializeField] private Rigidbody myRigidbody = null;

        private void Awake()
        {
            myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        
        public void OnClickedStartButton()
        {
            myRigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        }

        public void OnPause()
        {
            myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        
        public void OnUpdateGameStarted()
        {
            myRigidbody.position += forwardSpeed.value * Time.deltaTime * Vector3.forward;

            if (Input.GetKey(KeyCode.Space)) Jump();
        }

        private void Jump()
        {
            //TODO do something to improve the feeling of the mechanic
            myRigidbody.AddForce(Vector3.up * Time.deltaTime * upSpeed.value);
        }
    }
}