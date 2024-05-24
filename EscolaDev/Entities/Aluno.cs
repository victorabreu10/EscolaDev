namespace EscolaDev.Entities
{
    public class Aluno
    {
        /// <summary>
        /// 
        /// </summary>
        public Aluno() {}

        public Aluno(string nomeAluno, DateTime dataAniversario, string nomeEscola)
        {
            NomeAluno = nomeAluno;
            DataAniversario = dataAniversario;
            NomeEscola = nomeEscola;
            EstaAtivo = true;


        }

        public int Id { get; set; }

        public string NomeAluno { get; private set; }

        public DateTime DataAniversario { get; private set; }

        public string NomeEscola { get; private set; }

        public bool EstaAtivo { get; set; }
    }
}
