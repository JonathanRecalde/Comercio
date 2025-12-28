using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using presentacion;
using System.Windows.Forms;

namespace metodos
{
    class CargarImagen
    {
        private void cargarImagen(string imagen, frmArticulos pbxArticulo)
        {
            frmArticulos objeto = new frmArticulos();
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pbxArticulo.Load("https://archive.org/download/placeholder-image//placeholder-image.jpg");
            }
        }
    }
}
