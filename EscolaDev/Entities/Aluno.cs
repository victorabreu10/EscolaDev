namespace EscolaDev.Entities
{
    public class Aluno
    {
        public Aluno() {}

        public Aluno(string nomeAluno, DateTime dataAniversario, string nomeEscola)
        {
            NomeAluno = nomeAluno;
            DataAniversario = dataAniversario;
            EstaAtivo = true;
            NomeEscola = nomeEscola;
        }

        public int Id { get; private set; }

        public string NomeAluno { get; set; }

        public DateTime DataAniversario { get; set; }

        public bool EstaAtivo { get; private set; }

        public string NomeEscola { get; set; }  
    }
}
