namespace backend.Models.Apostas;

public record ApostasAdminDto(int id, string nome, string cpf, List<int> numerosEscolhidos, string data);
