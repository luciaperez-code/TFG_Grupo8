using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCanvasController : MonoBehaviour
{

    public Sprite sprite;
    public GameObject imagen1, imagen2, imagen3;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.llave1 == 1)
        {
            imagen1.GetComponent<Image>().sprite = sprite;
        }
        if (playerController.llave2 == 1)
        {
            imagen2.GetComponent<Image>().sprite = sprite;
        }
        if (playerController.llave3 == 1)
        {
            imagen3.GetComponent<Image>().sprite = sprite;
        }
    }
}
