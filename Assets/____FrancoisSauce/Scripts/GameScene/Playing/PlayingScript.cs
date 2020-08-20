using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrancoisSauce.Scripts.GameScene.Playing
{
    /// <summary>
    /// Container of all the mandatory references for the <see cref="View_Playing"/>
    /// </summary>
    public class PlayingScript : MonoBehaviour
    {
        /// <summary>
        /// resumeButton to allow <see cref="View_Playing"/> to register to his onClick event
        /// </summary>
        public Button pauseButton;
    }
}