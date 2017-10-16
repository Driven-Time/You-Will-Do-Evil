using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class GodScript : MonoBehaviour{


	#region Variables
	#endregion

	#region BuildIn Methods

    void Start ()
	{
		
	}

	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DEBUG.Reset.Scene ();
        }
	}
    
	#endregion

	#region Custom Methods 
	#endregion

	
}





