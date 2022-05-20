using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    private bool isMoving;

    private Vector2 input;
    public Vector3 targetPos;
    public LayerMask solidObjectsLayer;
    public LayerMask iceLayer;

    private Animator animator;

    public DialogueTrigger claseDlgTrigger;
    public DialogueManager claseDlgManager;

    public string usuario;
    public int llave1, llave2, llave3;

    public bool isOnIce, menuRegistroOpen;

    public GameObject menu, mensaje;

    //public Joystick joystick;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        usuario = PlayerPrefs.GetString("nombre");


        //Se comprueba si hay PlayerPrefs guardados. De ser así se usan para determinar las condiciones iniciales del player

        if (PlayerPrefs.HasKey("destinoX"))
        {

            Debug.Log("Entra con destino");
            var x = PlayerPrefs.GetFloat("destinoX");
            var y = PlayerPrefs.GetFloat("destinoY");

            transform.position = new Vector3(x, y, 0);

            //Borramos las variables de destino para que no interfieran con el guardado de la partida
            PlayerPrefs.DeleteKey("destinoX");
            PlayerPrefs.DeleteKey("destinoY");

        }
        else if (PlayerPrefs.HasKey("posX"))
        {


            InicializarPropiedades();
            Debug.Log(PlayerPrefs.GetFloat("posX") + " PlayerPref");
        }

        this.llave1 = PlayerPrefs.GetInt("llave1");
        this.llave2 = PlayerPrefs.GetInt("llave2");
        this.llave3 = PlayerPrefs.GetInt("llave3");

        Debug.Log("Ni destino ni pos");
    }

    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

        //Comprueba si se encuentra en una casilla de hielo y si la siguiente es una casilla sin obstáculo
        if (isIce(transform.position) && isWalkable(targetPos))
        {
            isOnIce = true;

            targetPos = transform.position;
            if (!isMoving)
            {

                if (input.x != 0)
                {
                    targetPos.x += input.x;
                    if (isWalkable(targetPos))
                    {
                        StartCoroutine(Desliza(targetPos));
                    }

                }
                else
                {
                    targetPos.y += input.y;
                    if (isWalkable(targetPos))
                    {
                        StartCoroutine(Desliza(targetPos));
                    }
                }

            }
            animator.SetBool("isMoving", false);

        }
        else
        {
            isOnIce = false;
            //Este if hace que el player se mueva de tile en tile, es decir, sumando uno a su posición cada vez que se presiona la tecla.
            if (!isMoving)
            {

                //Define el movimiento por teclado
                if(menuRegistroOpen == false)
                {
                    input.x = Input.GetAxisRaw("Horizontal");
                    input.y = Input.GetAxisRaw("Vertical");
                }
                




                //Define el movimiento con el Joystick

                /*input.x = joystick.Horizontal;
                input.y = joystick.Vertical;*/

                //Evita el movimiento en diagonal con movimiento definido por teclado
                if (input.x != 0) input.y = 0;

                if (input != Vector2.zero)
                {


                    //Determina hacia dónde miramos 
                    animator.SetFloat("moveX", input.x);
                    animator.SetFloat("moveY", input.y);

                    targetPos = transform.position;
                    targetPos.x += input.x;
                    targetPos.y += input.y;

                    if (isWalkable(targetPos))
                    {
                        Debug.Log("Se puede andar");
                        //usa la corrutina Move
                        StartCoroutine(Move(targetPos));
                    }

                }

            }

            //comprueba si estás andando para ejecutar la animación de andar
            animator.SetBool("isMoving", isMoving);
            

        }


        if (Input.GetKeyDown(KeyCode.P) && menuRegistroOpen == false)
        {
            if(isOnIce == false)
            {
                if (menu.activeSelf)
                {
                    menu.SetActive(false);
                    mensaje.SetActive(false);
                    
                }
                else
                {
                    menu.SetActive(true);
                   
                }
            }
            
        }
        


    }

    IEnumerator Move (Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    IEnumerator Desliza(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed * 3 * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    //Comprueba si puede andar sin chocarse con el layer que le digas
    private bool isWalkable(Vector3 targetPos)
    { 
        if (Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }


    //Comprueba si la siguiente casilla es hielo
    private bool isIce(Vector3 targetPos)
    {

        if (Physics2D.OverlapCircle(targetPos, 0.1f, iceLayer) != null)
        {
            return true;
        }

        return false;
    }

    public void nextSentencePlayer()
    {
        claseDlgManager.DisplayNextSentence();
    }


    //Si detecta colisión con una puerta almacena su posiciçon para cuando vuelva a esa escena
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Door")
        {
            PlayerPrefs.SetFloat("posX", transform.position.x);
            PlayerPrefs.SetFloat("posY", transform.position.y);
        }
    }



    private void InicializarPropiedades()
    {
        llave1 = PlayerPrefs.GetInt("llave1");
        llave2 = PlayerPrefs.GetInt("llave2");
        llave3 = PlayerPrefs.GetInt("llave3");



        var posX = PlayerPrefs.GetFloat("posX");
        var posY = PlayerPrefs.GetFloat("posY");

        var posicion = new Vector3(posX, posY, 0);
        transform.position = posicion;
    }

    public void sumarLlave3()
    {
        llave3 = llave3++;
    }
}
