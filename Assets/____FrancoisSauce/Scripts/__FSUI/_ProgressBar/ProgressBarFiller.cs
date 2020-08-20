using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrancoisSauce.Scripts.FSUI.ProgressBar
{
    /// <summary>
    /// Progress bar's filler component. To keep track of the <see cref="RectTransform"/> component
    /// </summary>
    public class ProgressBarFiller : MonoBehaviour
    {
        /// <summary>
        /// Used in the progress bar to correctly scale the filler with the progression
        /// </summary>
        [HideInInspector] public RectTransform myTransform;
        
        /// <summary>
        /// <see cref="MonoBehaviour"/>'s Awake method
        /// </summary>
        private void Awake()
        {
            myTransform = GetComponent<RectTransform>();
        }
    }
}
