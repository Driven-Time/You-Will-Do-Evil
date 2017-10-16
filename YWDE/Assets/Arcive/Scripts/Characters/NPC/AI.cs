using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class AI : MonoBehaviour
{


    #region Variables

    private Rigidbody Rigid;

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
    [Tooltip ("The Color of the texture if the 'LayerMask' is detected")]
    public Color Detected;
    [Tooltip ("The Color the of the texture if the 'LayerMask' is not in range")]
    public Color NotDetected;
    private bool Stop = false;  //  True if the Player is in range
    #endregion

    #region Movement
    [Header ("Movement")]
    [Tooltip ("The speed of the GameObject will while moving")]
    public float Speed;
    [Tooltip ("The Axis(Direction) the GameObject so move on. [Negativ value will move it left]")]
    public Vector3 Direction;
    [Space(20)]
    [Tooltip ("The First point on which the Gameobject should chance Direction")]
    public Transform PointA;
    [Tooltip ("The Second point on which the GameObject should chance direction")]
    public Transform PointB;
    #endregion

    #region Dialog
    [Header ("Dialog")]
    [Tooltip ("The position and Size of the Dialog GUI Box [Position: X, Y | Size: W, H]")]
    public Rect Position;
    #endregion



    #endregion

    #region BuildIn Methods

    void Start ()
	{
        Rigid = GetComponent<Rigidbody> ();
    }

	void FixedUpdate ()
	{
        Detection ();
        if (!Stop)
        {
            Movement ();
        }
        
	}

    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "WayPoint")
        {
            Direction *= -1;    //  Chance Direction
        }
    }

    private void OnGUI ()
    {
        if (Stop)
        {
            GUI.Box (new Rect (Position), "<color=red><b>NPC: </b></color>" + "<i>Talk to me and you'll die!</i>");
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
    /// Move the rigidbody along an axis
    /// </summary>
    private void Movement ()
    {
        Rigid.AddForce (Direction * Speed * Time.deltaTime, ForceMode.Impulse);
    }

    /// <summary>
    /// Detect the presence of Player
    /// </summary>
    private void Detection ()
    {
        Collider[] Colliders = Physics.OverlapSphere (transform.position, Radius, LayerMask);   //  All Colliders Touching or inside the Sphere

        /*If Colliders is not empty*/
        if (Colliders.GetLength(0) > 0)
        {
            GetComponentInChildren<Renderer> ().material.color = Detected;
            float ConvertToNegativ = ((Colliders[0].transform.position.x - transform.position.x) > 0 ? -1 : 1);

            bool InRange = (Colliders[0].transform.position.x - transform.position.x) * ConvertToNegativ < 0.1;

            if (InRange)
            {
                Stop = true;
            }
        }
        else
        {
            GetComponentInChildren<Renderer> ().material.color = NotDetected;
            Stop = false;
        }
    }
    #endregion


}
