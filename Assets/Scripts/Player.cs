using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Meter")]
    public float meterMultiplier = 1.2f;
    public int meterTotalInteger;
    public TextMeshProUGUI meterText;
    private float meterTotal;

    [Header("Faith")]
    public float faithMultiplier = 0.2f;
    static public int faithTotalInteger;
    static public int faithUsed;
    public TextMeshProUGUI faithText;
    private float faithTotal;

    [Header("Touch Management")]
    public GameObject goStartScreen;
    public GameObject goShopScreen;
    public GameObject goInventoryScreen;
    public bool isPause;
    private Rect tapZone;
    private Vector2 touchPosition;
    public bool isTouchingGameplayZone;

    [Header("Stuff")]
    public Animator playerAnimator;
    public GameObject goSmoke;
    public static bool isWalking; 


    void Start()
    {
        //Zone de jeu
        tapZone = new Rect(0,276,1080,1370);
    }


    void Update()
    {
        #region Pause Manager
        if (goStartScreen.activeSelf || goShopScreen.activeSelf || goInventoryScreen.activeSelf)
        {
            isPause = true;
        }

        else
        {
            isPause = false;
        }
        #endregion

        #region Variables to text
        //Passage en texte
        meterText.text = meterTotalInteger.ToString() + " m";
        faithText.text = faithTotalInteger.ToString();


        //Convert en Int
        meterTotalInteger = Mathf.FloorToInt(meterTotal);
        faithTotalInteger = Mathf.FloorToInt(faithTotal);
        #endregion

        #region Value control

        if (isTouchingGameplayZone)
        {
            isWalking = true;
            meterTotal = meterTotal + Time.deltaTime * meterMultiplier;
        }

        else
        {
            isWalking = false;
        }

        faithTotal = meterTotal * faithMultiplier - faithUsed;
        #endregion

        #region TapZone

        if (Input.touchCount > 0 && !isPause)
        {
            touchPosition = Input.GetTouch(0).position;
        }

        else
        {
            touchPosition = new Vector2(0, 0);
        }

        if (tapZone.Contains(touchPosition))
        {
            isTouchingGameplayZone = true;
        }

        else
        {
            isTouchingGameplayZone = false;
        }

        #endregion

        #region Anims
        if (isTouchingGameplayZone)
        {
            playerAnimator.SetBool("IsWalking", true);
        }

        else
        {
            playerAnimator.SetBool("IsWalking", false);
        }

        if(InventoryManager.isSmoking)
        {
            goSmoke.SetActive(true);
        }

        else
        {
            goSmoke.SetActive(false);
        }

        #endregion
    }
}
