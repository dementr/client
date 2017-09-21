using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class Chat : MonoBehaviour
{
    SocketIOComponent socket;

    public InputField chatInput;
    public Text chatText;

    public Scrollbar vertivalScroll;

    void Awake()
    {
        ActivateChatInput();
    }

    public void SendChatMessage()
    {
        if (Input.GetKeyDown(KeyCode.Return) && chatInput.text != "")
        {
            string str = JsonUtility.ToJson(new SendedChatMsg(chatInput.text));
            socket.Emit("msgChat", new JSONObject(str));

            chatInput.text = null;

            ActivateChatInput();

            Invoke("SetChatPos", 0.15f);
        }
    }

    public void ReciveChatMessage(SocketIOEvent e)
    {
        RecivedChatMsg msg = RecivedChatMsg.FromJson(e.data.ToString());
        OnReciveChatMessage(msg.nick, msg.msg);
    }

    public void OnReciveChatMessage(string sender, string msg)
    {
        string dataTimeNow = System.DateTime.Now.ToString();
        string time = dataTimeNow.Split(new char[] { ' ' }, 3)[1];

        chatText.text += time + " " + sender + ": " + msg + "\n";
    }

    void ActivateChatInput()
    {
        chatInput.ActivateInputField();
    }

    void SetChatPos()
    {
        vertivalScroll.value = 0;
    }

    void OnEnable()
    {
        socket = NetworkMain.inst.socket;
        socket.On("msgChat", ReciveChatMessage);
    }

    void OnDisable()
    {
        socket.Off("msgChat", ReciveChatMessage);
    }
}
