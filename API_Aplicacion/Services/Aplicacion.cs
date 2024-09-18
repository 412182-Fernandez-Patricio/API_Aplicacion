using AplicacionData.Dominio;
using AplicacionData.Data;

namespace API_Aplicacion.Services
{
    public class Aplicacion : IAplicacion
    {
        private static IArticuloRepository articuloRepository;
        public Aplicacion() 
        {
            articuloRepository = new ArticuloRepository();
        }
        public bool Agregar(Articulo articulo)
        {
            return articuloRepository.Save(articulo);
        }

        public List<Articulo> Consultar()
        {
            return articuloRepository.GetAll();
        }

        public bool Editar(int id, string descripcion)
        {
            return articuloRepository.Edit(id, descripcion);
        }

        public bool RegistarBaja(int id)
        {
            return articuloRepository.Delete(id);
        }
    }
}
