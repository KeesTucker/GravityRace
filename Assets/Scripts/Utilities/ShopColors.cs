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
            color.transform.GetChild(2).GetChild(0).GetComponent<TMPro.TMP_Text>().text = prices[i].ToString();
        }
    }
}
