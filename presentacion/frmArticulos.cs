using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace presentacion
{
    public partial class frmArticulos : Form
    {
        private List<Articulo> listaArticulos;
        public frmArticulos()
        {
            InitializeComponent();
        }

        private void frmArticulos_Load(object sender, EventArgs e)
        {
            cargar();
            cboCampo.Items.Add("Marca");
            cboCampo.Items.Add("Categoria");
            cboCampo.Items.Add("Precio");
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl);
            }
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pbxArticulo.Load("https://archive.org/download/placeholder-image//placeholder-image.jpg");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregar agregar = new frmAgregar();
            agregar.ShowDialog();
            cargar();
        }
        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;
                ocultarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmAgregar modificar = new frmAgregar(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Seguro desea eliminar?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    MessageBox.Show("Artículo eliminado exitosamente");
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            if (opcion == "Marca" || opcion == "Categoria")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene a ");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
            }

        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (validarFiltro())
                {
                    return;
                }

                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltro.Text;
                dgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmDetalle detalle = new frmDetalle(seleccionado);
            detalle.ShowDialog();
        }

        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada = new List<Articulo>();
            string filtro = txtFiltroRapido.Text;

            if (filtro.Length > 2)
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            else
                listaFiltrada = listaArticulos;

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
        }
        private void ocultarColumnas()
        {
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["Codigo"].Visible = false;
            dgvArticulos.Columns["Nombre"].Visible = false;
            dgvArticulos.Columns["Descripcion"].Visible = false;
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
        }
        private bool validarFiltro()
        {
            if (cboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un campo para filtrar");
                return true;
            }
            if (cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un criterio para filtrar");
                return true;
            }
            if (cboCampo.SelectedItem.ToString() == "Precio")
            {
                if (string.IsNullOrEmpty(txtFiltro.Text))
                {
                    MessageBox.Show("No puedes dejar el filtro vacío");
                    return true;
                }
                if (!(soloNumeros(txtFiltro.Text)))
                {
                    MessageBox.Show("Solo nros para filtrar por campo Precio");
                    return true;
                }
            }
            return false;

        }
        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cboCampo.Items.Clear();
            cboCriterio.Items.Clear();
            cboCampo.Items.Add("Marca");
            cboCampo.Items.Add("Categoria");
            cboCampo.Items.Add("Precio");
            txtFiltro.Text = "";
            cargar();
        }
    }
}
