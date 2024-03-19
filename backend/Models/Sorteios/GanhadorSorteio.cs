using System.ComponentModel.DataAnnotations;

namespace backend.Models.Sorteios;

public class GanhadorSorteio
{
    [Key]
    public Guid Id { get; private set; }
    public int IdAposta { get; private set; }
    public int IdSorteio { get; private set; }
    public string NomeGanhador { get; private set; }
    public string CpfGanhador { get; private set; }
    public List<int> NumerosEscolhidos { get; private set; }
    public DateTime DataAposta { get; private set; }

    public GanhadorSorteio(int idAposta, int idSorteio, string nomeGanhador, string cpfGanhador, List<int> numerosEscolhidos)
    {
        Id = Guid.NewGuid();
        IdAposta = idAposta;
        IdSorteio = idSorteio;
        NomeGanhador = nomeGanhador;
        CpfGanhador = cpfGanhador;
        NumerosEscolhidos = numerosEscolhidos;
        DataAposta = DateTime.Now;
    }
}