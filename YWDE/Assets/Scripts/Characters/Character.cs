using System.Collections;

using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Represents a Playble Character
/// </summary>
[CreateAssetMenu (fileName = "NewCharacter", menuName = "Character/Playable")]
public class Character : ScriptableObject
{
    #region Variables

    /// <summary>
    /// Represents the name of the Character
    /// </summary>
    [Header ("Info")]
    [Tooltip ("The Name of this Object")]
    new public string name;
    private bool isGod;

    /// <summary>
    /// The force applied to the Rigidbody when moving
    /// </summary>
    [Header ("Movement")]
    [Tooltip ("The amount of force applied to the Rigidbody when moving")]
    public float moveforce;
    private float internalMoveForce;   //  The internal force variable used to calculate the Current force Applied
    /// <summary>
    /// The amount 'MoveForce' is multiplied with when sprinting [1, 10]
    /// </summary>
    [Tooltip ("The amount of force the speed is multiplied with when sprinting [1, 10]")]
    [Range (1, 10)]
    public float sprintMultiplier;
    /// <summary>
    /// Should be set to true when the 'Sprintbutton' is pressed and false when released
    /// </summary>
    [Tooltip ("True if the sprint button is pressed")]
    public bool sprintButtonPressed;

    /// <summary>
    /// The velocity of the Rigidbody when initiating a Jump [0, 10]
    /// </summary>
    [Header ("Jumping")]
    [Tooltip ("The velocity of the Rigidbody while Jumping")]
    [Range (0, 10)]
    public float jumpVelocity;
    /// <summary>
    /// Represents the Gravitional pull when the Rigidbody is falling
    /// </summary>
    [Tooltip ("The Amount of force applied to the Rigidbody when for falling")]
    [Range (1, 10)]
    public float fallMultiplier;
    /// <summary>
    /// Represents the Gravitational pull in low altitude
    /// </summary>
    [Range (0, 10)]
    public float lowJumpMultiplier;
    /// <summary>
    /// Represents the Maximum amount of time the Rigidbody can stay in the air, when jumping, before falling (In seconds)
    /// </summary>
    [Tooltip ("The Maximum time the Rigidbody can be in the air after a jump (In seconds)")]
    public float maxAirTime;
    private float airTime; // The Current Amount of time the Rigidbody is in the Air.
    /// <summary>
    /// Represents the amount of time between each jump (In seconds)
    /// </summary>
    [Tooltip ("The amount time before next Jump is available (In Seconds)")]
    [Range(0, 10)]
    public float jumpInterval;
    private float internalJumpInterval;    //  Used for Calculting the time passed since jump
    /// <summary>
    /// Should be set to true if the 'Jumpbutton' is pressed and false when released 
    /// </summary>
    [Tooltip ("True if the JumpButton is pressed")]
    public bool jumpButtonPressed;
    [Tooltip ("True if the Rigidbody is ready to Jump")]
    [SerializeField]
    private bool readyToJump = false; //    True if the Rigidbody is ready to jump
    /// <summary>
    /// Should be set to true when the Object is touching the ground and false if not
    /// </summary>
    [Tooltip ("True if the GameObject is touching the ground")]
    public bool isGrounded = false;

