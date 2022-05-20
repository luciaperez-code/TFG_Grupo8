using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntregaTerceraLlave : MonoBehaviour
{
    float destinationX = 11.96f;
    float destinationY = 23.6f;
    PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        //pc.sumarLlave3(); aqui quiero entregar la llave
        //Debug.Log(pc.llave3);
        pc.llave3 = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.finDialogo)
        {
            PlayerPrefs.SetFloat("destinoX", destinationX);
            PlayerPrefs.SetFloat("destinoY", destinationY);
            SceneManager.LoadScene("Town");
        }
    }
}
