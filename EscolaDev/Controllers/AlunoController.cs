using Dapper;
using EscolaDev.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EscolaDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase 
    {
        private readonly string _connectionString;

        /// <summary>
        /// AlunoController
        /// </summary>
        /// <param name="configuration"></param>
        public AlunoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DataBase");  
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodosAlunos()
        {
            using (var sqlconnection = new SqlConnection(_connectionString))
            {
                const string sql = "SELECT * FROM Aluno Where EstaAtivo = 1";
                var aluno = await sqlconnection.QueryAsync<Aluno>(sql);
                return Ok(aluno);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> BuscarAlunoporId(int id)
        {   
            var parameters = new { id };

            using (var sqlconnection = new SqlConnection(_connectionString)) 
            {
                const string sql = "SELECT * FROM Aluno WHERE Id = @id";
                var aluno = await sqlconnection.QuerySingleOrDefaultAsync<Aluno>(sql, parameters);
                return Ok(aluno);   
            }
        }
    }
}
    