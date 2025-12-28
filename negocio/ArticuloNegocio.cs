using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;


namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select A.Id, Codigo, Nombre, A.Descripcion, ImagenUrl, M.Descripcion Marca, C.Descripcion Categoria, Precio, A.IdMarca, A.IdCategoria From ARTICULOS A, MARCAS M, CATEGORIAS C Where A.IdMarca = M.Id And A.IdCategoria = C.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    
                    if(!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    if(!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = Math.Round(Convert.ToDecimal(datos.Lector["Precio"]),2);

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void agregar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@cod, @nomb, @descr, @idmarca, @idcat, @img, @prec)");
                datos.setearParametros("@cod", articulo.Codigo);
                datos.setearParametros("@nomb", articulo.Nombre);
                datos.setearParametros("@descr", articulo.Descripcion);
                datos.setearParametros("@idmarca", articulo.Marca.Id);
                datos.setearParametros("@idcat", articulo.Categoria.Id);
                datos.setearParametros("@img", articulo.ImagenUrl);
                datos.setearParametros("@prec", articulo.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update ARTICULOS set Codigo = @cod, Nombre = @nomb, Descripcion = @desc, ImagenUrl = @img, IdMarca = @idmarca, IdCategoria = @idcat where Id = @id");
                datos.setearParametros("@cod", articulo.Codigo);
                datos.setearParametros("@nomb", articulo.Nombre);
                datos.setearParametros("@desc", articulo.Descripcion);
                datos.setearParametros("@img", articulo.ImagenUrl);
                datos.setearParametros("@idmarca", articulo.Marca.Id);
                datos.setearParametros("@idcat", articulo.Categoria.Id);
                datos.setearParametros("@id", articulo.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Delete from ARTICULOS where Id=@id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Select A.Id, Codigo, Nombre, A.Descripcion, ImagenUrl, M.Descripcion Marca, C.Descripcion Categoria, Precio, A.IdMarca, A.IdCategoria From ARTICULOS A, MARCAS M, CATEGORIAS C Where A.IdMarca = M.Id And A.IdCategoria = C.Id and ";
                if (campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "M.Descripcion like '"+ filtro +"%'";
                            break;
                        case "Termina con":
                            consulta+="M.Descripcion like '%"+ filtro +"'";
                            break;
                        default:
                            consulta += "M.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Categoria")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "C.Descripcion like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "C.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "C.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Precio >" + filtro;
                            break;
                        default:
                            consulta += "Precio <" + filtro;
                            break;
                    }
                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = Math.Round(Convert.ToDecimal(datos.Lector["Precio"]), 2);

                    lista.Add(aux);
                }
                
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Articulo> listaDetalle(int id)
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select A.Id, Codigo, Nombre, A.Descripcion, ImagenUrl, M.Descripcion Marca, C.Descripcion Categoria, Precio, A.IdMarca, A.IdCategoria From ARTICULOS A, MARCAS M, CATEGORIAS C Where A.IdMarca = M.Id And A.IdCategoria = C.Id and A.Id = @id ");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Precio = Math.Round(Convert.ToDecimal(datos.Lector["Precio"]), 2);

                    listaArticulos.Add(aux);

                }
                
                return listaArticulos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
