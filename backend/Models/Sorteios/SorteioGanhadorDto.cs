namespace backend.Models.Sorteios;

public record SorteioGanhadorDto(int id, int idSorteio, string nome, string cpf, List<int> numerosEscolhidos, DateTime data);