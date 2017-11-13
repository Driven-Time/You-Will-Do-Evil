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
        Type.currentHealth = Type.maxHealth;    //  Set Health of this Object
        //  If the Type = Null Destroy this Object
        if (Type == null)
        {
            Debug.Log ("<<" + this.name + ">> does not have a 'Type' instance");
            Destroy (gameObject);
        }

        Type.waitFlag = false;
        Type.attacking = false;
        Rigid = GetComponent<Rigidbody> (); //  Get Rigidody of this Component
        this.name = Type.name + "(NPC)";
        Type.SetTransitionValue = 0;

       
	}

    private void Update ()
    {
        DetectEdge ();
        if (!Type.waitFlag)
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
            //Debug.Log ("Before" + Rigid.velocity);
            if (Rigid.velocity.y < 0)
            {
                //Debug.Log (Rigid.velocity);
                Rigid.velocity += new Vector3 (Rigid.velocity.x, Rigid.velocity.y, 0) * Physics.gravity.y * (1 - 1) * Time.deltaTime;
            }
            else
            {
                Type.Movement (ref Rigid);
            }
            

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
            Type.instantTurn = true;   
        }
        else
        {
            Type.instantTurn = false;
        }
    }

    private void DetectPlayer ()
    {
        Collider[] colliders = Physics.OverlapSphere (transform.position, Radius, Player);
        if (colliders.Length > 0)
        {
            Type.inRange = true;
            if (!Type.attacking)
            {
                StartCoroutine (Type.Attack (1, colliders[0].gameObject));
            }
        }
        else
        {
            Type.inRange = false;
        }
    }

    /// <summary>
    /// Calculates the damage recieved. (God.DealDamage(float Damage, GameObject Object) sends a message to this method)
    /// </summary>
    /// <param name="Damage">The damage recieved</param>
    private void RecieveDamage (float Damage)
    {
        //Debug.Log ("Damage dealt to NPC<<<color=red>" + Type.name + "</color>>>: " + Damage);
        //God.Instance.WriteConsoleLine ("Damage dealt to NPC<<<color=red>" + Type.name + "</color>>>: " + Damage);

        Type.currentHealth -= Damage;
        Type.SetTransitionValue += Damage / Type.maxHealth;
    }
    #endregion
}





