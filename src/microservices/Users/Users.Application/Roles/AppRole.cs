using System.Text.Json.Serialization;

namespace Users.Application.Roles
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AppRole
    {
        Admin = 1,
        Manager = 2,
        User = 3
    }
}
