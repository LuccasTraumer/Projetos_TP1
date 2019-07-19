// Lucas Silva de Jesus 19372   
// Pedro Candido 19382

using System;
using static System.Console;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

/*
Classe em geral responsavel por armazenar os Metodos usados no Form de Cadastro 
     
*/

public enum Situacao
{ navegando, incluindo, editando, procurando, excluindo }

namespace ProjetoII
{
    class VetorPalavra
    {
        int tamanhoMaxVet;  // Tamanho do Vetor
        int qntsPalavrasDicas, posicaoAtual;    // Variavel de controle de Posicao do Vetor
        private PalavraDica[] vetPalavraDica; // Declaração do Vetor
        string[] vetorCaracteres = new string[15];
        Situacao situacaoAtual;

        public VetorPalavra(int tamanhoDesejado) // Construtor da Classe VetorPalavra
        {
            vetPalavraDica = new PalavraDica[tamanhoDesejado]; // Cria um Vetor da Classe Palavra e Dica com o Tamanho desejado
            posicaoAtual = qntsPalavrasDicas = 0;
            tamanhoMaxVet = tamanhoDesejado;
        }

        public void PosicionarNoInicio() // Posiciona no Primeiro indice do Vetor
        {
            posicaoAtual = 0;
        }

        public PalavraDica this[int qualPosicao]
        {
            get // Vai pegar os Valores
            {
                if (qualPosicao >= 0 && qualPosicao < qntsPalavrasDicas) // Se a posição for maior que 0 e menor que o vetor
                    return vetPalavraDica[qualPosicao];
                throw new Exception("Acesso a posição inválida: " + // Se a Posição for Menor que 0 e Maior que o vetor lanca um erro
                                    qualPosicao + "!");
            }
            set // Antes de pegar os Valores vai verificar 
            {
                if (qualPosicao >= 0 && qualPosicao < qntsPalavrasDicas)
                    vetPalavraDica[qualPosicao] = value;
                else
                    throw new Exception("Acesso a posição inválida: " +
                                        qualPosicao + "!");
            }
        }

        public int Tamanho // permite à aplicação consultar o número de registros armazenados
        {
            get => qntsPalavrasDicas;
        }

        public int PosicaoAtual
        {
            get => posicaoAtual;
            set => posicaoAtual = value;
        }
        public bool EstaVazio // permite à aplicação saber se o vetor dados está vazio
        {
            get => qntsPalavrasDicas <= 0;
        }
        public void AvancarPosicao() // Vai para o Proximo indice do Vetor
        {
            if (posicaoAtual < qntsPalavrasDicas - 1)
                posicaoAtual++;
        }

        public void RetrocederPosicao() // Volta uma posição do vetor 
        {
            if (posicaoAtual > 0)
                posicaoAtual--;
        }

        public void PosicionarNoUltimo() // Ira posicionar na ultima Posição do Vetor
        {
            posicaoAtual = qntsPalavrasDicas - 1;
        }

        public void LerDados(string nomeArq)   // Metodo que esta responsavel pela Leitura do Arquivo
        {
            var arq = new StreamReader(nomeArq);

            while (!arq.EndOfStream)
            {
                string linha = arq.ReadLine(); // String que vai aramazenar os dados de uma linha do Arquivo

                PalavraDica PalavraEDica = new PalavraDica(linha); // Passa a linha lida como parametro para a Classe PalavraDica
                InserirAposFim(PalavraEDica); // Chama o metodo InserirAposFim que vai colocar no Final do Arquivo       
            }
            arq.Close();
        }
        public void InserirAposFim(PalavraDica valorAInserir) // Metodo Inserir que tem como parametro o dado para inserir
        {
            if (qntsPalavrasDicas >= tamanhoMaxVet) // Verifica se não esta no fim do vetor
                ExpandirVetor(); // Se estiver no fim do vetor ele chama o metodo Expandir e Coloca mais espaço no Vetor

            vetPalavraDica[qntsPalavrasDicas] = valorAInserir; // Se não coloca o dado no vetor 
            qntsPalavrasDicas++; // Incrementa a variavel de controle de posição
        }
        private void ExpandirVetor() // Metodo responsavel por expandir o vetor
        {
            tamanhoMaxVet += 10; // Vai inserir mais 10 Posições no Vetor se necessario
            PalavraDica[] vetorMaior = new PalavraDica[tamanhoMaxVet];
            for (int indice = 0; indice < qntsPalavrasDicas; indice++)
                vetorMaior[indice] = vetPalavraDica[indice]; // Desloca o Vetor para incluir mais posições

            vetPalavraDica = vetorMaior;
        }

        public void Excluir(int posicaoAExcluir) // Exclui um dado do Arquivo
        {
            qntsPalavrasDicas--;
            for (int indice = posicaoAExcluir; indice < qntsPalavrasDicas; indice++)
                vetPalavraDica[indice] = vetPalavraDica[indice + 1];

            

        }

