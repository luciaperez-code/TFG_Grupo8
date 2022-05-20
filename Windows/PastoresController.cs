using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastoresController : MonoBehaviour
{
    public GameObject pastor1, pastor2;
    private int llave1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        llave1 = PlayerPrefs.GetInt("llave1");

        if (llave1 == 0)
        {
            pastor1.SetActive(true);
            pastor2.SetActive(false);
        }
        if (llave1 == 1)
        {
            pastor1.SetActive(false);
            pastor2.SetActive(true);
        }

    }
}
