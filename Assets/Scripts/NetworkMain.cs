#region License
/*
 * TestSocketIO.cs
 *
 * The MIT License
 *
 * Copyright (c) 2014 Fabio Panettieri
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion

using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class NetworkMain : MonoBehaviour
{
    public static NetworkMain inst;

    public SocketIOComponent socket;

    public Toggle isLocalToggle;

    public string login;
    public string password;
    public string nickname;

    public string onlineCount;

    public event System.Action<string> OnOnlineChanged = delegate { };

    void Awake()
    {
        inst = this;

        socket.isLocal = isLocalToggle.isOn;
    }

    public void Init()
    {
        //socket.On("open", TestOpen);
        //socket.On("error", TestError);
        //socket.On("close", TestClose);

        socket.On("countCon", (e) => { onlineCount = OnlineCount.FromJson(e.data.ToString()).count; OnOnlineChanged(onlineCount); });
    }

    #region Callbacks

    public void TestOpen(SocketIOEvent e)
    {
        Debug.Log("OPEN");
        //Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }

    public void TestError(SocketIOEvent e)
    {
        Debug.Log("ERROR");
        //Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
    }

    public void TestClose(SocketIOEvent e)
    {
        Debug.Log("CLOSE");
        //Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
    }

    #endregion
}