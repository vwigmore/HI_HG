using UnityEngine;

public class GestureClient
{
    #region Fields

    /// <summary>
    /// The strategy the client uses.
    /// </summary>
    private IMoveGesture gestureStrategy;

    #endregion Fields

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="GestureClient"/> class.
    /// </summary>
    /// <param name="strategy">The strategy to use.</param>
    public GestureClient(IMoveGesture strategy)
    {
        gestureStrategy = strategy;
    }

    #endregion Constructors

    #region Methods

    /// <summary>
    /// Moves the player.
    /// </summary>
    /// <param name="player">The player.</param>
    public void movePlayer(GameObject player)
    {
        gestureStrategy.movePlayer(player);
    }

    #endregion Methods
}