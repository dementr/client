using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class Chat : MonoBehaviour
{
    SocketIOComponent socket;

    public InputField chatInput;
    public Text chatText;

    public Scrollbar vertivalScroll;

    void Start()
    {
        socket = NetworkMain.inst.socket;

        ActivateChatInput();
        Init();
    }

    void Init()
    {
        socket.On("smsgChat", ReciveChatMessage);
    }

    void ActivateChatInput()
    {
        chatInput.ActivateInputField();
    }

    public void SendChatMessage()
    {
        if (Input.GetKeyDown(KeyCode.Return) && chatInput.text != "")
        {
            string str = JsonUtility.ToJson(new SendedChatMsg(chatInput.text));
            socket.Emit("msgChat", new JSONObject(str));

            chatInput.text = null;

            ActivateChatInput();
        }
    }

    public void ReciveChatMessage(SocketIOEvent e)
    {
        RecivedChatMsg msg = RecivedChatMsg.FromJson(e.data.ToString());
        OnReciveChatMessage(msg.nick, msg.msg);
    }

    public void OnReciveChatMessage(string sender, string msg)
    {
        chatText.text += sender + ": " + msg + "\n";
    }
}
