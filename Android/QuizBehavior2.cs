using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizBehavior2 : MonoBehaviour
{

    public Animator animator;
    Animation a;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isFalling", true);
        //animator.SetBool("isFalling", false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
