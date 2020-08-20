using UnityEngine;
using UnityEngine.UI;

namespace FrancoisSauce.Scripts.MainMenu.LoadingMenu
{
    /// <summary>
    /// Container of all the mandatory references for the <see cref="View_LoadingMenu"/>
    /// </summary>
    public class LoadingMenuScript : MonoBehaviour
    {
        /// <summary>
        /// startGameButton to allow <see cref="View_OptionMenu"/> to register to his onClick event
        /// </summary>
        public Button startGameButton;
    }
}