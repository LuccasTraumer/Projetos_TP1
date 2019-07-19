using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoChico
{

    
    class Aluno
    {
        string classe; // Atributos da classe
        string ra;
        double nota;
        // Tamanho dos De cada Palavra
        const int tamanhoPeriodoCurso = 6;
        const int tamanhoRA = 5;
        const int tamanhoNotaAluno = 4;

        // Onde cada um Começa e Termina.
        const int inicioPeriodoCurso = 0;
        const int inicioRA = inicioPeriodoCurso + tamanhoPeriodoCurso;
        const int inicioNotaAluno = tamanhoRA + inicioRA;

        public string Classe { get => classe; set => classe = value; }
        public string Ra { get => ra; set => ra = value; }
        public double Nota { get => nota; set => nota = value; }

        public Aluno(string classe, string ra, double nota) //Construtor da classe
        {
            this.classe = classe;
            this.ra = ra;
            this.nota = nota;

        }

        public static Aluno LerDados(string linha) // método para ler o arquivo de texto, vai ler linha por linha, conforme informei nas variáveis inicioClasse etc..
        {
            Aluno al = new Aluno(linha.Substring(inicioPeriodoCurso, tamanhoPeriodoCurso), linha.Substring(inicioRA, tamanhoRA),
            double.Parse(linha.Substring(inicioNotaAluno, tamanhoNotaAluno)));
            return al;
        }
    }
}
