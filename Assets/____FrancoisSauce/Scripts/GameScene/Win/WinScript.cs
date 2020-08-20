using UnityEngine;
using UnityEngine.UI;

namespace FrancoisSauce.Scripts.GameScene.Win
{
    /// <summary>
    /// Container of all the mandatory references for the <see cref="View_Win"/>
    /// </summary>
    public class WinScript : MonoBehaviour
    {
        /// <summary>
        /// quitButton to allow <see cref="View_Win"/> to register to his onClick event
        /// </summary>
        public Button quitButton;
        /// <summary>
        /// nextButton to allow <see cref="View_Win"/> to register to his onClick event
        /// </summary>
        public Button nextButton;
    }
}