using UnityEngine;
using TMPro;

public class TextReadout : MonoBehaviour
{
    public virtual void PrintToScreen(string text)
    {
        GetComponent<TextMeshProUGUI>().richText = true;
        GetComponent<TextMeshProUGUI>().SetText(text);
        //GetComponent<TextMeshProUGUI>().color = Color.red;

        //GetComponent<TextMeshProUGUI>().text = text;
    }

}
