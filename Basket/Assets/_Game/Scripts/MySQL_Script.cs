using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class MySQL_Script : MonoBehaviour
{

    private string linhaConn;
    private MySqlConnection conn;

    // Start is called before the first frame update
    void Start()
    {
        linhaConn = "Server=localhost;"+
        "Database=unity;"+
        "User ID=root;"+
        "Password=;"+
        "Pooling=false";

        ConnDatabase(linhaConn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ConnDatabase(string lConn)
    {
        conn = new MySqlConnection(lConn);
        conn.Open();
        print("Conectado");
    }
}
