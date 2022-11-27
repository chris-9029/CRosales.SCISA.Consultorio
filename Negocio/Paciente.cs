using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? FechaNacimiento { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Altura { get; set; }
        public string? Foto { get; set; }

        public List<object>? pacientes { get; set; }

        public static Result GetAll()
        {
            Result result = new Result();

            try
            {
                using (SqlConnection con = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
                {
                    string querySP = "PacienteGetAll";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = querySP;
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable tablaPaciente = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tablaPaciente);

                        if (tablaPaciente.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaPaciente.Rows)
                            {
                                Paciente paciente = new Paciente();
                                paciente.IdPaciente = int.Parse(row[0].ToString());
                                paciente.Nombre = row[1].ToString();
                                paciente.ApellidoPaterno = row[2].ToString();
                                paciente.ApellidoMaterno = row[3].ToString();
                                paciente.FechaNacimiento = (DateTime.Parse(row[4].ToString())).ToString("dd-MM-yyyy");
                                paciente.Peso = decimal.Parse(row[5].ToString());
                                paciente.Altura = decimal.Parse(row[6].ToString());
                                paciente.Foto = row[7].ToString();

                                result.Objects.Add(paciente);

                            }

                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontro la información solicitada";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;

        }

        public static Result GetById(int idPaciente)
        {
            Result result = new Result();

            try
            {
                using (SqlConnection context = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
                {
                    string querySP = "[PacienteGetById] @IdPaciente";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = querySP;
                        cmd.Parameters.Add("@IdPaciente", SqlDbType.Int);
                        cmd.Parameters["@IdPaciente"].Value = idPaciente;
                        cmd.Connection.Open();
                        cmd.ExecuteScalar();
                        Paciente paciente = new Paciente();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {

                                paciente.IdPaciente = int.Parse(dr[0].ToString());
                                paciente.Nombre = dr[1].ToString();
                                paciente.ApellidoPaterno = dr[2].ToString();
                                paciente.ApellidoMaterno = dr[3].ToString();
                                paciente.FechaNacimiento = (DateTime.Parse(dr[4].ToString())).ToString("dd-MM-yyyy");
                                paciente.Peso = decimal.Parse(dr[5].ToString());
                                paciente.Altura = decimal.Parse(dr[6].ToString());
                                paciente.Foto = dr[7].ToString();

                                result.Object = paciente;
                                result.Correct = true;

                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No se encontro la información solicitada";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;

        }

        public static Result Add(Paciente paciente)
        {
            Result result = new Result();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
                {
                    string query = "PacienteAdd";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = oconexion;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[7];
                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = paciente.Nombre;
                        collection[1] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = paciente.ApellidoPaterno;
                        collection[2] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[2].Value = paciente.ApellidoMaterno;
                        collection[3] = new SqlParameter("FechaNacimiento", SqlDbType.Date);
                        collection[3].Value =  DateTime.Parse(paciente.FechaNacimiento);
                        collection[4] = new SqlParameter("Peso", SqlDbType.Decimal);
                        collection[4].Value = paciente.Peso;
                        collection[5] = new SqlParameter("Altura", SqlDbType.Decimal);
                        collection[5].Value = paciente.Altura;
                        collection[6] = new SqlParameter("Foto", SqlDbType.VarChar);
                        collection[6].Value = paciente.Foto;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrio un error al registrar";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static Result Update(Paciente paciente)
        {
            Result result = new Result();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
                {
                    string query = "PacienteUpdate";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = oconexion;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[8];
                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = paciente.Nombre;
                        collection[1] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = paciente.ApellidoPaterno;
                        collection[2] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[2].Value = paciente.ApellidoMaterno;
                        collection[3] = new SqlParameter("FechaNacimiento", SqlDbType.Date);
                        collection[3].Value = DateTime.Parse(paciente.FechaNacimiento);
                        collection[4] = new SqlParameter("Peso", SqlDbType.Decimal);
                        collection[4].Value = paciente.Peso;
                        collection[5] = new SqlParameter("Altura", SqlDbType.Decimal);
                        collection[5].Value = paciente.Altura;
                        collection[6] = new SqlParameter("Foto", SqlDbType.VarChar);
                        collection[6].Value = paciente.Foto;
                        collection[7] = new SqlParameter("IdPaciente", SqlDbType.Int);
                        collection[7].Value = paciente.IdPaciente;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrio un error al actualizar el registro";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static Result Delete(int idPaciente)
        {
            Result result = new Result();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
                {
                    string query = "PacienteDelete";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = oconexion;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("IdPaciente", SqlDbType.Int);
                        collection[0].Value = idPaciente;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrio un error al Eliminar el registro";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}
