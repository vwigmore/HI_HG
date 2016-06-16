using Assets.src.model;
using ManusMachina;

public class GestureController
{
    #region Fields

    /// <summary>
    /// The bend threshold (above this we consider fingers bent).
    /// </summary>
    private const float BendThreshold = 0.4f;

    #endregion Fields

    #region Methods

    /// <summary>
    /// Returns the current made gesture.
    /// </summary>
    /// <param name="g">The glove that is making the gesture.</param>
    /// <returns></returns>
    public static IMoveGesture GetGesture(Glove g)
    {
        return DetermineGesture(g);
    }

    /// <summary>
    /// Determines the gesture.
    /// </summary>
    /// <param name="glove">The glove that is making the gesture.</param>
    /// <returns></returns>
    private static IMoveGesture DetermineGesture(Glove glove)
    {
        int bend = FingersBend(glove);

        if (bend == 5)
            return new GestureGrab();
        else if (bend == 4 && glove.Fingers[0] < BendThreshold)
            return new GestureThumb();
        else if (bend == 4 && glove.Fingers[4] < BendThreshold)
            return new GesturePinky();
        else if (glove.Fingers[1] < 0.4f && glove.Fingers[2] < BendThreshold && bend == 3)
            return new GesturePoint();
        else if (bend <= 1)
            return new GestureOpen();
        else return new GestureNone();
    }

    /// <summary>
    /// returns the number of bend fingers.
    /// </summary>
    /// <param name="glove">The glove used to determine the number of bend fingers.</param>
    /// <returns></returns>
    private static int FingersBend(Glove glove)
    {
        int fingersBent = 0;
        for (int i = 0; i < 5; i++)
        {
            if (glove.Fingers[i] >= BendThreshold)
            {
                fingersBent++;
            }
        }
        return fingersBent;
    }

    #endregion Methods
}