using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoorBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneDestination;
    public float destinationX, destinationY;

    private Animator animator;
    private PlayerController playerController;

    public DialogueTrigger claseDlgTrigger;
    public DialogueManager claseDlgManager;


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

            if(playerController.llave1 == 1 && playerController.llave2 == 1 && playerController.llave3 == 1)
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
            else
            {
                claseDlgTrigger.TriggerDialogue();
            }
           
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            claseDlgManager.EndDialogue();
        }
    }

    IEnumerator Transiciona(string scene)
    {
        animator.SetTrigger("salida");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }

    /*
    public void OnMouseDown()
    {
        PlayerPrefs.SetFloat("destinoX", destinationX);
        PlayerPrefs.SetFloat("destinoY", destinationY);

        StartCoroutine(Transiciona(sceneDestination));
    }
    */

}
