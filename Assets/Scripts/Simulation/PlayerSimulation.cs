using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimulation : MonoBehaviour
{
    public ServerSimulation server;

    Rigidbody myRb;

    public float speed;

    void Awake()
    {
        myRb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        server.SetStartPos(this);
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, 0, z).normalized * speed * Time.deltaTime;

        server.InputFromPlayer(dir, myRb.position);

        myRb.MovePosition(myRb.position + dir);
    }

    public void RecivePos(Vector3 pos)
    {
        transform.position = pos;
    }
}
