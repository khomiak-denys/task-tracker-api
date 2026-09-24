using System.ComponentModel.DataAnnotations;

namespace ServiceDefaults.Authentification
{
    public class JwtOptions
    {
        public const string SectionName = nameof(JwtOptions);
        [Required]
        public required string Key { get; init; }
        [Required]
        public required string Issuer { get; init; }
        [Required]
        public required string Audience { get; init; }
        [Required]
        public required int TokenValidityMins { get; init; }
    }
}
