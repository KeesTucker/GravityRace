using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplode : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
