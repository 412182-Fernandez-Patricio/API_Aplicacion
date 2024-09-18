using AplicacionData.Dominio;
namespace API_Aplicacion.Services
{
    public interface IAplicacion
    {
        List<Articulo> Consultar();
        bool Editar(int id, string descripcion);
        bool Agregar(Articulo articulo);
        bool RegistarBaja(int id);
    }
}
