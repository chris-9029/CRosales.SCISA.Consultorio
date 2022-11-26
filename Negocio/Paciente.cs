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
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public decimal Peso { get; set; }
        public decimal Altura { get; set; }
        public string Foto { get; set; }

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
                                paciente.FechaNacimiento = DateTime.Parse(row[4].ToString());
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
                                paciente.FechaNacimiento = DateTime.Parse(dr[4].ToString());
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
                    string query = "DoctorAdd";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = oconexion;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        //SqlParameter[] collection = new SqlParameter[5];
                        //collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        //collection[0].Value = doctor.Nombre;
                        //collection[1] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        //collection[1].Value = doctor.ApellidoPaterno;
                        //collection[2] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        //collection[2].Value = doctor.ApellidoMaterno;
                        //collection[3] = new SqlParameter("Cedula", SqlDbType.VarChar);
                        //collection[3].Value = doctor.Cedula;
                        //collection[4] = new SqlParameter("Foto", SqlDbType.VarChar);
                        //collection[4].Value = doctor.Foto;

                        //cmd.Parameters.AddRange(collection);
                        //cmd.Connection.Open();

                        //int RowsAffected = cmd.ExecuteNonQuery();

                        //if (RowsAffected > 0)
                        //{
                        //    result.Correct = true;
                        //}
                        //else
                        //{
                        //    result.Correct = false;
                        //    result.ErrorMessage = "Ocurrio un error al registrar";
                        //}
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
