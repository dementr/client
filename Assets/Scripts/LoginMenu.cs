using UnityEngine.SceneManagement;
using UnityEngine;
using SocketIO;

public class LoginMenu : MonoBehaviour
{
    NetworkMain network;

    SocketIOComponent socket;

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

    public void OnAuthSuccessful(SocketIOEvent e)
    {
        SceneManager.LoadScene("Profile");
    }

    public void OnFirstConnection(SocketIOEvent e)
    {
        Debug.Log("First connection");
    }

    public void OnAuthError(SocketIOEvent e)
    {
        Debug.Log("AuthError");
    }

    void OnEnable()
    {
        socket.On("authOk", OnAuthSuccessful);
        socket.On("firstCon", OnFirstConnection);
        socket.On("authError", OnAuthError);
    }

    void OnDisable()
    {
        socket.Off("authOk", OnAuthSuccessful);
        socket.Off("firstCon", OnFirstConnection);
        socket.Off("authError", OnAuthError);
    }
}
