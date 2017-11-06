using System.Collections;

using System.Collections.Generic;

using UnityEngine;


/// <summary>
/// Represents an NPC Character
/// </summary>
[CreateAssetMenu (fileName = "NewNPC", menuName = "Character/NPC")]
public class NPC : ScriptableObject{


    #region Variables

    /// <summary>
    /// Represents the name of the NPC
    /// </summary>
    [Header ("Info")]
    [Tooltip ("The name of NPC")]
    new public string name;

    /// <summary>
    /// Should be set to true if the Object should turn Instantly when moving
    /// </summary>
    [Header ("Movement")]
    [Tooltip ("Should be set to true if the object should turn instantly while moving")]
    public bool InstantTurn = false;
    /// <summary>
    /// The amount of force applied to the rigid body when moving
    /// </summary>
    [Tooltip ("The amount of force applied to the Rigidbody when moving")]
    public float MoveForce;
    private float _MoveForce;   //  Used to calculate the current force applied to the Rigidbody
    /// <summary>
    /// The amount 'MoveForce' is multiplied with when sprinting [1, 10]
    /// </summary>
    [Tooltip ("The amount of force the speed is multiplied with when sprinting [1, 10]")]
    [Range(1, 10)]
    public float SprintMultiplier;
    /// <summary>
    /// True if the NPC is Sprinting
    /// </summary>
    [Tooltip ("True if the NPC is sprinting")]
    public bool IsSprinting = false;
    /// <summary>
    /// True if the Object is waiting to move (Should be set to false the first frame this Object is activated)
    /// </summary>
    [Tooltip ("If false this NPC will move around in a random pattern")]
    public bool WaitFlag;

    /// <summary>
    /// Should be set to true if the Detected Object is in Range
    /// </summary>
    [Header ("Attack")]
    public bool InRange;
    /// <summary>
    /// True if the NPC is attacking (Should be set to false the first frame this Object is Activated)
    /// </summary>
    public bool Attacking;

    /// <summary>
    /// The maximum amount of health this NPC has
    /// </summary>
    [Header ("Attributes")]
    [Tooltip ("The maximum amount of health the NPC has")]
    public float MaxHealth;
    /// <summary>
    /// The amount of health this NPC currently has
    /// </summary>
    [Tooltip ("The current amount of health the NPC has")]
    public float CurrentHealth;
    /// <summary>
    /// The amount of Stamina this NPC has
    /// </summary>
    [Tooltip ("The amount of stamina the NPC has")]
    public float Stamina;
    /// <summary>
    /// The amount of Damage this NPC Inflicts when attacking
    /// </summary>
    [Tooltip ("The amount of damage the NPC does")]
    public float Damage;

    /// <summary>
    /// Wether this NPC is friendly or not
    /// </summary>
    [Header ("Behavior")]
    [Tooltip ("Should be set to true if this NPC is is friendly (False = Aggresive)")]
    public bool Friendly = false;

    /// <summary>
    /// The color of the material attached to (NOTE: Do not use the Standard material)
    /// </summary>
    [Header ("Material")]
    [Tooltip ("The color of the material attached to this Object")]
    public Color MaterialColor;
    /// <summary>
    /// The Color the Material should transist to
    /// </summary>
    [Tooltip ("The Color to transition to")]
    public Color ChangeToColor;
    /// <summary>
    /// 0 = 'MaterialColor', 1 = 'ChangeToColor' [0, 1]
    /// </summary>
    public float SetTransitionValue
    {
        get
        {
            return ColorTransitionValue;
        }

        set
        {
            if (value < 0)
            {
                ColorTransitionValue = 0;
            }
            else if (value > 1)
            {
                ColorTransitionValue = 1;
            }
            else
            {
                ColorTransitionValue = value;
            }

        }
    }
    [Tooltip ("Used to transition from one color to another. 0 = 'MaterialColor', 1 = 'ChangeToColor' [0, 1]")]
    [SerializeField]
    [Range (0, 1)]
    private float ColorTransitionValue;

