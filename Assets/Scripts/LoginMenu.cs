using UnityEngine.SceneManagement;
using UnityEngine;
using SocketIO;

public class LoginMenu : MonoBehaviour
{
    NetworkMain network;

    SocketIOComponent socket;

    public GameObject sendNicknamePanel;

    void Awake()
    {
        network = NetworkMain.inst;
        socket = network.socket;
    }

    #region Set login info

    public void SetLogin(string _login)
    {
        network.login = _login;
    }

    public void SetPassword(string _password)
    {
        network.password = _password;
    }

    public void SetNickname(string nickname)
    {
        NetworkMain.inst.nickname = nickname;
    }

    #endregion

    public void Login()
    {
        //network.Init();

        string logInfo = JsonUtility.ToJson(new LoginInfo(network.login, network.password));

        socket.Emit("auth", new JSONObject(logInfo));
    }

    public void SendNickname()
    {
        string nickJson = "{ \"nick\": \"" + network.nickname + "\" }";
        socket.Emit("setNick", new JSONObject(nickJson));
    }

    #region Callbacks

    void OnAuthSuccessful(SocketIOEvent e)
    {
        AuthInfo info = AuthInfo.FromJson(e.data.ToString());

        if (info.nick == "null")
        {
            ShowSetNickWindow();
        }
        else
        {
            NetworkMain.inst.nickname = info.nick;
            SceneManager.LoadScene("Profile");
        }
    }

    void OnAuthError(SocketIOEvent e)
    {
        MessageWindow.inst.ShowError("Password or login incorrect");
    }

    void OnNickSet(SocketIOEvent e)
    {
        SetNickInfo info = SetNickInfo.FromJson(e.data.ToString());

        switch (info.result)
        {
            case "ready": SceneManager.LoadScene("Profile"); break;
            case "busy": MessageWindow.inst.ShowError("This nickname is occupied"); break;
            case "error": MessageWindow.inst.ShowError("Error"); break;
        }
    }

    #endregion

    void ShowSetNickWindow()
    {
        sendNicknamePanel.SetActive(true);
        sendNicknamePanel.transform.parent.gameObject.SetActive(true);
    }

    void OnEnable()
    {
        socket.On("authOk", OnAuthSuccessful);
        socket.On("setNick", OnNickSet);
        socket.On("authError", OnAuthError);
    }

    void OnDisable()
    {
        socket.Off("authOk", OnAuthSuccessful);
        socket.Off("setNick", OnNickSet);
        socket.Off("authError", OnAuthError);
    }
}
