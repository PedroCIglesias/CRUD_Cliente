using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImoveisClienteMVC.Data;
using ImoveisClienteMVC.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace ImoveisClienteMVC.Controllers
{
    public class ImovelController : Controller
    {
        string connectionString = @"Data Source=PEDRO_IGLESIAS;Initial Catalog=dbTeste;Integrated Security=True";

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ViewTable([FromBody] int id)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);
                sqlCon.Open();
                string query = "SELECT ImovelId,CLI.Nome as NomeCliente,IMO.Nome as NomeImovel, Valor, Cidade,Bairro,Logradouro,Numero, IMO.DataCadastro,IMO.DataAtualizacao FROM IMOVEIS IMO,CLIENTE CLI WHERE IMO.ClienteId=CLI.ClienteId AND IMO.ClienteId=@ClienteId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ClienteId", id);
                DataTable dt = new DataTable();
                SqlDataAdapter sqldta = new SqlDataAdapter(sqlCmd);
                sqldta.Fill(dt);

                string html = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        html += "['" + dt.Rows[i]["ImovelId"] +
                            "','" + dt.Rows[i]["NomeCliente"] +
                            "','" + dt.Rows[i]["NomeImovel"] +
                            "','" + dt.Rows[i]["Valor"] +
                            "','" + dt.Rows[i]["Cidade"] +
                            "','" + dt.Rows[i]["Bairro"] +
                            "','" + dt.Rows[i]["Logradouro"] +
                            "','" + dt.Rows[i]["Numero"] +
                            "','" + dt.Rows[i]["DataCadastro"].ToString().Substring(0, 10);

                        if (string.IsNullOrEmpty(dt.Rows[i]["DataAtualizacao"].ToString()))
                        {
                            html += "','" + dt.Rows[i]["DataAtualizacao"];
                        }
                        else
                        {
                            html += "','" + dt.Rows[i]["DataAtualizacao"].ToString().Substring(0, 10);
                        }

                        html += "','<a onclick=fnEdit(" + dt.Rows[i]["ImovelId"] + ")><b>Edit /</b></a>" +
                            "<a onclick=fnDelete(" + dt.Rows[i]["ImovelId"] + ")><b> Delete</b></a>'],";
                    }
                }
                sqlCon.Close();
                html = html.Substring(0, Math.Max(0, html.Length - 1));

                return Json(new { html });
            }
            catch (Exception ex)
            {
                return Json("failure");
            }
        }
        public JsonResult Insert([FromBody] ImovelViewModel imovelViewModel)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);
                sqlCon.Open();
                string query = "INSERT INTO Imoveis(ClienteId,Nome,Valor,Cidade,Bairro,Logradouro,Numero,DataCadastro) " +
                    "VALUES(@ClienteId,@Nome,@Valor,@Cidade,@Bairro,@Logradouro,@Numero,@DataCadastro)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ClienteId", imovelViewModel.ClienteId);
                sqlCmd.Parameters.AddWithValue("@Nome", imovelViewModel.Nome);
                sqlCmd.Parameters.AddWithValue("@Valor", imovelViewModel.Valor);
                sqlCmd.Parameters.AddWithValue("@Cidade", imovelViewModel.Cidade);
                sqlCmd.Parameters.AddWithValue("@Bairro", imovelViewModel.Bairro);
                sqlCmd.Parameters.AddWithValue("@Logradouro", imovelViewModel.Logradouro);
                sqlCmd.Parameters.AddWithValue("@Numero", imovelViewModel.Numero);
                sqlCmd.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();

                return Json("success");
            }
            catch (Exception ex)
            {
                return Json("failure");
            }
        }
        public JsonResult Edit([FromBody] int id)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);
                sqlCon.Open();
                string query = "SELECT ClienteId,Nome,Valor,Cidade,Bairro,Logradouro,Numero FROM Imoveis WHERE ImovelId=@ImovelId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ImovelId", id);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                adapter.Fill(dt);

                string[] htmlValues = new string[dt.Columns.Count];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        htmlValues[i] += dt.Rows[0].ItemArray[i].ToString();
                    }
                }
                sqlCon.Close();

                return Json(new { htmlValues });
            }
            catch (Exception ex)
            {
                return Json("failure");
            }
        }
        public JsonResult Update([FromBody] ImovelViewModel imovelViewModel)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);
                sqlCon.Open();
                string query = "UPDATE Imoveis SET ClienteId=@ClienteId, Nome=@Nome, Valor=@Valor, Cidade=@Cidade, Bairro=@Bairro, Logradouro=@Logradouro, Numero=@Numero, DataAtualizacao=@DataAtualizacao" +
                    " WHERE ImovelId=@ImovelId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ClienteId", imovelViewModel.ClienteId);
                sqlCmd.Parameters.AddWithValue("@Nome", imovelViewModel.Nome);
                sqlCmd.Parameters.AddWithValue("@Valor", imovelViewModel.Valor);
                sqlCmd.Parameters.AddWithValue("@Cidade", imovelViewModel.Cidade);
                sqlCmd.Parameters.AddWithValue("@Bairro", imovelViewModel.Bairro);
                sqlCmd.Parameters.AddWithValue("@Logradouro", imovelViewModel.Logradouro);
                sqlCmd.Parameters.AddWithValue("@Numero", imovelViewModel.Numero);
                sqlCmd.Parameters.AddWithValue("@DataAtualizacao", DateTime.Now);
                sqlCmd.Parameters.AddWithValue("@ImovelId", imovelViewModel.ImovelId);
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();

                return Json("success");
            }
            catch (Exception ex)
            {
                return Json("failure");
            }
        }
        public JsonResult Delete([FromBody] int id)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);
                sqlCon.Open();
                string query = "DELETE FROM Imoveis WHERE ImovelId=@ImovelId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ImovelId", id);
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();

                return Json("success");
            }
            catch (Exception ex)
            {
                return Json("failure");
            }
        }
    }
}
