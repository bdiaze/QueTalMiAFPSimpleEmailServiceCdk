using Amazon.CDK;
using Amazon.CDK.AWS.Route53;
using Amazon.CDK.AWS.SES;
using Constructs;
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

            EmailIdentity emailIdentity = new(this, $"{appName}EmailIdentity", new EmailIdentityProps {
                Identity = Identity.Domain(mailDomain),
                MailFromDomain = mailFromDomain,
                MailFromBehaviorOnMxFailure = MailFromBehaviorOnMxFailure.USE_DEFAULT_VALUE,
            });
        }
    }
}
