using System;
using System.Collections.Generic;
using static System.Math;

class Matematica
{
  int numeroInteiro; // armazenará o valor a ser usado como base nos cálculos

    ////////////////////////////////////////////////////////////////
    /// CODIFIQUE A PARTIR DAQUI OS MÉTODOS SOLICITADOS NA PROVA ///
    ////////////////////////////////////////////////////////////////
    int somaFibo = 0;



    ////////////////////////////////////////////////////////////////
    /// TERMINAM AQUI OS MÉTODOS PARA CODIFICAR NESTA CLASSE     ///
    ////////////////////////////////////////////////////////////////

    // construtor da classe Matematica, que recebe como parâmetro o valor que
    // será usado como base nos cálculos e o armazena no atributo numeroInteiro

    public Matematica(int numeroDesejado)
    {
        numeroInteiro = numeroDesejado;
    }

    // Esta função calcula o fatorial do valor armazenado no atributo numeroInteiro
  public int Fatorial()
  {
    var fat = new Produtorio(); // cria na memória um objeto da classe Produtorio

    // abaixo criamos o gerador de inteiros de 1 a numeroInteiro (Contador)
    var parcela = new Contador(1, numeroInteiro, 1);

    while (parcela.Prosseguir()) // gera números e vê se prossegue a contagem
    {
      fat.Multiplicar(parcela.Valor);  // acumula o número gerado (parcela.Valor)
    }
    return (int)fat.Valor;  // (int) ---> Type Cast ou conversão de tipo
  }

  public int FatorialSemObjetos()
  {
    int fat = 1; // valor inicial de qualquer Produtorio
    // iniciamos com 1 o gerador de inteiros 
    int parcela = 1;
    while (parcela <= numeroInteiro) // vê se prossegue a contagem
    {
      fat = fat * parcela;  // acumula o número gerado (parcela.Valor)
      parcela = parcela + 1;      // gera próximo números inteiro da sequência
    }
    return fat;
  }

  public string ListaDosDivisores()
  {
    string resultado = " 1";
    var possivelDivisor = new Contador(2, numeroInteiro / 2, 1);
    while (possivelDivisor.Prosseguir())
    {
      int resto = numeroInteiro % possivelDivisor.Valor;
      if (resto == 0)
        resultado = resultado + ", " + possivelDivisor.Valor;
    }
    resultado += ", " + numeroInteiro;
    return resultado;
  }

  public int MDC(int outroValor)
  {
    int dividendo = numeroInteiro;
    int divisor = outroValor;
    int resto = 0;
    do
    {
      resto = dividendo % divisor;
      if (resto != 0)
      {
        dividendo = divisor;
        divisor = resto;
      }
    }
    while (resto != 0);

    return divisor;
  }

  public bool EhPalindromo()
  {
    int aoContrario = 0;
    int numero = numeroInteiro;
    while (numero > 0)
    {
      int quociente = numero / 10;
      int resto = numero - 10 * quociente;  // separo o dígito mais à direita do número
      aoContrario = aoContrario * 10 + resto; // 
      numero = quociente;
    }

    if (aoContrario == numeroInteiro)
      return true;  // é palíndromo
    else
      return false; // não é palíndromo
  }

  public int SomaDeQuadrados()
  {
    var somaValores = new Somatoria();
    var contador = new Contador(1, numeroInteiro, 1);
    while (contador.Prosseguir())
    {
      int umQuadrado = contador.Valor * contador.Valor;
      somaValores.Somar(umQuadrado);
    }

    return (int)somaValores.Valor;
  }

  public int SomaDosDigitos()
  {
    var somaDig = new Somatoria();
    int numeroADecompor = numeroInteiro;
    while (numeroADecompor > 0)
    {
      int quoc = numeroADecompor / 10;
      int digito = numeroADecompor - quoc * 10;
      somaDig.Somar(digito);

      numeroADecompor = quoc;
    }
    return (int)somaDig.Valor;
  }

