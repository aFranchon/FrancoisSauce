using FrancoisSauce.Scripts.FSScenes;

namespace FrancoisSauce.Scripts.FSViews
{
    /// <summary>
    /// Interface of FrancoisSauce scene/view implementation.
    /// Views are states of the <see cref="IFSScene{T}"/> state machine.
    /// Views are here to handle the scene lifetime
    /// </summary>
    /// <typeparam name="T">template class to allow code to be used everywhere</typeparam>
    public abstract class IFSView<T> where T : IFSScene<T>
    {
        /// <summary>
        /// Method to handle entering in the view
        /// </summary>
        /// <param name="scene">the <see cref="IFSScene{T}"/> that own the view.
        /// The scene contains every infos needed by the view.</param>
        public abstract void OnViewEnter(T scene);
        /// <summary>
        /// Method to handle exiting out the view
        /// </summary>
        /// <param name="scene">the <see cref="IFSScene{T}"/> that own the view.
        /// The scene contains every infos needed by the view.</param>
        public abstract void OnViewExit(T scene);
        /// <summary>
        /// Method to handle updating the view
        /// </summary>
        /// <param name="scene">the <see cref="IFSScene{T}"/> that own the view.
        /// The scene contains every infos needed by the view.</param>
        public abstract void OnUpdate(T scene);
    }
}