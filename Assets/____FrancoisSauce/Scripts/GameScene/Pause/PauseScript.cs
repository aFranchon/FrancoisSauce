using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrancoisSauce.Scripts.GameScene.Pause
{
    /// <summary>
    /// Container of all the mandatory references for the <see cref="View_Pause"/>
    /// </summary>
    public class PauseScript : MonoBehaviour
    {
        /// <summary>
        /// resumeButton to allow <see cref="View_Pause"/> to register to his onClick event
        /// </summary>
        public Button resumeButton;
        /// <summary>
        /// quitButton to allow <see cref="View_Pause"/> to register to his onClick event
        /// </summary>
        public Button quitButton;
    }
}