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
        [Tooltip("back button from the View_OptionMenu")]
        public Button backButton;
        
        /// <summary>
        /// Main volume <see cref="Slider"/> to allow <see cref="View_OptionMenu"/> to modify main volume
        /// </summary>
        [Tooltip("main volume slider from the View_OptionMenu")]
        public Slider mainVolumeSlider;
        /// <summary>
        /// music volume <see cref="Slider"/> to allow <see cref="View_OptionMenu"/> to modify music volume
        /// </summary>
        [Tooltip("music volume slider from the View_OptionMenu")]
        public Slider musicVolumeSlider;
        /// <summary>
        /// sfx volume <see cref="Slider"/> to allow <see cref="View_OptionMenu"/> to modify sfx volume
        /// </summary>
        [Tooltip("sfx volume slider from the View_OptionMenu")]
        public Slider sfxVolumeSlider;
        
        //TODO add more options there later
    }
}

