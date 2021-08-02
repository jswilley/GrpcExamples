using Bjork.UI.Infrastructure.Authorization;
using Bjork.UI.Infrastructure.Configs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Bjork.UI.Infrastructure
{
    public class RegisterAuthPolicies : IServiceRegistration
    {

        private Serilog.ILogger logger;

        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            HttpClientPolicyConfiguration policyConfigs = new HttpClientPolicyConfiguration();
            logger = Log.ForContext<RegisterAuthPolicies>();
            {
                Policies.InitPolicies(configuration);

                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                services.AddAuthorization(config =>
                {
                    config.AddPolicy("required", policy);
                    config.AddPolicy(Policies.IsAdmin, Policies.AdminPolicy());
                    config.AddPolicy(Policies.IsIntel, Policies.Intel());
                    config.AddPolicy(Policies.IsClassification, Policies.ClassificationPolicy());
                    config.AddPolicy(Policies.IsCorrectionHealth, Policies.CorrectionHealth());
                    config.AddPolicy(Policies.IsCorrections, Policies.Corrections());
                    config.AddPolicy(Policies.IsRecog, Policies.Recog());
                    // config.AddPolicy(Policies.IsCorrectionHealth, Policies.CorrectionHealth());
                    config.AddPolicy(Policies.IsPPID, Policies.PPID());
                    config.AddPolicy(Policies.IsCHSupervisor, Policies.CorrectionHealthSupervisor());
                    config.AddPolicy(Policies.IsCorrSupervisor, Policies.CorrectionsSupervisor());
                    config.AddPolicy(Policies.IsNoteReviewer, Policies.NoteReviewer());
                    config.AddPolicy(Policies.IsMedicalNeed, Policies.MedicalNeed());
                   // config.AddPolicy(Policies.IsEditSafetyAlert, Policies.EditSafetyAlert());
                    config.AddPolicy(Policies.IsUser, Policies.IsUserPolicy());
                });
            }

        }

       
    }
}
