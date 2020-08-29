using System.Collections;
using FrancoisSauce.Scripts.FSSingleton;
using FrancoisSauce.Scripts.FSUtils.FSGlobalVariables;
using TMPro;
using UnityEngine;

namespace FrancoisSauce.changeName.Scripts.Player
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

        [SerializeField] private Renderer myRenderer = null;
        [SerializeField] private Animator myAnimator = null;

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
        private void Start()
        {
            myTransform = transform;
            life.value = 3;
        }
    
        /// <summary>
        /// Called like o Update method of <see cref="MonoBehaviour"/> but each frame of started and not paused game
        /// </summary>
        public void OnUpdateGameStarted()
        {
            myAnimator.SetBool(Walking, false);
            
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
            myAnimator.SetBool(Walking, true);
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

        [SerializeField] private FSGlobalIntSO life = null;
        [HideInInspector] public bool isInvincible = false;
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Walking = Animator.StringToHash("Walking");

        public void OnPlayerGainLife(int lifeGained)
        {
            life.value += lifeGained;
        }
        
        public void OnPlayerHitByEnemy(int damageTaken)
        {
            life.value -= damageTaken;
            isInvincible = true;

            if (life.value <= 0)
            {
                myAnimator.SetBool(Dead, true);
                GameLogic.Instance.OnLose();
            }
            else
                StartCoroutine(ResetInvincible());
        }

        private IEnumerator ResetInvincible()
        {
            var time = 0f;
            
            myAnimator.SetTrigger(Hit);
            
            while (time < .75f)
            {
                time += Time.deltaTime;
                yield return null;
                myRenderer.enabled = !myRenderer.enabled;
            }

            myRenderer.enabled = true;
            
            isInvincible = false;
        }

        public void OnWin()
        {
            life.value += 1;
        }
    }
}

