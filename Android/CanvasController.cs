using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject gamePanel;//objetosIntro; // objetosInstrucciones;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Iniciar()
    {
        SceneManager.LoadScene("Game");
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Instrucciones()
    {
        Time.timeScale = 0;
        instructionsPanel.SetActive(true);
        gamePanel.SetActive(false);
        //objetosIntro.SetActive(false);
        //objetosInstrucciones.SetActive(true);

    }

    public void Volver()
    {
        instructionsPanel.SetActive(false);
        //objetosInstrucciones.SetActive(false);

        gamePanel.SetActive(true);
        Time.timeScale = 1;
        //objetosIntro.SetActive(true);

    }

}