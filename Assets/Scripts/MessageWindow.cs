using UnityEngine;
using TMPro;

public class MessageWindow : MonoBehaviour
{
    public static MessageWindow inst;

    public GameObject messageWindow;
    public TMP_Text messageText;
    
    void Awake()
    {
        inst = this;
    }

    public void Show(string message, Color messageColor)
    {
        messageText.alignment =  TextAlignmentOptions.Midline;
        messageText.color = messageColor;
        messageText.text = message;
        messageWindow.SetActive(true);
    }

    public void ShowError(string message)
    {
        messageText.color = Color.red;
        messageText.alignment = TextAlignmentOptions.Midline;
        messageText.text = message;
        messageWindow.SetActive(true);
    }

    public void Show(string message, Color messageColor, TextAlignmentOptions alignment)
    {
        messageText.alignment = alignment;
        messageText.color = messageColor;
        messageText.text = message;
        messageWindow.SetActive(true);
    }

    public void ShowError(string message,TextAlignmentOptions alignment)
    {
        messageText.color = Color.red;
        messageText.alignment = alignment;
        messageText.text = message;
        messageWindow.SetActive(true);
    }


}
