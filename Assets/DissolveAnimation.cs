using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveAnimation : MonoBehaviour
{
    public float changeSpeed = 2f; // Geschwindigkeit der Änderung
    public KeyCode inputKey = KeyCode.Space; // Die Taste, die gedrückt wird, um die Änderung auszulösen
    public float targetValue = 0f; // Der Zielwert des Floats
    public float currentValue = 0f; // Der aktuelle Wert des Floats
    public Material DissolveMat;



    public void Dissolving(bool on)
    {
        if(on) { targetValue = 0; }
        else { targetValue = 1; }

        /*if (Input.GetKeyDown(inputKey))
        {
            // Setzen des Zielwerts auf den entgegengesetzten Wert
            targetValue = Mathf.Abs(targetValue - 1);
        }*/
    }

    private void Update()
    {
        // Ändern des Float-Werts in Richtung des Zielwerts
        currentValue = Mathf.MoveTowards(currentValue, targetValue, changeSpeed * Time.deltaTime);

        // Hier kannst du currentValue verwenden, um irgendetwas zu tun, z.B. die Kamera zu bewegen oder ein Objekt zu skalieren
        // Zum Beispiel: transform.localScale = Vector3.one * currentValue;
        //GetComponent<Renderer>().sharedMaterial.SetFloat("_Dissolve", currentValue);
        DissolveMat.SetFloat("Dissolve", currentValue);
        DissolveMat.SetFloat("Vector1_FEFF47F1", currentValue);
    }
}


