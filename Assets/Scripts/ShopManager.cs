using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    int priceSoft = 5;
    int priceMedium = 15;
    int priceHard = 25;
    int priceDope = 40;

    private int price;
    private string weedName = "null";
    private int weedType = 0;
    private bool nerfOnClick;

    [Header("Textes")]
    public TextMeshProUGUI txtPriceSoft;
    public TextMeshProUGUI txtPriceMedium;
    public TextMeshProUGUI txtPriceHard;
    public TextMeshProUGUI txtPriceDope;

    public TextMeshProUGUI txtConfirmName;
    public TextMeshProUGUI txtConfirmPrice;

    [Header("Boutons")]
    public Button btnSoft;
    public Button btnMedium;
    public Button btnHard;
    public Button btnDope;

    public Button btnBuy;

    [Header("Sous-menus d'achat")]
    public GameObject ShopConfirm;
    public GameObject ShopError;

    // Start is called before the first frame update
    void Start()
    {
        txtPriceSoft.text = priceSoft.ToString();
        txtPriceMedium.text = priceMedium.ToString();
        txtPriceHard.text = priceHard.ToString();
        txtPriceDope.text = priceDope.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //achat
        txtConfirmName.text = weedName.ToString();
        txtConfirmPrice.text = price.ToString();
        btnBuy.onClick.AddListener(TestAchat);

        //Shop on click
        btnSoft.onClick.AddListener(WeedSoft);
        btnMedium.onClick.AddListener(WeedMedium);
        btnHard.onClick.AddListener(WeedHard);
        btnDope.onClick.AddListener(WeedDope);
    }

    void TestAchat()
    {
        if(!nerfOnClick)
        {
            nerfOnClick = true;
            StartCoroutine(WaitBeforeRebuy());

            //---test price---
            //Achat reussi
            if (Player.faithTotalInteger >= price)
            {
                ShopConfirm.SetActive(true);
                Player.faithUsed += price;


                //Switch : 1 soft, 2 medium, 3 hard, 4 dope
                switch (weedType)
                {
                    case 1:
                        InventoryManager.UnitWeedSoft += 1;
                        break;

                    case 2:
                        InventoryManager.UnitWeedMedium += 1;
                        break;

                    case 3:
                        InventoryManager.UnitWeedHard += 1;
                        break;

                    case 4:
                        InventoryManager.UnitWeedDope += 1;
                        break;
                }
            }

            //Achat echec
            else
            {
                //pas d'achat possible
                ShopError.SetActive(true);
                print("impossible d'acheter");
            }
        }
    }

    IEnumerator WaitBeforeRebuy()
    {
        yield return new WaitForSeconds(1f);
        nerfOnClick = false;
    }

    #region Weed Parameter
    void WeedSoft()
    {
        weedType = 1;
        weedName = "Jordan River";
        price = priceSoft;
    }

    void WeedMedium()
    {
        weedType = 2;
        weedName = "Sleeper";
        price = priceMedium;
    }

    void WeedHard()
    {
        weedType = 3;
        weedName = "Marijuanaut";
        price = priceHard;
    }

    void WeedDope()
    {
        weedType = 4;
        weedName = "Holy Prophecy";
        price = priceDope;
    }
    #endregion
}