    /// <summary>
    /// The maximum amount of health this Character has
    /// </summary>
    [Header ("Attributes")]
    [Tooltip ("The Maximum amount of Health this character has")]
    public float maxHealth;
    /// <summary>
    /// The amount of health the Character currently has (Should be set to match 'MaxHealth' at the first frame this object is activated)
    /// </summary>
    [Tooltip ("The amount of health the Character currently has")]
    public float currentHealth;
    /// <summary>
    /// The amount of stamina the Character hos
    /// </summary>
    [Space(10)]
    [Tooltip ("The Stamina of this Object")]
    public float maxStamina;
    /// <summary>
    /// The Current amout of stamina the Character has
    /// </summary>
    [Tooltip ("The current amount of stamina the character has")]
    public float currentStamina;
    /// <summary>
    /// The fast the stmaina will regenerate (In seconds) [0, 10]
    /// </summary>
    [Tooltip ("The fast the stmaina will regenerate (In seconds) [0, 10]")]
    [Range (0, 10)]
    public float staminaRegenTime;
    private float internalStaminaRegenTime;    //  Used for calculating the regeneration interval
    /// <summary>
    /// The amount of stamina to gain after each 'tick'
    /// </summary>
    [Tooltip ("The amount of stamina to gain after each 'tick'")]
    [Range (0, 10)]
    public float staminaGainAmount;
    /// <summary>
    /// How fast the stamina will deplete (In seconds) [0, 10]
    /// </summary>
    [Tooltip ("How fast stamina will deplete (In seconds) [0, 10]")]
    [Range (0, 10)]
    public float staminaDepletionTime;
    private float internalStaminaDepletionTime;    //  Used for calculating the depletion interval
    /// <summary>
    /// The amount of stamina to subtract after each 'tick'
    /// </summary>
    [Tooltip ("The amount of stamina to subtract after each 'tick'")]
    [Range (0, 10)]
    public float staminaSubtractionAmount;
    [Space (10)]
    /// <summary>
    /// The amount of Damage this Character can inflict in a Basic damage
    /// </summary>
    [Tooltip ("The Damage of this Object")]
    public float damage;
    /// <summary>
    /// The stat multiplier for health
    /// </summary>
    [Header ("Multipliers")]
    [Range (1, 10)]
    public float healthMultiplier;
    private float internalHealthMultiplier;    //  Stores the Current Multiplier Value for Health
    /// <summary>
    /// The stat multiplier for stamina
    /// </summary>
    [Range (1, 10)]
    public float staminaMultiplier;
    private float internalStaminaMultiplier;   //  Stores the Current Multiplier Value for Stamina
    /// <summary>
    /// The stat multiplier for damage
    /// </summary>
    [Range (1, 10)]
    public float damageMultiplier;

