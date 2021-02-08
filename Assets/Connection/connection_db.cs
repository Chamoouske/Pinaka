using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class connection_db : MonoBehaviour
{
    string Usuario;

    //Configure aqui para acessar o banco de dados, pondo o servidor, nome do banco, usuario e senha do banco respectivamente
    MySqlConnection conn = new MySqlConnection("Server = localhost; Database=pinaka;Uid=root;Pwd=;");
    
    void Start()
    {
        conn.Open();
        Debug.Log("Logado com sucesso");

        MySqlCommand query = new MySqlCommand("SELECT * FROM usuarios", conn);
        MySqlDataReader reader = query.ExecuteReader();
        reader.Read();

        Usuario = reader.GetString(0);
        Debug.Log(Usuario);
    }

    
    void Update()
    {
        
    }

    public void Insert(string Usuario, string Senha)
    {
        conn.Open();
        MySqlCommand query = new MySqlCommand("INSERT INTO usuarios (Nome, Senha, Score) VALUES ("+Usuario+","+Senha+")");
        query.ExecuteNonQuery();
    }
}
