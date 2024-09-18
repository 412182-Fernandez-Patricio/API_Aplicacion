using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionData.Dominio
{
    public class Articulo
    {
        private int id;
        private string? descripcion;
        private bool estado;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string? Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public Articulo()
        {
            id = 0;
            descripcion = string.Empty;
            estado = true;
        }
        public Articulo(int id, string descripcion, bool estado)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.estado = estado;
        }
    }
}
