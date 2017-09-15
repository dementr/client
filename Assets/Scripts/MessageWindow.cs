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
        messageText.color = messageColor;
        messageText.text = message;
        messageWindow.SetActive(true);
    }
}
