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
        God.Instance.OnLaunch ();
	}
}
