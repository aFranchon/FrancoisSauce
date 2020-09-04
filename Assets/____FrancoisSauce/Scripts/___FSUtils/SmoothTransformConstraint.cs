using System;
using UnityEngine;

namespace FrancoisSauce.Scripts.FSUtils
{
    /// <summary>
    /// Script to make the gameObject's transform follow smoothly the target's transform.
    /// FOR THE MOMENT ONLY USE 0 TIME TO FOLLOW
    /// </summary>
    public class SmoothTransformConstraint : MonoBehaviour
    {
        /// <summary>
        /// Target's <see cref="Transform"/> to be followed
        /// </summary>
        [Tooltip("Target's transform to be followed")]
        public Transform target = null;
        /// <summary>
        /// The vector to move to
        /// </summary>
        private Vector3 targetVector = Vector3.zero;
        /// <summary>
        /// <see cref="bool"/> to know if there is a null target or not
        /// </summary>
        private bool isTargetNotNull;

        /// <summary>
        /// more optimized way to handle personal <see cref="Transform"/>
        /// </summary>
        private Transform myTransform = null;

        /// <summary>
        /// If false the GameObject will NOT move on the X Axis
        /// </summary>
        [Tooltip("If false the GameObject will NOT move on the X Axis")]
        public bool XAxis = true;
        /// <summary>
        /// If false the GameObject will NOT move on the Y Axis
        /// </summary>
        [Tooltip("If false the GameObject will NOT move on the Y Axis")]
        public bool YAxis = true;
        /// <summary>
        /// If false the GameObject will NOT move on the Z Axis
        /// </summary>
        [Tooltip("If false the GameObject will NOT move on the Z Axis")]
        public bool ZAxis = true;

        /// <summary>
        /// The <see cref="MonoBehaviour"/> Awake function
        /// </summary>
        private void Awake()
        {
            myTransform = transform;

            if (target == null) targetVector = myTransform.position;
            oldPosition = myTransform.position;
        }
        
        /// <summary>
        /// The <see cref="MonoBehaviour"/> start function
        /// </summary>
        private void Start()
        {
            isTargetNotNull = target != null;
        }

        /// <summary>
        /// The <see cref="MonoBehaviour"/> update function
        /// </summary>
        private void Update()
        {
            FollowPosition(isTargetNotNull ? target.position : targetVector);
        }

        /// <summary>
        /// The time the object will take to move to the target's position
        /// </summary>
        [Tooltip("The time the object will take to move to the target's position")]
        public float timeToFollow = 0f;
        /// <summary>
        /// the actual timer on the movement of the player
        /// </summary>
        private float actualTimer = 0f;
        
        /// <summary>
        /// The old position of the gameObject
        /// </summary>
        private Vector3 oldPosition = Vector3.zero;
        
        /// <summary>
        /// The main function of the class, to move the GameObject
        /// </summary>
        /// <param name="targetPosition">the direction to go to</param>
        private void FollowPosition(Vector3 targetPosition)
        {
            //TODO repair lerp when timeToFollow is not 0f
            
            var myPosition = myTransform.position;
            
            //Reset timer if position has changed
            if (oldPosition != targetPosition)
            {
                oldPosition = targetPosition;
                actualTimer = 0f;
            }
            
            //Update timer
            actualTimer += Time.deltaTime;
            
            //Lerp depend of timeToFollow
            float lerpValue;
            if (Math.Abs(timeToFollow) > 0.00000001f)
                lerpValue = Mathf.Min(1, actualTimer / timeToFollow);
            else
                lerpValue = 1;
            
            //Manage not moving axis
            targetPosition = new Vector3(
                XAxis ? targetPosition.x : myPosition.x,
                YAxis ? targetPosition.y : myPosition.y,
                ZAxis ? targetPosition.z : myPosition.z);

            //Move transform with lerp
            myTransform.position = Vector3.Lerp(myPosition, targetPosition, lerpValue);

            
        }
    }
}