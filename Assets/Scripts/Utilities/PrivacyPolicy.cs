using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyPolicy : MonoBehaviour
{
    public GameObject policyDialogue;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("policyRead"))
        {
            Instantiate(policyDialogue, GameObject.Find("Canvas").transform);
            PlayerPrefs.SetInt("policyRead", 1);
        }
    }
}
