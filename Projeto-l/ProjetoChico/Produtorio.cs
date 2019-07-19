using System;

class Produtorio
{
  double produtorio; // atributo que acumulará o produto dos valores passados ao método Multiplicar()
  int quantosValoresForamMultiplicados;

  public Produtorio()
  {
    produtorio = 1;
    quantosValoresForamMultiplicados = 0;
  }
  public void Multiplicar(double valorASerMultiplicado)
  {
    produtorio *= valorASerMultiplicado;
    quantosValoresForamMultiplicados++;
  }

  public double MediaGeometrica()
  {
    return Math.Pow(produtorio, 1.0 / quantosValoresForamMultiplicados);
  }

  public double Valor
  {
    get => produtorio;
  }

  public int Quantos
  {
    get => quantosValoresForamMultiplicados;
  }


}

