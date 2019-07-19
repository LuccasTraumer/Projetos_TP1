// Lucas Silva de Jesus 19372   
// Pedro Candido 19382

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Classe que esta responsavel por armazenar o Nome e Pontos ganho pelo Jogador
    class Jogador
    {
       private int jogadorPontos;
       private string jogadorNome;

       public Jogador(string nome, int pontuacao) // construtor da classe jogador que recebu como parâmetro o nome e a pontuação do jogador
       {
         jogadorNome = nome; // a variavel que foi declarada fora do construtor vai receber a que esta dentro
         jogadorPontos = pontuacao; 
       }

       public int JogadorPontos { // Propriedades dos Pontos do Jogador
        get => jogadorPontos;
        set => jogadorPontos = value;
    }
       public string JogadorNome {// Propriedade do Nome do Jogador
        get => jogadorNome;
        set => jogadorNome = value;
    }
}

