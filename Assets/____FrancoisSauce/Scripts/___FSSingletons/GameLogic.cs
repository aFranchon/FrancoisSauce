using FrancoisSauce.Scripts.FSEvents.SO;
using FrancoisSauce.Scripts.FSUtils;
using UnityEngine;

namespace FrancoisSauce.Scripts.FSSingleton
{
    /// <summary>
    /// The logic of the game, should contain only behaviour that manage the state of the game
    /// events in relation with <see cref="GameScene"/>
    /// </summary>
    /// <inheritdoc/>
    public class GameLogic : Singleton<GameLogic>
    {
        /// <summary>
        /// <see cref="FSVoidEventSO"/> raised when game is win, called by levels game objects
        /// </summary>
        [Tooltip("Event raised when the game is won")]
        [SerializeField] private FSVoidEventSO onGameWin = null;
        /// <summary>
        /// <see cref="FSVoidEventSO"/> raised when game is lost, called by levels game objects
        /// </summary>
        [Tooltip("Event raised when the game is lost")]
        [SerializeField] private FSVoidEventSO onGameLose = null;
        /// <summary>
        /// <see cref="FSVoidEventSO"/> raised when game starts, called by <see cref="GameScene"/>
        /// </summary>
        [Tooltip("Event raised each frame after start button has been clicked")]
        [SerializeField] private FSVoidEventSO onUpdateGameStarted = null;

        /// <summary>
        /// to know if game is started or not
        /// </summary>
        private bool gameStarted = false;
        /// <summary>
        /// to know if game is paused or not
        /// </summary>
        private bool gamePaused = false;
        
        /// <summary>
        /// called as Update from <see cref="MonoBehaviour"/>
        /// raise the <see cref="FSVoidEventSO"/> onUpdateGameStarted event
        /// </summary>
        public void OnUpdate()
        {
#if UNITY_EDITOR //TESTING PURPOSE
            if (Input.GetKeyDown(KeyCode.Return))
                onGameWin.Invoke();
            else if (Input.GetKeyDown(KeyCode.Delete))
                onGameLose.Invoke();
 #endif
            if (!gameStarted) return;
            if (gamePaused) return;
            
            onUpdateGameStarted.Invoke();
        }

        /// <summary>
        /// called from <see cref="FSVoidEventSO"/> onClickedStartButton
        /// </summary>
        public void OnClickedStartButton()
        {
            gameStarted = true;
        }

        /// <summary>
        /// called from <see cref="FSVoidEventSO"/> onWin
        /// </summary>
        public void OnWin()
        {
            gameStarted = false;
            onGameWin.Invoke();
        }

        /// <summary>
        /// called from <see cref="FSVoidEventSO"/> onLose
        /// </summary>
        public void OnLose()
        {
            gameStarted = false;
            onGameLose.Invoke();
        }

        /// <summary>
        /// called from <see cref="FSVoidEventSO"/> onPause
        /// </summary>
        public void OnPause()
        {
            gamePaused = true;
        }

        /// <summary>
        /// called from <see cref="FSVoidEventSO"/> onResume
        /// </summary>
        public void OnResume()
        {
            gamePaused = false;
        }
    }
}