// Lucas Silva de Jesus 19372   
// Pedro Candido 19382
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
/*
 Responsavel pelo Jogo, botoes, Acoes e interação com o usuario 
*/
namespace ProjetoII
{
    public partial class frmForca : Form
    {
        // Declara as Variaveis como global para não ficar instanciando  
        VetorPalavra vetor; // Declaração do Objeto da classe VetorPalavra
        string nomeArq; // Nome do Arquivo que vai ser usado 
        public frmForca(string nomeArq) // Recebe como Parametro o Nome do Arquivo que foi aberto
        {
            InitializeComponent();
            vetor = new VetorPalavra(100); // instanciamos o vetor com no max 100 registros lidos
            this.nomeArq = nomeArq; // Atribuimos a Var Global a Local
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tmrAgora.Start(); // incia o timer do horário do dia
            
            
                vetor.LerDados(nomeArq); // Passa o Nome do Arquivo como parametro para fazer a leitura do Arquivo
           
                
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (txtSeuNome.Text != "")
            {
                string palavra = "", dica = ""; // variaveis que irao armazenar a Palavra e Dica
                AcessoDesafio(ref palavra, ref dica); //cria-se um método para acessar o desafio
                IniciarJogo(palavra, dica);  // inicia o jogo.
            }
            else
                MessageBox.Show("Digite seu nome!", "Campos faltantes", MessageBoxButtons.OK);
        }


        int qntsErros = 0, qntsAcertos = 0; // Variavel de controle dos acertos e erros
        Button[] quaisBotoes = new Button[30]; // criamos um vetor de botões para saber quais botões foram pressionados
        int qtosBotoes = 0;

        private void btnClick(object sender, EventArgs e) // ao clicar qualquer botão do teclado exibido, esse evento será chamado
        {
            try
            {
                Button botao = (Button)sender; // o objeto botão será o botão pressionado
                quaisBotoes[qtosBotoes] = botao; // vetor de botões pressionados para podermos habilitar-los depois
                qtosBotoes++;

                botao.Enabled = false; // desabilitamos o botão
                string letraBotao = botao.Text.ToLower();  // a string letra botao, declarada anteriormente, será o texto do botão pressionado,
                                                           // ou seja, a letra que o botão representa
                                                           // como no arquivo texto usamos letras minúsculas, usamos o método ToLower() para transformar o valor do botão para letras minúsculas também  

                int qtsOcorrencias = 0;
                int[] posicoesNaPalavra = vetor.PosicoesNaPalavra(letraBotao, ref qtsOcorrencias); // vetor que terá as posições de ocorrência da letra

                if (qtsOcorrencias > 0) // ou seja, se a letra foi encontrada na palvra
                {
                    qntsAcertos += qtsOcorrencias;  // somamos um ponto pra cada letra acertada
                    lbPontos.Text = "Pontos:" + qntsAcertos.ToString(); // adicionamos os pontos
                    botao.BackColor = Color.Green; // deixamos o botão verde para informar que a letra estava na palavra
                    AdicionarNoDgv(dgvPalavra, posicoesNaPalavra, qtsOcorrencias, letraBotao); //exibimos no datagridview
                }
                else // se a letra não for encontrada na palavra, ou seja, se o jogador errou
                {
                    qntsErros++; // conta-se mais um erro
                    lbErros.Text = "Erros(8): " + qntsErros.ToString(); //marcamos os erros
                    botao.BackColor = Color.Red; // deixamos os botões vermelho para informar que não tinha na palavra
                    ExibirErrosNaForca(qntsErros); // exibimos a imagem com o número do erro correspondente
                }

                if (qntsAcertos == vetor.QtosCaracteres) // se a pontuação for igual o tamanho da palavra
                {
                    tmrTempo.Stop(); // paramos o timer
                    tmrTempo.Enabled = false;
                    GameWin(); // o jogador ganha
                }

                if (qntsErros == 8) // se o jogador errar oito vezes ele perde
                {
                    tmrTempo.Stop(); // paramos o timer
                    tmrTempo.Enabled = false;
                    Perdeu(); // o jogador perde
                }
            }
            catch{ //deixamos o catch vazio para evitar que os clicks abusivos do usuário causem erro no programa

            }
        }
        void AcessoDesafio(ref string palavra, ref string dica)   //Método que acessará o desafio e devolverá a palavra e a dica da rodada
        {                                                          
            Random PDSorteada = new Random();
            int numeroSorteado = PDSorteada.Next(100);      // sorteia um número entre 0 e 99
            vetor.AcessarPalavraEDica(numeroSorteado, ref palavra, ref dica); // chama o método que acessa a palavra e a dica que entrarão
        }                                                               // na rodada. Ambas strings são devolvidas para o método

