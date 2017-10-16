using UnityEngine;
using UnityEngine.UI;


//[CreateAssetMenu (fileName = "Class", menuName = "Character/PlayerClass")]
public class Classes : ScriptableObject {


    #region Variables
    new public string name;
    public string Type
    {
        get { return _type; }
    }
    [SerializeField]
    private string _type;
    public bool IsDefaultType = false;

    #region Movement
    [Header ("Movement")]
    /// <summary>
    /// The Rigidbody to move
    /// </summary>
    public Rigidbody Rigid;
    /// <summary>
    /// The force applied to the Object while Moving
    /// </summary>
    public float Speed = 0;
    private float CurrentSpeed = 0;
    public float RunningSpeed = 0;

    private float JumpTime = 0; //  The time the Object is Airborn
                                /// <summary>
                                /// The Max Time the Object can be Airborn before it stops adding force
                                /// </summary>
    public float MaxJumpTime = 0;

    private bool IsGrounded = false;    //  True if the Object is touching the Ground

    /// <summary>
    /// The amount of force Applied to the object when Jumping
    /// </summary>
    public float JumpForce = 0;
    private float _JumpForce = 0;

    /// <summary>
    /// Where the LineCast Should begin
    /// </summary>
    public Transform RayStart;
    /// <summary>
    /// Where the LineCast Should stop
    /// </summary>
    public Transform RayEnd;
    #endregion

    #region Attributes
    [Header ("Attributes")]
    /// <summary>
    /// The slider Component, which handles the Visuals of the HealthBar
    /// </summary>
    public Slider HealthBar;
    private Text HealthText;    //  The text, which will be displayed on top of the Healthbar
                                /// <summary>
                                /// The Maximum amount health of the Character
                                /// </summary>
    public float MaxHealth;

    private float health;   //  This is the variable used to Calulate the amount of health the Character has left
                            /// <summary>
                            /// Characters Current Health. Ued to Calulate the amount of health the Character has left
                            /// </summary>
    public float Health
    {
        get { if (health <= 0) { health = 1; } return health; }
        set { health = value; }
    }

    /// <summary>
    /// The slider Component, which handles the visuals of the StaminaBar
    /// </summary>
    public Slider StaminaBar;
    private Text StaminaText;   //  The texh, which will be displayed on top of the Staminabar
                                /// <summary>
                                /// The Maximum amount of stamina of the Character
                                /// </summary>
    public float MaxStamina = 0;
    private float stamina;  //  This is the Variable used to Calculate the amount of Stamina the Character has left
                            /// <summary>
                            /// Characters Current Stamina. Used to Calculate the amount of Stamina the Character has left
                            /// </summary>
    public float Stamina
    {
        get { if (stamina <= 0) { stamina = 0; } if (stamina > MaxStamina) { stamina = MaxStamina; } return stamina; }
        set { stamina = value; }
    }
    #endregion

    #region Timer
    [Header ("Stamina Timer")]
    /// <summary>
    /// The amount of time, in seconds, it takes to regenrate 1 Stamina point
    /// </summary>
    public float StaminaRegenTime;
    private float _StaminaRegenTime = 0;
    /// <summary>
    /// The amount of time, in seconds, it takes to use 1 stamina point
    /// </summary>
    public float StaminaDepleatTime;
    private float _StaminaDepleatTime = 0;
    #endregion

    #endregion


    public virtual void Character ()
    {

    }

    #region Shared Methods 
    /// <summary>
    /// Health of the Character
    /// </summary>
    public void HealthSystem ()
    {
        HealthBar.minValue = 0;
        HealthBar.maxValue = MaxHealth;

        HealthText = HealthBar.GetComponentInChildren<Text> ();

        HealthText.text = (health + "/" + MaxHealth);

        HealthBar.value = health;
    }

    /// <summary>
    /// Stamina of the Character
    /// </summary>
    public void StaminaSystem ()
    {

        StaminaBar.minValue = 0;
        StaminaBar.maxValue = MaxStamina;

        StaminaText = StaminaBar.GetComponentInChildren<Text> ();

        StaminaText.text = (stamina + "/" + MaxStamina);

        StaminaBar.value = stamina;
    }

    /// <summary>
    /// A method including mechanics for Moving on the X-Axis and Z-Axis + Jumping
    /// </summary>
    public void Movement ()
    {
        float Horizontal = Input.GetAxis ("Horizontal");    //  Getting the X Axis to move on
        float Vertical = Input.GetAxis ("Vertical");    //   Getting the Y-Axis (Using the value on Z-Axis)

        Vector3 Move = new Vector3 (Horizontal, 0, Vertical);  //  Creating a Vector for movement

        Rigid.AddForce (Move * CurrentSpeed * Time.deltaTime, ForceMode.Impulse);  //  Adding force to the Rigidbody

        Jumping ();
        Running ();
    }

    private void Jumping ()
    {
        RaycastHit Target;

        /*Check for Ground*/
        if (Physics.Linecast (RayStart.position, RayEnd.position, out Target))
        {
            if (Target.collider.tag == "Ground")
            {
                IsGrounded = true;
            }
        }
        else
        {
            IsGrounded = false;
        }

        /*Applying force if key is held down*/
        if (Input.GetKey (KeyCode.Space))
        {
            if (IsGrounded)
            {
                JumpTime = 0;
            }
            else
            {
                JumpTime += Time.deltaTime;
            }

            if (JumpTime > MaxJumpTime)
            {
                _JumpForce = 0;
            }
            else
            {
                _JumpForce = JumpForce;
            }

            Vector3 Jump = new Vector3 (0, _JumpForce * Time.deltaTime, 0); //  Calculation of force

            Rigid.AddForce (Jump, ForceMode.VelocityChange);   //  Applying force
        }
    }

    private void Running ()
    {
        if (Input.GetKey (KeyCode.LeftShift) && stamina > 0 && IsGrounded)
        {
            _StaminaDepleatTime -= Time.deltaTime;
            CurrentSpeed = RunningSpeed;
            if (_StaminaDepleatTime <= 0)
            {
                _StaminaDepleatTime = StaminaDepleatTime;
                Stamina -= 1;
            }

        }
        else
        {
            CurrentSpeed = Speed;

            _StaminaRegenTime -= Time.deltaTime;
            if (_StaminaRegenTime <= 0 && Stamina != 100)
            {
                _StaminaRegenTime = StaminaRegenTime;
                Stamina += 1;
            }

        }
    }
    #endregion


}