  public double Elevado(double a)
  {
    var pot = new Produtorio();

    var cont = new Contador(1, numeroInteiro, 1);

    while (cont.Prosseguir())
      pot.Multiplicar(a);

    return pot.Valor;
  }
  public int SomaDosDivisores() // calcula a soma dos divisores de numeroInteiro (de 1 a numeroInteiro)
  {
    int resultado = 0;
    var possivelDivisor = new Contador(2, numeroInteiro / 2, 1);
    while (possivelDivisor.Prosseguir())
    {
      int resto = numeroInteiro % possivelDivisor.Valor;
      if (resto == 0)
        resultado = resultado + possivelDivisor.Valor;
    }
    resultado += 1 + numeroInteiro;
    return resultado;
  }

  public bool EhPerfeito()
  {
    int somaComONumero = SomaDosDivisores();
    int somaSemONumero = somaComONumero - numeroInteiro;
    if (somaSemONumero == numeroInteiro)
      return true;
    else
      return false;
  }

  public bool EhPrimo1()
  {
    int somaDiv = SomaDosDivisores();  /// esse método calcula a soma dos divisores de numeroInteiro
    if (somaDiv == numeroInteiro + 1)
      return true; // numeroInteiro é primo!
    else
      return false; // numeroInteiro não é primo!
  }

  public bool EhPrimo2()
  {
    int quantosDivisores = 2;  // todo inteiro tem pelo menos 2 divisores
    var possivelDivisor = new Contador(2, numeroInteiro / 2, 1);
    while (possivelDivisor.Prosseguir())
    {
      int resto = numeroInteiro % possivelDivisor.Valor;
      if (resto == 0)          // se isso acontece, achamos mais um divisor
        quantosDivisores++;  // e o contamos para determinar a quantidade 
                             // de divisores de numeroInteiro
    }

    if (quantosDivisores == 2)   // todo número primo tem somente 2 divisores
      return true; // numeroInteiro é primo!
    else                         // números não primos tem mais que 2 divisores
      return false; // numeroInteiro não é primo!
  }

  public bool EhPrimo3()
  {
    var possivelDivisor = new Contador(2, numeroInteiro / 2, 1);

    int resto = 1; // para não começar com zero e parar o while na 1a vez!!

    while (resto != 0 && possivelDivisor.Prosseguir())
      resto = numeroInteiro % possivelDivisor.Valor; // se deu divisão exata (resto = 0), paramos de repetir

    if (resto != 0)   // todo número primo não tem divisores exatos entre 2 e sua metade
      return true; // numeroInteiro é primo!
    else                         // números não primos tem divisores entre 2 e sua metade
      return false; // numeroInteiro não é primo!
  }

  public double Cosseno(double anguloEmGraus)
  {
    double x = anguloEmGraus*Math.PI/180;  // cpnverte medida em graus para medida em radianos

    var somaDosTermos = new Somatoria();

    int sinal = 1;
    var contador = new Contador(0, numeroInteiro*2, 2);

    while (contador.Prosseguir())
    {
      double potencia = Math.Pow(x, contador.Valor);

      var fat = new Matematica(contador.Valor);
      double fatorial = fat.Fatorial();  // já temos essa função pronta na classe Matematica

      double termo = potencia / fatorial;

      somaDosTermos.Somar(sinal*termo);
      sinal = -sinal;
    }

    return somaDosTermos.Valor;
  }

  public double Pi()
  {
    int sinal = 1;
    var soma = new Somatoria();
    var i = new Contador(1, numeroInteiro * 2, 2);
    while (i.Prosseguir())
    {
      double termo = 1.0 / (i.Valor * i.Valor * i.Valor);
      soma.Somar(sinal * termo);
      sinal = -sinal; // alterna  o sinal para calcular o próximo termo
    }

    double pi = Math.Pow(32 * soma.Valor, 1.0 / 3.0);
    return pi;
  }
   public double ValorFibonacci
    {
        get => somaFibo;
    }
  public int ValuFibonacci(int valorInformado)
    {
        
        somaFibo += valorInformado + 1;
        return somaFibo;
    }  
    public string Fibonacci()
    {
        return ValorFibonacci + ",";
    }
    