        public void Listar(ListBox lista)
        {
            lista.Items.Clear();
            for (int indice = 0; indice < qntsPalavrasDicas; indice++)
                lista.Items.Add(vetPalavraDica[indice]);
        }
        public void Listar(ComboBox lista)
        {
            lista.Items.Clear();
            for (int indice = 0; indice < qntsPalavrasDicas; indice++)
                lista.Items.Add(vetPalavraDica[indice]);
        }
        public void Listar(TextBox lista)
        {
            lista.Multiline = true;
            lista.ScrollBars = ScrollBars.Both;
            lista.Clear();
            for (int indice = 0; indice < qntsPalavrasDicas; indice++)
                lista.AppendText(vetPalavraDica[indice] + Environment.NewLine);
        }

        public void GravarDados(string nomeArquivo) // Salvar os Arquivos 
        {
            var arquivo = new StreamWriter(nomeArquivo);        // abre arquivo para escrita
            for (int indice = 0; indice < qntsPalavrasDicas; indice++)  // percorre elementos do vetor
                arquivo.WriteLine($"{vetPalavraDica[indice],5}");       // grava cada elemento
            arquivo.Close();
        }
        public override string ToString()  // retorna lista de valores separados por 
        {                                  // espaço
            return ToString(" ");
        }

        public string ToString(string separador) // retorna lista de valores separados 
        {                                        // por separador
            string resultado = "";
            for (int indice = 0; indice < qntsPalavrasDicas; indice++)
                resultado += vetPalavraDica[indice] + separador;
            return resultado;
        }

        public void AcessarPalavraEDica(int nmrLinha, ref string palavraAcessada, ref string dicaAcessada)
        {
            PalavraDica acessado = vetPalavraDica[nmrLinha]; // acessa o objeto que está no vetor do número que foi sorteado
            palavraAcessada = acessado.Palavra; // devolve para o programa a palavra e a dica do objeto sorteado
            dicaAcessada = acessado.Dica;
        }

        int qtosCaracteres = 0;

        

        public void SepararDigito(string palavra, DataGridView qualDgv) //função que separará a palavra em jogo por letras
        {
            qtosCaracteres = 0;
            while(qtosCaracteres < palavra.Trim().Length) // enquanto o indice for menor que o numero de letras da palavra(sem espaços) ->
            {
                vetorCaracteres[qtosCaracteres] = palavra.Substring(qtosCaracteres, 1); // atribui-se ao vetor de strings o valor da letra
                qtosCaracteres++;
            }
            qualDgv.ColumnCount = qtosCaracteres; // divide-se o DataGridView pelo tamanho de letras
            
        }

        public int QtosCaracteres { // variável que guardaremos o tamanho da palavra
            get => qtosCaracteres;
            set => qtosCaracteres = value;
        }  

        
        public int[] PosicoesNaPalavra(string letra, ref int qtsOcorrencias) // método que retornará um vetor de valores com a posição de cada letra
        {                                                
            int[] posicoes = new int[15]; // criação do vetor
            int indice = 0; // indice do vetor

            for (int i = 0; i < qtosCaracteres; i++) // percorremos os caracteres da palavra
                if (vetorCaracteres[i] == letra) // se o caracter da vez for igual a letra
                {
                    posicoes[indice] = i;
                    indice++;
                }

            qtsOcorrencias = indice; //quantas vezes a  letra apareceu
            return posicoes; // o vetor 'posicoes' retornado, terá o valor dos carácteres que ocorreram a letra.
        }

        public Situacao SituacaoAtual
        {
            get => situacaoAtual;
            set => situacaoAtual = value;
        }
        public bool Existe(PalavraDica palavraProc, ref int indice)
        {
            bool achouIgual = false;
            indice = 0; // para começar a percorrer o vetor dados
            while (!achouIgual && indice < qntsPalavrasDicas)
                if (vetPalavraDica[indice].Palavra == palavraProc.Palavra)
                    achouIgual = true;
                else
                    indice++;

            return achouIgual;
        }

        public void GravacaoEmDisco(string nomeArquivo) // Salva no Arquivo o registro
        {
            var arqPalavra = new StreamWriter(nomeArquivo);
            for (int i = 0; i < qntsPalavrasDicas; i++)
                arqPalavra.WriteLine(vetPalavraDica[i].ParaArquivo());
            arqPalavra.Close();
        }

        public void Incluir(PalavraDica novoValor, int posicaoDeInclusao)
        {
            if (qntsPalavrasDicas >= vetPalavraDica.Length)
                throw new Exception("Limite de Arquivo alcancado");

            for (int indice = qntsPalavrasDicas - 1; indice >= posicaoDeInclusao; indice--)
                vetPalavraDica[indice + 1] = vetPalavraDica[indice];
            vetPalavraDica[posicaoDeInclusao] = novoValor;
            qntsPalavrasDicas++;
        }

        public void Listar(TextBox lista, string cabecalho) // Lista na Tela
        {
            lista.Clear();
            lista.AppendText(cabecalho + Environment.NewLine);
            for (int indice = 0; indice < qntsPalavrasDicas; indice++)
                lista.AppendText($"{vetPalavraDica[indice].Palavra}     {vetPalavraDica[indice].Dica}" + Environment.NewLine);
        }
    }
}
