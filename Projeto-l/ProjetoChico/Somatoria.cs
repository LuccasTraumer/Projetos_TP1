using System;
public class Somatoria
{
  double soma,somaInverso;  // acumulará a soma dos valores passados ao método Somar()
  int qntsValoresForamSomados, qtdValoresInversosSomados;


    public Somatoria()
    {
        soma = 0;
        somaInverso = 0;
        qntsValoresForamSomados = 0; // Qnts vezes foram somados 
        qtdValoresInversosSomados = 0; // Qnts vezes foram somados 

    }

    public void Somar(double valorASerSomado)
    {
        soma = soma + valorASerSomado;   // soma += valorASerSomado;
        qntsValoresForamSomados = qntsValoresForamSomados + 1; // quantasSomas++;  ou   quantasSomas += 1;

    }

    public double Valor
  {
        get { return soma; }
        set => soma = value;  
    }

  public int Quantos
    {
        get { return qntsValoresForamSomados; }
        set => qntsValoresForamSomados = 0;
    }
  public double MediaAritmetica // Propriedade que Pega o Valor da Media Aritmetica
  {
        get
        {
            if (qntsValoresForamSomados <= 0)
                throw new Exception("Divisão por zero!");

            return soma / qntsValoresForamSomados;
        }
    }
    public double MediaHarmonica // Propriedade que Pega o Valor da Media Harmonica
    {
        get
        {
            if (somaInverso == 0)
                throw new Exception("macaco");

            if (qtdValoresInversosSomados < 0)
                throw new Exception("Jácare");

            return qtdValoresInversosSomados / somaInverso;
        }
    }

    public void SomaInversos(double vSomado) // Propriedade que Pega o Valor do Calculo dos Inversos
    {
        if (vSomado == 0)
            throw new Exception("não pode ser zero");
        somaInverso += 1 / vSomado;
        qtdValoresInversosSomados++;
    }
}

