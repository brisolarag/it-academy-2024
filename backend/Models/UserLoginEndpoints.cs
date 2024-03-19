using backend.Models.Users;
using backend.Repositories;
using backend.Services;

namespace backend.Models;

public static class UserLoginEndpoints
{
    public static void AddLoginEndpoints(this WebApplication app)
    {
        app.MapPost("/login", (UserLoginReq model) =>
        {
            var user = UserRepository.Get(model.user, model.password);

            if (user == null)
                return Results.NotFound(new
                {
                    err = true,
                    msg = "Invalid username or password"
                });
            var tokenG = TokenService.GenerateToken(user);
            user.Password = "";

            return Results.Ok(new
            {
                err = false,
                msg = "Logado como Admin",
                userLogged = user,
                token = tokenG
            });
        });
    }
}