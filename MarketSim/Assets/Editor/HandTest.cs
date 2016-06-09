using UnityEngine;
using UnityEditor;
using NUnit.Framework;

namespace Assets.Editor
{ 
[TestFixture]

   public class HandTest : HandTest {


        #region Fields

        /// <summary>
        /// The root transform
        /// </summary>
        public Transform RootTransform;

        /// <summary>
        /// Left or right hand.
        /// </summary>
        protected GLOVE_HAND glove_hand;

        /// <summary>
        /// Manus Glove instance.
        /// </summary>
        protected Glove glove;

        /// <summary>
        /// The root (wrist) of the hand.
        /// </summary>
        protected GameObject root;

        /// <summary>
        /// The hand model
        /// </summary>
        protected GameObject handModel;

        /// <summary>
        /// The manus grab
        /// </summary>
        protected ManusGrab manusGrab;

        /// <summary>
        /// The game transforms
        /// </summary>
        protected Transform[][] gameTransforms;

        /// <summary>
        /// The time factor
        /// </summary>
        private const float timeFactor = 10.0f;

        /// <summary>
        /// The bend threshold.
        /// </summary>
        private const float BendThreshold = 0.4f;

        /// <summary>
        /// The sphere collider radius
        /// </summary>
        private const float SphereColliderRadius = 0.035f;

        /// <summary>
        /// The vibrate time
        /// </summary>
        private readonly float vibrateTime = (float)Manager.VibrationTime / 1000;

        /// <summary>
        /// The vibration power
        /// </summary>
        private readonly float vibrationForce = Manager.VibrationForce;

        /// <summary>
        /// The base hand collider size
        /// </summary>
        private Vector3 BaseHandColliderSize = new Vector3(0.1f, 0.05f, 0.10f);

        /// <summary>
        /// Game object hand.
        /// </summary>
        private GameObject hand;

        /// <summary>
        /// The sphere collider
        /// </summary>
        private SphereCollider sphereCollider;

        /// <summary>
        /// The model transforms
        /// </summary>
        private Transform[][] modelTransforms;

        /// <summary>
        /// The animation clip
        /// </summary>
        private AnimationClip animationClip;

        /// <summary>
        /// The select color.
        /// </summary>
        private Color highlightColor;

        /// <summary>
        /// The timer
        /// </summary>
        private float timer;

        /// <summary>
        /// The last touched gameobject
        /// </summary>
        private GameObject lastTouched;

        /// <summary>
        /// Bool if the glove should be vibrated.
        /// </summary>
        private bool vibrateGlove;

        #endregion Fields

        #region Methods

        [SetUp]
        public new void Setup()
        {
        }

        [TearDown]
        public new void TearDown()
        {
        }

        public void EditorTest()
    {
        //Arrange
        var gameObject = new GameObject();

        //Act
        //Try to rename the GameObject
        var newGameObjectName = "My game object";
        gameObject.name = newGameObjectName;

        //Assert
        //The object has a new name
        Assert.AreEqual(newGameObjectName, gameObject.name);
    }
  }

    #endregion Methods
}