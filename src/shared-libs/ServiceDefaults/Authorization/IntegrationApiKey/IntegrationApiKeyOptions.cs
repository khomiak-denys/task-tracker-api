using System.ComponentModel.DataAnnotations;

namespace ServiceDefaults.Authorization.IntegrationApiKey
{
    public class IntegrationApiKeyOptions
    {
        public const string SectionName = nameof(IntegrationApiKeyOptions);
        [Required]
        public required string ApiKey { get; init; }
    }
}
