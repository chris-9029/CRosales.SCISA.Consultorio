using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Presentacion.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario usuario)
        {
            bool registrado;
            string mensaje;

            if (usuario.Correo == null || usuario.Clave == null || usuario.ConfirmarClave == null)
            {
                ViewData["Mensaje"] = "Por favor, ingrese la información correcta";
                return View();
            }
            else
            {
                if (usuario.Clave == usuario.ConfirmarClave)
                {
                    usuario.Clave = ConvertirMD5(usuario.Clave);

                }
                else
                {
                    ViewData["Mensaje"] = "Las contraseñas no coinciden";
                    return View();
                }
                using (SqlConnection conexion = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
                {
                    string query = "UsuarioRegister";
                    SqlCommand cmd = new SqlCommand(query, conexion);

                    cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("Clave", usuario.Clave);
                    cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                    registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();


                }

                ViewData["Mensaje"] = mensaje;

                if (registrado)
                {
                    return RedirectToAction("Login", "Usuario");
                }
                else
                {
                    return View();
                }
            }

        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            if (usuario.Correo == null || usuario.Clave == null)
            {
                ViewData["Mensaje"] = "Por favor ingrese un usuario y contraseña validos";
                return View();

            }
            else
            {
                usuario.Clave = ConvertirMD5(usuario.Clave);

                using (SqlConnection conexion = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
                {
                    string query = "UsuarioValidate";
                    SqlCommand cmd = new SqlCommand(query, conexion);

                    cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("Clave", usuario.Clave);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    usuario.IdUsuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                }

                if (usuario.IdUsuario != 0)
                {

                    HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(usuario));
                    //Session["usuario"] = usuario;

                    return RedirectToAction("Index", "Cita");
                }
                else
                {
                    ViewData["Mensaje"] = "usuario no encontrado";
                    return View();
                }
            }

        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");

        }

        public static string ConvertirMD5(string texto)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] dataBytes = System.Text.Encoding.ASCII.GetBytes(texto);
                byte[] hash = md5.ComputeHash(dataBytes);
                string result = BitConverter.ToString(hash).Replace("-", "").ToLower();

                return result;
            }
        }

    }
}
