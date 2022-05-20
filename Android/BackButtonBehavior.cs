using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonBehavior : MonoBehaviour
{

    public string sceneDestination;
    public float destinationX, destinationY;

    private Animator animator;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("PanelCambio").GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Almacena las coordenadas en las que debe aparecer el jugador al cambiar de escena por esa puerta en concreto
            PlayerPrefs.SetFloat("destinoX", destinationX);
            PlayerPrefs.SetFloat("destinoY", destinationY);

            //Almacena el valor de las llaves para mostrarlas en otras pantallas
            PlayerPrefs.SetInt("llave1", playerController.llave1);
            PlayerPrefs.SetInt("llave2", playerController.llave2);
            PlayerPrefs.SetInt("llave3", playerController.llave3);


            StartCoroutine(Transiciona(sceneDestination));
        }
    }

    IEnumerator Transiciona(string scene)
    {
        animator.SetTrigger("salida");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
    public void OnMouseDown()
    {
        PlayerPrefs.SetFloat("destinoX", destinationX);
        PlayerPrefs.SetFloat("destinoY", destinationY);

        StartCoroutine(Transiciona(sceneDestination));
    }
}
