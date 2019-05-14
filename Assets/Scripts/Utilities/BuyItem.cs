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
        if (PlayerPrefs.HasKey(type + index.ToString()))
        {
            bought = true;
            
            ColorBlock colors = GetComponent<Button>().colors;
            colors.normalColor = Color.grey;
            colors.selectedColor = Color.grey;
            GetComponent<Button>().colors = colors;
        }
    }

    public void ShowBuyDialogue()
    {
        GameObject confirm = Instantiate(buy, GameObject.Find("Canvas").transform);
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
                if (price < PlayerPrefs.GetInt("Stars"))
                {
                    ShowBuyDialogue();
                    PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") - price);
                    PlayerPrefs.SetInt(type + index.ToString(), 1);
                    bought = true;
                    PlayerPrefs.SetInt(type + "Index", index);
                    Start();
                    FindObjectOfType<UpdateStarTotal>().UpdateTotal();
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
            PlayerPrefs.SetInt(type + "Index", index);
        }
    }
}