    public List<string> Amigos() // Metodo que Retorna os Conjuntos de Numeros que Continuem os Numeros Amigos de 1 ao Numero Informado
    {
        if(numeroInteiro < 284)// Caso o Numero sejá Menor que 220 ele ja retorna que não existe Numero Amigo, não coloquei chaves por ser um Unico comando

            throw new Exception("Nao Existe Numero Amigo antes de 220. "); // Caso o Numero sejá Menor que 220 ele ja retorna que não existe Numero Amigo 
        List<string> resultado = new List<string>(); // Lista que vai ser Armzaenada os Resultados
        for (int i = 220; i <= numeroInteiro; i++) // Vai comecar com 220, e se for Maior vai Incrementar e Somar os Divisores
        {
            int a = SomaDosDivisores(i); // a vai receber a Soma dos Divisores de i
            if (a > i && SomaDosDivisores(a) == i) // vai verificar se a é Maior que i E se a Soma dos Divisores de a é igual a i
            {
                resultado.Add($"[\" {i.ToString()} , {a.ToString()} \"]  "); // se a condição for Verdadeira ele executa a função de adicionar na lista
                //resultado.Add("" +i.ToString() + "");
                //resultado.Add(a + "");

            }




            // return resultado;
        }
        return resultado; // Retorna para o Program o Resultado da Lista
    }
    public int SomaDosDivisores(int n) // Vai Fazer a Verificação da Soma dos Divisores dos Dois Numeros
    {
        int somaDivisores = 1;
        for (int x = 2; x <= n / 2; x++) // vai verificar se x é menor que a metade de n(valor que vem do Metodo Amigos), se for menor ele desce para a prox condição
            if (n % x == 0) // Se n resto da divisão por x for 0, ou seja ele é Par, SomaDivisores Recebe o valor.
                somaDivisores += x;
        return somaDivisores; // retorna o valores 
    }
    public string ParaBinario() // Metodo que Fz o calculo e Retorna o Valor da String do Cod. Binario.
    {
        int divisor = 2; // A Base Binaria o divisor é 2.
        string resto = "";// Variavel que vai receber o Valor do Resto da Divisão a e muda a cada calculo.
        string valores = "";// Variavel que Vai armazenar os Valores do Resto da Divisão.
       if(numeroInteiro > 64 || numeroInteiro < 0) // Se o Valor for Menor que 0 e Maior que 64 nem vai ser Calculado, Já para .
       {
            throw new Exception("Não Pode ser Maior que 64 e Menor que 0.");
       }
        do // Caso a condição seja falsa ele Desse e execulta o Calculo.
        {
            if(numeroInteiro % 2 == 0)
            {
                resto = "0";
            }
            else
            if(numeroInteiro % 2 == 1)
            {
                resto = "1";
            }
            numeroInteiro = numeroInteiro / 2;

            valores += resto;
        } while (numeroInteiro >= 1);
        
        return valores; // Retorna o sequencia de Caracteres do Resto da Divisão.
    }

    public double ConstanteCatalan() // Metodo que Retorna para o Programa o Resultado.
    {
        double resultado = 0;
        bool sinal = true;
        for (int i = 0; i < numeroInteiro; i++)
        {


            if (sinal)
            {
                resultado += CalculaCatalan(i);
                sinal = false;
            }
            else
            {
                resultado = resultado - (CalculaCatalan(i));
                sinal = true;
            }

        }
        return resultado;
    }

    public double CalculaCatalan(int indice) // Metodo que Calcula o Catalan
    {

        double resultadocata = Pow(1, indice) / Pow((2 * indice + 1), 2);
        return resultadocata;
    }


}
        
    

    
    
    
      
    
    

    

