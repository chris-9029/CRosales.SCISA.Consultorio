using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Cita
    {
        public int IdCita { get; set; }
        //public int IdDoctor { get; set; }
        //public int IdPaciente { get; set; }
        public string? Detalle { get; set; }
        public string? Fecha { get; set; }

        public Negocio.Doctor doctor { get; set; }
        public Negocio.Paciente paciente { get; set; }

        public List<object> citas { get; set; }

        public static Result Add(Cita cita)
        {
            Result result = new Result();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
                {
                    string query = "PacienteCitaAdd";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = oconexion;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[10];
                        collection[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                        collection[0].Value = cita.paciente.Nombre;
                        collection[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = cita.paciente.ApellidoPaterno;
                        collection[2] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
                        collection[2].Value = cita.paciente.ApellidoMaterno;
                        collection[3] = new SqlParameter("@FechaNacimiento", SqlDbType.Date);
                        collection[3].Value = DateTime.Parse(cita.paciente.FechaNacimiento.ToString());
                        collection[4] = new SqlParameter("@Peso", SqlDbType.Decimal);
                        collection[4].Value = cita.paciente.Peso;
                        collection[5] = new SqlParameter("@Altura", SqlDbType.Decimal);
                        collection[5].Value = cita.paciente.Altura;
                        collection[6] = new SqlParameter("@Foto", SqlDbType.VarChar);
                        collection[6].Value = cita.paciente.Foto;

                        collection[7] = new SqlParameter("@IdDoctor", SqlDbType.Int);
                        collection[7].Value = cita.doctor.IdDoctor;


                        collection[8] = new SqlParameter("@Detalle", SqlDbType.VarChar);
                        collection[8].Value = cita.Detalle;
                        collection[9] = new SqlParameter("@Fecha", SqlDbType.DateTime);
                        collection[9].Value = DateTime.Parse(cita.Fecha.ToString());

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

        public static Result CitaList()
        {
            Result result = new Result();

            try
            {
                using (SqlConnection con = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
                {
                    string querySP = "CitaList";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = querySP;
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable tablaCita = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tablaCita);

                        if (tablaCita.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaCita.Rows)
                            {
                                Cita cita = new Cita();
                                cita.IdCita = int.Parse(row[0].ToString());
                                cita.doctor = new Negocio.Doctor();
                                cita.doctor.IdDoctor = int.Parse(row[1].ToString());
                                cita.doctor.Nombre = row[2].ToString();
                                cita.doctor.ApellidoPaterno = row[3].ToString();
                                cita.doctor.ApellidoMaterno = row[4].ToString();

                                cita.paciente = new Paciente();
                                cita.paciente.IdPaciente = int.Parse(row[5].ToString());
                                cita.paciente.Nombre = row[6].ToString();
                                cita.paciente.ApellidoPaterno = row[7].ToString();
                                cita.paciente.ApellidoMaterno = row[8].ToString();

                                cita.Detalle = row[9].ToString();
                                cita.Fecha = row[10].ToString();

                                result.Objects.Add(cita);

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


        public static Result Delete(int idCita)
        {
            Result result = new Result();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
                {
                    string query = "CitaDelete";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = oconexion;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("@IdCita", SqlDbType.Int);
                        collection[0].Value = idCita;

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

        //public static Result GetAll()
        //{
        //    Result result = new Result();

        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
        //        {
        //            string querySP = "CitaGetAll";

        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandText = querySP;
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                DataTable tablaCita = new DataTable();

        //                SqlDataAdapter da = new SqlDataAdapter(cmd);
        //                da.Fill(tablaCita);

        //                if (tablaCita.Rows.Count > 0)
        //                {
        //                    result.Objects = new List<object>();
        //                    foreach (DataRow row in tablaCita.Rows)
        //                    {
        //                        Cita cita = new Cita();
        //                        cita.IdCita = int.Parse(row[0].ToString());
        //                        cita.doctor= new Negocio.Doctor();
        //                        cita.doctor.IdDoctor = int.Parse(row[1].ToString());
        //                        cita.paciente= new Paciente();
        //                        cita.paciente.IdPaciente = int.Parse(row[2].ToString());
        //                        cita.Detalle = row[3].ToString();
        //                        cita.Fecha = row[4].ToString();

        //                        result.Objects.Add(cita);

        //                    }

        //                    result.Correct = true;

        //                }
        //                else
        //                {
        //                    result.Correct = false;
        //                    result.ErrorMessage = "No se encontro la información solicitada";
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;

        //}

        //public static Result Create(Cita cita)
        //{
        //    Result result = new Result();

        //    try
        //    {
        //        using (SqlConnection oconexion = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
        //        {
        //            string query = "CitaAdd";

        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                cmd.Connection = oconexion;
        //                cmd.CommandText = query;
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                SqlParameter[] collection = new SqlParameter[4];
        //                collection[0] = new SqlParameter("IdDoctor", SqlDbType.Int);
        //                collection[0].Value = cita.doctor.IdDoctor;
        //                collection[1] = new SqlParameter("IdPaciente", SqlDbType.Int);
        //                collection[1].Value = cita.paciente.IdPaciente;
        //                collection[2] = new SqlParameter("Detalle", SqlDbType.VarChar);
        //                collection[2].Value = cita.Detalle;
        //                collection[3] = new SqlParameter("Fecha", SqlDbType.DateTime);
        //                collection[3].Value = DateTime.Parse(cita.Fecha);

        //                cmd.Parameters.AddRange(collection);
        //                cmd.Connection.Open();

        //                int RowsAffected = cmd.ExecuteNonQuery();

        //                if (RowsAffected > 0)
        //                {
        //                    result.Correct = true;
        //                }
        //                else
        //                {
        //                    result.Correct = false;
        //                    result.ErrorMessage = "Ocurrio un error al registrar";
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;
        //}

        //public static Result Update(Cita cita)
        //{
        //    Result result = new Result();

        //    try
        //    {
        //        using (SqlConnection oconexion = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
        //        {
        //            string query = "CitaUpdate";

        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                cmd.Connection = oconexion;
        //                cmd.CommandText = query;
        //                cmd.CommandType = CommandType.StoredProcedure;


        //                SqlParameter[] collection = new SqlParameter[5];

        //                collection[0] = new SqlParameter("IdCita", SqlDbType.Int);
        //                collection[0].Value = cita.IdCita;
        //                collection[1] = new SqlParameter("IdDoctor", SqlDbType.Int);
        //                collection[1].Value = cita.doctor.IdDoctor;
        //                collection[2] = new SqlParameter("IdPaciente", SqlDbType.Int);
        //                collection[2].Value = cita.paciente.IdPaciente;
        //                collection[3] = new SqlParameter("Detalle", SqlDbType.VarChar);
        //                collection[3].Value = cita.Detalle;
        //                collection[4] = new SqlParameter("Fecha", SqlDbType.DateTime);
        //                collection[4].Value = DateTime.Parse(cita.Fecha.ToString());

        //                cmd.Parameters.AddRange(collection);
        //                cmd.Connection.Open();

        //                int RowsAffected = cmd.ExecuteNonQuery();

        //                if (RowsAffected > 0)
        //                {
        //                    result.Correct = true;
        //                }
        //                else
        //                {
        //                    result.Correct = false;
        //                    result.ErrorMessage = "Ocurrio un error al actualizar el registro";
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;
        //}

        //public static Result Delete(int idCita)
        //{
        //    Result result = new Result();

        //    try
        //    {
        //        using (SqlConnection oconexion = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
        //        {
        //            string query = "CitaDelete";

        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                cmd.Connection = oconexion;
        //                cmd.CommandText = query;
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                SqlParameter[] collection = new SqlParameter[1];
        //                collection[0] = new SqlParameter("IdCita", SqlDbType.Int);
        //                collection[0].Value = idCita;

        //                cmd.Parameters.AddRange(collection);
        //                cmd.Connection.Open();

        //                int RowsAffected = cmd.ExecuteNonQuery();

        //                if (RowsAffected > 0)
        //                {
        //                    result.Correct = true;
        //                }
        //                else
        //                {
        //                    result.Correct = false;
        //                    result.ErrorMessage = "Ocurrio un error al Eliminar el registro";
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;
        //}

    }
}
