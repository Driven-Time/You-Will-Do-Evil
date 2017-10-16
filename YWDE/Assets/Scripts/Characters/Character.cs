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

    /// <summary>
    /// The force applied to the Rigidbody when moving
    /// </summary>
    [Header ("Movement")]
    [Tooltip ("The amount of force applied to the Rigidbody when moving")]
    public float Moveforce;
    private float _MoveForce;   //  The internal force variable used to calculate the Current force Applied
    /// <summary>
    /// The amount 'MoveForce' is multiplied with when sprinting [1, 10]
    /// </summary>
    [Tooltip ("The amount of force the speed is multiplied with when sprinting [1, 10]")]
    [Range (1, 10)]
    public float SprintMultiplier;
    /// <summary>
    /// Should be set to true when the 'Sprintbutton' is pressed and false when released
    /// </summary>
    [Tooltip ("True if the sprint button is pressed")]
    public bool SprintButtonPressed;

    /// <summary>
    /// The velocity of the Rigidbody when initiating a Jump [0, 10]
    /// </summary>
    [Header ("Jumping")]
    [Tooltip ("The velocity of the Rigidbody while Jumping")]
    [Range (0, 10)]
    public float JumpVelocity;
    /// <summary>
    /// Represents the Gravitional pull when the Rigidbody is falling
    /// </summary>
    [Tooltip ("The Amount of force applied to the Rigidbody when for falling")]
    [Range (1, 10)]
    public float FallMultiplier;
    /// <summary>
    /// Represents the Gravitational pull in low altitude
    /// </summary>
    [Range (0, 10)]
    public float LowJumpMultiplier;
    /// <summary>
    /// Represents the Maximum amount of time the Rigidbody can stay in the air, when jumping, before falling (In seconds)
    /// </summary>
    [Tooltip ("The Maximum time the Rigidbody can be in the air after a jump (In seconds)")]
    public float MaxAirTime;
    private float AirTime; // The Current Amount of time the Rigidbody is in the Air.
    /// <summary>
    /// Represents the amount of time between each jump (In seconds)
    /// </summary>
    [Tooltip ("The amount time before next Jump is available (In Seconds)")]
    [Range(0, 10)]
    public float JumpInterval;
    private float _JumpInterval;    //  Used for Calculting the time passed since jump
    /// <summary>
    /// Should be set to true if the 'Jumpbutton' is pressed and false when released 
    /// </summary>
    [Tooltip ("True if the JumpButton is pressed")]
    public bool JumpButtonPressed;
    [Tooltip ("True if the Rigidbody is ready to Jump")]
    [SerializeField]
    private bool ReadyToJump = false; //    True if the Rigidbody is ready to jump
    /// <summary>
    /// Should be set to true when the Object is touching the ground and false if not
    /// </summary>
    [Tooltip ("True if the GameObject is touching the ground")]
    public bool IsGrounded = false;

    /// <summary>
    /// The maximum amount of health this Character has
    /// </summary>
    [Header ("Attributes")]
    [Tooltip ("The Maximum amount of Health this character has")]
    public float MaxHealth;
    /// <summary>
    /// The amount of health the Character currently has (Should be set to match 'MaxHealth' at the first frame this object is activated)
    /// </summary>
    [Tooltip ("The amount of health the Character currently has")]
    public float CurrentHealth;
    /// <summary>
    /// The amount of stamina the Character hos
    /// </summary>
    [Space(10)]
    [Tooltip ("The Stamina of this Object")]
    public float MaxStamina;
    /// <summary>
    /// The Current amout of stamina the Character has
    /// </summary>
    [Tooltip ("The current amount of stamina the character has")]
    public float CurrentStamina;
    /// <summary>
    /// The fast the stmaina will regenerate (In seconds) [0, 10]
    /// </summary>
    [Tooltip ("The fast the stmaina will regenerate (In seconds) [0, 10]")]
    [Range (0, 10)]
    public float StaminaRegenTime;
    private float _StaminaRegenTime;    //  Used for calculating the regeneration interval
    /// <summary>
    /// The amount of stamina to gain after each 'tick'
    /// </summary>
    [Tooltip ("The amount of stamina to gain after each 'tick'")]
    [Range (0, 10)]
    public float StaminaGainAmount;
    /// <summary>
    /// How fast the stamina will deplete (In seconds) [0, 10]
    /// </summary>
    [Tooltip ("How fast stamina will deplete (In seconds) [0, 10]")]
    [Range (0, 10)]
    public float StaminaDepletionTime;
    private float _StaminaDepletionTime;    //  Used for calculating the depletion interval
    /// <summary>
    /// The amount of stamina to subtract after each 'tick'
    /// </summary>
    [Tooltip ("The amount of stamina to subtract after each 'tick'")]
    [Range (0, 10)]
    public float StaminaSubtractionAmount;
    [Space (10)]
    /// <summary>
    /// The amount of Damage this Character can inflict in a Basic damage
    /// </summary>
    [Tooltip ("The Damage of this Object")]
    public float Damage;
    /// <summary>
    /// The stat multiplier for health
    /// </summary>
    [Header ("Multipliers")]
    [Range (1, 10)]
    public float HealthMultiplier;
    private float _HealthMultiplier;    //  Stores the Current Multiplier Value for Health
    /// <summary>
    /// The stat multiplier for stamina
    /// </summary>
    [Range (1, 10)]
    public float StaminaMultiplier;
    private float _StaminaMultiplier;   //  Stores the Current Multiplier Value for Stamina
    /// <summary>
    /// The stat multiplier for damage
    /// </summary>
    [Range (1, 10)]
    public float DamageMultiplier;

    /// <summary>
    /// The time between each attack (In seconds)
    /// </summary>
    [Header ("Attack")]
    [Tooltip ("The time between each attack (In seconds)")]
    public float AttackInterval;
    /// <summary>
    /// True if the Character is attacking (Should be set to false the first frame this Object is activated)
    /// </summary>
    [Tooltip ("True if the Character is attacking (Should be set to false the first frame this Object is activated)")]
    public bool Attacking;

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
        Debug.Log ("Playble Character created: \n<<" + this.name +  ">>\nHealth: " + this.MaxHealth + ",\nStamina: " + this.MaxStamina + ",\nDamage: " + this.Damage);
    }
    #endregion

    #region Custom Methods 

    /// <summary>
    /// Set stats whenever a Multiplier changes
    /// </summary>
    public void SetStats ()
    {
        if (HealthMultiplier != _HealthMultiplier)
        {
            _HealthMultiplier = HealthMultiplier;
            MaxHealth = MaxHealth * HealthMultiplier;
            CurrentHealth = MaxHealth;
        }
        if (StaminaMultiplier != _StaminaMultiplier)
        {
            _StaminaMultiplier = StaminaMultiplier;
            MaxStamina = MaxStamina * _StaminaMultiplier;
            CurrentStamina = MaxStamina;
        }
        if (true)
        {

        }
    }

    /// <summary>
    /// Control of the Object trough Keyboard
    /// </summary>
    /// <param name="Rigid">Rigidbody of the Object</param>
    public void Movement (ref Rigidbody Rigid)
    {
        float Xaxis = Input.GetAxis ("Horizontal"); //  Getting the Input value of the Horizontal Axis (Between '-1' and '1', with '0' being no movement)

        Vector3 Movement = new Vector3 (Xaxis * _MoveForce, 0, 0);

        Rigid.AddForce (Movement * Time.deltaTime, ForceMode.Impulse);  //  Applying force to the Rigidbody
    }

    /// <summary>
    /// Jumping Mechanic
    /// </summary>
    /// <param name="Rigid">Rigidbody of the Object</param>
    public void Jumping (ref Rigidbody Rigid)
    {

        #region Airtime
        if (IsGrounded) //  If the Rigidbody is touching the ground
        {
            AirTime = 0;

            _JumpInterval -= Time.deltaTime;

            //  Preventing Bunny Jumps
            if (_JumpInterval <= 0)
            {
                ReadyToJump = true;
            }
        }
        else // If the Rigidbody is in the air
        {
            AirTime += Time.deltaTime;
        }
        #endregion

        #region Jumping
        Vector3 JumpVector = new Vector3 (Rigid.velocity.x, JumpVelocity, Rigid.velocity.z);

        if (JumpButtonPressed && AirTime <= MaxAirTime && ReadyToJump)
        {
            _JumpInterval = JumpInterval;

            Rigid.velocity = JumpVector;
        }

        //  Preventing Double Jump
        if (!JumpButtonPressed)
        {
            ReadyToJump = false;
        }
        #endregion

        #region Falling
        if (Rigid.velocity.y < 0)
        {
            ReadyToJump = false;
            Rigid.velocity += JumpVector *  Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
        }
        else if (Rigid.velocity.y < 0 && !Input.GetButton("Jump"))
        {
            ReadyToJump = false;
            Rigid.velocity += JumpVector * Physics.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
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
    /// <param name="Rigid">The Rigidbody of the Object</param>
    public void Sprinting (ref Rigidbody Rigid)
    {
        if (SprintButtonPressed)
        {
            #region Stamina Depletion timer
            _StaminaDepletionTime -= Time.deltaTime;

            if (_StaminaDepletionTime < 0 && CurrentStamina > 0)
            {
                CurrentStamina -= StaminaSubtractionAmount;
                _StaminaDepletionTime = StaminaDepletionTime;
            }
            #endregion

            if (CurrentStamina > 0)
            {
                _MoveForce = Moveforce * SprintMultiplier;
                Debug.Log ("<<<color=green>" + name + "</color>>> initiated sprintmode. [Moveforce = [<i>" + _MoveForce + "</i>]]");
                //God.Instance.WriteConsoleLine ("<<<color=green>" + name + "</color>>> initiated sprintmode. [Moveforce = [<i>" + _MoveForce + "</i>]]");
            }
            else
            {
                _MoveForce = Moveforce;
                Debug.Log ("<<<color=green>" + name + "</color>>> ran out of stamina. [Moveforce = [<i>" + _MoveForce + "</i>]]");
                God.Instance.WriteConsoleLine ("<<<color=green>" + name + "</color>>> ran out of stamina. [Moveforce = [<i>" + _MoveForce + "</i>]]");
            }
        }
        else
        {
            #region Stamina Regeneration Timer
            _StaminaRegenTime -= Time.deltaTime;

            if (_StaminaRegenTime < 0 && CurrentStamina < MaxStamina)
            {
                CurrentStamina += StaminaGainAmount;
                _StaminaRegenTime = StaminaRegenTime;
            }
            #endregion

            _MoveForce = Moveforce;
        }
        
    }

    /// <summary>
    /// Basic attack Sequence
    /// </summary>
    /// <param name="Object">The object to inflict damage onto</param>
    /// <returns></returns>
    public IEnumerator Attack (GameObject Object)
    {
        Attacking = true;
        God.Instance.DealDamage (Damage * DamageMultiplier, Object.gameObject);
        yield return new WaitForSeconds (AttackInterval);
        Attacking = false;
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

    /// <summary>
    /// Use this to Initate God mode [Status: <Mode>_"On"|"Off"]
    /// </summary>
    /// <param name="Rigid">The Rigidbody of this Character</param>
    /// <param name="Status">Wether God mode should be On or off</param>
    public void SetMode (Rigidbody Rigid, string Status)
    {
        #region GodMode
        if (Status == "God_On")
        {
            HealthMultiplier = 10000;
            StaminaMultiplier = 10000;
            DamageMultiplier = 10000;

            Rigid.useGravity = false;   // Disable Gravity
            float Vertical = Input.GetAxis ("Vertical");    //  Get Y-Axis Input

            Vector3 MoveVector = new Vector3 (Rigid.velocity.x, Vertical * _MoveForce, 0);

            Rigid.AddForce (MoveVector * Time.deltaTime, ForceMode.Impulse);
        }
        else if (Status == "God_Off")
        {
            MaxHealth = MaxHealth / HealthMultiplier;
            MaxStamina = MaxStamina / StaminaMultiplier;

            HealthMultiplier = 1;
            StaminaMultiplier = 1;
            DamageMultiplier = 1;

            Rigid.useGravity = true;    //  Enable Gravity
        }
        else
        {
            Debug.Log ("<<<color=green>" + this.name + "</color>>>: SetMode does not contain an action for: " + Status);  //  Troubleshoot
        }

        #endregion
    }
    #endregion

}
