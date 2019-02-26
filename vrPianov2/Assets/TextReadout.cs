using UnityEngine;
using TMPro;

public class TextReadout : MonoBehaviour
{
    public virtual void PrintToScreen(string text)
    {
        GetComponent<TextMeshProUGUI>().text = text;
    }

}
