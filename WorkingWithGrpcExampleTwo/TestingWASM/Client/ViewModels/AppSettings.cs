using System.ComponentModel.DataAnnotations;

namespace Example2.Api.Common.Settings
{
    public class AppSettings
    {
        [Required]
        public AzureAdConfigurations AzureAd { get; set; }

        [Required]
        public EndpointSettingsConfigurations EndpointSettings { get; set; }

        public RoleConfiguration Roles { get; set; }
        public HttpClientPolicyConfiguration HttpClientPolicies { get; set; }
    }

    public class EndpointSettingsConfigurations
    {
        public string ApiBaseUrl { get; set; }

        public string FormEntryUrl { get; set; }
    }

    public class RoleConfiguration
    {
        public string Admin { get; set; }
    }

    //public class DefaultValuesConfiguration
    //{
    //}

    public class AzureAdConfigurations
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public bool ValidateAuthority { get; set; }
    }

    public class HttpClientPolicyConfiguration
    {
        public int RetryCount { get; set; }
        public int RetryDelayInMs { get; set; }
        public int RetryTimeoutInSeconds { get; set; }
        public int BreakDurationInSeconds { get; set; }
        public int MaxAttemptBeforeBreak { get; set; }
        public int HandlerTimeoutInMinutes { get; set; }
    }
}