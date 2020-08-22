using System;
using UnityEngine;

//TODO comment this class

namespace FrancoisSauce.Scripts.FSUtils
{
    public class SmoothTransformConstraint : MonoBehaviour
    {
        public Transform target = null;
        private Vector3 targetVector = Vector3.zero;
        private bool istargetNotNull;

        private Transform myTransform = null;

        public bool XAxis = true;
        public bool YAxis = true;
        public bool ZAxis = true;

        private void Awake()
        {
            myTransform = transform;

            if (target == null) targetVector = myTransform.position;
            oldPosition = myTransform.position;
        }
        
        private void Start()
        {
            istargetNotNull = target != null;
        }

        private void Update()
        {
            FollowPosition(istargetNotNull ? target.position : targetVector);
        }

        public float timeToFollow = 0f;
        private float actualTimer = 0f;
        
        private Vector3 oldPosition = Vector3.zero;
        
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
            
            //Lerp depend of timeToFollow
            float lerpValue;
            if (Math.Abs(timeToFollow) > .0001f)
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

            //Update timer
            actualTimer += Time.deltaTime;
        }
    }
}