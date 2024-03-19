namespace backend.Models.Sorteios;

public record GanhadorSorteioDto(int idAposta, int idSorteio, string nomeGanhador, string cpfGanhador, List<int> numerosEscolhidos, DateTime data);