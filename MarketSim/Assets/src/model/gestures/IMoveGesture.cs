using UnityEngine;

public interface IMoveGesture
{
    #region Methods

    void movePlayer(GameObject player);

    #endregion Methods
}

/// <summary>
/// Strategy to move the player forward.
/// </summary>
internal class Forward : IMoveGesture
{
    #region Methods

    /// <summary>
    /// Moves the player forward.
    /// </summary>
    /// <param name="player">The player game object.</param>
    public void movePlayer(GameObject player)
    {
        player.GetComponent<PlayerController>().walkForward();
    }

    #endregion Methods
}

/// <summary>
/// Strategy to move the player backward.
/// </summary>
internal class Backward : IMoveGesture
{
    #region Methods

    /// <summary>
    /// Moves the player backward.
    /// </summary>
    /// <param name="player">The player.</param>
    public void movePlayer(GameObject player)
    {
        player.GetComponent<PlayerController>().walkBackwards();
    }

    #endregion Methods
}

/// <summary>
/// Strategy to rotate the player right
/// </summary>
internal class RotateRight : IMoveGesture
{
    #region Methods

    public void movePlayer(GameObject player)
    {
        player.GetComponent<PlayerController>().rotateRight();
    }

    #endregion Methods
}