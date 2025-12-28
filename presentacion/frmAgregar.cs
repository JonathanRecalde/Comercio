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
using System.IO;
using System.Configuration;

namespace presentacion
{
    public partial class frmAgregar : Form
    {
        private OpenFileDialog archivo = null;
        private Articulo articulo = null;
        public frmAgregar()
        {
            InitializeComponent();
        }
        public frmAgregar(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (articulo == null)
                {
                    articulo = new Articulo();
                }
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;

                if (archivo != null && !(txtImagen.Text.ToUpper().Contains("HTTP")))
                    articulo.ImagenUrl = ConfigurationManager.AppSettings["App-c#Nivel2"] + archivo.SafeFileName;
                else
                    articulo.ImagenUrl = txtImagen.Text;

                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;

                if (validarAceptar())
                    return;
                
                articulo.Precio = Math.Round(Convert.ToDecimal(txtPrecio.Text), 2);

                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }
                if(archivo!=null && !(txtImagen.Text.ToUpper().Contains("HTTP")))
                {
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["App-c#Nivel2"] + archivo.SafeFileName);
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool validarAceptar()
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Debes cargar el campo Código");
                return true;
            }
            if (cboMarca.SelectedIndex < 0)
            {
                MessageBox.Show("Debes seleccionar el campo Marca");
                return true;
            }
            if (cboCategoria.SelectedIndex < 0)
            {
                MessageBox.Show("Debes seleccionar el campo Categoría");
                return true;
            }
            if (txtPrecio.Text == "")
            {
                MessageBox.Show("Debes cargar el campo Precio");
                return true;
            }
            else if (!(soloNumeros(txtPrecio.Text)))
            {
                MessageBox.Show("Ingrese solo nros en el campo Precio");
                return true;
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
        private void frmAgregar_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {

                cboMarca.DataSource=marcaNegocio.listar();
                cboCategoria.DataSource = categoriaNegocio.listar();

                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                cboMarca.SelectedIndex = -1;
                cboCategoria.SelectedIndex = -1;

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtImagen.Text = articulo.ImagenUrl;
                    cargarImagen(articulo.ImagenUrl);
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    txtPrecio.Text = articulo.Precio.ToString();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxCargarImagen.Load(imagen);
            }
            catch (Exception)
            {
                pbxCargarImagen.Load("https://archive.org/download/placeholder-image//placeholder-image.jpg");
            }
        }
        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }

        private void btnImagenLocal_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg";
            archivo.ShowDialog();
            if (archivo.ShowDialog()== DialogResult.OK)
            {
                txtImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);

                string fecha = DateTime.Now.ToString();
                //File.Copy(archivo.FileName, ConfigurationManager.AppSettings["App-c#Nivel2"] + archivo.SafeFileName);   
            }
        }
    }
}
