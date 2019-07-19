using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console; // Não ter que ficar Digitando Console.
using static Utilitarios; // Para não ficar digitando toda vez a Classe EsperarEnter.
using System.IO;// Para Ler o Arquivo
using static System.Math; // Para colocar um limite nas Casas Decimais.

namespace ProjetoChico
{
    class Program
    {

        static void MenuOpcoes()
        {
            WriteLine("-------------------------------------------------");
            WriteLine("Projeto Prof. Francisco da Fonseca- DS101: TecPro");
            WriteLine("-------------------------------------------------");
            WriteLine("Pressione A: Para Numeros Amigos.");
            WriteLine("Pressione B: Para Decimal para Binario.");
            WriteLine("Pressione C: Para Constante de Catalan.");
            WriteLine("Pressione D: Para /nProcessamento de Dados em Arquivos de Texto.");
            WriteLine("Pressione E: Para Finalizar .");


        }

        static void SeletorOpcoes()
        {

            string opcao = ""; // tentei mudar pra char mas deu erro .
            do
            {

                MenuOpcoes();
                Write("Qual Opção Deseja realizar?: ");
                opcao = ReadLine().ToUpper(); // .ToUpper pq se as opçoes estivessem em Minusculo e ele digitasse em Maiusculo daria erro.
                switch (opcao)
                {
                    case ("A"):
                        ExercicioA();
                        EsperarEnter();
                        break;
                    case ("B"):
                        ExercicioB();
                        EsperarEnter();
                        break;
                    case ("C"):
                        ExercicioC();
                        EsperarEnter();
                        break;
                    case ("D"):
                        ExercicioD();
                        EsperarEnter();
                        break;
                    default: // Caso a Opção digitada seja Diferente das Previstas.
                        WriteLine("Opção Invalida!.");
                        break;
                }
            } while (opcao != "E");
        }
        static void ExercicioA()
        {
            Clear();
            WriteLine("-=-=--=-Numeros Amigos.-=-=-==--=-====--");

            Write("Digite um número inteiro para verificarmos \nquais os números amigos:");
            int numeroAmigo = int.Parse(ReadLine());

            var amigo = new Matematica(numeroAmigo); // Para passar o número para classe mátematica, e o método amigo pegar
            try

            {
                List<string> numeros = amigo.Amigos(); // cria uma Lista .
                foreach (string umItem in numeros)
                {
                    Write(umItem);
                }

            }
            catch
            {
                WriteLine("Não existe números amigos antes do " + numeroAmigo + " Digitado"); // Como no Enunciado esta que se o Numero for Maior que 64, não é pra ser calculado.
            }

            Write("  \n");

        }
        static void ExercicioB()
        {
            Clear();
            WriteLine("-=-=-=- Conversor de Decimal para Binario.-=-=-=-==-=- ");
            Write("Informe o Numero que Deseja Converter(Tem de Ser Menor que 64): ");
            int NumeroInformado = int.Parse(ReadLine()); // Pega o Valor informado pelo Usuario.
            var DecimalPBinario = new Matematica(NumeroInformado); //Construtor da Classe Matematica
            string inverso = ""; // Variavel que vai receber a String do Metodo ParaBinario() e vai inverter.
            try
            {


                inverso = DecimalPBinario.ParaBinario();
                for (int indice = inverso.Length - 1; indice >= 0; indice--) // Inverte a Ordem dos Caracteres
                {
                    Write($"{inverso[indice]}"); // Mostra os Caracteres Invertidos.
                }
                Write("  \n"); // Pra pular Linha.
            }
            catch
            {

                WriteLine("O Valor Não Pode ser Maior que 64 e Menor que 0."); // Caso a Excessão do Metodo Para binario dispare ele trata o erro.
            }


        }
        static void ExercicioC()
        {
            Clear();
            WriteLine("-=-=-=- Constante de Catalan.-=-=-=-==-=- ");
            WriteLine("Informe Quantos termos a série terá: ");
            int termos = int.Parse(ReadLine());

            var qtdtermos = new Matematica(termos); // Contrutor que Vai mandar o valor para a Classe Matematica.

            WriteLine($"G = {qtdtermos.ConstanteCatalan()}");// Valor Retornado do Metodo.
            Write("  \n");
            

        }
        static void ExercicioD()
        {
            Clear();
            // O Nome do Arquivo de Texto é : teste.txt
            WriteLine("Digite o nome do arquivo desejado?");
            StreamReader leitor = new StreamReader(ReadLine());

            var soma = new Somatoria(); // para passar os as notas para classe somatoria 

            int qtdAprovados = 0, qtadRecuperacao = 0, qtdRetidos = 0;
            string melhorAluno = "";
            double maiorNota = 0;

            Aluno alunoAtual = null;
            Aluno alunoAnterior = null;

            while (!leitor.EndOfStream) // Enquanto não for o Fim do Arquivo vai executar.
            {
                alunoAnterior = alunoAtual; // Vai receber o Aluno anterio para Verificar se é o Atual
                alunoAtual = Aluno.LerDados(leitor.ReadLine()); // Aluno vai ser lido do arquivo

                if (alunoAnterior != null) // Verifica se tem o Aluno anterior for nulo ele inicia
                {
                    if (alunoAtual.Classe != alunoAnterior.Classe)
                    {
                        WriteLine("Total da classe " + alunoAnterior.Classe + ":");
                        WriteLine("Média Harmônica: " + Math.Round(soma.MediaHarmonica,2));

                        soma = new Somatoria(); // Construtor da Classe Somatoria.

                        WriteLine("Aprovados: " + qtdAprovados + " Em recuperação: " + qtadRecuperacao + " Retidos " + qtdRetidos);
                        WriteLine("Aluno com melhor desempenho: " + melhorAluno + " (" + Math.Round(maiorNota,2) + ")");

                        qtdAprovados = qtadRecuperacao = qtdRetidos = 0;
                        melhorAluno = "";
                        maiorNota = 0;

                        WriteLine("-----------------------------\n Classe: " + alunoAtual.Classe + "\nRA        Nota");
                    }

                    if (alunoAtual.Ra == alunoAnterior.Ra) // Vai Somar as Notas caso o Aluno anterior for o Aluno Atual (Se os RA's forem iguais)
                    {
                        soma.Somar(alunoAtual.Nota); // Aqui estamos passando a nota para o método somar 
                        WriteLine(alunoAtual.Nota.ToString().PadLeft(11));
                    }
                    else
                    {
                        double media = soma.MediaAritmetica;
                        //Math.Round(media, 2);
                        Write("Media: " + Math.Round(media, 2).ToString().PadRight(9)); // Math.Round é para Mostrar apenas duas Casas após a Virgula.
                        // Faz a Verificação da Média, se for Menor que 3 Retido, se for maior que 5 Aporovado, Senão Recuperação
                        if (media < 3)
                        {
                            WriteLine("Retido");
                            qtdRetidos++;
                        }
                        else
                           if (media >= 5)
                        {
                            WriteLine("Aprovado");
                            qtdAprovados++;
                        }
                        else
                        {
                            WriteLine("Recuperação");
                            qtadRecuperacao++;
                        }

                        if (media > maiorNota)
                        {
                            maiorNota = media;
                            melhorAluno = alunoAnterior.Ra;
                        }

                        soma.Valor = 0; 
                        soma.Quantos = 0;

                        soma.SomaInversos(media); // Executa com a Media a Soma dos Inversos

                        WriteLine(alunoAtual.Ra.PadRight(7) + alunoAtual.Nota);
                        soma.Somar(alunoAtual.Nota);
                    }
                }
                else
                {
                    soma.Somar(alunoAtual.Nota);
                    WriteLine("-----------------------------\n Classe: " + alunoAtual.Classe + "\nRA        Nota");
                    WriteLine(alunoAtual.Ra.PadRight(7) + alunoAtual.Nota);
                }
                
            }
        }


        static void Main(string[] args)
        {

            SeletorOpcoes(); // Mostra o Menu e Executa o Seletor de Opçoes.
        }
    }
}
