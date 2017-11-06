using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class God : MonoBehaviour{


    #region Variables
    
    #region Singleton
    private static God instance = null;

    /// <summary>
    /// Represents a Static instance to the 'God' Script (If no God Object exists it will create a 'God' object and attach the GodScript to it)
    /// </summary>
    public static God Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType (typeof (God)) as God;
            }

            if (instance == null)
            {
                GameObject Obj = GameObject.Find ("GOD");
                if (Obj == null)
                {
                    Debug.Log ("No Object named 'GOD' found. Object generated Automaticly");
                    Obj = new GameObject ("GOD");
                    DontDestroyOnLoad (Obj);
                }

                Debug.Log ("No instance if type 'GOD' was found. Instance generated Automaticly");
                instance = Obj.AddComponent (typeof (God)) as God;
            }

            return instance;
        }
    }

    private void OnApplicationQuit ()
    {
        Debug.Log ("Instance of type 'God' is equal to null duo to the Game shutting down");
        instance = null;
    }
    #endregion

    [Header ("DEBUG")]
    [System.Obsolete ("This variable is no longer in use", true)]
    [Tooltip ("True if the build is deployed as 'Development Build'")]
    public bool IsDevelopmentBuild;
    public bool FlyMode;
    /*private  bool _IsDevelopmentBuild
    {
        get { return IsDevelopmentBuild; }
        set
        {
            if (Debug.isDebugBuild)
            {
                value = true;
            }
            else
            {
                value = false;
            }
        }
    }*/
    [System.Obsolete ("This variable is no longer in use", true)]
    public Queue<string> ConsoleLines = new Queue<string> ();

    private int MaxLineNumber = 28;
    private int CurrentLines = 0;

    private ScrollRect ScrollBar;
    string DefaultString;
    Text ConsoleText;
    #endregion

    #region BuildIn Methods

    void Start ()
	{
        ConsoleText = GameObject.Find ("ConsoleText").GetComponentInChildren<Text> ();
        DefaultString = ConsoleText.text;
    }

	void Update ()
	{

    }
    
	#endregion

	#region Custom Methods 

    /// <summary>
    /// Close game
    /// </summary>
    public void CloseGame ()
    {
        Debug.Log ("Game Closed");
        Application.Quit ();
    }

    public void DealDamage (float Damage, GameObject Object)
    {
        Object.SendMessage ("RecieveDamage", Damage, SendMessageOptions.RequireReceiver);
    }

    /// <summary>
    /// Respawns a prefab from the Resource folder
    /// </summary>
    /// <param name="ToFind">The name of the Prefab to find</param>
    public void Respawn (string ToFind)
    {
        //string Tofind = "Doll";
        GameObject ToRespawn = Resources.Load ("Prefabs/" + ToFind, typeof (GameObject)) as GameObject;
        if (ToRespawn != null)
        {
            Instantiate (ToRespawn, ToRespawn.transform.position, transform.rotation);
            Debug.Log ("Spawned in <<" + ToRespawn.name + ">>");
            WriteConsoleLine ("Spawned in <<<Color=red>" + ToRespawn.name + "</color>>>");


        }
        else
        {
            Debug.Log ("No asset named " + ToFind);
        }
        
    }

    /// <summary>
    /// Temp Method for Restarting Loop when Death accurs
    /// </summary>
    public void Die (GameObject Object)
    {
        Destroy (Object);
        GameObject PlayerPrefab = Resources.Load ("Prefabs/Player", typeof (GameObject)) as GameObject;
        CameraFollow Camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
        GameObject Player = Instantiate (PlayerPrefab, PlayerPrefab.transform.position, PlayerPrefab.transform.rotation);
        Camera.Target = Player.transform;
        WriteConsoleLine ("Player: <<<color=green>" + Object.name + "</color>>> reset duo to Death");
        Debug.Log ("Player: <<<color=green>" + Object.name + "</color>>> reset duo to Death");
    }

    /// <summary>
    /// Write a string to the Console
    /// </summary>
    /// <param name="Line"></param>
    public void WriteConsoleLine (string Line)
    {
        CurrentLines++;
        ConsoleText = GameObject.Find ("ConsoleText").GetComponentInChildren<Text> ();
        ScrollBar = GameObject.Find ("ScrollRect").GetComponent<ScrollRect> ();
        if (CurrentLines == MaxLineNumber)
        {
            ConsoleText.text = DefaultString;
            ScrollBar.verticalNormalizedPosition = 0.856f;
            CurrentLines = 0;
        }
        ConsoleText.text += "<|" + Line + "|>\n";
        ConsoleText.text += "----------------------------------------------------------------------------\n";
        ScrollBar.verticalNormalizedPosition -= 0.0315f;    //  Scroll with Console Output (NOTE: Not perfect!)

    }
    #endregion
}
