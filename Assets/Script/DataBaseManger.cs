using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Data.SqlClient;


public class DataBaseManger : MonoBehaviour
{
    public static SqlConnection Connection;
    public static string ConString =
      //@"Data Source=ADAMS\SQLEXPRESS;
      //Initial Catalog=SomeCity;
      //Integrated Security=True";
      @"Server=ADAMS\SQLEXPRESS;
        Database=SomeCity;
        Integrated Security=True";
    void Start()
    {
        //Connection = new SqlConnection(ConString);
        //Connection.Open();

        //var q = "select * from OnGround";
        //var c = new SqlCommand(q, Connection);
        //var reader = c.ExecuteReader();

        //reader.Read();
        //Debug.Log(reader["ZPoz"]);

        //reader.Close();
        //Connection.Close();
    }

    public static void LoadAll()
    {

    }

}