    /// <summary>
    /// The time between each attack (In seconds)
    /// </summary>
    [Header ("Attack")]
    [Tooltip ("The time between each attack (In seconds)")]
    public float attackInterval;
    /// <summary>
    /// True if the Character is attacking (Should be set to false the first frame this Object is activated)
    /// </summary>
    [Tooltip ("True if the Character is attacking (Should be set to false the first frame this Object is activated)")]
    public bool attacking;

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
        Debug.Log ("Playble Character created: \n<<" + this.name +  ">>\nHealth: " + this.maxHealth + ",\nStamina: " + this.maxStamina + ",\nDamage: " + this.damage);
    }
    #endregion

    #region Custom Methods 

    /// <summary>
    /// Set stats whenever a Multiplier changes
    /// </summary>
    public void SetStats ()
    {
        if (healthMultiplier != internalHealthMultiplier)
        {
            internalHealthMultiplier = healthMultiplier;
            maxHealth = maxHealth * healthMultiplier;
            currentHealth = maxHealth;
        }
        if (staminaMultiplier != internalStaminaMultiplier)
        {
            internalStaminaMultiplier = staminaMultiplier;
            maxStamina = maxStamina * internalStaminaMultiplier;
            currentStamina = maxStamina;
        }
        if (true)
        {

        }
    }

    /// <summary>
    /// Control of the Object trough Keyboard
    /// </summary>
    /// <param name="_rigid">Rigidbody of the Object</param>
    public void Movement (ref Rigidbody _rigid)
    {
        float xAxis = Input.GetAxis ("Horizontal"); //  Getting the Input value of the Horizontal Axis (Between '-1' and '1', with '0' being no movement)

        Vector3 movement = new Vector3 (xAxis * internalMoveForce, 0, 0);

        _rigid.AddForce (movement * Time.deltaTime, ForceMode.Impulse);  //  Applying force to the Rigidbody
    }

    /// <summary>
    /// Jumping Mechanic
    /// </summary>
    /// <param name="_rigid">Rigidbody of the Object</param>
    public void Jumping (ref Rigidbody _rigid)
    {

        #region Airtime
        if (isGrounded) //  If the Rigidbody is touching the ground
        {
            airTime = 0;

            internalJumpInterval -= Time.deltaTime;

            //  Preventing Bunny Jumps
            if (internalJumpInterval <= 0)
            {
                readyToJump = true;
            }
        }
        else // If the Rigidbody is in the air
        {
            airTime += Time.deltaTime;
        }
        #endregion

        #region Jumping
        Vector3 jumpVector = new Vector3 (_rigid.velocity.x, jumpVelocity, _rigid.velocity.z);

        if (jumpButtonPressed && airTime <= maxAirTime && readyToJump)
        {
            internalJumpInterval = jumpInterval;

            _rigid.velocity = jumpVector;
        }

        //  Preventing Double Jump
        if (!jumpButtonPressed)
        {
            readyToJump = false;
        }
        #endregion

        #region Falling
        if (_rigid.velocity.y < 0 && !isGod)
        {
            readyToJump = false;
            _rigid.velocity += jumpVector *  Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rigid.velocity.y < 0 && !Input.GetButton("Jump") && !isGod)
        {
            readyToJump = false;
            _rigid.velocity += jumpVector * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        /*if (God.Instance.IsDevelopmentBuild)
        {
            Debug.Log("Velocity of Rigidbody when falling: " + Rigid.velocity);
        }*/
        #endregion
    }

    /// <summary>
    /// Sprinting Mechanic
    /// </summary>
    /// <param name="_rigid">The Rigidbody of the Object</param>
    public void Sprinting (ref Rigidbody _rigid)
    {
        if ((sprintButtonPressed && _rigid.velocity.x >= .1) || (sprintButtonPressed &&_rigid.velocity.x <= -.1))
        {
            #region Stamina Depletion timer
            internalStaminaDepletionTime -= Time.deltaTime;

            if (internalStaminaDepletionTime < 0 && currentStamina > 0)
            {
                currentStamina -= staminaSubtractionAmount;
                internalStaminaDepletionTime = staminaDepletionTime;
            }
            #endregion

            if (currentStamina > 0)
            {
                internalMoveForce = moveforce * sprintMultiplier;
                //Debug.Log ("<<<color=green>" + name + "</color>>> initiated sprintmode. [Moveforce = [<i>" + _MoveForce + "</i>]]");
                //God.Instance.WriteConsoleLine ("<<<color=green>" + name + "</color>>> initiated sprintmode. [Moveforce = [<i>" + _MoveForce + "</i>]]");
            }
            else
            {
                internalMoveForce = moveforce;
                //Debug.Log ("<<<color=green>" + name + "</color>>> ran out of stamina. [Moveforce = [<i>" + _MoveForce + "</i>]]");
                //God.Instance.WriteConsoleLine ("<<<color=green>" + name + "</color>>> ran out of stamina. [Moveforce = [<i>" + _MoveForce + "</i>]]");
            }
        }
        else
        {
            #region Stamina Regeneration Timer
            internalStaminaRegenTime -= Time.deltaTime;

            if (internalStaminaRegenTime < 0 && currentStamina < maxStamina)
            {
                currentStamina += staminaGainAmount;
                internalStaminaRegenTime = staminaRegenTime;
            }
            #endregion

            internalMoveForce = moveforce;
        }
        
    }

    /// <summary>
    /// Basic attack Sequence
    /// </summary>
    /// <param name="_object">The object to inflict damage onto</param>
    /// <returns></returns>
    public IEnumerator Attack (GameObject _object)
    {
        attacking = true;
        God.Instance.DealDamage (damage * damageMultiplier, _object.gameObject);
        yield return new WaitForSeconds (attackInterval);
        attacking = false;
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

    /// <summary>
    /// Use this to Initate God mode [Status: <Mode>_"On"|"Off"]
    /// </summary>
    /// <param name="_rigid">The Rigidbody of this Character</param>
    /// <param name="_status">Wether God mode should be On or off</param>
    public void SetMode (Rigidbody _rigid, string _status)
    {
        #region GodMode
        if (_status == "God_On")
        {
            isGod = true;
            healthMultiplier = 10000;
            staminaMultiplier = 10000;
            damageMultiplier = 10000;

            _rigid.useGravity = false;   // Disable Gravity
            float Vertical = Input.GetAxis ("Vertical");    //  Get Y-Axis Input

            Vector3 moveVector = new Vector3 (_rigid.velocity.x, Vertical * internalMoveForce, 0);

            _rigid.AddForce (moveVector * Time.deltaTime, ForceMode.Impulse);
        }
        else if (_status == "God_Off")
        {
            isGod = false;
            maxHealth = maxHealth / healthMultiplier;
            maxStamina = maxStamina / staminaMultiplier;

            healthMultiplier = 1;
            staminaMultiplier = 1;
            damageMultiplier = 1;

            _rigid.useGravity = true;    //  Enable Gravity
        }
        else
        {
            Debug.LogError ("<<<color=green>" + this.name + "</color>>>: SetMode does not contain an action for: " + _status);  //  Troubleshoot
        }

        #endregion
    }
    #endregion

}
