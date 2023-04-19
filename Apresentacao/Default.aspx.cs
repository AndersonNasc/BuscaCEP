using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Apresentacao.Data;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Net.Http;
using System.Text;

namespace Apresentacao
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCEP.Focus();

                //Carrega o combo de UFs
                ddlUf.DataSource = GetUfs();
                ddlUf.DataBind();
            }
        }
       
        protected void btnSearchCEP_Click(object sender, EventArgs e)
        {            
            string cep = txtCEP.Text;
            this.clearField();

            //Valida se o CEP é menor que 9(contando com o hífen) caracteres
            if (!validationSizeCaracter(cep))
            {
                msgReturn.Visible = true;
                msgReturn.Text = "CEP Inválido";
                return;
            }
            
            if (!string.IsNullOrEmpty(cep))
            {
                var resultCEP = callServiceCEP(cep);

                connData connData = new connData();

                if (!string.IsNullOrEmpty(filterResult(resultCEP)))
                {
                    //Verifica se o CEP informado existe na base
                    var objCEP = connData.checkIfExistCEPInData(cep);

                    //Se não existir o cep na base, o mesmo será inserido na base
                    if (objCEP == null)
                    {
                        connData.executeQuery(filterResult(resultCEP));
                    }
                    else
                    {
                        txtCEPResp.Text = objCEP.cep?.ToString();
                        txtLogradouro.Text = objCEP.logradouro?.ToString();
                        txtComplemento.Text = objCEP.complemento?.ToString();
                        txtBairro.Text = objCEP.bairro?.ToString();
                        txtLocalidade.Text = objCEP.localidade?.ToString();
                        txtUF.Text = objCEP.uf?.ToString();
                        txtUnidade.Text = objCEP.unidade?.ToString();
                        txtIbge.Text = objCEP.ibge?.ToString();
                        txtGia.Text = objCEP.gia?.ToString();
                    }

                    divComplement.Visible = true;
                }
                else
                {
                    msgReturn.Visible = true;
                    msgReturn.Text = "CEP Inválido";
                }   
            }
        }

        protected void btnSearchUF_Click(object sender, EventArgs e)
        {
            this.clearField();
            connData connData = new connData();

            var listCEP = connData.getCEPByUF(ddlUf.SelectedValue);

            //Se não existir o cep na base inseri
            if (listCEP.Count == 0)
            {
                msgReturn.Visible = true;
                msgReturn.Text = "Não consta dados para a UF selecionada.";
            }
            else
            {
                gridViewCEP.DataSource = listCEP;
                gridViewCEP.DataBind();

                divGrid.Visible = true;
            }
        }

        private JObject callServiceCEP(string cep)
        {
            JObject jsonRetorno = null;
            string minhaChave = ConfigurationManager.AppSettings["urlViaCEP"];
            string viaCEPUrl = $"{minhaChave}/{cep}/json/";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(viaCEPUrl).Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;

                // Corrige caracteres especiais
                Encoding iso = Encoding.GetEncoding("ISO-8859-1");
                Encoding utf8 = Encoding.UTF8;
                byte[] isoBytes = iso.GetBytes(responseBody);
                byte[] utf8Bytes = Encoding.Convert(iso, utf8, isoBytes);
                string utf8String = utf8.GetString(utf8Bytes);

                jsonRetorno = JsonConvert.DeserializeObject<JObject>(utf8String);
            }

            return jsonRetorno;
        }

        private string filterResult(JObject jsonRetorno)
        {
            var error = jsonRetorno["erro"]?.ToString();

            if (!string.IsNullOrEmpty(error))
                return null;

            string query = "INSERT INTO [dbo].[CEP] ([cep], [logradouro], [complemento], [bairro], [localidade], [uf], [unidade], [ibge], [gia]) VALUES (";
            query = query + $"'{jsonRetorno["cep"]}'";
            query = query + $",'{jsonRetorno["logradouro"]}'";
            query = query + $",'{jsonRetorno["complemento"]}'";
            query = query + $",'{jsonRetorno["bairro"]}'";
            query = query + $",'{jsonRetorno["localidade"]}'";
            query = query + $",'{jsonRetorno["uf"]}'";
            query = query + $",'{jsonRetorno["unidade"]}'";
            query = query + $",'{jsonRetorno["ibge"]}'";
            query = query + $",'{jsonRetorno["gia"]}'" + ")";

            txtCEPResp.Text = jsonRetorno["cep"]?.ToString();
            txtLogradouro.Text = jsonRetorno["logradouro"]?.ToString();
            txtComplemento.Text = jsonRetorno["complemento"]?.ToString();
            txtBairro.Text = jsonRetorno["bairro"]?.ToString();
            txtLocalidade.Text = jsonRetorno["localidade"]?.ToString();
            txtUF.Text = jsonRetorno["uf"]?.ToString();
            txtUnidade.Text = jsonRetorno["unidade"]?.ToString();
            txtIbge.Text = jsonRetorno["ibge"]?.ToString();
            txtGia.Text = jsonRetorno["gia"]?.ToString();

            return query;
        }

        private List<string> GetUfs()
        {
            List<string> ufs = new List<string>();

            ufs.Add("AC");
            ufs.Add("AL");
            ufs.Add("AM");
            ufs.Add("AP");
            ufs.Add("BA");
            ufs.Add("CE");
            ufs.Add("DF");
            ufs.Add("ES");
            ufs.Add("GO");
            ufs.Add("MA");
            ufs.Add("MG");
            ufs.Add("MS");
            ufs.Add("MT");
            ufs.Add("PA");
            ufs.Add("PB");
            ufs.Add("PE");
            ufs.Add("PI");
            ufs.Add("PR");
            ufs.Add("RJ");
            ufs.Add("RN");
            ufs.Add("RO");
            ufs.Add("RR");
            ufs.Add("RS");
            ufs.Add("SC");
            ufs.Add("SE");
            ufs.Add("SP");
            ufs.Add("TO");

            return ufs;
        }

        private void clearField()
        {
            divComplement.Visible = false;
            msgReturn.Visible = false;
            divGrid.Visible = false;
            txtCEP.Text = string.Empty;
        }

        private bool validationSizeCaracter(string cep)
        {
            if (cep.Length < 9)
            {
                return false;
            }

            return true;
        }
    }
}