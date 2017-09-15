using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class Registration : MonoBehaviour
{
    SocketIOComponent socket;

    public RegistrationInfo registrationInfo;

    #region SetRegistrationInfo

    public void SetEmail(string _email)
    {
        registrationInfo.email = _email;
    }

    public void SetPassword(string _password)
    {
        registrationInfo.password = _password;
    }

    public void SetConfirmPassword(string _password)
    {
        registrationInfo.confirmPassword = _password;
    }

    #endregion

    public void SendRegistrationInfo()
    {
        string json = JsonUtility.ToJson(registrationInfo);
        socket.Emit("reg", new JSONObject(json));
    }

    void OnRegistrationSuccessful(SocketIOEvent e)
    {
        MessageWindow.inst.Show("Registration is successful", Color.green);
    }

    void OnRegistrationError(SocketIOEvent e)
    {
        MessageWindow.inst.Show("Registration error", Color.red);
    }

    bool CheckPassord()
    {
        if (registrationInfo.password == registrationInfo.confirmPassword)
            return true;

        return false;
    }

    void OnEnable()
    {
        socket = NetworkMain.inst.socket;

        socket.On("regOk", OnRegistrationSuccessful);
        socket.On("regError", OnRegistrationError);
    }

    void OnDisable()
    {
        socket.Off("regOk", OnRegistrationSuccessful);
        socket.Off("regError", OnRegistrationError);
    }
}
