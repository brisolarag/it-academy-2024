using backend.Models.Users;

namespace backend.Repositories;

public static class UserRepository
{
    public static User Get(string username, string password)
    {
        var user = new User { Id = 1, Username = "admin", Password = "admindell", Role = "admin" };
    
        if (username.ToLower() == user.Username.ToLower() && password == user.Password)
            return user;

        return null!;
    }
    
}