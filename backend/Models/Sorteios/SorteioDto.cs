using backend.Models.Apostas;

namespace backend.Models.Sorteios;

public record SorteioDto(int id, List<int> numerosGanhadores, DateTime data, ICollection<int> todasApostasIds, SorteioStatus status);
public record SorteioDtoAdmin(int id, List<int> numerosGanhadores, DateTime data, ICollection<int> todasApostasIds, SorteioStatus status);
public record NumerosGanhadoresRodadaDto(List<int> numerosGanhadores);