using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CreateLevels : MonoBehaviour
{
    public int NumOfLevels;
    public GameObject button;
    public Image[] stars;
    // Start is called before the first frame update
    void Start()
    {
        Create();
    }

    public void Create()
    {
        GameObject instantiated;
        for (int i = 0; i < NumOfLevels; i++)
        {
            instantiated = Instantiate(button);
            instantiated.transform.parent = transform;
            instantiated.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = (i + 1).ToString();
            instantiated.GetComponent<Navigation>().selected = i + 1;
            Random.InitState(i);
            Color color = Color.white;//Color.HSVToRGB(Random.Range(0, 1f), Random.Range(0.5f, 0.8f), Random.Range(0.8f, 1f));
            instantiated.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().color = color;

            if (!PlayerPrefs.HasKey("Level" + (i + 1).ToString()))
            {
                instantiated.GetComponent<Button>().interactable = false;
                instantiated.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().color = Color.gray;
            }
            else
            {
                stars = instantiated.GetComponent<Navigation>().stars;
                for (int u = 0; u < PlayerPrefs.GetInt("Level" + (i + 1).ToString()); u++)
                {
                    stars[u].color = color;
                }
            }
            if (PlayerPrefs.HasKey("Level" + (i).ToString()) || i == 0)
            {
                instantiated.GetComponent<Button>().interactable = true;
                instantiated.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().color = color;
            }
        }
    }
}
