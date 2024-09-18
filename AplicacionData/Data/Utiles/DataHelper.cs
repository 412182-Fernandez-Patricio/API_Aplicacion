using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace AplicacionData.Data.Utiles
{
    public class DataHelper
    {
        private static DataHelper? _instancia = null;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.cnnString);
        }

        public static DataHelper GetInstance()
        {
            if (_instancia == null)
                _instancia = new DataHelper();

            return _instancia;
        }

        public DataTable? ExecuteSPQuery(string sp, List<ParameterSQL>? parametros)
        {
            DataTable? t;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }
                t = new DataTable();
                t.Load(cmd.ExecuteReader());
                _connection.Close();
            }
            catch (SqlException)
            {
                t = null;
            }

            return t;
        }


        public int ExecuteSPDML(string sp, List<ParameterSQL> parametros) //CAMBIE EL List<ParameterSQL>? parametros por List<ParameterSQL> parametros (no puede ser nulo)
        {
            int rows;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parametros.Any()) //item a linea 54 (public int ExecuteSPDML) cambie parametros != null por parametros.Any(), para saber si no esta vacia
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }

                rows = cmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (SqlException)
            {
                rows = 0;
            }

            return rows;
        }



        /*public int ExecuteSPDMLTransact(string sp, List<ParameterSQL>? parametros, SqlConnection conn, object? parameterOut = null)
        {
            return 0;
        }*/

        public SqlConnection GetConnection()
        {
            //devolver una coneccion
            return _connection;
        }

    }
}
