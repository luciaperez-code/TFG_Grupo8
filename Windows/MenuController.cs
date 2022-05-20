using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System;

public class MenuController : MonoBehaviour
{

    public Text texto;
    private string usuario;
    private int logado;

    public GameObject menu, mensaje, panelRegistro, panelUsuario, panelExito;
    public InputField usuarioTxt, passTxt;

    private PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        usuario = PlayerPrefs.GetString("nombre");
        texto.text = "Hola, " + usuario + "!";
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        logado = PlayerPrefs.GetInt("logado");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Guarda el progreso de un jugador que no tenga ya una partida guardada
    public void Guardar()
    {
        if(logado == 1)
        {
            DbController controlador = GameObject.Find("DatabaseController").GetComponent<DbController>();
            var id = controlador.ObtenerIdUser(usuario);
            var escena = SceneManager.GetActiveScene().name;
            var llave1 = playerScript.llave1;
            var llave2 = playerScript.llave2;
            var llave3 = playerScript.llave3;
            var posX = playerScript.transform.position.x;
            var posY = playerScript.transform.position.y;

            var posicionX = posX.ToString();
            var posicionY = posY.ToString();

            controlador.InsertarProgreso(id, escena, posicionX, posicionY, llave1, llave2, llave3);

            Debug.Log("Guardado con éxito");
            mensaje.SetActive(false);
        }
        else
        {
            AbrirCerrarUsuario();
        }
        

    }

    //Actualiza los datos del jugador en la base de datos
    public void Actualizar()
    {
        DbController controlador = GameObject.Find("DatabaseController").GetComponent<DbController>();
        var id = controlador.ObtenerIdUser(usuario);
        var escena = SceneManager.GetActiveScene().name;
        var llave1 = playerScript.llave1;
        var llave2 = playerScript.llave2;
        var llave3 = playerScript.llave3;
        var posX = playerScript.transform.position.x;
        var posY = playerScript.transform.position.y;

        var posicion = posX.ToString();

        var posicionY = posY.ToString();

        Debug.Log("Llega a antes del update");
        controlador.ActualizaProgreso(id, escena, posicion, posicionY, llave1, llave2, llave3);

        Debug.Log("Guardado con éxito");
        mensaje.SetActive(false);
    }

    //Trae los progresos del jugador de la base de datos y los almacena en PlayerPrefs para que sean asignados por el Player
    public void Cargar()
    {
        DbController controlador = GameObject.Find("DatabaseController").GetComponent<DbController>();
        var id = controlador.ObtenerIdUser(usuario);
        var log = "progresos WHERE id_usuario = " + id;

        var resultado = controlador.Select(log);
        var escena = resultado.GetString(2);
        var posicionX = resultado.GetString(3);
        var posicionY = resultado.GetString(4);
        var llave1 = resultado.GetInt32(5);
        var llave2 = resultado.GetInt32(6);
        var llave3 = resultado.GetInt32(7);
        resultado.Close();

        var posX = float.Parse(posicionX);
        var posY = float.Parse(posicionY);

        PlayerPrefs.SetFloat("posX", posX);
        PlayerPrefs.SetFloat("posY", posY);
        PlayerPrefs.SetInt("llave1", llave1);
        PlayerPrefs.SetInt("llave2", llave2);
        PlayerPrefs.SetInt("llave3", llave3);

        Debug.Log("Cargando......");

    }

    //Comprueba si ya existe un progreso guardado para ese jugador en la base de datos
    public void Comprobar()
    {
        if(logado == 1)
        {
            DbController controlador = GameObject.Find("DatabaseController").GetComponent<DbController>();
            var id = controlador.ObtenerIdUser(usuario);
            var log = "progresos WHERE id_usuario =" + id;

            var resultado = controlador.Select(log);

            //Comprueba si ya existe un registro para ese usuario. Si ya existe un registro muestra la advertencia, si no, guarda

            if (resultado.HasRows)
            {
                mensaje.SetActive(true);
                resultado.Close();
            }
            else
            {
                Guardar();

            }
        }
        else
        {
            Guardar();
        }
    }

    public void registrarUsuario()
    {
        if (usuarioTxt.text.Length >= 3 && usuarioTxt.text.Length <= 20)
        {
            if (passTxt.text != null)
            {

                string log = "usuarios WHERE usuario LIKE '" + usuarioTxt.text + "';";
                DbController controller = GameObject.Find("DatabaseController").GetComponent<DbController>();
                SqliteDataReader resultado = controller.Select(log);

                if (resultado.HasRows)
                {
                    Debug.Log("El usuario ya existe");
                    resultado.Close();

                }
                else
                {
                    resultado.Close();
                    controller.AddUser(usuarioTxt.text, passTxt.text);
                    usuario = usuarioTxt.text;
                    PlayerPrefs.SetString("nombre", usuario);
                    Debug.Log("Usuario creado con éxito");
                    Guardar();
                    passTxt.text = "";
                    AbrirCerrarExito();
                }
            }
            else
            {
                Debug.Log("Las contraseñas no coinciden");
            }

        }
        else
        {
            Debug.Log("El usuario debe contener entre 3 y 20 caracteres");
        }


    }

    public void AbrirMenu()
    {
        menu.SetActive(true);
    }

    public void CerrarMenu()
    {
        menu.SetActive(false);
        mensaje.SetActive(false);
        panelExito.SetActive(false);
        panelRegistro.SetActive(false);
        panelUsuario.SetActive(false);
    }

    public void AbrirCerrarMensaje()
    {
        if (mensaje.activeSelf)
        {
            mensaje.SetActive(false);
        }else
        {
            mensaje.SetActive(true);
        }
        
    }

    public void AbrirCerrarUsuario()
    {
        if (panelUsuario.activeSelf)
        {
            panelUsuario.SetActive(false);
        }
        else
        {
            panelUsuario.SetActive(true);
        }
    }

    public void AbrirCerrarRegistro()
    {
        if (panelRegistro.activeSelf)
        {
            panelRegistro.SetActive(false);
            playerScript.menuRegistroOpen = false;
        }
        else
        {
            panelRegistro.SetActive(true);
            playerScript.menuRegistroOpen = true;
        }
    }

    public void AbrirCerrarExito()
    {
        if (panelExito.activeSelf)
        {
            panelExito.SetActive(false);
        }
        else
        {
            panelExito.SetActive(true);
        }
    }

    public void SalirJuego()
    {
        Application.Quit();
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
}
