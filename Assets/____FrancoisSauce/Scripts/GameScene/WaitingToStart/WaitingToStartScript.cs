using UnityEngine;
using UnityEngine.UI;

namespace FrancoisSauce.Scripts.GameScene.WaitingToStart
{
    /// <summary>
    /// Container of all the mandatory references for the <see cref="View_WaitingToStart"/>
    /// </summary>
    public class WaitingToStartScript : MonoBehaviour
    {
        /// <summary>
        /// startButton to allow <see cref="View_WaitingToStart"/> to register to his onClick event
        /// </summary>
        public Button startButton;
    }
}