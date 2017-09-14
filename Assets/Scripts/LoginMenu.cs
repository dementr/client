using UnityEngine.SceneManagement;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    NetworkMain network;

    SocketIOComponent socket;

    public Toggle isLocalToggle;

    private void Start()
    {
        network = NetworkMain.inst;
        socket = network.socket;

        socket.isLocal = isLocalToggle.isOn;
    }

    public void Login()
    {
        socket.Init();
        //network.Init();
        if (!socket.socket.IsConnected)
            return;

        socket.On("authOk", LoadProfileScene);
        socket.On("errors", LoginError);

        string logInfo = JsonUtility.ToJson(new LoginInfo(network.login, network.password));

        socket.Emit("auth", new JSONObject(logInfo));
    }

    public void LoginError(SocketIOEvent e)
    {
        Debug.Log("LoginError");
    }

    public void LoadProfileScene(SocketIOEvent e)
    {
        SceneManager.LoadScene("Profile");
    }

    void OnDisable()
    {
        socket.Off("authOk", LoadProfileScene);
        socket.Off("errors", LoginError);
    }
}
