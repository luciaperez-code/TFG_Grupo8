using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizBehavior : MonoBehaviour
{
    bool finDialogo = false;
    DialogueManager manager;
    float destinationX = 11.96f;
    float destinationY = 23.6f;
    public Animator animator;
    public static bool michicae = false;
   

    // Start is called before the first frame update
    void Start()
    {
        //GameObject canvasPadreRespuestas = GameObject.FindGameObjectWithTag("QuizCanvas");
        //GameObject botonesRespuestas = GameObject.FindGameObjectWithTag("QuizPanel");
        animator.SetBool("mostrarBotones", false);

    }

    // Update is called once per frame
    void Update()
    {
        GameObject botonesRespuestas = GameObject.FindGameObjectWithTag("QuizPanel");
        GameObject canvasPadreRespuestas = GameObject.FindGameObjectWithTag("QuizCanvas");
      


        if (DialogueManager.finDialogo)
        {
            animator.SetBool("mostrarBotones", true);
            Debug.Log("ok M Aullido se ha callado");
            DialogueManager.finDialogo = false;

        }
        else
        {
           // animator.SetBool("mostrarBotones", false);
        }
    }

    public void startQuiz()
    {
        GameObject puertaSueloDojo = GameObject.FindGameObjectWithTag("OnGroundGrid");
        animator.SetBool("mostrarBotones", false);
        puertaSueloDojo.SetActive(false);
        michicae = true;
       // pausita();
        SceneManager.LoadScene("Dojo_puzle2");


    }
    public void noQuiz()
    {
        animator.SetBool("mostrarBotones", false);
        DialogueManager.finDialogo = false;
        PlayerPrefs.SetFloat("destinoX", destinationX);
        PlayerPrefs.SetFloat("destinoY", destinationY);
        SceneManager.LoadScene("Town");
        CorrectScreenBehavior.contadorPreguntas = 0;
    }

    public void acierto()
    {
        animator.SetBool("mostrarBotones", false);
        DialogueManager.finDialogo = false;
        SceneManager.LoadScene("Correct!");
    }

    public void goodEnding()
    {
        animator.SetBool("mostrarBotones", false);
        DialogueManager.finDialogo = false;
        SceneManager.LoadScene("FIN");
    }

    public void badEnding()
    {
        animator.SetBool("mostrarBotones", false);
        DialogueManager.finDialogo = false;
        SceneManager.LoadScene("GAME OVER");
    }


    IEnumerator pausita()
    {
        yield return new WaitForSeconds(2);
        
    }

    public void quitaQuiz()
    {
        animator.SetBool("mostrarBotones", false);
    }
}
