using Assets.src.model;
using UnityEngine;

public interface IMoveGesture
{
    #region Methods

    void MovePlayer(GameObject player);

    void UpdateGrabbed(Quaternion quat, Grab grab);

    #endregion Methods
}

/// <summary>
///
/// </summary>
internal class GestureGrab : IMoveGesture
{
    #region Methods

    public void MovePlayer(GameObject player)
    {
    }

    public void UpdateGrabbed(Quaternion quat, Grab grab)
    {
        if (!grab.IsGrabbing())
        {
            grab.GrabHighlightedObject();
            grab.prevGrabberRot = quat;
        }
    }

    #endregion Methods
}

/// <summary>
/// Strategy to move the player forward.
/// </summary>
internal class GesturePoint : IMoveGesture
{
    #region Methods

    /// <summary>
    /// Moves the player forward.
    /// </summary>
    /// <param name="player">The player game object.</param>
    public void MovePlayer(GameObject player)
    {
        player.GetComponent<PlayerController>().walkForward();
    }

    public void UpdateGrabbed(Quaternion quat, Grab grab)
    {
    }

    #endregion Methods
}

/// <summary>
/// Strategy to move the player backward.
/// </summary>
internal class GestureThumb : IMoveGesture
{
    #region Methods

    /// <summary>
    /// Moves the player backward.
    /// </summary>
    /// <param name="player">The player.</param>
    public void MovePlayer(GameObject player)
    {
        player.GetComponent<PlayerController>().walkBackwards();
    }

    public void UpdateGrabbed(Quaternion quat, Grab grab)
    {
    }

    #endregion Methods
}

/// <summary>
/// Strategy to rotate the player right
/// </summary>
internal class GesturePinky : IMoveGesture
{
    #region Methods

    public void MovePlayer(GameObject player)
    {
        player.GetComponent<PlayerController>().rotateRight();
    }

    public void UpdateGrabbed(Quaternion quat, Grab grab)
    {
    }

    #endregion Methods
}

/// <summary>
///
/// </summary>
internal class GestureNone : IMoveGesture
{
    #region Methods

    public void MovePlayer(GameObject player)
    {
    }

    public void UpdateGrabbed(Quaternion quat, Grab grab)
    {
    }

    #endregion Methods
}

/// <summary>
///
/// </summary>
internal class GestureOpen : IMoveGesture
{
    #region Methods

    public void MovePlayer(GameObject player)
    {
    }

    public void UpdateGrabbed(Quaternion quat, Grab grab)
    {
        if (grab.IsGrabbing())
        {
            grab.DropObject();
        }
    }

    #endregion Methods
}