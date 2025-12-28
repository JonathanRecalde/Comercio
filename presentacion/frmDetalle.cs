using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using dominio;

namespace presentacion
{
    public partial class frmDetalle : Form
    {
        private List<Articulo> listaArticulos = new List<Articulo>();
        private Articulo articulo;
        public frmDetalle()
        {
            InitializeComponent();
        }
        public frmDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void frmDetalle_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulos = negocio.listaDetalle(articulo.Id);
                dgvArticuloDetalles.DataSource = listaArticulos;
                dgvArticuloDetalles.Columns["Id"].Visible = false;
                dgvArticuloDetalles.Columns["ImagenUrl"].Visible = false;
                cargarImagen(articulo.ImagenUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxDetalle.Load(imagen);
            }
            catch (Exception)
            {
                pbxDetalle.Load("https://archive.org/download/placeholder-image//placeholder-image.jpg");
            }
        }
    }
}
