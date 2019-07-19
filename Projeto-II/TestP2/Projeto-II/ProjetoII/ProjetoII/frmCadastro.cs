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
/* A Classe do Form, só estara responsavel por poucos Metodos Proprios(armazenara poucos Metodos)
 ela ficara responsavel pelos Metodos de Click e por chamar Metodos de outras Classes
 No Caso dessa Classe tem apeenas dois metodos internos são eles a de Atualizar a Tela,Limpar */
namespace ProjetoII
{
    public partial class frmCadastro : Form
    {
        string nomeArq;
        public frmCadastro(string nomeArq) // Recebe como parametro o Nome do Arquivo
        {
            InitializeComponent();
            this.nomeArq = nomeArq;
        }

        // declaração global; asPalavras fica acessível em todo o programa
        VetorPalavra asPalavras = null;

        int ondeIncluir;
        private void FrmFunc_Load(object sender, EventArgs e)
        {
            // prepara o toolstrip para exibir os ícones dos botões:
            tsBotoes.ImageList = imlBotoes;
            int indice = 0;
            foreach (ToolStripItem item in tsBotoes.Items)
                if (item is ToolStripButton)
                    (item as ToolStripButton).ImageIndex = indice++;

           
                asPalavras = new VetorPalavra(10);
                asPalavras.LerDados(nomeArq);
                asPalavras.PosicionarNoInicio();
                AtualizarTela();
            
        }
        void LimparTela() // Metodo para Limpar os Campos 
        {
            txtDicas.Clear(); // Limpa a Caixa do Seletor
            txtPalavra.Clear(); // Limpa a Caixa da Matricula

        }
        void AtualizarTela() // Metodo extremamente importante que atualiza a tela 
        {
            if (asPalavras.EstaVazio)
                LimparTela();
            else
            {
                PalavraDica qualPalav = asPalavras[asPalavras.PosicaoAtual];
                txtDicas.Text = qualPalav.Dica + "";
                txtPalavra.Text = qualPalav.Palavra + "";

            }
            stlbMensagem.Text = "Registro " + (asPalavras.PosicaoAtual + 1) +
                                        " de " + asPalavras.Tamanho;
        }

        private void btnProximo_Click(object sender, EventArgs e) // Chama o Metodo AvancarPosicao da Classe VetorFuncionario e Atualiza a tela
        {
            asPalavras.AvancarPosicao(); // Chama o Metodo da Classe VetorPalavra para ir ao proximo
            AtualizarTela(); // Chama o metodo responsavel por atualizar os dados na tela
        }

        private void btnInicio_Click(object sender, EventArgs e) // Vai para o Primeiro Indice e Atualiza a Tela
        {
            asPalavras.PosicionarNoInicio(); // Posiciona no Primeiro indice 
            AtualizarTela();
        }

        private void btnAnterior_Click(object sender, EventArgs e) // Volta uma Posição do vetor, de acordo onde você esteja no vetor
        {
            asPalavras.RetrocederPosicao(); // Retorna o indice, mas caso se o Primeiro não faz isso
            AtualizarTela();
        }

        private void btnUltimo_Click(object sender, EventArgs e) // Vai para o Ultimo Indice do vetor
        {
            asPalavras.PosicionarNoUltimo(); // Posiciona no ultimo indice do Vetor
            AtualizarTela();
        }

        private void btnSair_Click(object sender, EventArgs e) // Sai do Programa 
        {
            asPalavras.GravacaoEmDisco(nomeArq); // Salva o Arquivo antes de fechar 
            Close(); // Fecha a Janela 
        }

        private void FrmFunc_FormClosing(object sender, FormClosingEventArgs e) // Antes de Fechar o Programa Salva os Dados
        {
            asPalavras.GravacaoEmDisco(nomeArq); // Salva os Dados no Nome do Arquivo que foi instanciado 
        }

        private void btnNovo_Click(object sender, EventArgs e) // Cria um Novo Funcionario
        {
            asPalavras.SituacaoAtual = Situacao.incluindo;
            LimparTela();
            stlbMensagem.Text = "Digite a Nova Palavra e Dica";
            txtPalavra.Focus();
        }

