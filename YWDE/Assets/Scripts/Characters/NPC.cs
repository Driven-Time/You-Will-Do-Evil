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
    public bool instantTurn = false;
    /// <summary>
    /// The amount of force applied to the rigid body when moving
    /// </summary>
    [Tooltip ("The amount of force applied to the Rigidbody when moving")]
    public float moveForce;
    private float internalMoveForce;   //  Used to calculate the current force applied to the Rigidbody
    /// <summary>
    /// The amount 'MoveForce' is multiplied with when sprinting [1, 10]
    /// </summary>
    [Tooltip ("The amount of force the speed is multiplied with when sprinting [1, 10]")]
    [Range(1, 10)]
    public float sprintMultiplier;
    /// <summary>
    /// True if the NPC is Sprinting
    /// </summary>
    [Tooltip ("True if the NPC is sprinting")]
    public bool isSprinting = false;
    /// <summary>
    /// True if the Object is waiting to move (Should be set to false the first frame this Object is activated)
    /// </summary>
    [Tooltip ("If false this NPC will move around in a random pattern")]
    public bool waitFlag;

    /// <summary>
    /// Should be set to true if the Detected Object is in Range
    /// </summary>
    [Header ("Attack")]
    public bool inRange;
    /// <summary>
    /// True if the NPC is attacking (Should be set to false the first frame this Object is Activated)
    /// </summary>
    public bool attacking;

    /// <summary>
    /// The maximum amount of health this NPC has
    /// </summary>
    [Header ("Attributes")]
    [Tooltip ("The maximum amount of health the NPC has")]
    public float maxHealth;
    /// <summary>
    /// The amount of health this NPC currently has
    /// </summary>
    [Tooltip ("The current amount of health the NPC has")]
    public float currentHealth;
    /// <summary>
    /// The amount of Stamina this NPC has
    /// </summary>
    [Tooltip ("The amount of stamina the NPC has")]
    public float stamina;
    /// <summary>
    /// The amount of Damage this NPC Inflicts when attacking
    /// </summary>
    [Tooltip ("The amount of damage the NPC does")]
    public float damage;

    /// <summary>
    /// Wether this NPC is friendly or not
    /// </summary>
    [Header ("Behavior")]
    [Tooltip ("Should be set to true if this NPC is is friendly (False = Aggresive)")]
    public bool friendly = false;

    /// <summary>
    /// The color of the material attached to (NOTE: Do not use the Standard material)
    /// </summary>
    [Header ("Material")]
    [Tooltip ("The color of the material attached to this Object")]
    public Color materialColor;
    /// <summary>
    /// The Color the Material should transist to
    /// </summary>
    [Tooltip ("The Color to transition to")]
    public Color changeToColor;
    /// <summary>
    /// 0 = 'MaterialColor', 1 = 'ChangeToColor' [0, 1]
    /// </summary>
    public float SetTransitionValue
    {
        get
        {
            return colorTransitionValue;
        }

        set
        {
            if (value < 0)
            {
                colorTransitionValue = 0;
            }
            else if (value > 1)
            {
                colorTransitionValue = 1;
            }
            else
            {
                colorTransitionValue = value;
            }

        }
    }
    [Tooltip ("Used to transition from one color to another. 0 = 'MaterialColor', 1 = 'ChangeToColor' [0, 1]")]
    [SerializeField]
    [Range (0, 1)]
    private float colorTransitionValue;

    #endregion

    #region BuildIn Methods

    public void OnEnable ()
    {
        Debug.Log ("NPC Character created: \n<<" + this.name + ">>\nHealth: " + this.currentHealth + ",\nStamina: " + this.stamina + ",\nDamage: " + this.damage);
    }

    public void OnDestroy ()
    {
        Debug.LogError ("Death of NPC: <<" + this.name +">>");
    }

    #endregion

    #region Custom Methods
    /// <summary>
    /// Movement of the Rigidbody
    /// </summary>
    /// <param name="_rigid">Rigidbody of the Object</param>
    public void Movement (ref Rigidbody _rigid)
    {
        //  Changes Direction if no 'Ground' is found
        if (instantTurn)
        {
            moveForce *= -1;
            internalMoveForce = moveForce;
        }
        Vector3 moveVector = Vector3.left * internalMoveForce;

        _rigid.velocity = moveVector * Time.deltaTime;
    }

    /// <summary>
    /// Creates a Rigidbody at runtime
    /// </summary>
    /// <param name="_object">The Object the Rigidbody should be attached to</param>
    /// <returns></returns>
    public Rigidbody CreateRigidBody (GameObject _object)
    {
        Debug.Log ("<<" + this.name + ">>" + " Does not have a Rigidbody. Component created Automaticly");
        Rigidbody rigid = _object.AddComponent<Rigidbody> ();
        rigid.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        return rigid;
    }

    /// <summary>
    /// Will make the NPC change Direction -> Wait([1, 5]) -> Move -> Wait ([1, 5]) in a loop
    /// </summary>
    /// <returns></returns>
    public IEnumerator RandomMove ()
    {
        waitFlag = true;
        internalMoveForce = 0;
        yield return new WaitForSeconds (Random.Range (1, 5));
        #region Change Direction
        float Num = Random.Range (0, 1f);
        if (Num < 0.5)
        {
            moveForce *= -1;
        }
        #endregion
        internalMoveForce = moveForce;
        yield return new WaitForSeconds (Random.Range (1, 5));
        
        waitFlag = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_waitTime">The amount of time the NPC should wait before attacking again</param>
    /// <param name="_object">The Object this instance is attached to</param>
    /// <returns></returns>
    public IEnumerator Attack (float _waitTime, GameObject _object)
    {
        attacking = true;
        while (inRange)
        {
            //Debug.Log ("<<<color=red>" + this.name + "</color>>>" + " is Attacking <<<color=green>" + Object.name + "</color>>>");
            //God.Instance.WriteConsoleLine ("<<<color=red>" + this.name + "</color>>>" + " is Attacking <<<color=green>" + Object.name + "</color>>>");
            God.Instance.DealDamage (damage, _object);
            yield return new WaitForSeconds (_waitTime);
        }
        attacking = false;
    }

    /// <summary>
    /// When the NPC dies
    /// </summary>
    /// <param name="_object"></param>
    public void Die (GameObject _object)
    {
        if (currentHealth <= 0 || _object.transform.position.y < -5)
        {
            Destroy (_object);
            currentHealth = maxHealth;
            Debug.LogError (_object.name.Remove (_object.name.IndexOf ("("), _object.name.Length - (_object.name.IndexOf ("("))));
            God.Instance.Respawn (_object.name.Remove(_object.name.IndexOf("("), _object.name.Length - (_object.name.IndexOf ("("))));
        }

    }

    /// <summary>
    /// Transitions from one color to another
    /// </summary>
    /// <param name="_object">The object this Component is attached to</param>
    public void ChangeColor (GameObject _object)
    {
        Renderer ren = _object.GetComponent<Renderer> ();
        Material mat = ren.sharedMaterial;
        mat.color = Color.Lerp (materialColor, changeToColor, colorTransitionValue);
    }
    #endregion
}





