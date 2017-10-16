using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Characters;



public class Warrior : MonoBehaviour
{


    #region Variables
    WarriorClass warrior = new WarriorClass ();

    #region Attributes
    [Header ("Attributes")]

    [Tooltip ("Max Health of the Character")]
    public float Health = 0;
    [Tooltip("Max Stamina of the Character")]
    public float Stamina = 0;

    [Tooltip ("The amount of time, in seconds, it takes to regenerate 1 stamina point")]
    public float StaminaRegeneration;
    [Tooltip ("The amount of time, in seconds, it takes to use 1 stamina point")]
    public float StaminaDepleation;

    [Tooltip ("The BloodLust UI Element")]
    public Image BloodLustBar;
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
        #region Movement Variables
        warrior.Rigid = GetComponent<Rigidbody> ();
        warrior.Speed = Speed;
        warrior.RunningSpeed = RunningSpeed;
        warrior.StaminaRegenTime = StaminaRegeneration;
        warrior.StaminaDepleatTime = StaminaDepleation;
        warrior.JumpForce = JumpForce;
        warrior.MaxJumpTime = MaxJumpTime;
        warrior.RayStart = RayStart;
        warrior.RayEnd = RayEnd;
        #endregion

        #region Attributes

        //Health
        warrior.HealthBar = GameObject.Find ("HealthBar").GetComponentInChildren<Slider>();
        warrior.MaxHealth = Health;
        warrior.Health = Health;
        
        //Stamina
        warrior.StaminaBar = GameObject.Find ("StaminaBar").GetComponentInChildren<Slider> ();
        warrior.MaxStamina = Stamina;
        warrior.Stamina = Stamina;

        //BloodLust
        warrior.BloodLustBar = GameObject.Find ("BloodLust").GetComponentInChildren<Image> ();
        warrior.BloodLust = 1;
        #endregion
    }

    void FixedUpdate ()
	{
        warrior.Movement ();
        Detection ();
    }

    private void Update ()
    {

        warrior.HealthSystem ();
        warrior.StaminaSystem ();
        warrior.BloodLustSystem ();

        if (Input.GetKeyDown (KeyCode.E) && Interact)
        {
            Interaction = !Interaction;
        }

        #region DEBUG
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            Cheat = !Cheat;
        }

        DEBUG.Character.Fly (Rigid, Cheat);
        #endregion
    }

    private void OnGUI ()
    {
        if (Interact)
        {
            GUI.Box (new Rect (Position), "E");
            if (Interaction)
            {
                GUI.Box (new Rect (new Vector2 (200, 500), new Vector2 (250, 25)), "<Color=green><b>Player:</b></color><i> Oh shit Oh shit!!</i>");
                warrior.Health -= 1;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos ()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawSphere (transform.position, Size); // Draws a sphere in the editor
    }
    #endif
#endregion

    #region Custom Methods
    /// <summary>
    /// Detect the presence of Player
    /// </summary>
    private void Detection ()
    {
        Collider[] Colliders = Physics.OverlapSphere (transform.position, Radius, LayerMask);   //  All Colliders Touching or inside the Sphere

        /*If Colliders is not empty*/
        if (Colliders.GetLength (0) > 0)
        {
            float ConvertToNegativ = ((Colliders[0].transform.position.x - transform.position.x) > 0 ? -1 : 1);

            bool InRange = (Colliders[0].transform.position.x - transform.position.x) * ConvertToNegativ < 0.1;

            if (InRange)
            {
                Interact = true;
            }
        }
        else
        {
            Interact = false;
            Interaction = false;
        }
    }
    #endregion

}
