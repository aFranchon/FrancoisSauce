using FrancoisSauce.Scripts.FSViews;
using UnityEngine;

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
            
            viewUI.backButton.onClick.AddListener(() =>
            {
                scene.clickSound.Play();
                scene.ChangeView(scene.baseMenu);
            });
            viewUI.mainVolumeSlider.onValueChanged.AddListener(
                (value) =>
                {
                    PlayerPrefs.SetFloat("MainVolume", value);
                    scene.mainAudioMixer.SetFloat("MainVolume", value);
                });
            viewUI.musicVolumeSlider.onValueChanged.AddListener(
                (value) =>
                {
                    PlayerPrefs.SetFloat("MusicVolume", value);
                    scene.mainAudioMixer.SetFloat("MusicVolume", value);
                });
            viewUI.sfxVolumeSlider.onValueChanged.AddListener(
                (value) =>
                {
                    PlayerPrefs.SetFloat("SFXVolume", value);
                    scene.mainAudioMixer.SetFloat("SFXVolume", value);
                });
            
            var volumeValue = PlayerPrefs.GetFloat("MainVolume");
            viewUI.mainVolumeSlider.value = volumeValue;
            scene.mainAudioMixer.SetFloat("MainVolume", volumeValue);
            
            volumeValue = PlayerPrefs.GetFloat("MusicVolume");
            viewUI.musicVolumeSlider.value = volumeValue;
            scene.mainAudioMixer.SetFloat("MusicVolume", volumeValue);
            
            volumeValue = PlayerPrefs.GetFloat("SFXVolume");
            viewUI.sfxVolumeSlider.value = volumeValue;
            scene.mainAudioMixer.SetFloat("SFXVolume", volumeValue);
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

