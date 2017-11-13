using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour{

    #region Variables
    [Header ("Info")]
    [Tooltip ("The type of the Object")]
    public Character type;

    private Rigidbody rigid;    //  The rigidbody of this Object

    [Header ("Status UI")]
    [Tooltip ("The UI element of Stamina and Health")]
    public Image healthbar, staminabar;

    [Header ("Ground Detection")]
    [Tooltip ("Where the LineCast should end")]
    public float isGroundedDetection;
    public Transform rayEnd;
    [Tooltip ("What can the Player Jump on?")]
    public LayerMask jumpable;

    #region [Cheat]
    /// <summary>
    /// Wether flight mode is on or off (Value set when KeyCode.Home is pressed [Fly = !Flye])
    /// </summary>
    [Header ("Cheats")]
    [Tooltip ("Wether God mode is on or off [Read Only]")]
    public bool god;
    #endregion

    #region Gizmo
    [Header ("Gizmo")]
    public LayerMask foe;
    [Tooltip ("The size of the drawn Box Gizmo")]
    public float radius;
    [Tooltip ("Color of the Box Gizmo drawn in editor")]
    public Color gizmoColor;
    #endregion
    #endregion

    #region BuildIn Methods


    void Start ()
	{
        if (healthbar == null)
        {
            healthbar = GameObject.Find ("Healthbar").GetComponentInChildren<Image> ();
        }

        if (staminabar == null)
        {
            staminabar = GameObject.Find ("Staminabar").GetComponentInChildren<Image> ();
        }
        name = type.name;
        type.attacking = false; //  If false Player can initiate an attack
        rigid = GetComponent<Rigidbody> (); //  Get the Rigidbody component
        type.currentHealth = type.maxHealth;    //  Set Health at first frame

        

        type.currentStamina = type.maxStamina;
        type.SetTransitionValue = 0;   //  Set Transition
	}

    private void Update ()
    {
        type.SetStats ();

        UpdateUI ();

        #region Movemet
        //  Ground Check
        Collider[] col = Physics.OverlapSphere (rayEnd.transform.position, isGroundedDetection, jumpable);
        if (col.Length >= 1)
        {
            type.isGrounded = true;
        }
        else
        {
            type.isGrounded = false;
        }

        //  Jump button press
        if (Input.GetButton("Jump"))
        {
            type.jumpButtonPressed = true;
        }
        else
        {
            type.jumpButtonPressed = false;
        }

        //  Sprint button press
        if (Input.GetKey(KeyCode.LeftShift))
        {
            type.sprintButtonPressed = true;
        }
        else
        {
            type.sprintButtonPressed = false;
        }
        #endregion

        #region Combat
        if (Input.GetKey (KeyCode.C))
        {
            Collider[] colliders = Physics.OverlapSphere (transform.position, radius, foe); //  Casta a SPhere that detects all nearby Objects with the Laymask value of Foe
            if (colliders.Length > 0)
            {
                if (!type.attacking)
                {
                    StartCoroutine (type.Attack (colliders[0].gameObject)); //  Attack sequens
                }
            }

        }

        type.ChangeColor (gameObject);  //  Updates the Material Color
        #endregion
        //  Death
        if (rigid.position.y < -5 || type.currentHealth <= 0)
        {
            global::God.Instance.Die (gameObject);
        }

        #region [Cheat] God Mode
        if (Input.GetKeyDown (KeyCode.Home))
        {
            god = !god;
            Debug.Log ("GodMode = [<i>" + god.ToString () + "</i>]");
            //global::God.Instance.WriteConsoleLine ("GodMode = [<i>" + God.ToString() + "</i>]");
        }
        if (god)
        {
            type.SetMode (rigid, "God_On");
        }
        else
        {
            type.SetMode (rigid, "God_Off");
        }
        #endregion

    }

    private void OnDrawGizmos ()
    {
        Gizmos.color = gizmoColor;  //  Set color of Gizmo
        Gizmos.DrawSphere (transform.position, radius); //  Draw the SPhere in editor
        Gizmos.DrawLine (transform.position, rayEnd.position);  //  Draw line in editor
        Gizmos.DrawSphere (rayEnd.transform.position, isGroundedDetection);
    }

    void FixedUpdate ()
	{
        if (rigid != null)
        {
            type.Movement (ref rigid);
            type.Jumping (ref rigid);
            type.Sprinting (ref rigid);
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
    /// <param name="_damage">The damage recieved</param>
    private void RecieveDamage (float _damage)
    {
        //global::God.Instance.WriteConsoleLine ("Damage dealt to player<<<color=green>" + Type.name + "</color>>>: " + Damage);
        Debug.Log ("Damage dealt to player<<<color=green>" + type.name + "</color>>>: " + _damage);
        type.currentHealth -= _damage;
        type.SetTransitionValue += _damage / type.maxHealth;

    }

    /// <summary>
    /// Updates the UI for Health and Stamina
    /// </summary>
    private void UpdateUI ()
    {
        healthbar.fillAmount = type.currentHealth / type.maxHealth;
        staminabar.fillAmount = type.currentStamina / type.maxStamina;
    } 

	#endregion
}
