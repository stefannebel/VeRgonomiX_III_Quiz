using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FixColor : MonoBehaviour
{

    public Image imgBackground;
    public Text answerLabel2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeColor());

    }

 


    IEnumerator changeColor()
    {
        yield return new WaitForSeconds(.2f);
        imgBackground.color = Color.white;
        answerLabel2.color = Color.black;
    }
}
