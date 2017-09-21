using UnityEngine;
using SocketIO;

public class Registration : MonoBehaviour
{
    SocketIOComponent socket;

    public RegistrationInfo registrationInfo;

    void Awake()
    {
        socket = NetworkMain.inst.socket;
    }

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
        if (!registrationInfo.CheckPasswordCorrectness())
        {
            MessageWindow.inst.ShowError("Password length must be more than 6 characters \nPassword must contain at least 1 digit", TMPro.TextAlignmentOptions.MidlineLeft);

            return;
        }

        if (!registrationInfo.CheckPassordIdentity())
        {
            MessageWindow.inst.ShowError("Passwords do not match");

            return;
        }

        string json = JsonUtility.ToJson(new RegistrationInfoBase(registrationInfo));
        socket.Emit("reg", new JSONObject(json));
    }

    #region Callbacks

    void OnRegistrationSuccessful(SocketIOEvent e)
    {
        MessageWindow.inst.Show("Registration is successful", Color.green);
    }

    void OnRegistrationError(SocketIOEvent e)
    {
        MessageWindow.inst.ShowError("Registration error");
    }

    void OnEmaiError(SocketIOEvent e)
    {
        MessageWindow.inst.ShowError("This email occupied");
    }

    #endregion

    void OnEnable()
    {
        socket.On("regOk", OnRegistrationSuccessful);
        socket.On("regError", OnRegistrationError);
        socket.On("emailError", OnEmaiError);
    }

    void OnDisable()
    {
        socket.Off("regOk", OnRegistrationSuccessful);
        socket.Off("regError", OnRegistrationError);
        socket.Off("emailError", OnEmaiError);
    }
}
