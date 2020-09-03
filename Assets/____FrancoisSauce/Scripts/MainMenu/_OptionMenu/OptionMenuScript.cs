using UnityEngine;
using UnityEngine.UI;

namespace FrancoisSauce.Scripts.MainMenu.OptionMenu
{
    /// <summary>
    /// Container of all the mandatory references for the <see cref="View_OptionMenu"/>
    /// </summary>
    public class OptionMenuScript : MonoBehaviour
    {
        /// <summary>
        /// backButton to allow <see cref="View_OptionMenu"/> to register to his onClick event
        /// </summary>
        public Button backButton;
        
        //TODO add more options there later

        //TODO comment
        public Slider mainVolumeSlider;
        public Slider musicVolumeSlider;
        public Slider sfxVolumeSlider;
    }
}

