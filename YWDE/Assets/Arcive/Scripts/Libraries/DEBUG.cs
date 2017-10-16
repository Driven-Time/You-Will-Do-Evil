using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace DEBUG
{  
    /// <summary>
    /// A collection of functions for resetting
    /// </summary>
    public static class Reset
    {
        /// <summary>
        /// Reset scene back to when it was first loaded
        /// </summary>
        public static void Scene ()
        {
            Debug.Log ("Resetting scene");
            SceneManager.LoadScene ("HermitHole");
        }

        /// <summary>
        /// Reset Character back to origin
        /// </summary>
        public static void Character ()
        {

        }
    }

    /// <summary>
    /// Debug 
    /// </summary>
    public static class Character
    {
        private static Rigidbody Rigid;
        public static void Fly (Rigidbody _Rigid, bool OnOff)
        {
            Rigid = _Rigid;
            if (OnOff)
            {
                Rigid.useGravity = false;
                FlyControls ();
            }
            else
            {
                Rigid.useGravity = true;
            }
            
        }

        private static void FlyControls ()
        {
            Vector3 Fly = new Vector3 (0, 25, 0);
            if (Input.GetKey(KeyCode.Space))
            {
                Rigid.AddForce (Fly * Time.deltaTime, ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Rigid.AddForce (-Fly * Time.deltaTime, ForceMode.Impulse);
            }
        }
    }
}





