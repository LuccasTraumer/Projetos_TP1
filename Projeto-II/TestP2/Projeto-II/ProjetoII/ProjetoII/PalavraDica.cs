// Lucas Silva de Jesus 19372   
// Pedro Candido 19382

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 Classe responsavel pela separação das string. Contendo os Contrutores e Propriedades  das Palavras e Dicas
*/
namespace ProjetoII
{
    class PalavraDica 
    {
        // Declaração das Variaveis que vão receber a String Separada
        private string palavra; 
        private string dica;

        // Tamanho das Strings
        const int tamanhoPalavra = 15; 
        const int tamanhoDica = 100;
        
        // Inicio das Strings
        const int inicioPalavra = 0;
        const int inicioDica = inicioPalavra + tamanhoPalavra;

        // Construtor da Classe PalavraDica
        public PalavraDica(string linha) // são lidos e divididos em strings a palavra e sua respectiva dica
        {
            palavra = linha.Substring(inicioPalavra, tamanhoPalavra); 
            dica = linha.Substring(inicioDica, tamanhoDica); 
        }

        public PalavraDica(string palavra, string dica) // Dados que são lidos do Form
        {
            this.palavra = palavra;
            this.dica = dica;
        }

        // Propriedades
        public string Palavra { // Propriedade da Palavra usada 
            get => palavra;
            set => palavra = value;
        } 
        public string Dica { // Propriedade da Dica
            get => dica;
            set => dica = value;
        }  
        //para dar espaço entre as palavras
        public string ParaArquivo()
        {
            return $"{Palavra.PadRight(tamanhoPalavra, ' ')}{Dica.PadRight(tamanhoDica, ' ')}";
        }
    }
}
