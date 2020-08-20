using UnityEngine;
using UnityEngine.UI;

namespace FrancoisSauce.Scripts.MainMenu.BaseMenu
{
    /// <summary>
    /// Container of all the mandatory references for the <see cref="View_BaseMenu"/>
    /// </summary>
    public class BaseMenuScript : MonoBehaviour
    {
        /// <summary>
        /// playButton to allow <see cref="View_OptionMenu"/> to register to his onClick event
        /// </summary>
        public Button playButton;
        /// <summary>
        /// optionMenuButton to allow <see cref="View_OptionMenu"/> to register to his onClick event
        /// </summary>
        public Button optionMenuButton;
        /// <summary>
        /// quitButton to allow <see cref="View_OptionMenu"/> to register to his onClick event
        /// </summary>
        public Button quitButton;
    }
}