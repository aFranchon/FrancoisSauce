using UnityEngine;
using UnityEngine.UI;

namespace FrancoisSauce.Scripts.GameScene.Lose
{
    /// <summary>
    /// Container of all the mandatory references for the <see cref="View_Lose"/>
    /// </summary>
    public class LoseScript : MonoBehaviour
    {
        /// <summary>
        /// quitButton to allow <see cref="View_Lose"/> to register to his onClick event
        /// </summary>
        public Button quitButton;
        /// <summary>
        /// replayButton to allow <see cref="View_Lose"/> to register to his onClick event
        /// </summary>
        public Button replayButton;
    }
}

