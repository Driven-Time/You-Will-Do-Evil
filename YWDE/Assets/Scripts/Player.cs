using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour{

    #region Variables
    [Header ("Info")]
    [Tooltip ("The type of the Object")]
    public Character Type;

    private Rigidbody Rigid;    //  The rigidbody of this Object

    [Header ("Ground Detection")]
    [Tooltip ("Where the LineCast should end")]
    public Transform RayEnd;
    [Tooltip ("What can the Player Jump on?")]
    public LayerMask Jumpable;

    #region [Cheat]
    /// <summary>
    /// Wether flight mode is on or off (Value set when KeyCode.Home is pressed [Fly = !Flye])
    /// </summary>
    [Header ("Cheats")]
    [Tooltip ("Wether God mode is on or off [Read Only]")]
    public bool God;
    #endregion

    #region Gizmo
    [Header ("Gizmo")]
    public LayerMask Foe;
    [Tooltip ("The size of the drawn Box Gizmo")]
    public float Radius;
    [Tooltip ("Color of the Box Gizmo drawn in editor")]
    public Color GizmoColor;
    #endregion
    #endregion

    #region BuildIn Methods


    void Start ()
	{
        name = Type.name;
        Type.Attacking = false; //  If false Player can initiate an attack
        Rigid = GetComponent<Rigidbody> (); //  Get the Rigidbody component
        Type.CurrentHealth = Type.MaxHealth;    //  Set Health at first frame
        Type.CurrentStamina = Type.MaxStamina;
        Type.SetTransitionValue = 0;   //  Set Transition
	}

    private void Update ()
    {
        Type.SetStats ();

        #region Movemet
        //  Ground Check
        if (Physics.Linecast (transform.position, RayEnd.position, Jumpable))
        {
            Type.IsGrounded = true;
        }
        else
        {
            Type.IsGrounded = false;
        }

        //  Jump button press
        if (Input.GetButton("Jump"))
        {
            Type.JumpButtonPressed = true;
        }
        else
        {
            Type.JumpButtonPressed = false;
        }

        //  Sprint button press
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Type.SprintButtonPressed = true;
        }
        else
        {
            Type.SprintButtonPressed = false;
        }
        #endregion

        #region Combat
        if (Input.GetKey (KeyCode.C))
        {
            Collider[] colliders = Physics.OverlapSphere (transform.position, Radius, Foe); //  Casta a SPhere that detects all nearby Objects with the Laymask value of Foe
            if (colliders.Length > 0)
            {
                if (!Type.Attacking)
                {
                    StartCoroutine (Type.Attack (colliders[0].gameObject)); //  Attack sequens
                }
            }

        }

        Type.ChangeColor (gameObject);  //  Updates the Material Color
        #endregion
        //  Death
        if (Rigid.position.y < -5 || Type.CurrentHealth <= 0)
        {
            global::God.Instance.Die (gameObject);
        }

        #region [Cheat] God Mode
        if (Input.GetKeyDown (KeyCode.Home))
        {
            God = !God;
            Debug.Log ("GodMode = [<i>" + God.ToString () + "</i>]");
            global::God.Instance.WriteConsoleLine ("GodMode = [<i>" + God.ToString() + "</i>]");
        }
        if (God)
        {
            Type.SetMode (Rigid, "God_On");
        }
        else
        {
            Type.SetMode (Rigid, "God_Off");
        }
        #endregion

    }

    private void OnDrawGizmos ()
    {
        Gizmos.color = GizmoColor;  //  Set color of Gizmo
        Gizmos.DrawSphere (transform.position, Radius); //  Draw the SPhere in editor
        Gizmos.DrawLine (transform.position, RayEnd.position);  //  Draw line in editor
    }

    void FixedUpdate ()
	{
        if (Rigid != null)
        {
            Type.Movement (ref Rigid);
            Type.Jumping (ref Rigid);
            Type.Sprinting (ref Rigid);
        }
        else
        {
            Debug.LogError ("Missing Rigidbody");
        }
        
    }
    
	#endregion

	#region Custom Methods

    /// <summary>
    /// Calculates the damage recieved. (God.DealDamage(float Damage, GameObject Object) sends a message to this method)
    /// </summary>
    /// <param name="Damage">The damage recieved</param>
    private void RecieveDamage (float Damage)
    {
        global::God.Instance.WriteConsoleLine ("Damage dealt to player<<<color=green>" + Type.name + "</color>>>: " + Damage);
        Debug.Log ("Damage dealt to player<<<color=green>" + Type.name + "</color>>>: " + Damage);
        Type.CurrentHealth -= Damage;
        Type.SetTransitionValue += Damage / Type.MaxHealth;

    }

	#endregion
}
