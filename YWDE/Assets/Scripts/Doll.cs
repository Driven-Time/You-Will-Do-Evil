using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class Doll : MonoBehaviour{

    #region Variables
    [Header ("Info")]
    [Tooltip ("The Type of the Objects")]
    public NPC Type;

    private Rigidbody Rigid;
    [Header ("Ground detection")]
    [Tooltip ("Where should the RayCast stop?")]
    public Transform RayEnd;
    [Tooltip ("What is ground?")]
    public LayerMask Ground;

    #region Gizmo
    [Header ("Gizmo")]
    public LayerMask Player;
    [Tooltip ("The size of the drawn Box Gizmo")]
    public float Radius;
    [Tooltip ("Color of the Box Gizmo drawn in editor")]
    public Color GizmoColor;
    #endregion
    #endregion

    #region BuildIn Methods

    void Start ()
	{
        Type.CurrentHealth = Type.MaxHealth;    //  Set Health of this Object
        //  If the Type = Null Destroy this Object
        if (Type == null)
        {
            Debug.Log ("<<" + this.name + ">> does not have a 'Type' instance");
            Destroy (gameObject);
        }

        Type.WaitFlag = false;
        Type.Attacking = false;
        Rigid = GetComponent<Rigidbody> (); //  Get Rigidody of this Component
        this.name = Type.name + "(NPC)";
        Type.SetTransitionValue = 0;

       
	}

    private void Update ()
    {
        DetectEdge ();
        if (!Type.WaitFlag)
        {
            StartCoroutine (Type.RandomMove ());    //  Initiate Move sequens

        }
        DetectPlayer ();
        Type.Die (gameObject);
        Type.ChangeColor (gameObject); //   UPdate the Material Color
        
    }

    private void OnDrawGizmos ()
    {
        Gizmos.color = GizmoColor;  //  Set Gizmo Color
        Gizmos.DrawSphere (transform.position, Radius); // Draws a sphere in the editor
    }

    void FixedUpdate ()
	{
        if (Rigid != null)
        {
            Type.Movement (ref Rigid);
            
        }
        else
        {
            Rigid = Type.CreateRigidBody (gameObject);
        }
        
	}
    
	#endregion

	#region Custom Methods 
    private void DetectEdge ()
    {
        if (!Physics.Linecast (transform.position, RayEnd.position, Ground))
        {
            Type.InstantTurn = true;   
        }
        else
        {
            Type.InstantTurn = false;
        }
    }

    private void DetectPlayer ()
    {
        Collider[] colliders = Physics.OverlapSphere (transform.position, Radius, Player);
        if (colliders.Length > 0)
        {
            Type.InRange = true;
            if (!Type.Attacking)
            {
                StartCoroutine (Type.Attack (1, colliders[0].gameObject));
            }
        }
        else
        {
            Type.InRange = false;
        }
    }

    /// <summary>
    /// Calculates the damage recieved. (God.DealDamage(float Damage, GameObject Object) sends a message to this method)
    /// </summary>
    /// <param name="Damage">The damage recieved</param>
    private void RecieveDamage (float Damage)
    {
        Debug.Log ("Damage dealt to NPC<<<color=red>" + Type.name + "</color>>>: " + Damage);
        God.Instance.WriteConsoleLine ("Damage dealt to NPC<<<color=red>" + Type.name + "</color>>>: " + Damage);
        Type.CurrentHealth -= Damage;
        Type.SetTransitionValue += Damage / Type.MaxHealth;
    }
    #endregion
}





