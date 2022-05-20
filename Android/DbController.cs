using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
public class DbController : MonoBehaviour
{
    //Crea la base de datos en la carpeta del archivo y le pone el nombre. En este caso se llamará michiland.db
    private string dbName = "URI=file:michiland.db";
    private SqliteConnection connection;

    // Start is called before the first frame update
    void Start()
    {
        CreateDb();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Conectar()
    {
        connection = new SqliteConnection(dbName);

        try
        {
            connection.Open();
            Debug.Log("Conexión con BBDD exitosa");
        }
        catch (SqliteException error)
        {
            Debug.LogError("imposible conectar con base de datos: " + error);
        }
    }

    public void CreateDb()
    {
        //Crea la conexión a la base de datos
        Conectar();
        using (var command = connection.CreateCommand())
        {
            //Crea la tabla de usuarios si no existe 
            command.CommandText = "CREATE TABLE IF NOT EXISTS usuarios (id INTEGER PRIMARY KEY AUTOINCREMENT, usuario TEXT NOT NULL, " +
                "pass TEXT NOT NULL);";
            command.ExecuteNonQuery();
        }

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "CREATE TABLE IF NOT EXISTS progresos (id INTEGER PRIMARY KEY AUTOINCREMENT, id_usuario INTEGER  NOT NULL, " +
                "escena TEXT NOT NULL, posX TEXT NOT NULL, posY TEXT NOT NULL, llave1 INTEGER CHECK(llave1 IN(0,1))," +
                " llave2 INTEGER CHECK(llave2 IN (0,1)), llave3 INTEGER CHECK (llave3 IN(0,1))," +
                " FOREIGN KEY(id_usuario) REFERENCES usuarios (id));";
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    //Crea un nuevo usuario
    public void AddUser(string usuario, string pass)
    {
        Conectar();
        using (var command = connection.CreateCommand())
        {
            //Crea el comando de inserción en la tabla y lo ejecuta
            command.CommandText = "INSERT INTO usuarios (usuario, pass) VALUES ('" + usuario + "', '" + pass + "');";
            command.ExecuteNonQuery();
        }
        //Cierra la conexión con la base de datos

        connection.Close();
    }

    public SqliteDataReader Select(string select)
    {
        Conectar();
        //Crea un comando para realizar las queries
        SqliteCommand cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM " + select;
        //Almacena el resultado de la consulta en la variable
        SqliteDataReader resultado = cmd.ExecuteReader();
        return resultado;
    }

    //Inserta nuevos registros en la tabla de progresos. Primer guardado de una partida
    public void InsertarProgreso(int idUser, string escena, string posX, string posY, int llave1, int llave2, int llave3)
    {
        Conectar();
        //Crea el commando a insertar en la base de datos
        SqliteCommand cmd = connection.CreateCommand();
        cmd.CommandText = "INSERT INTO progresos (id_usuario, escena, posX, posY, llave1, llave2, llave3) VALUES(" + idUser +
            ",'" + escena + "','" + posX + "','" + posY + "'," + llave1 + "," + llave2 + "," + llave3 + ");";
        //Ejecutamos el comando
        cmd.ExecuteNonQuery();

        connection.Close();
    }

    //Actualiza registros de la tabla progresos. Sobreescribe los datos guardados anteriormente.
    public void ActualizaProgreso(int idUser, string escena, string posX, string posY, int llave1, int llave2, int llave3)
    {
        Conectar();

        //Crea el comando para actualizar la base de datos
        SqliteCommand cmd = connection.CreateCommand();
        cmd.CommandText = "UPDATE progresos SET escena = '" + escena + "', posX='" + posX + "',posY='" + posY + "',llave1=" + llave1 +
            ",llave2=" + llave2 + ",llave3 = " + llave3 + " WHERE id_usuario = " + idUser + ";";

        //Ejecutamos el comando
        cmd.ExecuteNonQuery();

        connection.Close();
    }

    public int ObtenerIdUser(string usuario)
    {
        Conectar();

        //Crea la query para buscar el id del usuario
        SqliteCommand cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT id FROM usuarios WHERE usuario LIKE '" + usuario + "';";

        //Se ejecuta el comando y se almacena el resultado
        SqliteDataReader resultado = cmd.ExecuteReader();

        resultado.Read();

        int resul = resultado.GetInt32(0);
        resultado.Close();

        return resul;
    }

}
