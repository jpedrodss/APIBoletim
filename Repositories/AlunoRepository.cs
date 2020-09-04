using APIBoletim.Context;
using APIBoletim.Domains;
using APIBoletim.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APIBoletim.Repositories
{
    public class AlunoRepository : IAluno
    {
        // Chamamos nosso contexto de conexao
        BoletimContext conexao = new BoletimContext();

        // Chamamos a classe que permite colocar consultas de banco
        SqlCommand cmd = new SqlCommand();

        public Aluno Alterar(int id, Aluno a)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "UPDATE Aluno SET " +
                "Nome = @nome, " +
                "RA = @ra, " +
                "Idade = @idade WHERE IdAluno = @id";
            cmd.Parameters.AddWithValue("@id", a.IdAluno);
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@ra", a.RA);
            cmd.Parameters.AddWithValue("@idade", a.Idade);

            cmd.ExecuteNonQuery();

            conexao.Desconectar();
            return a;
        }

        public Aluno BuscarID(int id)
        {
            cmd.Connection = conexao.Conectar();

            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "SELECT * FROM Aluno WHERE IdAluno = @id";
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dados = cmd.ExecuteReader();

            Aluno aluno = new Aluno();

            while (dados.Read())
            {
                aluno.IdAluno = Convert.ToInt32(dados.GetValue(0));
                aluno.Nome = dados.GetValue(1).ToString();
                aluno.RA = dados.GetValue(2).ToString();
                aluno.Idade = Int32.Parse(dados.GetValue(3).ToString());
            }

            conexao.Desconectar();

            return aluno;
        }

        public Aluno Cadastrar(Aluno a)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText =
                "INSERT INTO Aluno (Nome, RA, Idade) " +
                "VALUES" +
                "(@nome, @ra, @idade)";
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@ra", a.RA);
            cmd.Parameters.AddWithValue("@idade", a.Idade);

            cmd.ExecuteNonQuery();

            conexao.Desconectar();

            return a;
        }

        public void Excluir(int id)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "DELETE FROM Aluno WHERE IdAluno = @id";
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            conexao.Desconectar();
        }

        public List<Aluno> ListarTodos()
        {
            // Abrimos a conexão
            cmd.Connection = conexao.Conectar();

            // Atribuimos nossa conexão
            cmd.CommandText = "SELECT * FROM Aluno";

            // Lemos os dados
            SqlDataReader dados = cmd.ExecuteReader();

            // Criamos uma lista para ser populada
            List<Aluno> alunos = new List<Aluno>();

            // Criamos um laço para ler todas as lnihas
            while (dados.Read())
            {
                alunos.Add(
                    new Aluno
                    {
                        IdAluno = Convert.ToInt32(dados.GetValue(0)),
                        Nome    = dados.GetValue(1).ToString(),
                        RA      = dados.GetValue(2).ToString(),
                        Idade   = Convert.ToInt32(dados.GetValue(3))
                    }
                );
            }

            // Fechamos a conexão
            conexao.Desconectar();

            return alunos;
        }
    }
}
