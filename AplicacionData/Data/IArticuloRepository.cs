using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionData.Dominio;

namespace AplicacionData.Data
{
    public interface IArticuloRepository
    {
        List<Articulo> GetAll();
        bool Exists(int id);
        bool Edit(int id, string descripcion);
        bool Save(Articulo articulo);
        bool Delete(int id);

    }
}
