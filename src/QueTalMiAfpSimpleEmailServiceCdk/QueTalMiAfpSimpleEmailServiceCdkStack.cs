using Amazon.CDK;
using Amazon.CDK.AWS.Route53;
using Amazon.CDK.AWS.SES;
using Amazon.CDK.AWS.SSM;
using Constructs;
using Newtonsoft.Json;
using System;

namespace QueTalMiAfpSimpleEmailServiceCdk
{
    public class QueTalMiAfpSimpleEmailServiceCdkStack : Stack
    {
        internal QueTalMiAfpSimpleEmailServiceCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            string appName = System.Environment.GetEnvironmentVariable("APP_NAME") ?? throw new ArgumentNullException("APP_NAME");
            string region = System.Environment.GetEnvironmentVariable("REGION_AWS") ?? throw new ArgumentNullException("REGION_AWS");

            string mailDomain = System.Environment.GetEnvironmentVariable("MAIL_DOMAIN") ?? throw new ArgumentNullException("MAIL_DOMAIN");
            string mailFromDomain = System.Environment.GetEnvironmentVariable("MAIL_FROM_DOMAIN") ?? throw new ArgumentNullException("MAIL_FROM_DOMAIN");

            string nombreDeDefecto = System.Environment.GetEnvironmentVariable("NOMBRE_DE_DEFECTO") ?? throw new ArgumentNullException("NOMBRE_DE_DEFECTO");
            string correoDeDefecto = System.Environment.GetEnvironmentVariable("CORREO_DE_DEFECTO") ?? throw new ArgumentNullException("CORREO_DE_DEFECTO");

            EmailIdentity emailIdentity = new(this, $"{appName}EmailIdentity", new EmailIdentityProps {
                Identity = Identity.Domain(mailDomain),
                MailFromDomain = mailFromDomain,
                MailFromBehaviorOnMxFailure = MailFromBehaviorOnMxFailure.USE_DEFAULT_VALUE,
            });

            _ = new StringParameter(this, $"{appName}StringParameterDireccionDeDefecto", new StringParameterProps {
                ParameterName = $"/{appName}/SES/DireccionDeDefecto",
                Description = $"Direccion por defecto a usar como emisor de correos de la aplicacion {appName}",
                StringValue = JsonConvert.SerializeObject(new { 
                    Nombre = nombreDeDefecto,
                    Correo = correoDeDefecto
                }),
                Tier = ParameterTier.STANDARD,
            });
        }
    }
}
