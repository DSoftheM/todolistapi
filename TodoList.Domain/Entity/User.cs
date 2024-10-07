namespace TodoList.Domain.Entity;

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }
}

public class UserSiteDto
{
    public string Id { get; set; } = "";
    public string Name { get; set; }
}

public static class UserConverterExtension
{
    public static UserSiteDto ToSiteDto(this User user)
    {
        return new UserSiteDto() { Name = user.Name };
    }
}