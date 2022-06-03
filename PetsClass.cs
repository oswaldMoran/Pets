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
        public List<string> GetNamePets()
        {
            List<string> mascotas = new List<string>();
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
                            string name = resultInDataReader[0].ToString();
                            mascotas.Add(name);
                        }
                    }

                }
                //

            }
            catch (Exception e)
            {

                Console.WriteLine("La conexion no sepudo realizar");
            }

            return mascotas;
        }
        /// <summary>
        /// this method return false when the main transaction failed otherwise returns true.
        /// </summary>
        /// <returns>boolean</returns>
        public bool InsertPets(string name,string description,String gender,bool isStillAlive)
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
                Console.WriteLine("La conexion no sepudo realizar");
            }
            return res;
        }

    }
}
