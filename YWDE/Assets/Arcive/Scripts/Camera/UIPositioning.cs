using System.Collections;

using System.Collections.Generic;

using UnityEngine.UI;

using UnityEngine;

using UI;


[ExecuteInEditMode]
public class UIPositioning : MonoBehaviour
{


    #region Variables
    [SerializeField]
    private float ScreenWidth;
    [SerializeField]
    private float ScreenHeight;

    #region Prefabs
    public RectTransform HealthBarPrefab;
    UIElement HealthBar = new UIElement ();

    public RectTransform StaminaBarPrefab;
    UIElement StaminaBar = new UIElement ();

    public RectTransform BloodLustbarPrefab;
    UIElement BloodLustBar = new UIElement ();
    #endregion

    #region Positions

    [Header ("HealtBar")]
    public float HealthBarPositionX;
    public float HealthBarPositionY;

    public float HealthBarWidth;
    public float HealthBarHeight;

    [Header ("Stamina Bar")]
    public float StaminaBarPositionX;
    public float StaminaBarPositionY;

    public float StaminaBarWidth;
    public float StaminaBarHeight;

    [Header ("BloodLust Bar")]
    public float BloodLustBarPositionX;
    public float BloodLustBarPositionY;

    public float BloodLustBarWidth;
    public float BloodLustBarHeight;
    #endregion

    #endregion

    #region BuildIn Methods

    void Start ()
	{

	}

	void Update ()
	{

        HealthBar.Element = HealthBarPrefab;
        StaminaBar.Element = StaminaBarPrefab;
        BloodLustBar.Element = BloodLustbarPrefab;

        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;

        #region Healthbar
        HealthBar.ScreenWidth = ScreenWidth;
        HealthBar.ScreenHeight = ScreenHeight;

        HealthBar.ElementWidth = HealthBarWidth;
        HealthBar.ElementHeight = HealthBarHeight;

        HealthBar.PositionX = HealthBarPositionX;
        HealthBar.PositionY = HealthBarPositionY;

        HealthBar.AutoScale ();
        #endregion

        #region StaminaBar
        StaminaBar.ScreenWidth = ScreenWidth;
        StaminaBar.ScreenHeight = ScreenHeight;

        StaminaBar.ElementWidth = StaminaBarWidth;
        StaminaBar.ElementHeight = StaminaBarHeight;

        StaminaBar.PositionX = StaminaBarPositionX;
        StaminaBar.PositionY = StaminaBarPositionY;
        StaminaBar.OffSetY = (HealthBar.Height);

        StaminaBar.AutoScale ();

        #endregion

        #region BloodLustBar
        BloodLustBar.ScreenWidth = ScreenWidth;
        BloodLustBar.ScreenHeight = ScreenHeight;

        BloodLustBar.ElementWidth = BloodLustBarWidth;
        BloodLustBar.ElementHeight = BloodLustBarHeight;

        BloodLustBar.PositionX = BloodLustBarPositionX;
        BloodLustBar.PositionY = BloodLustBarPositionY;


        BloodLustBar.AutoScale ();
        #endregion



    }

    #endregion

    #region Custom Methods 
    #endregion
}