        void IniciarJogo(string palavra, string dica) 
        {
            vetor.SepararDigito(palavra, dgvPalavra); // método que dividirá a palavra em jogo em vetores de strings,  
                                                      // e também dividirá o DataGridView

            btnIniciar.Visible = false;
            chkComDica.Visible = false; 
            txtSeuNome.Enabled = false; 
            panelTeclado.Enabled = true;

            if (ComDica()) // se o jogador deseja jogar com dica
            {
                HabilitarTimer(tmrTempo, lbTempo); // hablitamos o timer e a contagem é iniciada
                lbDica.Text = "Dica: " + dica; // exibimos a dica para o jogador

            }        
            // Nosso jogo está iniciado, os outros eventos estarão no Click dos botões.
        }

        void HabilitarTimer(Timer qualTimer, Label qualLabel) // habilitamos o timer
        {
            qualTimer.Enabled = true;
            qualTimer.Start();
            qualLabel.Visible = true;
        }

        bool ComDica() // se tem dica
        {
            return chkComDica.Checked;
        }
        void AdicionarNoDgv(DataGridView qualDgv, int[] posicoes, int qtsOcorrencias, string qualLetra)
        {
            for(int i = 0; i < qtsOcorrencias; i++) // o índice do vetor será até quantas vezes a letra escolhida ocorreu
            {
                dgvPalavra.Rows[0].Cells[posicoes[i]].Value = qualLetra.ToUpper(); 
            }
        }

        void GameWin()
        {
            if (MessageBox.Show(txtSeuNome.Text + "\nVocê ganhou!\nPontuação: " + qntsAcertos +
                   "\nDeseja jogar novamente?", "Parabéns", MessageBoxButtons.YesNo) == DialogResult.Yes) //Pergunta ao usuario se deseja jogar Novamente
            {
                ImagensGanhou(); // Mostra quando vencer
                System.Threading.Thread.Sleep(1500); // esperamos 1.5 segundos para resetar
                Resetar();    // resetamos o jogo
            }
            else
            {   
                Close(); //fechamos o jogo 
            }
        }

        void Perdeu()
        {
            if (MessageBox.Show(txtSeuNome.Text + "\nVocê perdeu!\nDeseja jogar novamente?", "Derrota", MessageBoxButtons.YesNo)
                == DialogResult.Yes) // vemos se  o jogador quer jogar mais
            {
                ImagensPerdeu();
                System.Threading.Thread.Sleep(3000); // esperamos 3  segundos         
                Resetar(); // resetamos o jogo
            }
            else
            {
                Close(); //se ele não quiser jogar mais fechamos o jogo
            }
        }

        void Resetar()
        {
            ResetarImagens(); // Oculta novamente as imagens
            ResetarBotoes();  // Voltamos os botoes ao normal
            panelTeclado.Enabled = false; // desabilitamos o teclado
            txtSeuNome.Clear(); // limpamos a área do nome
            txtSeuNome.Enabled = true; // habilitamos a área do nome
            lbDica.Text = "Dica:______________________ "; 
            lbPontos.Text = "Pontos:____ "; // Volta ao Normal
            lbErros.Text = "Erros(8):____ ";
            lbTempo.Text = "Tempo Restante:___s";
            lbTempo.Visible = false; 
            LimparDgv(dgvPalavra); // limpamos o data grid view
            btnIniciar.Visible = true;  
            chkComDica.Checked = false;
            qntsAcertos = 0; // resetamos a pontuação
            qntsErros = 0; //resetamos os erros
            chkComDica.Visible = true; // deixamos visível o check box da dica
            tempoRestante = 60; //deixamos o timer com 60segundos novamente
        }


        Jogador[] vetorJogadores = new Jogador[30]; //vetor de objetos da classe jogador
        bool cabecalho = true;
        int qntsJogadores = 0;
         
        void SalvarJogador(string nome, int pontos)
        {
            if(cabecalho)
            {
                
                cabecalho = false;
            }

            int posiJog = -1;

            if (!UsouNome(nome, ref posiJog)) // se o nome ainda não foi utilizado
            {
                Jogador jog = new Jogador(nome, pontos); // cria-se um objeto da classe Jogador com nome e pontos do jogador ganhador
                vetorJogadores[qntsJogadores] = jog; // atribui-se o objeto ao vetor
                qntsJogadores++; // Incrementa 1 no contador dos Jogadores
                
            }
            else //se o nome já foi utilizado
            {
                vetorJogadores[posiJog].JogadorPontos += qntsAcertos; // somamos a pontuação dessa jogada à pontuação da jogada anterior, já armazenada no vetor dos jogadores
            }                                                     
        }


