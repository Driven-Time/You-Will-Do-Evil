using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Launcher;
using System;

public class CheckerBrain : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        char[] key = new char[] { 'O', 'T', 'L', ':' };
        bool OTL = Config.LoadSettings (key);

        key = new char[] { 'C', 'O', 'L', ':' };
        bool COL = Config.LoadSettings (key);

        if (OTL)
        {
            Config.SaveSetting ("OTL", false.ToString (), false);
            Config.SaveSetting ("COL", COL.ToString (), true);
            Destroy (this.gameObject);
        }
        else
        {
            Debug.Log("OTL Error");
            Application.Quit();
        }
	}

   
}
