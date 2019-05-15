using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopColors : MonoBehaviour
{
    public Color[] colors;

    public int[] prices;

    public Sprite circle;

    public GameObject colorShopPrefab;

    private Color color;

    public Sprite current;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            GameObject color = Instantiate(colorShopPrefab, transform);
            color.transform.GetChild(1).GetComponent<Image>().color = colors[i];
            color.transform.GetChild(1).GetComponent<Image>().sprite = circle;
            color.GetComponent<BuyItem>().index = i;
            color.GetComponent<BuyItem>().isShip = false;
            color.GetComponent<BuyItem>().price = prices[i];
            if (PlayerPrefs.HasKey("color" + i.ToString()))
            {
                color.transform.GetChild(2).GetComponent<Image>().enabled = false;
                color.transform.GetChild(2).GetChild(0).GetComponent<TMPro.TMP_Text>().enabled = false;
            }
            else
            {
                color.transform.GetChild(2).GetChild(0).GetComponent<TMPro.TMP_Text>().text = prices[i].ToString();
            }
            if (PlayerPrefs.HasKey("colorIndex"))
            {
                if (PlayerPrefs.GetInt("colorIndex") == i)
                {
                    color.transform.GetChild(0).GetComponent<Image>().sprite = current;
                    ColorBlock colors = color.GetComponent<Button>().colors;
                    colors.normalColor = Color.white;
                    colors.selectedColor = Color.white;
                    color.GetComponent<Button>().colors = colors;
                }
                if (i == 0)
                {
                    color.GetComponent<BuyItem>().bought = true;
                }
            }
            else if (i == 0)
            {
                color.transform.GetChild(0).GetComponent<Image>().sprite = current;
                color.GetComponent<BuyItem>().bought = true;
                ColorBlock colors = color.GetComponent<Button>().colors;
                colors.normalColor = Color.white;
                colors.selectedColor = Color.white;
                color.GetComponent<Button>().colors = colors;
            }
        }
    }
}
