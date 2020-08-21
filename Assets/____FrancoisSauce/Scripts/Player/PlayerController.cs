using FrancoisSauce.Scripts.FSUtils.FSGlobalVariables;
using UnityEngine;

namespace FrancoisSauce.Scripts.Player
{
    /// <summary>
    /// the class to control o player with arrows
    /// using InControl
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// private ref to the <see cref="Transform"/> component of this object, optimization purpose
        /// </summary>
        private Transform myTransform;

        /// <summary>
        /// enum to name different directions of the player
        /// </summary>
        private enum Directions
        {
            RIGHT = 0,
            LEFT = 1,
            FORWARD = 2,
            BACKWARD = 3,
        }
    
        /// <summary>
        /// Awake method of <see cref="MonoBehaviour"/>
        /// </summary>
        private void Awake()
        {
            myTransform = transform;
        }
    
        /// <summary>
        /// Called like o Update method of <see cref="MonoBehaviour"/> but each frame of started and not paused game
        /// </summary>
        public void OnUpdateGameStarted()
        {
            if (Input.GetKey(KeyCode.D)) Move(Directions.RIGHT);
            if (Input.GetKey(KeyCode.Q)) Move(Directions.LEFT);
            if (Input.GetKey(KeyCode.Z)) Move(Directions.FORWARD);
            if (Input.GetKey(KeyCode.S)) Move(Directions.BACKWARD);
        }
        
        /// <summary>
        /// rotate speed of the player
        /// </summary>
        [Tooltip("Player rotate speed, global variable")]
        [SerializeField] private FSGlobalFloatSO rotateSpeed = null;
        /// <summary>
        /// move speed of the player
        /// </summary>
        [Tooltip("Player move speed, global variable")]
        [SerializeField] private FSGlobalFloatSO moveSpeed = null;
    
        /// <summary>
        /// private method to move the <see cref="Transform"/> of the player
        /// </summary>
        /// <param name="direction"></param>
        private void Move(Directions direction)
        {
            switch (direction)
            {
                case (Directions.RIGHT):
                    myTransform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed.value);
                    break;
                case (Directions.LEFT):
                    myTransform.Rotate(Vector3.down * Time.deltaTime * rotateSpeed.value);
                    break;
                case (Directions.FORWARD):
                    myTransform.position += myTransform.forward * Time.deltaTime * moveSpeed.value;
                    break;
                case (Directions.BACKWARD):
                    myTransform.position += -myTransform.forward * Time.deltaTime * moveSpeed.value;
                    break;
                default:
                    break;
            }
        }
    }
}