    #endregion

    #region BuildIn Methods

    public void OnEnable ()
    {
        Debug.Log ("NPC Character created: \n<<" + this.name + ">>\nHealth: " + this.CurrentHealth + ",\nStamina: " + this.Stamina + ",\nDamage: " + this.Damage);
    }

    public void OnDestroy ()
    {
        Debug.Log ("Death of NPC: <<" + this.name +">>");
    }

    #endregion

    #region Custom Methods
    /// <summary>
    /// Movement of the Rigidbody
    /// </summary>
    /// <param name="Rigid">Rigidbody of the Object</param>
    public void Movement (ref Rigidbody Rigid)
    {
        //  Changes Direction if no 'Ground' is found
        if (InstantTurn)
        {
            MoveForce *= -1;
            _MoveForce = MoveForce;
        }
        Vector3 MoveVector = Vector3.left * _MoveForce;

        Rigid.velocity = MoveVector * Time.deltaTime;
    }


    /// <summary>
    /// Creates a Rigidbody at runtime
    /// </summary>
    /// <param name="Object">The Object the Rigidbody should be attached to</param>
    /// <returns></returns>
    public Rigidbody CreateRigidBody (GameObject Object)
    {
        Debug.Log ("<<" + this.name + ">>" + " Does not have a Rigidbody. Component created Automaticly");
        Rigidbody Rigid = Object.AddComponent<Rigidbody> ();
        Rigid.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        return Rigid;
    }

    /// <summary>
    /// Will make the NPC change Direction -> Wait([1, 5]) -> Move -> Wait ([1, 5]) in a loop
    /// </summary>
    /// <returns></returns>
    public IEnumerator RandomMove ()
    {
        WaitFlag = true;
        _MoveForce = 0;
        yield return new WaitForSeconds (Random.Range (1, 5));
        #region Change Direction
        float Num = Random.Range (0, 1f);
        if (Num < 0.5)
        {
            MoveForce *= -1;
        }
        #endregion
        _MoveForce = MoveForce;
        yield return new WaitForSeconds (Random.Range (1, 5));
        
        WaitFlag = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="WaitTime">The amount of time the NPC should wait before attacking again</param>
    /// <param name="Object">The Object this instance is attached to</param>
    /// <returns></returns>
    public IEnumerator Attack (float WaitTime, GameObject Object)
    {
        Attacking = true;
        while (InRange)
        {
            Debug.Log ("<<<color=red>" + this.name + "</color>>>" + " is Attacking <<<color=green>" + Object.name + "</color>>>");
            God.Instance.WriteConsoleLine ("<<<color=red>" + this.name + "</color>>>" + " is Attacking <<<color=green>" + Object.name + "</color>>>");
            God.Instance.DealDamage (Damage, Object);
            yield return new WaitForSeconds (WaitTime);
        }
        Attacking = false;
    }

    /// <summary>
    /// When the NPC dies
    /// </summary>
    /// <param name="Object"></param>
    public void Die (GameObject Object)
    {
        if (CurrentHealth <= 0)
        {
            Destroy (Object);
            CurrentHealth = MaxHealth;
            Debug.Log (Object.name.Remove (Object.name.IndexOf ("("), Object.name.Length - (Object.name.IndexOf ("("))));
            God.Instance.Respawn (Object.name.Remove(Object.name.IndexOf("("), Object.name.Length - (Object.name.IndexOf ("("))));
        }

    }

    /// <summary>
    /// Transitions from one color to another
    /// </summary>
    /// <param name="Object">The object this Component is attached to</param>
    public void ChangeColor (GameObject Object)
    {
        Renderer Ren = Object.GetComponent<Renderer> ();
        Material Mat = Ren.sharedMaterial;
        Mat.color = Color.Lerp (MaterialColor, ChangeToColor, ColorTransitionValue);
    }
    #endregion
}





