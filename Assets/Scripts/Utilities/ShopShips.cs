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
            ship.transform.GetChild(2).GetChild(0).GetComponent<TMPro.TMP_Text>().text = prices[i].ToString();
        }
    }
}
