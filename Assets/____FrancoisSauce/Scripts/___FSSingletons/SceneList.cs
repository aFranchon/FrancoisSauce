using System.Collections.Generic;
using System.Linq;
using FrancoisSauce.Scripts.FSUtils.FSGlobalVariables;
using UnityEngine;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace FrancoisSauce.Scripts.FSUtils
{
    /// <summary>
    /// use this singleton to keep track of all the built scenes in the project + the levels scenes.
    /// if in unity editor it also search for all levels scenes at the given path to add them to built scenes
    /// </summary>
    /// <inheritdoc/>
    public class SceneList : Singleton<SceneList>
    {
        /// <summary>
        /// Dictionary with all the scenes' index link to their names
        /// </summary>
        [Tooltip("The dictionary that contains all the built scenes")]
        public Dictionary<string, int> scenes = new Dictionary<string, int>();
        /// <summary>
        /// the given path to the levels folder to add to the built scenes
        /// </summary>
        [Tooltip("Path to levels' folder")]
        public string pathToLevels;

        /// <summary>
        /// Awake method from <see cref="MonoBehaviour"/>
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            
            DontDestroyOnLoad(this);
            
            var levels = System.IO.Directory.GetFiles(pathToLevels);
            
#if UNITY_EDITOR
            // Find valid Scene paths and make a list of EditorBuildSettingsScene
            var editorBuildSettingsScenes = (
                from editorBuildSettingsScene in EditorBuildSettings.scenes 
                where !editorBuildSettingsScene.path.Contains("Level")
                select new EditorBuildSettingsScene(editorBuildSettingsScene.path, true)).ToList();
            
            editorBuildSettingsScenes.AddRange(
                from levelPath in levels
                where !string.IsNullOrEmpty(levelPath) && Path.GetExtension(levelPath) == ".unity"
                select new EditorBuildSettingsScene(levelPath, true));

            // Set the Build Settings window Scene list
            EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
#endif
            for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                var sceneName = Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[i].path);
                scenes.Add(sceneName, i);
            }
        }
    }
}