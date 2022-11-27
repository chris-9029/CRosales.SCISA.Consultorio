using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Doctor
    {
        public int IdDoctor { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Cedula { get; set; }
        public string? Foto { get; set; }

        public string? NombreCompleto { get; set; }

        public List<object>? doctores { get; set; }
        public static Result GetAll()
        {
            Result result = new Result();

            try
            {
                using (SqlConnection con = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
                {
                    string querySP = "DoctorGetAll";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = querySP;
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable tablaDoctor = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tablaDoctor);

                        if (tablaDoctor.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaDoctor.Rows)
                            {
                                Doctor doctor = new Doctor();
                                doctor.IdDoctor = int.Parse(row[0].ToString());
                                doctor.Nombre = row[1].ToString();
                                doctor.ApellidoPaterno = row[2].ToString();
                                doctor.ApellidoMaterno = row[3].ToString();
                                doctor.Cedula = row[4].ToString();
                                doctor.Foto = row[5].ToString();

                                doctor.NombreCompleto = doctor.Nombre + " " + doctor.ApellidoPaterno + " " + doctor.ApellidoMaterno;
                                result.Objects.Add(doctor);

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

        public static Result GetById(int id)
        {
            Result result = new Result();

            try
            {
                using (SqlConnection context = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
                {
                    string querySP = "[DoctorGetById] @IdDoctor";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = querySP;
                        cmd.Parameters.Add("@IdDoctor", SqlDbType.Int);
                        cmd.Parameters["@IdDoctor"].Value = id;
                        cmd.Connection.Open();
                        cmd.ExecuteScalar();
                        Doctor doctor = new Doctor();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                doctor.IdDoctor = int.Parse(dr[0].ToString());
                                doctor.Nombre = dr[1].ToString();
                                doctor.ApellidoPaterno = dr[2].ToString();
                                doctor.ApellidoMaterno = dr[3].ToString();
                                doctor.Cedula = dr[4].ToString();
                                doctor.Foto = dr[5].ToString();

                                result.Object = doctor;
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

        public static Result Add(Doctor doctor)
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

                        SqlParameter[] collection = new SqlParameter[5];
                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = doctor.Nombre;
                        collection[1] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = doctor.ApellidoPaterno;
                        collection[2] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[2].Value = doctor.ApellidoMaterno;
                        collection[3] = new SqlParameter("Cedula", SqlDbType.VarChar);
                        collection[3].Value = doctor.Cedula;
                        collection[4] = new SqlParameter("Foto", SqlDbType.VarChar);
                        collection[4].Value = doctor.Foto;

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


        public static Result Update(Doctor doctor)
        {
            Result result = new Result();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
                {
                    string query = "DoctorUpdate";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = oconexion;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;


                        SqlParameter[] collection = new SqlParameter[6];
                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = doctor.Nombre;
                        collection[1] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = doctor.ApellidoPaterno;
                        collection[2] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[2].Value = doctor.ApellidoMaterno;
                        collection[3] = new SqlParameter("Cedula", SqlDbType.VarChar);
                        collection[3].Value = doctor.Cedula;
                        collection[4] = new SqlParameter("Foto", SqlDbType.VarChar);
                        collection[4].Value = doctor.Foto;
                        collection[5] = new SqlParameter("IdDoctor", SqlDbType.Int);
                        collection[5].Value = doctor.IdDoctor;
                        
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

        public static Result Delete(int idDoctor)
        {
            Result result = new Result();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(AccesoDatos.Conexion.GetConnectionString()))
                {
                    string query = "DoctorDelete";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = oconexion;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("IdDoctor", SqlDbType.Int);
                        collection[0].Value = idDoctor;

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
