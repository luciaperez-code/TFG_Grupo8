using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
    public InputField usuarioTxt;
    public InputField passTxt;

    public GameObject registro;
    public GameObject login;

    private PlayerController playerController;

    public GameObject botonLogin, botonCargar, botonRegistro, botonVolver, panelLogin, panelStart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Entrar()
    {
        string _log = "`usuarios` WHERE `usuario` LIKE '" + usuarioTxt.text + "' AND `pass` LIKE '" + passTxt.text + "'";

        DbController dbController = GameObject.Find("DatabaseController").GetComponent<DbController>();
        var resultado = dbController.Select(_log);

        //Comprobamos si hay usuario o no. Si el resultado tiene lineas quiere decir que el select devuelve un registro. Es decir, hay usuario
        if (resultado.HasRows)
        {
            Debug.Log("Login correcto");
            resultado.Close();
            PlayerPrefs.SetString("nombre", usuarioTxt.text);

            DbController controlador = GameObject.Find("DatabaseController").GetComponent<DbController>();
            var id = controlador.ObtenerIdUser(usuarioTxt.text);
            var log = "progresos WHERE id_usuario =" + id;

            var intermedia = controlador.Select(log);
            //Comprueba si ya existe un registro para ese usuario
            if (intermedia.HasRows)
            {
                //Si ya hay un progreso guardado extraemos la escenena a cargar y almacenamos los otros datos en PLayerPrefs
                resultado = controlador.Select(log);
                resultado.Read();
                var escena = resultado.GetString(2);
                var posX = float.Parse(resultado.GetString(3));
                var posY = float.Parse(resultado.GetString(4));
                var llave1 = resultado.GetInt32(5);
                var llave2 = resultado.GetInt32(6);
                var llave3 = resultado.GetInt32(7);
                resultado.Close();
                intermedia.Close();

                //Se guardan los datos del jugador en PLayerPrefs
                PlayerPrefs.SetFloat("posX", posX);
                PlayerPrefs.SetFloat("posY", posY);
                PlayerPrefs.SetInt("llave1", llave1);
                PlayerPrefs.SetInt("llave2", llave2);
                PlayerPrefs.SetInt("llave3", llave3);
                PlayerPrefs.SetInt("logado", 1);

                //Se carga la escena guardada en la base de datos
                SceneManager.LoadScene(escena);

            }
            else
            {
                //Si no hay progreso guardado se ponen a "cero" los PlayerPrefs y se accede normal
                PlayerPrefs.DeleteKey("posX");
                PlayerPrefs.DeleteKey("posY");
                PlayerPrefs.DeleteKey("llave1");
                PlayerPrefs.DeleteKey("llave2");
                PlayerPrefs.DeleteKey("llave3");
                PlayerPrefs.SetInt("logado", 1);

                SceneManager.LoadScene("Town");

            }


        }
        else
        {
            Debug.Log("Usuario o contraseña incorrectos");
            resultado.Close();
        }
    }

    //Registra un nuevo usuario y su password en la base de datos
    public void Registrar()
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
                    Debug.Log("Usuario creado con éxito");
                    passTxt.text = "";
                    MostrarRegistroCorrecto();
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

    //Abre o cierra el panel que contiene los elementos para hacer login
    public void AbrirCerrarLogin()
    {
        if (panelLogin.activeSelf)
        {
            panelLogin.SetActive(false);
            panelStart.SetActive(true);
        }
        else
        {
            panelLogin.SetActive(true);
            panelStart.SetActive(false);
        }
    }

    public void MostrarRegistroCorrecto()
    {

    }
}
