using Assets.src.model;
using UnityEngine;

public interface IMoveGesture
{
    #region Methods

    /// <summary>
    /// Moves the player in a direction according to gesture.
    /// </summary>
    /// <param name="player">The player object.</param>
    void MovePlayer(GameObject player);

    /// <summary>
    /// Updates the behavior when grabbing
    /// </summary>
    /// <param name="quat">The quaternion.</param>
    /// <param name="grab">The type of grabbing.</param>
    void UpdateGrabbed(Quaternion quat, Grab grab);

    #endregion Methods
}

/// <summary>
/// Strategy for the gesture grab.
/// </summary>
public class GestureGrab : IMoveGesture
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
/// Strategy for the gesture pointing.
/// </summary>
public class GesturePoint : IMoveGesture
{
    #region Methods

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
/// Strategy for the gesture thumb.
/// </summary>
public class GestureThumb : IMoveGesture
{
    #region Methods

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
/// Strategy for the gesture pinky.
/// </summary>
public class GesturePinky : IMoveGesture
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
/// Strategy for no gesture.
/// </summary>
public class GestureNone : IMoveGesture
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
/// Strategy for the gesture open.
/// </summary>
public class GestureOpen : IMoveGesture
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