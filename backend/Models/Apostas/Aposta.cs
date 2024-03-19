using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models.Sorteios;

namespace backend.Models.Apostas;

public class Aposta
{
    [Key]
    public int Id { get; set; } 
    
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public List<int>? NumerosEscolhidos { get; set; }
    public DateTime DataAposta { get; init; }
    
    public int SorteioId { get; set; }
    public Sorteio Sorteio { get; set; }
    
    public int? SorteioGanhadorId { get; set; }
    public Sorteio SorteioGanhador { get; set; }

    private static int idStarter = 999;
    private List<int> idsGerados = new List<int>();

    private void updateIdsGerados()
    {
        idStarter++;
        if (idsGerados.Contains(idStarter))
        {
            updateIdsGerados();
        }
        idsGerados.Add(idStarter);
    } 
    

    private Random rnd = new Random();
    private void randomizarNumerosFaltantes(List<int> numerosEscolhidos)
    {
        while (numerosEscolhidos.Count < 5)
        {
            int numToAdd;
            do
            {
                numToAdd = rnd.Next(1, 50);
            } while (numerosEscolhidos.Contains(numToAdd));

            numerosEscolhidos.Add(numToAdd);
        }
    }

    public Aposta(string nome, string cpf, List<int> numerosEscolhidos)
    {
        updateIdsGerados();
        Id = idStarter;
        Nome = nome;
        Cpf = cpf;
        randomizarNumerosFaltantes(numerosEscolhidos);
        NumerosEscolhidos = numerosEscolhidos;
        DataAposta = DateTime.Now;
    }

}