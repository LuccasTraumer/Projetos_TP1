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
/*
 Janela Principal que ira chamar os dois outros Forms e que vai selecionar o Arquivo de Texto
*/
namespace ProjetoII
{
    public partial class frmPrincipal : Form
    {
        frmCadastro frmCadast = null;
        frmForca frmJogo = null;
        string nomeArq; // Variavel que vai receber o nome do Arquivo selecinado
        public frmPrincipal()
        {
            // Assim que iniciar o Principal vai pedir para selecionar o Arquivo 
            // o arquivo sera usado para o Jogo e Cadastro 
            InitializeComponent();
            if(dlgOpen.ShowDialog() == DialogResult.OK)
            {
                nomeArq = dlgOpen.FileName;
            }
            
        }

        private void JogarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmJogo = new frmForca(nomeArq); // Passa como parametro o nome do Arquivo 
            frmJogo.Show(); // Mostra a Janela do Jogo
        }

        private void SairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CadastroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadast = new frmCadastro(nomeArq);// Passa como parametro o nome do Arquivo 
            frmCadast.Show(); // Abre a Janela do Cadastro
            
        }
    }
}
