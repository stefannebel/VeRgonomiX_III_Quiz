using UnityEngine;
using UnityEngine.UI;

public class CanvasButtonToggleController : MonoBehaviour
{
    public Toggle toggle;
    public Button button;

    void Update()
    {
        // Überprüfe, ob toggle oder button null sind
        if (toggle == null || button == null)
        {
            Debug.LogError("Toggle or Button reference not set in CanvasButtonToggleController!");
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == toggle.gameObject)
                {
                    // Toggle wurde angeklickt
                    toggle.isOn = !toggle.isOn;
                }
                else if (hit.collider.gameObject == button.gameObject)
                {
                    // Überprüfe, ob das button-Objekt eine onClick-Zuweisung hat
                    if (button.onClick != null)
                    {
                        // Button wurde angeklickt
                        button.onClick.Invoke();
                    }
                }
            }
        }
    }
}
