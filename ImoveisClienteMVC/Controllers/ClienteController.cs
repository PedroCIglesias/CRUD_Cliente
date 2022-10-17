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
    public class ClienteController : Controller
    {
        string connectionString = @"Data Source=PEDRO_IGLESIAS;Initial Catalog=dbTeste;Integrated Security=True";

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ViewTable()
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Cliente", sqlCon);
                DataTable dt = new DataTable();
                SqlDataAdapter sqldta = new SqlDataAdapter(sqlCmd);
                sqldta.Fill(dt);

                string html = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        html += "['" + dt.Rows[i]["ClienteId"] +
                            "','" + dt.Rows[i]["Nome"] +
                            "','" + dt.Rows[i]["Tipo"] +
                            "','" + dt.Rows[i]["TelefoneContato"] +
                            "','" + dt.Rows[i]["DataCadastro"].ToString().Substring(0, 10);
                        if (string.IsNullOrEmpty(dt.Rows[i]["DataAtualizacao"].ToString()))
                        {
                            html += "','" + dt.Rows[i]["DataAtualizacao"];
                        }
                        else
                        {
                            html += "','" + dt.Rows[i]["DataAtualizacao"].ToString().Substring(0, 10);
                        }
                        html += "','<a onclick=fnImovel(" + dt.Rows[i]["ClienteId"] + ")><b>Imóvel /</b></a>" +
                            "<a onclick=fnEdit(" + dt.Rows[i]["ClienteId"] + ")><b> Editar /</b></a>" +
                            "<a onclick=fnDelete(" + dt.Rows[i]["ClienteId"] + ")><b> Deletar</b></a>'],";
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
        public JsonResult Insert([FromBody] ClienteViewModel clienteViewModel)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);
                sqlCon.Open();
                string query = "INSERT INTO Cliente(Nome,Tipo,TelefoneContato,DataCadastro) VALUES(@Nome,@Tipo,@TelefoneContato,@DataCadastro)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Nome", clienteViewModel.Nome);
                sqlCmd.Parameters.AddWithValue("@Tipo", clienteViewModel.Tipo);
                sqlCmd.Parameters.AddWithValue("@TelefoneContato", clienteViewModel.TelefoneContato);
                sqlCmd.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();

                return Json("success");
            }
            catch(Exception ex)
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
                string query = "SELECT Nome,Tipo,TelefoneContato,ClienteId FROM Cliente WHERE ClienteId=@ClienteId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ClienteId", id);
                DataTable dt = new DataTable(); 
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                adapter.Fill(dt);

                string[] htmlValues = new string[dt.Columns.Count];

                if(dt.Rows.Count > 0)
                {
                    for(int i=0; i<dt.Columns.Count; i++)
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
        public JsonResult Update([FromBody] ClienteViewModel clienteViewModel)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);
                sqlCon.Open();
                string query = "UPDATE Cliente SET Nome=@Nome, Tipo=@Tipo, TelefoneContato=@TelefoneContato, DataAtualizacao=@DataAtualizacao WHERE ClienteId=@ClienteId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Nome", clienteViewModel.Nome);
                sqlCmd.Parameters.AddWithValue("@Tipo", clienteViewModel.Tipo);
                sqlCmd.Parameters.AddWithValue("@TelefoneContato", clienteViewModel.TelefoneContato);
                sqlCmd.Parameters.AddWithValue("@DataAtualizacao", DateTime.Now);
                sqlCmd.Parameters.AddWithValue("@ClienteId", clienteViewModel.ClienteId);
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
                string query = "DELETE FROM Cliente WHERE ClienteId=@ClienteId";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ClienteId", id);
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();

                return Json("success");
            }
            catch (Exception ex)
            {
                return Json("failure");
            }
        }
        public JsonResult Popula()
        {
            try
            {
                for(int i= 0; i <= 5000; i++)
                {
                    ClienteViewModel clienteViewModel = new ClienteViewModel();
                    clienteViewModel.Nome = "Cliente" + i;
                    clienteViewModel.Tipo = "PF";
                    clienteViewModel.TelefoneContato = "("+i+i+")"+i+i+i+i+i+"-"+i+i+i+i;
                    Insert(clienteViewModel);
                }

                return Json("success");
            }
            catch (Exception ex)
            {
                return Json("failure");
            }
        }
    }
}
