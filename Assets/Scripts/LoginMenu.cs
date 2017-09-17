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
        socket.Emit("nick", new JSONObject(nickJson));
    }

    public void OnAuthSuccessful(SocketIOEvent e)
    {
        SceneManager.LoadScene("Profile");
    }

    public void OnFirstConnection(SocketIOEvent e)
    {
        sendNicknamePanel.SetActive(true);
    }

    public void OnAuthError(SocketIOEvent e)
    {
        MessageWindow.inst.ShowError("Password or login incorrect");
    }

    public void OnNickError(SocketIOEvent e)
    {
        MessageWindow.inst.ShowError("Nick error");
    }

    void OnEnable()
    {
        socket.On("authOk", OnAuthSuccessful);
        socket.On("firstCon", OnFirstConnection);
        socket.On("nickError", OnNickError);
        socket.On("authError", OnAuthError);
    }

    void OnDisable()
    {
        socket.Off("authOk", OnAuthSuccessful);
        socket.Off("firstCon", OnFirstConnection);
        socket.Off("nickError", OnNickError);
        socket.Off("authError", OnAuthError);
    }
}
