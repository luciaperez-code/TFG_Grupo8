using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    private float speed = 5f;
    public GameObject player;
    private Vector3 tempPos;
    private string nombre;
    public Button buttonplay;

    /**
    void Start()
    {
        buttonplay = GetComponent<Button>();
    }

    void Update()
    {
        nombre = PlayerPrefs.GetString("nombre");

        if (nombre == null)
        {
            buttonplay.interactable = false;
        }
        if (nombre != null)
        {
            buttonplay.interactable = true;
        }
    }
    **/

    public void MoveToScene()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("logado", 0);
        StartCoroutine("WaitScene");
    }

    public void ExitGame()
    {
        StartCoroutine("WaitExit");
    }

    public void Instructions()
    {
        string target = "http://michiland.atwebpages.com/index.html";
        try
        {
            System.Diagnostics.Process.Start(target);
        }
        catch (System.ComponentModel.Win32Exception noBrowser)
        {
            if (noBrowser.ErrorCode == -2147467259)
                
                Console.WriteLine(noBrowser.Message);
        }
        catch (System.Exception other)
        {
            Console.WriteLine(other.Message);
        }

        //StartCoroutine("WaitInstructions");
    }


    IEnumerator WaitScene()
    {
        player.transform.position += new Vector3(-67f, 0, 0) * speed * Time.deltaTime;
        yield return new WaitForSeconds(0.5f);
        player.transform.position += new Vector3(0, 10f, 0) * speed * Time.deltaTime;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Town");
    }

    IEnumerator WaitExit()
    {
        player.transform.position += new Vector3(60f, 0, 0) * speed * Time.deltaTime;
        yield return new WaitForSeconds(1f);
        player.transform.position += new Vector3(0, 30f, 0) * speed * Time.deltaTime;
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }

    /**
     * IEnumerator WaitInstructions()
    {
        player.transform.position += new Vector3(0, 27f, 0) * speed * Time.deltaTime;
        yield return new WaitForSeconds(2f);
        //Abrir web

    }
    **/
}