        bool UsouNome(string nomeJog, ref int posiJogadorEncontrado)
        {
            bool achou = false;
            for (int i = 0; i < qntsJogadores && !achou; i++) // percorremos o vetor dos jogadores já registrados
            {
                if (nomeJog == vetorJogadores[i].JogadorNome) //se o nome usado na jogada for igual a um já usado anteriormente
                {
                    achou = true; // quer dizer que o nome já existe
                    posiJogadorEncontrado = i; //devolvemos em qual indice do vetor de jogadores o nome foi encontrado
                } 
            }
            return achou; //retorna o bool se achou ou não o nome
        }

        void ResetarBotoes()
        {
            for (int i = 0; i < qtosBotoes; i++) // percorremos o vetor de botões pressionados
            { 
                quaisBotoes[i].Enabled = true; // habilitamos de volta
                quaisBotoes[i].BackColor = Color.White; // mudamos sua cor para a anterior
            }
            qtosBotoes = 0; //zeramos a quantidade de botões pressionados
        }

        void LimparDgv(DataGridView qualDgv)
        {
            for(int i = 0; i < vetor.QtosCaracteres; i++) // percorremos o vetor de caracteres
            {
                qualDgv.Rows[0].Cells[i].Value = "";  // para cada caracter(que está exibido no data grid view), limpamos o dgv
            }
            qualDgv.ColumnCount = 15; // voltamos para a posição normal do data grid view
        }

        int tempoRestante = 60; // 60 segundos para o jogador adivinhar a palavra
        private void tmrTempo_Tick(object sender, EventArgs e)
        {
            lbTempo.Text = "Tempo Restante: " + tempoRestante + " s"; // exibimos quantos segundos faltam
            tempoRestante--;  // diminuimos 1 segundo a cada contagem

            if (tempoRestante < 0) // se o timer chegou ao zero, ou seja, passou-se o tempo limite de 60 segundos
            {
                tmrTempo.Stop(); //paramos o timer
                tmrTempo.Enabled = false; // e também o desabilitamos
                Perdeu(); // o jogador perde
            }
        }

        

        void ResetarImagens() // deixamos as imagens da forca invisíveis
        {
            imgErro1.Visible = false;
            imgAuxErro1.Visible = false;
            imgErro2.Visible = false;
            imgErro3.Visible = false;
            imgErro4.Visible = false;
            imgErro5.Visible = false;
            imgErro6.Visible = false;
            imgErro7.Visible = false;
            imgErro8.Visible = false;
            gifMorto.Visible = false;
            picGanhou.Visible = false;
            picGanhou2.Visible = false;
        }

        void ExibirErrosNaForca(int nmrErro) // conforme o número de erros, exibimos a personagem na forca
        {
            switch (nmrErro) // utilizamos o switch para determinar qual imagem exibir conforme o número do erro
            {                
                case 1:
                    imgErro1.Visible = true;
                    imgAuxErro1.Visible = true;
                    imgAuxErro1.BringToFront();
                    break;
                case 2:
                    imgErro2.Visible = true;
                    break;
                case 3:
                    imgErro3.Visible = true;
                    imgErro3.BringToFront();
                    imgAuxErro1.BringToFront();
                    break;
                case 4:
                    imgErro4.Visible = true;
                    imgErro4.BringToFront();
                    break;
                case 5:
                    imgErro5.Visible = true;
                    break;
                case 6:
                    imgErro6.Visible = true;
                    break;
                case 7:
                    imgErro7.Visible = true;
                    break;
                case 8:
                    imgErro8.Visible = true;
                    imgErro8.BringToFront();
                    gifMorto.Visible = true;
                    break;
            }
        }

        void ImagensPerdeu() //exibimos as imagens da derrota
        {
            imgErro1.Visible = true;
            imgAuxErro1.Visible = true;
            imgErro2.Visible = true;
            imgErro3.Visible = true;
            imgErro4.Visible = true;
            imgErro5.Visible = true;
            imgErro6.Visible = true;
            imgErro7.Visible = true;
            imgErro8.Visible = true;
            gifMorto.Visible = true;

            this.Refresh();  // atualizamos o form para exibir as imagens
            Application.DoEvents();
        }

        private void tmrAgora_Tick(object sender, EventArgs e)
        {
            lbData.Text = "Data: " + DateTime.Now.ToLongDateString(); // Exibe o dia em que o usuário está jogando
            lbHorario.Text = "Horário: " + DateTime.Now.ToString("hh:mm:ss"); // exibe o horário
        }

        private void txtSeuNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        void ImagensGanhou() //exibimos as imagens da vitória
        {
            imgErro1.Visible = true;
            imgAuxErro1.Visible = true;
            imgErro2.Visible = true;
            imgErro3.Visible = true;
            imgErro4.Visible = true;
            imgErro5.Visible = true;
            imgErro6.Visible = true;
            imgErro7.Visible = true;
            picGanhou.Visible = true;
            picGanhou2.Visible = true;

            this.Refresh(); // atualizamos o form para exibir as imagens
            Application.DoEvents();
        }
    }
}
