using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionData.Data.Utiles;
using AplicacionData.Dominio;
using System.ComponentModel.DataAnnotations;


namespace AplicacionData.Data
{
    public class ArticuloRepository : IArticuloRepository
    {
        //private SqlConnection _conn;
        private List<ParameterSQL> parameters;
        private DataHelper helper = DataHelper.GetInstance();
        public ArticuloRepository()
        {
            //_conn = helper.GetConnection();
            parameters = new List<ParameterSQL>();
        }
        private void clearParameters()
        {
            parameters.Clear();
        }
        public bool Delete(int id)
        {
            bool deleted = false;
            parameters.Add(new ParameterSQL("@id", id));
            if (helper.ExecuteSPDML("SP_DELETE", parameters) > 0)
            {
                deleted = true;
            }
            clearParameters();
            return deleted;
        }

        public List<Articulo> GetAll()
        {
            List<Articulo> list = new List<Articulo>();
            var t = helper.ExecuteSPQuery("SP_GET_ALL", parameters);
            int id;
            string descripcion = string.Empty;
            bool estado = false;
            if (t != null)
            {
                foreach (DataRow row in t.Rows)
                {
                    id = Convert.ToInt32(row["id_articulo"]);
                    descripcion = Convert.ToString(row["descripcion"]);
                    estado = Convert.ToInt32(row["estado"]) == 1;
                    list.Add(new Articulo(id, descripcion, estado));
                }
            }
            clearParameters();
            return list;
        }

        public bool Exists(int id)
        {
            bool result = false;
            parameters.Add(new ParameterSQL("@id", id));

            result = helper.ExecuteSPDML("SP_GET_BY_ID", parameters) == 1;
            
            parameters.Clear();
            return result;
        }

        public bool Edit(int id, string descripcion)
        {
            bool guardado = false;
            if (!Exists(id))
            {
                parameters.Add(new ParameterSQL("@id", id));
                parameters.Add(new ParameterSQL("@descripcion", descripcion));

                if (helper.ExecuteSPDML("SP_EDIT", parameters) == 1)
                {
                    guardado = true;
                }
            }
            clearParameters();
            return guardado;
        }
        public bool Save(Articulo articulo)
        {
            bool guardado = false;
            if (!Exists(articulo.Id))
            {
                parameters.Add(new ParameterSQL("@descripcion", articulo.Descripcion));

                if (helper.ExecuteSPDML("SP_SAVE", parameters) == 1)
                {
                    guardado = true;
                }
            }
            clearParameters();
            return guardado;
        }


    }
}
