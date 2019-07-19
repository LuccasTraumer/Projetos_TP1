using System;

class Contador
{
  int valorInicial, valorFinal, passo;
  int contador;  // variável (atributo) com a qual fazemos a contagem

  bool primeiraRepeticao;

  public int Valor { get => contador; }

  public Contador(int vI, int vF, int p)
  {
    if (p <= 0)  // fará entrar em loop, pois cont nunca ficará > valorFinal
       throw new Exception("Valor de passo inválido!");

    if (valorFinal < valorInicial)
       throw new Exception("Valor final inconsistente com o valor inicial!");

    valorInicial = contador = vI;
    valorFinal = vF;
    passo = p;

    primeiraRepeticao = true;
  }

  public void Contar()
  {
    if (contador <= valorFinal)
       contador = contador + passo ;
  }

  public bool Prosseguir()
  {
    if (primeiraRepeticao)
       primeiraRepeticao = false;
    else
      Contar();

    return contador <= valorFinal;
  }
    public bool Fim() // Verifica se o Contador é Maior que o Valor Final 
    {
        if (contador >= valorFinal)
            return true;
        return false;
    }

  public void Iniciar()
  {
    contador = valorInicial;
    primeiraRepeticao = true;
  }
}

