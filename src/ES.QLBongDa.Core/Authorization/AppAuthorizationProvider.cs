using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace ES.QLBongDa.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var rankings = pages.CreateChildPermission(AppPermissions.Pages_Rankings, L("Rankings"), multiTenancySides: MultiTenancySides.Host);
            rankings.CreateChildPermission(AppPermissions.Pages_Rankings_Create, L("CreateNewRanking"), multiTenancySides: MultiTenancySides.Host);
            rankings.CreateChildPermission(AppPermissions.Pages_Rankings_Edit, L("EditRanking"), multiTenancySides: MultiTenancySides.Host);
            rankings.CreateChildPermission(AppPermissions.Pages_Rankings_Delete, L("DeleteRanking"), multiTenancySides: MultiTenancySides.Host);

            var matchs = pages.CreateChildPermission(AppPermissions.Pages_Matchs, L("Matchs"), multiTenancySides: MultiTenancySides.Host);
            matchs.CreateChildPermission(AppPermissions.Pages_Matchs_Create, L("CreateNewMatch"), multiTenancySides: MultiTenancySides.Host);
            matchs.CreateChildPermission(AppPermissions.Pages_Matchs_Edit, L("EditMatch"), multiTenancySides: MultiTenancySides.Host);
            matchs.CreateChildPermission(AppPermissions.Pages_Matchs_Delete, L("DeleteMatch"), multiTenancySides: MultiTenancySides.Host);

            var managers = pages.CreateChildPermission(AppPermissions.Pages_Managers, L("Managers"), multiTenancySides: MultiTenancySides.Host);
            managers.CreateChildPermission(AppPermissions.Pages_Managers_Create, L("CreateNewManager"), multiTenancySides: MultiTenancySides.Host);
            managers.CreateChildPermission(AppPermissions.Pages_Managers_Edit, L("EditManager"), multiTenancySides: MultiTenancySides.Host);
            managers.CreateChildPermission(AppPermissions.Pages_Managers_Delete, L("DeleteManager"), multiTenancySides: MultiTenancySides.Host);

            var players = pages.CreateChildPermission(AppPermissions.Pages_Players, L("Players"), multiTenancySides: MultiTenancySides.Host);
            players.CreateChildPermission(AppPermissions.Pages_Players_Create, L("CreateNewPlayer"), multiTenancySides: MultiTenancySides.Host);
            players.CreateChildPermission(AppPermissions.Pages_Players_Edit, L("EditPlayer"), multiTenancySides: MultiTenancySides.Host);
            players.CreateChildPermission(AppPermissions.Pages_Players_Delete, L("DeletePlayer"), multiTenancySides: MultiTenancySides.Host);

            var nations = pages.CreateChildPermission(AppPermissions.Pages_Nations, L("Nations"), multiTenancySides: MultiTenancySides.Host);
            nations.CreateChildPermission(AppPermissions.Pages_Nations_Create, L("CreateNewNation"), multiTenancySides: MultiTenancySides.Host);
            nations.CreateChildPermission(AppPermissions.Pages_Nations_Edit, L("EditNation"), multiTenancySides: MultiTenancySides.Host);
            nations.CreateChildPermission(AppPermissions.Pages_Nations_Delete, L("DeleteNation"), multiTenancySides: MultiTenancySides.Host);

            var tables = pages.CreateChildPermission(AppPermissions.Pages_Tables, L("Tables"), multiTenancySides: MultiTenancySides.Host);
            tables.CreateChildPermission(AppPermissions.Pages_Tables_Create, L("CreateNewTable"), multiTenancySides: MultiTenancySides.Host);
            tables.CreateChildPermission(AppPermissions.Pages_Tables_Edit, L("EditTable"), multiTenancySides: MultiTenancySides.Host);
            tables.CreateChildPermission(AppPermissions.Pages_Tables_Delete, L("DeleteTable"), multiTenancySides: MultiTenancySides.Host);

            var vilages = pages.CreateChildPermission(AppPermissions.Pages_Vilages, L("Vilages"), multiTenancySides: MultiTenancySides.Host);
            vilages.CreateChildPermission(AppPermissions.Pages_Vilages_Create, L("CreateNewVilage"), multiTenancySides: MultiTenancySides.Host);
            vilages.CreateChildPermission(AppPermissions.Pages_Vilages_Edit, L("EditVilage"), multiTenancySides: MultiTenancySides.Host);
            vilages.CreateChildPermission(AppPermissions.Pages_Vilages_Delete, L("DeleteVilage"), multiTenancySides: MultiTenancySides.Host);

            var clubs = pages.CreateChildPermission(AppPermissions.Pages_Clubs, L("Clubs"), multiTenancySides: MultiTenancySides.Host);
            clubs.CreateChildPermission(AppPermissions.Pages_Clubs_Create, L("CreateNewClub"), multiTenancySides: MultiTenancySides.Host);
            clubs.CreateChildPermission(AppPermissions.Pages_Clubs_Edit, L("EditClub"), multiTenancySides: MultiTenancySides.Host);
            clubs.CreateChildPermission(AppPermissions.Pages_Clubs_Delete, L("DeleteClub"), multiTenancySides: MultiTenancySides.Host);

            var stadiums = pages.CreateChildPermission(AppPermissions.Pages_Stadiums, L("Stadiums"), multiTenancySides: MultiTenancySides.Host);
            stadiums.CreateChildPermission(AppPermissions.Pages_Stadiums_Create, L("CreateNewStadium"), multiTenancySides: MultiTenancySides.Host);
            stadiums.CreateChildPermission(AppPermissions.Pages_Stadiums_Edit, L("EditStadium"), multiTenancySides: MultiTenancySides.Host);
            stadiums.CreateChildPermission(AppPermissions.Pages_Stadiums_Delete, L("DeleteStadium"), multiTenancySides: MultiTenancySides.Host);

            pages.CreateChildPermission(AppPermissions.Pages_QLBongDaUiComponents, L("QLBongDaUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Unlock, L("Unlock"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeDefaultLanguage, L("ChangeDefaultLanguage"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            var webhooks = administration.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription, L("Webhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Create, L("CreatingWebhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Edit, L("EditingWebhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_ChangeActivity, L("ChangingWebhookActivity"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Detail, L("DetailingSubscription"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ListSendAttempts, L("ListingSendAttempts"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ResendWebhook, L("ResendingWebhook"));

            var dynamicProperties = administration.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties, L("DynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Create, L("CreatingDynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Edit, L("EditingDynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Delete, L("DeletingDynamicProperties"));

            var dynamicPropertyValues = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue, L("DynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Create, L("CreatingDynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Edit, L("EditingDynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Delete, L("DeletingDynamicPropertyValue"));

            var dynamicEntityProperties = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties, L("DynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Create, L("CreatingDynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Edit, L("EditingDynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Delete, L("DeletingDynamicEntityProperties"));

            var dynamicEntityPropertyValues = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue, L("EntityDynamicPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Create, L("CreatingDynamicEntityPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Edit, L("EditingDynamicEntityPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Delete, L("DeletingDynamicEntityPropertyValue"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, QLBongDaConsts.LocalizationSourceName);
        }
    }
}