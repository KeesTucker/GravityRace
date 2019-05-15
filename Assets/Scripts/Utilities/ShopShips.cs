using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopShips : MonoBehaviour
{
    public Sprite[] ships;
    public Color[] colors;

    public int[] prices;

    public ShopColors shopColors;

    public GameObject shipShopPrefab;

    public Sprite current;

    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        colors = shopColors.colors;

        if (PlayerPrefs.HasKey("colorIndex"))
        {
            color = colors[PlayerPrefs.GetInt("colorIndex")];
        }
        else
        {
            color = colors[0];
        }
        
        for (int i = 0; i < ships.Length; i++)
        {
            GameObject ship = Instantiate(shipShopPrefab, transform);
            ship.transform.GetChild(1).GetComponent<Image>().color = color;
            ship.transform.GetChild(1).GetComponent<Image>().sprite = ships[i];
            ship.GetComponent<BuyItem>().index = i;
            ship.GetComponent<BuyItem>().isShip = true;
            ship.GetComponent<BuyItem>().price = prices[i];
            if (PlayerPrefs.HasKey("ship" + i.ToString()))
            {
                ship.transform.GetChild(2).GetComponent<Image>().enabled = false;
                ship.transform.GetChild(2).GetChild(0).GetComponent<TMPro.TMP_Text>().enabled = false;
            }
            else
            {
                ship.transform.GetChild(2).GetChild(0).GetComponent<TMPro.TMP_Text>().text = prices[i].ToString();
            }
            if (PlayerPrefs.HasKey("shipIndex"))
            {
                if (PlayerPrefs.GetInt("shipIndex") == i)
                {
                    ship.transform.GetChild(0).GetComponent<Image>().sprite = current;
                    ColorBlock colors = ship.GetComponent<Button>().colors;
                    colors.normalColor = Color.white;
                    colors.selectedColor = Color.white;
                    ship.GetComponent<Button>().colors = colors;
                }
                if (i == 0)
                {
                    ship.GetComponent<BuyItem>().bought = true;
                }
            }
            else if(i == 0)
            {
                ship.transform.GetChild(0).GetComponent<Image>().sprite = current;
                ship.GetComponent<BuyItem>().bought = true;
                ColorBlock colors = ship.GetComponent<Button>().colors;
                colors.normalColor = Color.white;
                colors.selectedColor = Color.white;
                ship.GetComponent<Button>().colors = colors;
            }
        }
    }
}
