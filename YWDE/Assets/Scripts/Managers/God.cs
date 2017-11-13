using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Launcher;

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
                GameObject obj = GameObject.Find ("GOD");
                if (obj == null)
                {
                    Debug.LogError ("No Object named 'GOD' found. Object generated Automaticly");
                    obj = new GameObject ("GOD");
                    DontDestroyOnLoad (obj);
                }

                Debug.LogError ("No instance if type 'GOD' was found. Instance generated Automaticly");
                instance = obj.AddComponent (typeof (God)) as God;
            }

            return instance;
        }
    }

    private void OnApplicationQuit ()
    {
        Debug.LogError ("Instance of type 'God' is equal to null duo to the Game shutting down");
        instance = null;
    }
    #endregion

    [Header ("On launch checker")]
    public bool openTroughLauncher = false;

    [System.Obsolete ("This variable is no longer in use", true)]
    public Queue<string> consoleLines = new Queue<string> ();

    private int maxLineNumber = 28;
    private int currentLines = 0;

    private ScrollRect scrollBar;
    string defaultString;
    Text consoleText;
    #endregion

    #region BuildIn Methods

    void Start ()
	{

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

    /// <summary>
    /// Deals damage to a specified object
    /// </summary>
    /// <param name="_damage">The amount of damage</param>
    /// <param name="_object">The object to apply damage to</param>
    public void DealDamage (float _damage, GameObject _object)
    {
        _object.SendMessage ("RecieveDamage", _damage, SendMessageOptions.RequireReceiver);
    }

    /// <summary>
    /// Respawns a prefab from the Resource folder
    /// </summary>
    /// <param name="_toFind">The name of the Prefab to find</param>
    public void Respawn (string _toFind)
    {
        //string Tofind = "Doll";
        GameObject _toRespawn = Resources.Load ("Prefabs/" + _toFind, typeof (GameObject)) as GameObject;
        if (_toRespawn != null)
        {
            Instantiate (_toRespawn, _toRespawn.transform.position, transform.rotation);
            Debug.LogError ("Spawned in <<" + _toRespawn.name + ">>");
            //WriteConsoleLine ("Spawned in <<<Color=red>" + ToRespawn.name + "</color>>>");


        }
        else
        {
            Debug.Log ("No asset named " + _toFind);
        }
        
    }

    /// <summary>
    /// Temp Method for Restarting Loop when Death accurs
    /// </summary>
    public void Die (GameObject _object)
    {
        Destroy (_object);
        GameObject playerPrefab = Resources.Load ("Prefabs/Player", typeof (GameObject)) as GameObject;
        CameraFollow camera = GameObject.Find ("Main Camera").GetComponent<CameraFollow> ();
        GameObject player = Instantiate (playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);
        camera.target = player.transform;
        //WriteConsoleLine ("Player: <<<color=green>" + Object.name + "</color>>> reset duo to Death");
        Debug.LogError ("Player: <<<color=green>" + _object.name + "</color>>> reset duo to Death");
    }

    /// <summary>
    /// Write a string to the Console
    /// </summary>
    /// <param name="_line"></param>
    [System.Obsolete("This system is marked as 'standby' at the moment", true)]
    public void WriteConsoleLine (string _line)
    {
        currentLines++;
        consoleText = GameObject.Find ("ConsoleText").GetComponentInChildren<Text> ();
        scrollBar = GameObject.Find ("ScrollRect").GetComponent<ScrollRect> ();
        if (currentLines == maxLineNumber)
        {
            consoleText.text = defaultString;
            scrollBar.verticalNormalizedPosition = 0.856f;
            currentLines = 0;
        }
        consoleText.text += "<|" + _line + "|>\n";
        consoleText.text += "----------------------------------------------------------------------------\n";
        scrollBar.verticalNormalizedPosition -= 0.0315f;    //  Scroll with Console Output (NOTE: Not perfect!)

    }

    /// <summary>
    /// Not in use yet
    /// </summary>
    public void WorldManagement ()
    {

    }

    /// <summary>
    /// Loads a scene
    /// </summary>
    /// <param name="_sceneName"></param>
    public void LoadScene (string _sceneName)
    {
        SceneManager.LoadScene (_sceneName);
    }

    /// <summary>
    /// The procedure to ensure launch trough Launcher.
    /// </summary>
    public void OnLaunch ()
    {
        if (!openTroughLauncher)
        {
            char[] key = new char[] { 'O', 'T', 'L', ':' };
            bool otl = Config.LoadSettings (key);

            key = new char[] { 'C', 'O', 'L', ':' };
            bool col = Config.LoadSettings (key);

            if (otl)
            {
                
                Config.SaveSetting ("OTL", false.ToString (), false);
                Config.SaveSetting ("COL", col.ToString (), true);
                openTroughLauncher = true;
            }
            else
            {
                Debug.LogError ("OTL Error");
                //Application.Quit ();
                Debug.LogError ("OTL = " + openTroughLauncher.ToString ());
            }
        }
    }
    #endregion
}
