namespace backend.Models.Apostas;

public record NewApostaReq(string nome, string cpf, List<int> numerosEscolhidos);