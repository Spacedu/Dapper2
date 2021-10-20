using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using eCommerce.API.Models;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipsController : ControllerBase
    {
        private IDbConnection _connection;
        public TipsController()
        {
            _connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=eCommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            string sql =    "SELECT * FROM Usuarios WHERE Id = @Id;" +
                            "SELECT * FROM Contatos WHERE UsuarioId = @Id;" +
                            "SELECT * FROM EnderecosEntrega WHERE UsuarioId = @Id;" +
                            "SELECT D.* FROM UsuariosDepartamentos UD INNER JOIN Departamentos D ON UD.DepartamentoId = D.Id WHERE UD.UsuarioId = @Id;";

            using (var multipleResultSets = _connection.QueryMultiple(sql, new { Id = id }))
            {
                var usuario = multipleResultSets.Read<Usuario>().SingleOrDefault();
                var contato = multipleResultSets.Read<Contato>().SingleOrDefault();
                var enderecos = multipleResultSets.Read<EnderecoEntrega>().ToList();
                var departamentos = multipleResultSets.Read<Departamento>().ToList();

                if(usuario != null)
                {
                    usuario.Contato = contato;
                    usuario.EnderecosEntrega = enderecos;
                    usuario.Departamentos = departamentos;

                    return Ok(usuario);
                }
            }

            return NotFound();
        }
    }
}
