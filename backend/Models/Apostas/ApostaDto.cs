namespace backend.Models.Apostas;
public record ApostaDto(int id, string nome, string cpf, DateTime data);
public record ApostaAdminDto(int id, string nome, string cpf, List<int> numerosEscolhidos, DateTime data);