        private void txtPalavra_Leave(object sender, EventArgs e) // Colocar uma nova Palavra 
        {
            if (txtPalavra.Text == "")
                MessageBox.Show("Digite uma Palavra válida!"); // Caso não colocar nada na palavra
            else
            if (asPalavras.SituacaoAtual == Situacao.incluindo)
            {
                var palavra = new PalavraDica(txtPalavra.Text, txtDicas.Text);
                ondeIncluir = -1;
                if (asPalavras.Existe(palavra, ref ondeIncluir))
                {
                    MessageBox.Show("Palavra repetida, não pode ser incluída"); // Caso a Palavra ja exista
                    asPalavras.SituacaoAtual = Situacao.navegando;
                    AtualizarTela();
                }
                else
                {
                    txtDicas.Focus();
                    stlbMensagem.Text = "Digite os demais campos e pressione [Salvar]"; // Mensagem que fica embaixo 
                }
            }
            else
              if (asPalavras.SituacaoAtual == Situacao.procurando)
              {
                var palavra = new PalavraDica(txtPalavra.Text, "");
                int ondeEsta = -1;
                if (asPalavras.Existe(palavra, ref ondeEsta))
                    asPalavras.PosicaoAtual = ondeEsta;  // reposiciona para exibir
                else
                    MessageBox.Show("Matrícula não encontrada!");

                asPalavras.SituacaoAtual = Situacao.navegando;
                AtualizarTela();
              }
        }


        private void btnSalvar_Click(object sender, EventArgs e) // Vai Salvar os Dados incluidos ou alterados 
        {
            try
            {
                if (asPalavras.SituacaoAtual == Situacao.incluindo)
                {
                    var novaPalavra = new PalavraDica(txtPalavra.Text, txtDicas.Text);
                    asPalavras.Incluir(novaPalavra, ondeIncluir);
                    asPalavras.PosicaoAtual = ondeIncluir;
                    AtualizarTela();
                }
                else
                  if (asPalavras.SituacaoAtual == Situacao.editando)
                  {
                    var novaPalavra = new PalavraDica(txtPalavra.Text, txtDicas.Text);

                    asPalavras[asPalavras.PosicaoAtual] = novaPalavra;
                    asPalavras.SituacaoAtual = Situacao.navegando;
                    AtualizarTela();
                  }
            }
            catch
            {
                MessageBox.Show("Numero Limite de Arquivo");
            }
        }


        private void btnOrdenar_Click(object sender, EventArgs e) // Ordena os Dados coloca na Primeira Posição e Atualiza a Tela 
        {
            //asPalavras.Ordenar();
            asPalavras.PosicionarNoInicio();
            AtualizarTela();
        }

        private void btnExcluir_Click(object sender, EventArgs e) // Exclui um Registro do Arquivo
        {
            if (MessageBox.Show("Deseja realmente excluir?",
                                "Atenção para exclusão!",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                asPalavras.Excluir(asPalavras.PosicaoAtual);

                if (asPalavras.PosicaoAtual >= asPalavras.Tamanho)
                    asPalavras.PosicionarNoUltimo();

                AtualizarTela();
            }
        }

        private void btnProcurar_Click(object sender, EventArgs e) // Vai Procura o registro apartir do Numero da Matricula
        {
            asPalavras.SituacaoAtual = Situacao.procurando;
            LimparTela();
            stlbMensagem.Text = "Digite a Palavra Desejada";
            txtPalavra.Focus();
        }

        private void tpLista_Enter(object sender, EventArgs e)
        {
            asPalavras.Listar(txtLista,
             "Palavras                    Dicas");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            asPalavras.SituacaoAtual = Situacao.navegando;
            AtualizarTela();
        }

        private void btnEditar_Click(object sender, EventArgs e) // Vai editar um registro, já existente obs: não pode ser a Matricula
        {
            txtPalavra.ReadOnly = true; // para não alterar matrícula
            asPalavras.SituacaoAtual = Situacao.editando;
            txtDicas.Focus();
            stlbMensagem.Text = "Digite os novos valores e pressione [Salvar]";
        }

        private void TxtPalavra_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDicas_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLista_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
