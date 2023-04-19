using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Apresentacao.Model;

namespace Apresentacao.Data
{
    public class connData
    {
        private SqlConnection connection;

        public void conn()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
            }              
        }

        public void executeQuery(string query)
        {
            this.conn();
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.CommandType = CommandType.Text;

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                sqlCommand.Dispose();
                throw;
            }
            finally
            {
                sqlCommand.Dispose();
            }

            connection.Close();
            connection.Dispose();
        }

        public cepModel checkIfExistCEPInData(string cep)
        {
            this.conn();
            cepModel modelCEP = null;
            string query = $"SELECT TOP 1 * FROM CEP WHERE CEP='{cep}'";

            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.CommandType = CommandType.Text;

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    modelCEP = new cepModel();
                    modelCEP.cep = row["cep"]?.ToString();
                    modelCEP.logradouro = row["logradouro"]?.ToString();
                    modelCEP.bairro = row["bairro"]?.ToString();
                    modelCEP.complemento = row["complemento"]?.ToString();
                    modelCEP.localidade = row["localidade"]?.ToString();
                    modelCEP.unidade = row["unidade"]?.ToString();
                    modelCEP.ibge = row["ibge"]?.ToString();
                    modelCEP.gia = row["gia"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                sqlCommand.Dispose();
                throw;
            }
            finally
            {
                sqlCommand.Dispose();
            }

            connection.Close();
            connection.Dispose();

            return modelCEP;

        }
        public List<cepModel> getCEPByUF(string uf)
        {
            this.conn();
            
            List<cepModel> lstCEP = new List<cepModel>();
            string query = $"SELECT * FROM CEP WHERE UF='{uf}'";

            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.CommandType = CommandType.Text;

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    cepModel modelCEP = new cepModel();

                    modelCEP.cep = row["cep"]?.ToString();
                    modelCEP.logradouro = row["logradouro"]?.ToString();
                    modelCEP.bairro = row["bairro"]?.ToString();
                    modelCEP.complemento = row["complemento"]?.ToString();
                    modelCEP.localidade = row["localidade"]?.ToString();
                    modelCEP.unidade = row["unidade"]?.ToString();
                    modelCEP.ibge = row["ibge"]?.ToString();
                    modelCEP.gia = row["gia"]?.ToString();

                    lstCEP.Add(modelCEP);
                }
            }
            catch (Exception ex)
            {
                sqlCommand.Dispose();
                throw;
            }
            finally
            {
                sqlCommand.Dispose();
            }

            connection.Close();
            connection.Dispose();

            return lstCEP;

        }
    }
}