using InControl;


namespace FrancoisSauce.Scripts.Controls
{
    /// <summary>
    /// Implementation of the <see cref="PlayerActionSet"/> to make the player move with arrow keys
    /// see parent class to get more infos
    /// </summary>
    /// <inheritdoc/>
    public class PlayerControls : PlayerActionSet
    {
        public PlayerAction Left;
        public PlayerAction Right;
        public PlayerAction Forward;
        public PlayerAction Backward;

        public PlayerControls()
        {
            Left = CreatePlayerAction("Move Left" );
            Right = CreatePlayerAction("Move Right" );
            Forward = CreatePlayerAction("Move Forward");
            Backward = CreatePlayerAction(" Move Backward");
        }
    }
}