using UnityEngine;
using System.Collections;
using Assets.src.model;
using ManusMachina;


public class VibrateHand : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// The vibrate time.
    /// </summary>
    private readonly float vibrateTime = (float)Manager.VibrationTime / 1000;

    /// <summary>
    /// The vibration power.
    /// </summary>
    private readonly float vibrationForce = Manager.VibrationForce;

    /// <summary>
    /// The timer.
    /// </summary>
    private float timer;

    /// <summary>
    /// Bool if the glove should be vibrated.
    /// </summary>
    private bool vibrateGlove;

    /// <summary>
    /// Manus Glove instance.
    /// </summary>
    protected Glove glove;

    #endregion Fields

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="VibrateHand"/> class.
    /// </summary>
    public VibrateHand(Glove glove)
    {
        this.glove = glove;
    }

    #endregion Constructor

    #region Methods

    public void Update()
    {
        UpdateTimer();
        UpdateVibration();
    }

    /// <summary>
    /// Resets the timer.
    /// </summary>
    public void ResetTimer()
    {
        timer = 0;
    }

    /// <summary>
    /// Updates the timer.
    /// </summary>
    public void UpdateTimer()
    {
        timer += Time.deltaTime;
    }

    ///// <summary>
    ///// Updates the vibration of the glove.
    ///// </summary>
    public void UpdateVibration()
    {

        if (timer <= vibrateTime && vibrateGlove)
        {
            glove.SetVibration(vibrationForce);
        }
        else
        {
            glove.SetVibration(0.0f);
            ResetTimer();
            vibrateGlove = false;
        }
    }

    /// <summary>
    /// Vibrates the hand.
    /// </summary>
    public void Vibrate()
    {
        vibrateGlove = true;
    }

    #endregion Methods

}
   