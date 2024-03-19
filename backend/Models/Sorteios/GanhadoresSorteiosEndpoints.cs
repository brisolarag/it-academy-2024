using backend.Data;
using backend.Models.Apostas;
using Microsoft.EntityFrameworkCore;

namespace backend.Models.Sorteios;

public static class GanhadoresSorteiosEndpoints
{
    public static void AddGanhadoresEndpoints(this WebApplication app)
    {
        // Recebe todos ganhadores : ADMIN
        var ganhadoresRoutes = app.MapGroup("ganhadores");
        ganhadoresRoutes.MapGet("admin", async (AppDbContext context, CancellationToken ct) =>
        {
            var todosGanhadores = await context.Ganhadores
                .Select(td => new GanhadorSorteioDto(td.IdAposta, td.IdSorteio, td.NomeGanhador, td.CpfGanhador, td.NumerosEscolhidos, td.DataAposta))
                .ToListAsync(ct);
            return Results.Ok(todosGanhadores);
        });
        
        // Deletar ganhadores : PREVISORIO
        ganhadoresRoutes.MapDelete("{id:guid}", async (Guid id, AppDbContext context, CancellationToken ct) =>
        {
            var ganhadorDelete = await context.Ganhadores.FirstOrDefaultAsync(c => c.Id == id);
            context.Ganhadores.Remove(ganhadorDelete!);
            await context.SaveChangesAsync(ct);
        });
        ganhadoresRoutes.MapGet("rodada", async (AppDbContext context, CancellationToken ct) =>
        {
            var apostasGanhadoras = await context.Sorteios
                .Include(sorteio => sorteio.TodasApostas)
                .Where(s => s.Status != SorteioStatus.SorteioEncerrado)
                .SelectMany(s => s.TodasApostas)
                .Where(a => a.NumerosEscolhidos!.All(numero => a.Sorteio.NumerosGanhadores.Contains(numero)))
                .Select(a => new ApostaAdminDto(a.Id, a.Nome, a.Cpf, a.NumerosEscolhidos!, a.DataAposta))
                .ToListAsync(ct);
            if (apostasGanhadoras.Count <= 0)
                return Results.NoContent();
            return Results.Ok(apostasGanhadoras);
        });

        // Adicionar um ganhador : ADMIN
        ganhadoresRoutes.MapPost("adicionar/admin", async (AppDbContext context, CancellationToken ct) =>
        {
            var sorteioRodada = await context.Sorteios
                .FirstOrDefaultAsync(s => s.Status != SorteioStatus.SorteioEncerrado);
            var apostasGanhadoras = await context.Sorteios
                .Include(sorteio => sorteio.TodasApostas)
                .Where(s => s.Status != SorteioStatus.SorteioEncerrado)
                .SelectMany(s => s.TodasApostas)
                .Where(a => a.NumerosEscolhidos!.All(numero => a.Sorteio.NumerosGanhadores.Contains(numero)))
                .ToListAsync(ct);
            foreach (var aposta in apostasGanhadoras)
            {
                await context.Ganhadores.AddAsync(new GanhadorSorteio(aposta.Id, sorteioRodada!.Id, aposta.Nome, aposta.Cpf,
                    aposta.NumerosEscolhidos!));
            }

            await context.SaveChangesAsync(ct);
            return Results.Ok();
        });
    }
}
