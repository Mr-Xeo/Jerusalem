using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static int UnitWeedSoft = 0;
    public static int UnitWeedMedium = 0;
    public static int UnitWeedHard = 0;
    public static int UnitWeedDope = 0;

    private float timerWeedSoft = 30;
    private float timerWeedMedium = 75;
    private float timerWeedHard = 150;
    private float timerWeedDope = 300;
    private int weedType;
    private bool nerfOnClick;

    [Header("Texte Unitées")]
    public TextMeshProUGUI txtUnitsSoft;
    public TextMeshProUGUI txtUnitsMedium;
    public TextMeshProUGUI txtUnitsHard;
    public TextMeshProUGUI txtUnitsDope;
    private string weedName = "null";
    public TextMeshProUGUI txtConfirmName;


    [Header("Boutons")]
    public Button btnSmoke;
    public Button btnSoft;
    public Button btnMedium;
    public Button btnHard;
    public Button btnDope;

    [Header("Sous-menus de confirmation")]
    public GameObject InventoryConfirm;
    public GameObject InventoryError;

    [Header("Smoke Mecanics")]
    //UI
    public TextMeshProUGUI txtSmokePercentage;
    private float smokePercentage;
    private int smokePercentageToInt;
    public Slider smokeSlider;
    static public bool isSmoking;

    //Code
    public float smokeCurrentTimer;
    public float smokeMaxTimer;
    public bool isInCountdown;

    [Header("Smoke Mecanics")]

    //Weed 1
    public AudioSource gameMusic;

    //Weed 2
    public UnityStandardAssets.ImageEffects.BlurOptimized camBlur;

    //Weed 3
    public GameObject camGame;
    private Quaternion camGameinitialPos;

    //Weed 4
    public Sprite daySandFront;
    public Sprite daySandBack;
    public Sprite dayMoutain;
    public Sprite daySky;

    public Sprite nightSandFront;
    public Sprite nightSandBack;
    public Sprite nightMoutain;
    public Sprite nightSky;

    public SpriteRenderer srSandFront;
    public SpriteRenderer srSandBack;
    public SpriteRenderer srMoutain;
    public SpriteRenderer srSky;
    public SpriteRenderer srGroundColor;

    private Color32 groundColorNormal = new Color32(130, 50, 29, 255);
    private Color32 groundColorDope = new Color32(42, 10, 47, 255);


    void Start()
    {
        camGameinitialPos = camGame.transform.rotation; 
    }

    void Update()
    {
        CountdownWeed();

        txtConfirmName.text = weedName.ToString();

        //Inventory on click
        btnSoft.onClick.AddListener(WeedSoftInventory);
        btnMedium.onClick.AddListener(WeedMediumInventory);
        btnHard.onClick.AddListener(WeedHardInventory);
        btnDope.onClick.AddListener(WeedDopeInventory);

        //Use on click
        btnSmoke.onClick.AddListener(TestSmoke);


        #region Weed units UI
        //Weed units
        txtUnitsSoft.text = "X " + UnitWeedSoft.ToString();
        txtUnitsMedium.text = "X " + UnitWeedMedium.ToString();
        txtUnitsHard.text = "X " + UnitWeedHard.ToString();
        txtUnitsDope.text = "X " + UnitWeedDope.ToString();
        #endregion

        #region Normalization UI
        //Normalization slider
        smokeSlider.value = (smokeCurrentTimer - 0) / (smokeMaxTimer - 0);

        //Normalization Percentage
        smokePercentage = ((smokeCurrentTimer - 0) / (smokeMaxTimer - 0)) * 100;
        smokePercentageToInt = Mathf.FloorToInt(smokePercentage);
        txtSmokePercentage.text = smokePercentageToInt.ToString() + "%";
        #endregion       
    }

    void TestSmoke()
    {
        if (!nerfOnClick)
        {
            nerfOnClick = true;
            StartCoroutine(WaitBeforeUse());

            //Switch : 1 soft, 2 medium, 3 hard, 4 dope
            switch (weedType)
            {
                case 1:
                    if(UnitWeedSoft >= 1)
                    {
                        UnitWeedSoft -= 1;

                        smokeMaxTimer += timerWeedSoft;

                        smokeCurrentTimer += timerWeedSoft;

                        isInCountdown = true;
                        InventoryConfirm.SetActive(true);

                        //effect
                        gameMusic.pitch = 0.70f;
                    }

                    else
                    {
                        InventoryError.SetActive(true); 
                    }
                    break;

                case 2:
                    if (UnitWeedMedium >= 1)
                    {
                        UnitWeedMedium -= 1;

                        smokeMaxTimer += timerWeedMedium;

                        smokeCurrentTimer += timerWeedMedium;

                        isInCountdown = true;
                        InventoryConfirm.SetActive(true);

                        //effect
                        camBlur.enabled = true;
                    }

                    else
                    {
                        InventoryError.SetActive(true);
                    }
                    break;

                case 3:
                    if (UnitWeedHard >= 1)
                    {
                        UnitWeedHard -= 1;

                        smokeMaxTimer += timerWeedHard;

                        smokeCurrentTimer += timerWeedHard;

                        isInCountdown = true;
                        InventoryConfirm.SetActive(true);

                        //effect
                        camGame.transform.rotation = new Quaternion(0,0,180,0);
                    }

                    else
                    {
                        InventoryError.SetActive(true);
                    }
                    break;

                case 4:
                    if (UnitWeedDope >= 1)
                    {
                        UnitWeedDope -= 1;

                        smokeMaxTimer += timerWeedDope;

                        smokeCurrentTimer += timerWeedDope;

                        isInCountdown = true;
                        InventoryConfirm.SetActive(true);

                        //effect
                        srMoutain.sprite = nightMoutain;
                        srSandBack.sprite = nightSandBack;
                        srSandFront.sprite = nightSandFront;
                        srSky.sprite = nightSky;

                        srGroundColor.color = groundColorDope;
                    }

                    else
                    {
                        InventoryError.SetActive(true);
                    }
                    break;
            }
        }
    }


    void CountdownWeed()
    {
        if(isInCountdown)
        {
            isSmoking = true;
            smokeCurrentTimer -= Time.deltaTime;

            //Quand le countdown est terminé
            if (smokeCurrentTimer <= 0)
            {
                isSmoking = false;
                smokeCurrentTimer = 0;
                isInCountdown = false;
                smokeMaxTimer = 1f;


                #region WeedEffectReset

                //Weed Soft - Audio
                gameMusic.pitch = 1f;

                //Weed Medium - Flou
                camBlur.enabled = false;

                //Weed Hard - Rotation cam
                camGame.transform.rotation = camGameinitialPos;

                //Weed Dope - Environement 
                srMoutain.sprite = dayMoutain;
                srSandBack.sprite = daySandBack;
                srSandFront.sprite = daySandFront;
                srSky.sprite = daySky;
                srGroundColor.color = groundColorNormal;
                #endregion

            }
        }
    }


    IEnumerator WaitBeforeUse()
    {
        yield return new WaitForSeconds(1f);
        nerfOnClick = false;
    }

    #region Weed Parameter
    void WeedSoftInventory()
    {
        weedType = 1;
        weedName = "Jordan River";
    }

    void WeedMediumInventory()
    {
        weedType = 2;
        weedName = "Sleeper";
    }

    void WeedHardInventory()
    {
        weedType = 3;
        weedName = "Marijuanaut";
    }

    void WeedDopeInventory()
    {
        weedType = 4;
        weedName = "Holy Prophecy";
    }
    #endregion
}
