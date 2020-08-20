using UnityEngine;
using FrancoisSauce.Scripts.FSViews;
using UnityEngine.SceneManagement;
using FrancoisSauce.Scripts.FSSingleton;

namespace FrancoisSauce.Scripts.FSScenes
{
    /// <summary>
    /// Interface of FrancoisSauce scene/view implementation.
    /// Scenes are big containers made of <see cref="IFSView{T}"/>.
    /// Scenes are here to handle the starting/ending of the <see cref="Scene"/>.
    /// </summary>
    /// <typeparam name="T">template class to allow code to be used everywhere</typeparam>
    public abstract class IFSScene<T> : MonoBehaviour where T : IFSScene<T>
    {
        /// <summary>
        /// current <see cref="IFSView{T}"/> for the finite state machine implementation
        /// </summary>
        protected IFSView<T> currentView;

        #region Scene Managment

        /// <summary>
        /// subscribe to the onSceneLoaded raised by <see cref="GameManager"/>
        /// </summary>
        /// <param name="index">index of the loaded <see cref="Scene"/></param>
        public abstract void OnSceneLoaded(int index);
        /// <summary>
        /// subscribe to the onSceneChanged raised by <see cref="GameManager"/>
        /// </summary>
        /// <param name="index">index of the changed <see cref="Scene"/></param>
        public abstract void OnSceneChanged(int index);
        /// <summary>
        /// subscribe to the onLoadProgression raised by <see cref="GameManager"/>
        /// </summary>
        /// <param name="index">index of the current <see cref="Scene"/> progression</param>
        /// <param name="progression">value of the current <see cref="Scene"/> progression</param>
        public abstract void OnLoadProgression(int index, float progression);
        /// <summary>
        /// subscribe to the onSceneUnloaded raised by <see cref="GameManager"/>
        /// </summary>
        /// <param name="index">index of the unloaded <see cref="Scene"/></param>
        public abstract void OnSceneUnloaded(int index);

        #endregion

        #region View Managment

        /// <summary>
        /// subscribe to the onUpdate raised by <see cref="GameManager"/>
        /// </summary>
        public abstract void OnUpdate();
        /// <summary>
        /// Implementation of finite state machine. Permits to change the current view
        /// </summary>
        public abstract void ChangeView(IFSView<T> newView);

        #endregion
    }
}