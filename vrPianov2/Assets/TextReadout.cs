using UnityEngine;
using TMPro;

public class TextReadout : MonoBehaviour
{
    public void PrintToScreen(string text)
    {
        GetComponent<TextMeshProUGUI>().text = text;
    }

}
