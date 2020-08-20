using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrancoisSauce.Scripts.FSUI.ProgressBar
{
    /// <summary>
    /// Simple implementation of a progress bar
    /// </summary>
    public class ProgressBar : MonoBehaviour
    {
        /// <summary>
        /// The filler component of the progress bar
        /// </summary>
        private ProgressBarFiller filler = null;

        /// <summary>
        /// Variable to keep track of the baseWidth of the filler
        /// </summary>
        private float baseWidth;
        /// <summary>
        /// Variable to keep track of the baseHeight of the filler
        /// </summary>
        private float baseHeight;
        
        /// <summary>
        /// <see cref="MonoBehaviour"/>'s Start method
        /// </summary>
        private void Start()
        {
            filler = GetComponentInChildren<ProgressBarFiller>();

            //storing base delta size of the filler
            var sizeDelta = filler.myTransform.sizeDelta;
            baseWidth = sizeDelta.x;
            baseHeight = sizeDelta.y;
        }

        /// <summary>
        /// Call this function each time you need to update the ProgressBar
        /// </summary>
        /// <param name="progression">Progression to update in the progress bar</param>
        public void UpdateProgression(float progression)
        {
            if (filler == null) return;

            filler.myTransform.localPosition = Vector3.left * (baseWidth - baseWidth * progression) / 2;
            filler.myTransform.sizeDelta = new Vector2(baseWidth * progression, baseHeight);
        }
    }
}