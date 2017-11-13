
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraFollow : MonoBehaviour
{

    #region Variables
    [Header ("Follow")]
    [Tooltip ("The GameObject to Follow")]
    public Transform target;
    [Tooltip ("The distance on each axis from the GameObject to follow")]
    public Vector3 offset;  //  The Distance to Target
    [Tooltip ("The speed of the Camera while moving")]
    public float speed;

   
    [Header ("Rotation")]
    [Tooltip ("The speed of the Camera While turning")]
    public float turnSpeed;
    [Tooltip ("The angle the Camera should turn to")]
    public float turnAngle; //  HOw far the Camera will turn
	#endregion

	#region BuildIn Methods
	void Start ()
	{

        if (GameObject.Find ("Sarenn"))
        {
            target = GameObject.Find ("Sarenn").transform;
        }
	}

	void FixedUpdate ()
	{
        #region Turning
        float horizontal = Input.GetAxis ("Horizontal");    //  Getting the Axis

        /*Deciding which direction to turn and then turn*/
        if (horizontal > 0)    //   Right
        {
            Turn (turnAngle, turnSpeed);
        }
        else if (horizontal < 0)  //   Left
        {
            Turn (-turnAngle, turnSpeed);
        }

        /*Normalize*/
        else
        {
            Turn (0, turnSpeed);
        }
        #endregion

        #region Movement
        Vector3 follow = target.position + offset;  //  Target
        transform.position = Vector3.Slerp (transform.position, follow, speed); //  Delaying Camera movement
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
        Quaternion turnTo = Quaternion.Euler (8, Direction, 0);
        transform.rotation = Quaternion.Slerp (transform.rotation, turnTo, Speed * Time.deltaTime);
    }
	#endregion

}
