using backend.Data;
using backend.Models.Apostas;
using Microsoft.EntityFrameworkCore;

namespace backend.Models.Sorteios;

public static class SorteioEndpoints
{
    private static SorteioDto generateSorteioDto(Sorteio sorteio)
    {
        var dtoToReturn = new SorteioDto(
            sorteio.Id,
            sorteio.NumerosGanhadores,
            sorteio.DataSorteio,
            sorteio.TodasApostas.Select(aposta => aposta.Id).ToList(),
            sorteio.Status);
        return dtoToReturn;
    }
    
    public static void AddSorteioEndpoints(this WebApplication app)
    {
        var sorteiosRoutes = app.MapGroup("sorteio");
        
        
        
        
        // Recebe todos sorteios relizados : ADMIN
        sorteiosRoutes.MapGet("/admin", async (AppDbContext context, CancellationToken ct) =>
        {
            var todosSorteios = await context.Sorteios
                .Select(sorteio => new SorteioDtoAdmin(
                    sorteio.Id,
                    sorteio.NumerosGanhadores,
                    sorteio.DataSorteio,
                    sorteio.TodasApostas.Select(aposta => aposta.Id).ToList(),
                    sorteio.Status))
                .ToListAsync(ct);
            return Results.Ok(todosSorteios);
        });
        sorteiosRoutes.MapGet("", async (AppDbContext context, CancellationToken ct) =>
        {
            var todosSorteios = await context.Sorteios
                .Where(sorteio => sorteio.Status != SorteioStatus.SorteioEncerrado)
                .Select(sorteio => new SorteioDtoAdmin(
                    sorteio.Id,
                    sorteio.NumerosGanhadores,
                    sorteio.DataSorteio,
                    sorteio.TodasApostas.Select(aposta => aposta.Id).ToList(),
                    sorteio.Status))
                .FirstOrDefaultAsync(ct);
            return Results.Ok(todosSorteios);
        });
        // Deleta um sorteio
        sorteiosRoutes.MapDelete("admin/{id:int}", async(int id, AppDbContext context, CancellationToken ct) =>
        {
            var sorteioToDelete = await context.Sorteios.FirstOrDefaultAsync(s => s.Id == id, ct);
            if (sorteioToDelete is null)
            {
                return Results.NotFound();
            }

            context.Sorteios.Remove(sorteioToDelete);
            await context.SaveChangesAsync(ct);
            return Results.Ok(new { err = false, idRemoved = sorteioToDelete.Id });

        });
        
        
        
        // Retorna todas apostas no momento : USER
        sorteiosRoutes.MapGet("apostas-ativas", async (AppDbContext context, CancellationToken ct) =>
        {
            var todasApostasAtivas = await context.Sorteios
                .Include(sorteio => sorteio.TodasApostas)
                .Where(s => s.Status != SorteioStatus.SorteioEncerrado)
                .SelectMany(s => s.TodasApostas)
                .Select(aposta => new ApostaDto(
                    aposta.Id,
                    aposta.Nome,
                    aposta.Cpf,
                    aposta.DataAposta
                ))
                .ToListAsync(ct);
            if (todasApostasAtivas.Count <= 0)
                return Results.NoContent();
            return Results.Ok(todasApostasAtivas);
        });
        
        // Retorna todas apostas no momento : ADMIN
        sorteiosRoutes.MapGet("admin/apostas-ativas", async (AppDbContext context, CancellationToken ct) =>
        {
            var todasApostasAtivas = await context.Sorteios
                .Include(sorteio => sorteio.TodasApostas)
                .Where(s => s.Status != SorteioStatus.SorteioEncerrado)
                .SelectMany(s => s.TodasApostas)
                .Select(aposta => new ApostaAdminDto(
                    aposta.Id,
                    aposta.Nome,
                    aposta.Cpf,
                    aposta.NumerosEscolhidos!,
                    aposta.DataAposta
                ))
                .ToListAsync(ct);
            if (todasApostasAtivas.Count <= 0)
                return Results.NoContent();
            return Results.Ok(todasApostasAtivas);
        });
        
        sorteiosRoutes.MapGet("admin/numeros-mais-escolhidos-rodada", async (AppDbContext context, CancellationToken ct) =>
        {
            var todasApostasAtivas = await context.Sorteios
                .Include(sorteio => sorteio.TodasApostas)
                .Where(s => s.Status != SorteioStatus.SorteioEncerrado)
                .SelectMany(s => s.TodasApostas)
                .Select(aposta => new ApostaAdminDto(
                    aposta.Id,
                    aposta.Nome,
                    aposta.Cpf,
                    aposta.NumerosEscolhidos!,
                    aposta.DataAposta
                ))
                .ToListAsync(ct);
            if (todasApostasAtivas.Count <= 0)
                return Results.NoContent();

            var numerosEscolhidos = todasApostasAtivas
                .SelectMany(aposta => aposta.numerosEscolhidos)
                .GroupBy(numero => numero)
                .Select(group => new { Numero = group.Key, Quantidade = group.Count() })
                .OrderByDescending(item => item.Quantidade)
                .ToList();

            if (numerosEscolhidos.Count <= 0)
                return Results.NoContent();
            
            return Results.Ok(numerosEscolhidos);
        });
        
        // Recebe os numeros ganhadores : ADMIN
        sorteiosRoutes.MapGet("admin/numeros-ganhadores", async (AppDbContext context, CancellationToken ct) =>
        {
            var numerosGanhadoresRodada = await context.Sorteios
                .Include(sorteio => sorteio.TodasApostas)
                .Where(s => s.Status != SorteioStatus.SorteioEncerrado)
                .Select(s => new NumerosGanhadoresRodadaDto(s.NumerosGanhadores))
                .FirstOrDefaultAsync(ct);
            return Results.Ok(numerosGanhadoresRodada);
        });

        
        var sorteioStatus = app.MapGroup("status");
        // Iniciar Apostas = Novo Sorteio
        sorteioStatus.MapPost("/apostas", async (AppDbContext context, CancellationToken ct) =>
        {
            var sorteioIniciado = new Sorteio();
            await context.Sorteios.AddAsync(sorteioIniciado, ct);
            await context.SaveChangesAsync(ct);
            return Results.Ok(sorteioIniciado);
        });
        // Iniciar Sorteio
        sorteioStatus.MapPut("/sorteio", async (AppDbContext context, CancellationToken ct) =>
        {
            // acha o sorteio em andamento
            var sorteioInSorteios =
                await context.Sorteios.FirstOrDefaultAsync(s => s.Status != SorteioStatus.SorteioEncerrado, ct);
            
            if (sorteioInSorteios is null)
                return Results.NotFound();
            
            sorteioInSorteios.Status = SorteioStatus.SorteioEmAndamento;
            
            var apostas = await context.Apostas
                .Where(aposta => aposta.SorteioId == sorteioInSorteios.Id)
                .ToListAsync(ct);
            
            bool encontrouGanhador = false;
            foreach (var aposta in apostas)
            {
                if (aposta.NumerosEscolhidos!.All(numero => sorteioInSorteios.NumerosGanhadores.Contains(numero)))
                {
                    encontrouGanhador = true;
                }
            }

            if (encontrouGanhador == false)
            {
                int _i = 0;
                while (!encontrouGanhador && _i < 25)
                {
                    sorteioInSorteios.AddNumero();
                    foreach (var aposta in apostas)
                    {
                        if (aposta.NumerosEscolhidos!.All(numero => sorteioInSorteios.NumerosGanhadores.Contains(numero)))
                        {
                            encontrouGanhador = true;
                            _i = 25;
                            break;
                        }
                    }

                    _i++;
                }
            }

            await context.SaveChangesAsync(ct);
            return Results.Ok();

        });
        // Iniciar Apuracao
        sorteioStatus.MapPut("/apuracao", async (AppDbContext context, CancellationToken ct) =>
        {
            // acha o sorteio em andamento
            var sorteioInSorteios =
                await context.Sorteios.FirstOrDefaultAsync(s => s.Status == SorteioStatus.SorteioEmAndamento, ct);
            
            if (sorteioInSorteios is null)
                return Results.NotFound();
            
            sorteioInSorteios.Status = SorteioStatus.ApuracaoEmAndamento;

            await context.SaveChangesAsync(ct);
            return Results.Ok();

        });
        // Iniciar Premiacao
        sorteioStatus.MapPut("/premiacao", async (AppDbContext context, CancellationToken ct) =>
        {
            // acha o sorteio em andamento
            var sorteioInSorteios =
                await context.Sorteios.FirstOrDefaultAsync(s => s.Status == SorteioStatus.ApuracaoEmAndamento, ct);
            
            if (sorteioInSorteios is null)
                return Results.NotFound();
            
            sorteioInSorteios.Status = SorteioStatus.PremiacaoEmAndamento;

            await context.SaveChangesAsync(ct);
            return Results.Ok();

        });
        // Encerrar Sorteio
        sorteioStatus.MapPut("/encerrar", async (AppDbContext context, CancellationToken ct) =>
        {
            // acha o sorteio em andamento
            var sorteioInSorteios =
                await context.Sorteios.FirstOrDefaultAsync(s => s.Status == SorteioStatus.PremiacaoEmAndamento, ct);
            
            if (sorteioInSorteios is null)
                return Results.NotFound();
            
            sorteioInSorteios.Status = SorteioStatus.SorteioEncerrado;

            await context.SaveChangesAsync(ct);
            return Results.Ok();

        });
        
    }
}