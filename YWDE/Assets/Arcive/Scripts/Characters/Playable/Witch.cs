using System.Collections;

using System.Collections.Generic;

using Characters;

using UnityEngine;
using UnityEngine.UI;

public class Witch : MonoBehaviour{


    #region Variables
    WitchClass witch = new WitchClass ();

    #region Attributes
    [Header ("Attributes")]

    [Tooltip ("Max Health of the Character")]
    public float Health = 0;
    [Tooltip ("Max Stamina of the Character")]
    public float Stamina = 0;

    [Tooltip ("The amount of time, in seconds, it takes to regenerate 1 stamina point")]
    public float StaminaRegeneration;
    [Tooltip ("The amount of time, in seconds, it takes to use 1 stamina point")]
    public float StaminaDepleation;

    [Tooltip ("The UI Element of the Magica Bar")]
    public Image MagicaBar;
    #endregion

    #region Movement Variables

    public Rigidbody Rigid;

    [Header ("Movement")]
    [Tooltip ("The Force Applied to the GameObject when moving")]
    public float Speed;
    public float RunningSpeed;

    [Header ("Jumping")]
    [Tooltip ("The force Applied to the GameObject when Jumping")]
    public float JumpForce;    //  Original Value

    [Tooltip ("Max Time the GameObject can be airborn")]
    public float MaxJumpTime;  // Max Air Time

    [Tooltip ("The position from which the LineCast should begin")] 
    public Transform RayStart;
    [Tooltip ("The position from which the LineCast should end")]
    public Transform RayEnd;
    #endregion

    #region Gizmo
    [Header ("Gizmo")]
    [Tooltip ("The size of the drawn Box Gizmo")]
    public float Size;
    [Tooltip ("Color of the Box Gizmo drawn in editor")]
    public Color GizmoColor;
    #endregion

    #region Detection
    [Header ("Detection")]
    [Tooltip ("The radius of the detection field")]
    public float Radius;
    [Tooltip ("The LayerMask to check for")]
    public LayerMask LayerMask;
    [Tooltip ("True if any Friendly NPC is in Range")]
    public bool Interact = false;
    #endregion

    #region Dialog
    [Header ("Dialog")]
    [Tooltip ("The position and Size of the Dialog GUI Box [Position: X, Y | Size: W, H]")]
    public Rect Position;
    private bool Interaction = false;
    #endregion

    #region Cheats
    private bool Cheat;
    #endregion

	#endregion

	#region BuildIn Methods

    void Start ()
	{
        #region Movement
        witch.Rigid = GetComponent<Rigidbody> ();
        witch.Speed = Speed;
        witch.RunningSpeed = RunningSpeed;
        witch.StaminaRegenTime = StaminaRegeneration;
        witch.StaminaDepleatTime = StaminaDepleation;

        witch.RayStart = RayStart;
        witch.RayEnd = RayEnd;

        witch.JumpForce = JumpForce;
        witch.MaxJumpTime = MaxJumpTime;
        #endregion

        #region Attributes

        //Health
        witch.HealthBar = GameObject.Find ("HealthBar").GetComponentInChildren<Slider> ();
        witch.MaxHealth = Health;
        witch.Health = Health;

        //Stamina
        witch.StaminaBar = GameObject.Find ("StaminaBar").GetComponentInChildren<Slider> ();
        witch.MaxStamina = Stamina;
        witch.Stamina = Stamina;
        #endregion
    }

    private void Update ()
    {
        witch.HealthSystem ();
        witch.StaminaSystem ();
        witch.MagicaSystem ();
    }

    void FixedUpdate ()
	{
        witch.Movement ();
	}
    
	#endregion

	#region Custom Methods 
	#endregion

	
}





