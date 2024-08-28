using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ToggleColorChanger2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Toggle toggle;
    public Color selectedColor = Color.red;
    public Color unselectedColor = Color.white;
    public Color hoverColor = Color.yellow;

    public Image ImgKnob;
    public Sprite imageKnobOn;
    public Sprite imageKnobOff;

    public Image ImgBackground;
    public Text Label;

    void Start()
    {
        // Füge die Methode ToggleValueChanged als Eventhandler für den Toggle-Button hinzu
        toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(); });

        // Füge diese Klasse als Eventhandler für PointerEnter und PointerExit hinzu
        EventTrigger trigger = toggle.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = toggle.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry pointerEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        pointerEnter.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        trigger.triggers.Add(pointerEnter);

        EventTrigger.Entry pointerExit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        pointerExit.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });
        trigger.triggers.Add(pointerExit);

        // Initialisiere die Farbe basierend auf dem Startzustand
        SetToggleColor();
        SetImageColor(unselectedColor);
    }

    void ToggleValueChanged()
    {
        // Hier kannst du die Logik für die Zustandsänderung implementieren
        SetToggleColor();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Hier kannst du die Logik für das Hover-Event implementieren
        SetHoverColor();
        SetImageColor(hoverColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hier kannst du die Logik für das Exit-Event implementieren
        SetToggleColor();
        //SetImageColor(unselectedColor);
    }

    void SetToggleColor()
    {
        // Ändere die Farbe basierend auf dem Zustand des Toggle-Buttons
        //ColorBlock colors = toggle.colors;
        ColorBlock colors = toggle.colors;


        if (toggle.isOn)
        {
            /*colors.normalColor = selectedColor;
            colors.highlightedColor = hoverColor; // Ändere auf hoverColor beim Hover
            colors.pressedColor = selectedColor;
            colors.selectedColor = selectedColor;*/

            ImgKnob.sprite = imageKnobOn;
            
            ImgBackground.color = selectedColor;
            Label.color = unselectedColor;
        }
        else
        {
            /*colors.normalColor = unselectedColor;
            colors.highlightedColor = hoverColor; // Ändere auf hoverColor beim Hover
            colors.pressedColor = unselectedColor;
            colors.selectedColor = unselectedColor;*/

            ImgKnob.sprite = imageKnobOff;

            ImgBackground.color = unselectedColor;
            Label.color = selectedColor;
        }

        toggle.colors = colors;
    }

    void SetHoverColor()
    {
        // Setze die Farbe auf hoverColor, wenn der Mauszeiger darüber schwebt
        //ColorBlock colors = toggle.colors;
        //colors.normalColor = hoverColor;
        //toggle.colors = colors;
        ImgKnob.color = Color.white;
    }

    void SetImageColor(Color color)
    {
        // Ändere die Farbe des Image-Komponenten
        if (ImgBackground != null)
        {
            ImgBackground.color = color;
        }
    }
}
