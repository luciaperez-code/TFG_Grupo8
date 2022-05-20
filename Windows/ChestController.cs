using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Sprite sprite;
    private PlayerController playerController;
    public GameObject resplandor;
    public GameObject llave;

    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {



        if (collision.tag == "Player")
        {
            Debug.Log("Detecta al player");
            if(isOpen == false)
            {
                Debug.Log("El cofre está cerrado");
                this.GetComponent<SpriteRenderer>().sprite = sprite;
                playerController.llave2 = 1;
                Instantiate(resplandor, transform.position, Quaternion.identity);
                StartCoroutine(CreaLlave());
                isOpen = true;
            }
            
        }
    }

    IEnumerator CreaLlave()
    {


        Vector3 target = new Vector3(transform.position.x, (transform.position.y + 1), transform.position.z);
        var creada = Instantiate(llave, target, Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(creada);
    }
}
