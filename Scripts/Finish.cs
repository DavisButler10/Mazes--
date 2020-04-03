using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public TextMesh text;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            text.text = "You have been victorious \n in completing my maze! \n Press G to do it again!";
        }
    }
}
