using FrancoisSauce.Scripts.FSUtils.FSGlobalVariables;
using UnityEngine;

namespace FrancoisSauce.changeName.Scripts.Player
{
    public class PlayerControllerBadlands : MonoBehaviour
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

        public void OnResume()
        {
            myRigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        }
        
        public void OnUpdateGameStarted()
        {
            myRigidbody.position += forwardSpeed.value * Time.deltaTime * Vector3.forward;

            if (Input.GetKey(KeyCode.Space)) Jump();
        }

        private void Jump()
        {
            //if (myRigidbody.velocity.y < 0f) myRigidbody.velocity = Vector3.zero;
            myRigidbody.AddForce(Vector3.up * Time.deltaTime * upSpeed.value * (myRigidbody.velocity.y < 3f ? 2 : 1));
        }
    }
}