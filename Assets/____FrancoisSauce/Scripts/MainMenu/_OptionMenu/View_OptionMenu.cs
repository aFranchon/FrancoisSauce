using FrancoisSauce.Scripts.FSViews;

namespace FrancoisSauce.Scripts.MainMenu.OptionMenu
{
    /// <summary>
    /// <see cref="Scene_MainMenu"/> implementation of <see cref="IFSView{T}"/> of the OptionMenu
    /// </summary>
    /// <inheritdoc/>
    public class View_OptionMenu : IFSView<Scene_MainMenu>
    {
        /// <summary>
        /// Method to initialize this view
        /// </summary>
        /// <param name="scene">The <see cref="Scene_MainMenu"/> that own this view</param>
        public void Awake(Scene_MainMenu scene)
        {
            var viewUI = scene.optionMenuUI.GetComponent<OptionMenuScript>();
            
            viewUI.backButton.onClick.AddListener(() => scene.ChangeView(scene.baseMenu));
            viewUI.volumeSlider.onValueChanged.AddListener(
                (float value) => scene.mainAudioMixer.SetFloat("MainVolume", value));
        }
        
        public override void OnViewEnter(Scene_MainMenu scene)
        {
            scene.optionMenuUI.SetActive(true);
        }
    
        public override void OnViewExit(Scene_MainMenu scene)
        {
            scene.optionMenuUI.SetActive(false);
        }
    
        public override void OnUpdate(Scene_MainMenu scene)
        {
            ;
        }
    }
}

