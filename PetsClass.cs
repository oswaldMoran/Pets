using PetFamily.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily
{
    internal class PetsClass
    {
        private readonly string ConexionWithSPets = ConfigurationManager.AppSettings["cnnStrValue"];
        /// <summary>
        /// test
        /// </summary>
        /// <returns></returns>
        public List<MyPets> GetNamePets()
        {
            List<MyPets> mascotas = new List<MyPets>();
            try
            {

                using (SqlConnection conexion = new SqlConnection(ConexionWithSPets))
                {
                    conexion.Open();
                    if (conexion.State == ConnectionState.Open)
                    {
                        SqlCommand comandoSql = new SqlCommand("SP_PetsMoran", conexion);
                        comandoSql.CommandType = CommandType.StoredProcedure;
                        SqlDataReader resultInDataReader = comandoSql.ExecuteReader();
                        while (resultInDataReader.Read())
                        { 
                            mascotas.Add(new MyPets
                            {
                                Id = (int)resultInDataReader[0],
                                Name = resultInDataReader[1].ToString(),
                                Description = resultInDataReader[2].ToString(),
                                Gender = resultInDataReader[3].ToString(),
                                IsStillAlive = (bool)resultInDataReader[4],
                            });
                        }
                    }

                }
            }
            catch (Exception e)
            {

                Console.WriteLine("La conexion no sepudo realizar");
            }

            return mascotas;
        }

        public MyPets GetPetById(int id)
        {
            MyPets mascota = new MyPets();
            try
            {

                using (SqlConnection conexion = new SqlConnection(ConexionWithSPets))
                {
                    conexion.Open();
                    if (conexion.State == ConnectionState.Open)
                    {
                        SqlCommand comandoSql = new SqlCommand("SP_GetPetsById", conexion);
                        comandoSql.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        comandoSql.CommandType = CommandType.StoredProcedure;
                        SqlDataReader dt = comandoSql.ExecuteReader();
                        while (dt.Read())
                        {
                            mascota.Id = (int)dt[0];
                            mascota.Name = dt[1].ToString();
                            mascota.Description = dt[2].ToString();
                            mascota.Gender = dt[3].ToString();
                            mascota.IsStillAlive = (bool)dt[4];
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("La conexion no sepudo realizar");
            }

            return mascota;
        }
        /// <summary>
        /// this method return false when the main transaction failed otherwise returns true.
        /// </summary>
        /// <returns>boolean</returns>
        public bool InsertPets(string name, string description, string gender, bool isStillAlive)
        {
            bool res = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionWithSPets))
                {
                    conexion.Open();
                    if (conexion.State == ConnectionState.Open)
                    {
                        SqlCommand comandoSql = new SqlCommand("SP_InsertPet", conexion);
                        comandoSql.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                        comandoSql.Parameters.Add("@Description ", SqlDbType.VarChar).Value = description;
                        comandoSql.Parameters.Add("@Gender ", SqlDbType.VarChar).Value = gender;
                        comandoSql.Parameters.Add("@IsStilAlive ", SqlDbType.Bit).Value = isStillAlive;

                        comandoSql.CommandType = CommandType.StoredProcedure;
                        int insertResult = comandoSql.ExecuteNonQuery();
                        if (insertResult > 0)
                        {
                            res = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("La acción no se pudo completar, intentalo mas tarde");
            }
            return res;
        }

        public bool UpdatePets(int Id, string name, string description, char gender, bool isStillAlive)
        {
            bool resp1 = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionWithSPets))
                {
                    conexion.Open();
                    if (conexion.State == ConnectionState.Open)
                    {
                        SqlCommand comandoSql = new SqlCommand("SP_UpdatePet", conexion);
                        comandoSql.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                        comandoSql.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                        comandoSql.Parameters.Add("@Description ", SqlDbType.VarChar).Value = description;
                        comandoSql.Parameters.Add("@Gender ", SqlDbType.Char).Value = gender;
                        comandoSql.Parameters.Add("@IsStilAlive ", SqlDbType.Bit).Value = isStillAlive;

                        comandoSql.CommandType = CommandType.StoredProcedure;
                        int UpdateResult = comandoSql.ExecuteNonQuery();
                        if (UpdateResult > 0)
                        {
                            resp1 = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("La acción no se pudo completar, intentalo mas tarde.");
            }
            return resp1;
        }


        public bool DeletePets(int Id)
        {
            bool resp2 = false;
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionWithSPets))
                {
                    conexion.Open();
                    if (conexion.State == ConnectionState.Open)
                    {
                        SqlCommand comandoSql = new SqlCommand("SP_DeletePet", conexion);
                        comandoSql.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                        comandoSql.CommandType = CommandType.StoredProcedure;
                        int delteResult = comandoSql.ExecuteNonQuery();
                        if (delteResult > 0)
                        {
                            resp2 = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("La acción no se pudo completar, intentalo mas tarde.");
            }
            return resp2;
        }


    }
}
