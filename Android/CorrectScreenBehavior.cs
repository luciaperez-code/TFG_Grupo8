using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CorrectScreenBehavior : MonoBehaviour
{

    public static int contadorPreguntas = 1;

    public void preguntaSiguiente()
    {
        if (contadorPreguntas == 1)
        {
            SceneManager.LoadScene("Dojo_puzle3");
            contadorPreguntas++;
        }else if (contadorPreguntas == 2)
        {
            SceneManager.LoadScene("Dojo_puzle4");
            contadorPreguntas++;
        }else if (contadorPreguntas == 3)
        {
            PlayerPrefs.SetInt("llave3", 1);
            SceneManager.LoadScene("Dojo_puzle5");
            contadorPreguntas++;
        }
        
    }
}
