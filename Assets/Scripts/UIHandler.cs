using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public Text money;
    public Text[] textUI;
    private int moneyInternal;
    public List<int> prices = new List<int>();

    public TMP_Text unlockText;
    public TMP_Text buyText;
    
    public GameObject shopUI;
    public GameObject mainMenuUI;

    public GameObject buyOrUnlock;
    public GameObject fillUncommon;
    public GameObject fillRare;
    public GameObject fillLegendary;

    private void Start() {
        disableFillOverlay();
    }

    public void btnPlay() {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

        FindObjectOfType<SkinSelector>().ParseSkin(prices[1]);
    }

    #region SHOP
    public void btnShop() {
        mainMenuUI.SetActive(false);
        shopUI.SetActive(true);
    }

    public void selectUncommon() {
        fillUncommon.SetActive(true);
        selectItem(100, 1000, 1);
    }

    public void selectRare() {
        fillRare.SetActive(true);
        selectItem(500, 10000, 2);
    }

    public void selectLegendary() {
        fillLegendary.SetActive(true);
        selectItem(2000, 100000, 3);
    }

    public void btnBack() {
        mainMenuUI.SetActive(true);
        shopUI.SetActive(false);
    }

    public void btnAddMoney() {
        if (moneyInternal < 1000000) {
            moneyInternal += 100000;
            money.text = moneyInternal.ToString();
        }
    }

    public void btnUnlock() {

    }

    public void btnBuy() {
        // If we can afford to make the purchase
        if (moneyInternal >= prices[0]) {
            moneyInternal -= prices[0];
            money.text = moneyInternal.ToString();
            
            changeTextColor(Color.white);
            disableFillOverlay();
        }
    }

    public void deselectItem() {
        changeTextColor(Color.white);
        disableFillOverlay();
    }

    private void selectItem(int unlock, int coins, int itemIndex) {
        changeTextColor(Color.black);

        buyOrUnlock.SetActive(true);
        unlockText.text = $"Get {unlock.ToString()} Score";
        buyText.text = $"Pay {coins.ToString()} coins";
        prices.Clear();
        prices.Add(coins);
        prices.Add(itemIndex);
    }

    private void changeTextColor(Color clr) {
        foreach (var text in textUI)
        {
            text.color = clr;
        }
    }

    private void disableFillOverlay() {
        fillUncommon.SetActive(false);
        fillRare.SetActive(false);
        fillLegendary.SetActive(false);

        buyOrUnlock.SetActive(false);
    }

    #endregion SHOP

    public void btnQuit() {
        Application.Quit();
    }
}
