using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public GameManager gm;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        gm.Back();
    }
}
