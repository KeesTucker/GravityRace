using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    public bool isShip;

    public string type;

    public int price;

    public int index;

    public bool bought = false;

    public GameObject buy;
    public GameObject cant;

    public Sprite current;
    public Sprite circle;

    void Start()
    {
        if (isShip)
        {
            type = "ship";
        }
        else
        {
            type = "color";
        }
        if (PlayerPrefs.HasKey(type + "Index"))
        {
            if (PlayerPrefs.GetInt(type + "Index") != index && PlayerPrefs.HasKey(type + index.ToString()))
            {
                bought = true;
            }
        }
        if (bought && index != 0 && PlayerPrefs.GetInt(type + "Index") != index)
        {
            ColorBlock colors = GetComponent<Button>().colors;
            colors.normalColor = Color.grey;
            colors.selectedColor = Color.grey;
            GetComponent<Button>().colors = colors;
        }
        if (index == 0)
        {
            transform.GetChild(2).GetComponent<Image>().enabled = false;
            transform.GetChild(2).GetChild(0).GetComponent<TMPro.TMP_Text>().enabled = false;
        }
    }

    public void ShowBuyDialogue()
    {
        GameObject confirm = Instantiate(buy, GameObject.Find("Canvas").transform);
        confirm.GetComponent<ConfirmPurchase>().buyItem = this;
    }
    public void ShowCantDialogue()
    {
        GameObject cantBuy = Instantiate(cant, GameObject.Find("Canvas").transform);
    }

    public void Buy()
    {
        if (!bought)
        {
            if (PlayerPrefs.HasKey("Stars"))
            {
                if (price <= PlayerPrefs.GetInt("Stars"))
                {
                    ShowBuyDialogue();
                }
                else
                {
                    ShowCantDialogue();
                }
            }
            else
            {
                ShowCantDialogue();
            }
        }
        else
        {
            if (PlayerPrefs.HasKey(type + "Index"))
            {
                transform.parent.GetChild(PlayerPrefs.GetInt(type + "Index")).GetChild(0).GetComponent<Image>().sprite = circle;
                ColorBlock colorsa = transform.parent.GetChild(PlayerPrefs.GetInt(type + "Index", index)).GetComponent<Button>().colors;
                colorsa.normalColor = Color.grey;
                colorsa.selectedColor = Color.grey;
                transform.parent.GetChild(PlayerPrefs.GetInt(type + "Index")).GetComponent<Button>().colors = colorsa;
                transform.parent.GetChild(PlayerPrefs.GetInt(type + "Index")).GetComponent<BuyItem>().bought = true;
            }
            else
            {
                transform.parent.GetChild(0).GetChild(0).GetComponent<Image>().sprite = circle;
                ColorBlock colorsa = transform.parent.GetChild(0).GetComponent<Button>().colors;
                colorsa.normalColor = Color.grey;
                colorsa.selectedColor = Color.grey;
                transform.parent.GetChild(0).GetComponent<Button>().colors = colorsa;
                transform.parent.GetChild(0).GetComponent<BuyItem>().bought = true;
            }
            transform.GetChild(0).GetComponent<Image>().sprite = current;
            ColorBlock colorsb = GetComponent<Button>().colors;
            colorsb.normalColor = Color.white;
            colorsb.selectedColor = Color.white;
            GetComponent<Button>().colors = colorsb;

            PlayerPrefs.SetInt(type + "Index", index);
        }
    }

    public void Confirmed()
    {
        PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") - price);
        PlayerPrefs.SetInt(type + index.ToString(), 1);
        bought = true;

        if (PlayerPrefs.HasKey(type + "Index"))
        {
            transform.parent.GetChild(PlayerPrefs.GetInt(type + "Index")).GetChild(0).GetComponent<Image>().sprite = circle;
            ColorBlock colorsa = transform.parent.GetChild(PlayerPrefs.GetInt(type + "Index", index)).GetComponent<Button>().colors;
            colorsa.normalColor = Color.grey;
            colorsa.selectedColor = Color.grey;
            transform.parent.GetChild(PlayerPrefs.GetInt(type + "Index")).GetComponent<Button>().colors = colorsa;
            transform.parent.GetChild(PlayerPrefs.GetInt(type + "Index")).GetComponent<BuyItem>().bought = true;
        }
        else
        {
            transform.parent.GetChild(0).GetChild(0).GetComponent<Image>().sprite = circle;
            ColorBlock colorsa = transform.parent.GetChild(0).GetComponent<Button>().colors;
            colorsa.normalColor = Color.grey;
            colorsa.selectedColor = Color.grey;
            transform.parent.GetChild(0).GetComponent<Button>().colors = colorsa;
            transform.parent.GetChild(0).GetComponent<BuyItem>().bought = true;
        }
        
        PlayerPrefs.SetInt(type + "Index", index);

        transform.GetChild(0).GetComponent<Image>().sprite = current;
        transform.GetChild(2).GetComponent<Image>().enabled = false;
        transform.GetChild(2).GetChild(0).GetComponent<TMPro.TMP_Text>().enabled = false;
        ColorBlock colorsb = GetComponent<Button>().colors;
        colorsb.normalColor = Color.white;
        colorsb.selectedColor = Color.white;
        GetComponent<Button>().colors = colorsb;

        Start();
        FindObjectOfType<UpdateStarTotal>().UpdateTotal();
    }
}
