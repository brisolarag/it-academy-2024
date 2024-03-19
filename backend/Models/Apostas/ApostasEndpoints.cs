using backend.Data;
using backend.Models.Sorteios;
using Microsoft.EntityFrameworkCore;

namespace backend.Models.Apostas;

public static class ApostasEndpoints
{
    private static ApostaAdminDto generateAdminDto(Aposta aposta)
    {
        var apostaDto = new ApostaAdminDto(aposta.Id, aposta.Nome, aposta.Cpf, aposta.NumerosEscolhidos!, aposta.DataAposta);
        return apostaDto;
    }
    private static ApostaDto generateDto(Aposta aposta)
    {
        var apostaDto = new ApostaDto(aposta.Id, aposta.Nome, aposta.Cpf, aposta.DataAposta);
        return apostaDto;
    }
    
    
    
    
    public static void AddApostasEndpoints(this WebApplication app)
    {
        
        var apostasRoutes = app.MapGroup("aposta");
        
        
        // USER ROUTES:
        // Criar nova aposta:
        apostasRoutes.MapPost("", async (NewApostaReq req, AppDbContext context, CancellationToken ct) =>
        {
            if (req.numerosEscolhidos.Any(n => n <= 0 || n > 50))
            {
                return Results.BadRequest(new { err = true, msg = "Os números devem ser entre 1 e 50" });
            }
            
            var sorteioAtivo = await context.Sorteios.FirstOrDefaultAsync(s => s.Status == SorteioStatus.ApostasEmAndamento, ct);
            if (sorteioAtivo is null)
            {
                return Results.BadRequest(new { err = true, msg = "Nenhum sorteio ativo" });
            }

            var novaAposta = new Aposta(req.nome, req.cpf, req.numerosEscolhidos);
            sorteioAtivo.AddAposta(novaAposta);
            
            await context.Apostas.AddAsync(novaAposta, ct);
            await context.SaveChangesAsync(ct);
            
            return Results.Ok(new
            {
                err=false, 
                msg="Aposta criada com sucesso",
                aposta= generateDto(novaAposta)
            });
        });
        
        // ADMIN ROUTES:
        //Get all apostas:
        apostasRoutes.MapGet("/admin", async (AppDbContext context, CancellationToken ct) =>
        {
            var todasApostas = await context.Apostas.ToListAsync(ct);
            return Results.Ok(todasApostas);
        });
        //Deletar aposta:
        apostasRoutes.MapDelete("admin/{id:int}", async (int id, AppDbContext context, CancellationToken ct) =>
        {
            var apostaToDelete = await context.Apostas.FirstOrDefaultAsync(c => c.Id == id, ct);
            if (!(apostaToDelete is null))
            {
                return Results.NotFound();
            }

            context.Apostas.Remove(apostaToDelete!);
            await context.SaveChangesAsync(ct);
            return Results.Ok();
        });
        
        
        
    }
}