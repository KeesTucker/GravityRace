using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStarTotal : MonoBehaviour
{
    public TMPro.TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTotal();
    }

    public void UpdateTotal()
    {
        if (PlayerPrefs.HasKey("Stars"))
        {
            text.text = PlayerPrefs.GetInt("Stars").ToString();
        }
        else
        {
            text.text = "0";
        }
    }
}
