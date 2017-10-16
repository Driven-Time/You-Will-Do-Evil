
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraFollow : MonoBehaviour
{

    #region Variables
    [Header ("Follow")]
    [Tooltip ("The GameObject to Follow")]
    public Transform Target;
    [Tooltip ("The distance on each axis from the GameObject to follow")]
    public Vector3 Offset;  //  The Distance to Target
    [Tooltip ("The speed of the Camera while moving")]
    public float Speed;

   
    [Header ("Rotation")]
    [Tooltip ("The speed of the Camera While turning")]
    public float TurnSpeed;
    [Tooltip ("The angle the Camera should turn to")]
    public float TurnAngle; //  HOw far the Camera will turn
	#endregion

	#region BuildIn Methods
	void Start ()
	{

        Target = GameObject.Find ("Player").transform;
	
	}

	

	void FixedUpdate ()
	{
        #region Turning
        float Horizontal = Input.GetAxis ("Horizontal");    //  Getting the Axis

        /*Deciding which direction to turn and then turn*/
        if (Horizontal > 0)    //   Right
        {
            Turn (TurnAngle, TurnSpeed);
        }
        else if (Horizontal < 0)  //   Left
        {
            Turn (-TurnAngle, TurnSpeed);
        }

        /*Normalize*/
        else
        {
            Turn (0, TurnSpeed);
        }
        #endregion

        #region Movement
        Vector3 Follow = Target.position + Offset;  //  Target
        transform.position = Vector3.Slerp (transform.position, Follow, Speed); //  Delaying Camera movement
        #endregion

    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Turn to a specific Cordinate
    /// </summary>
    /// <param name="Direction"></param>
    /// <param name="Speed"></param>
    private void Turn (float Direction, float Speed)
    {
        Quaternion TurnTo = Quaternion.Euler (8, Direction, 0);
        transform.rotation = Quaternion.Slerp (transform.rotation, TurnTo, Speed * Time.deltaTime);
    }
	#endregion

}
