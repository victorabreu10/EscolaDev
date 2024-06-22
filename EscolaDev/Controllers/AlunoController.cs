using Dapper;
using EscolaDev.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EscolaDev.Controllers
{
    [Route("Api/[controller]")]
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

        /// <summary>
        /// Busca todos os alunos existentes
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Realiza a busca do aluno pelo identificador id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
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

        /// <summary>
        /// Cria um aluno no Banco de dados
        /// </summary>
        /// <param name="Criar"></param>
        /// <returns></returns>
        [HttpPost("Criar")]
        public async Task<IActionResult> CriarAluno(Aluno criar)
        { 
            var aluno = new Aluno(criar.NomeAluno, criar.DataAniversario, criar.NomeEscola);

            var parameters = new { aluno.NomeAluno, aluno.DataAniversario, aluno.EstaAtivo, aluno.NomeEscola};

            using (var sqlconnection = new SqlConnection(_connectionString))
            {
                const string sql = "INSERT INTO dbo.Aluno VALUES (@NomeAluno, @DataAniversario, @EstaAtivo, @NomeEscola)";

                var response = await sqlconnection.ExecuteAsync(sql, parameters);
                return Ok(parameters);
            }
        }

        /// <summary>
        /// Atualiza um aluno expecifico no Banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <param name="aluno"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarAluno(int id, Aluno aluno)
        {
            var parameters = new {id, aluno.NomeAluno, aluno.DataAniversario, aluno.NomeEscola};

            using (var sqlconnection = new SqlConnection(_connectionString))
            {
                const string sql = "UPDATE Aluno SET NomeAluno = @NomeAluno, DataAniversario = @DataAniversario, NomeEscola = @NomeEscola  WHERE Id = @id";

                await sqlconnection.ExecuteAsync(sql, parameters);
                return Ok(parameters);
            }
        }
    }
}
    