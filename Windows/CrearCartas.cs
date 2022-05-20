using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearCartas : MonoBehaviour
{

    public GameObject CartaPrefab;
    public int Ancho;

    public Material[] materials;

    public void Start()
    {
        Crear();
    }

    public void Crear()
    {
        int cont = 0;
        for (int i=0; i<Ancho; i++)
        {
            for (int x=0; x<Ancho; x++)
            {
                GameObject casillaTemp = Instantiate(CartaPrefab, new Vector3(x, i, 0), Quaternion.identity);


                //casillaTemp.GetComponent<Carta>().NumCasilla = cont;
                cont++;
            }
        }
    }
}
