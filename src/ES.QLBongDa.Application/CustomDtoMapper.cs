

using ES.QLBongDa.Clubs.Dtos;
using ES.QLBongDa.Clubs;
using ES.QLBongDa.Managers.Dtos;
using ES.QLBongDa.Managers;

using ES.QLBongDa.Rankings.Dtos;
using ES.QLBongDa.Rankings;

using ES.QLBongDa.Matchs.Dtos;
using ES.QLBongDa.Matchs;

using ES.QLBongDa.Players.Dtos;
using ES.QLBongDa.Players;

using ES.QLBongDa.Nations.Dtos;
using ES.QLBongDa.Nations;

using ES.QLBongDa.Vilages.Dtos;
using ES.QLBongDa.Vilages;

using ES.QLBongDa.Stadiums.Dtos;
using ES.QLBongDa.Stadiums;

using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.DynamicEntityProperties;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using Abp.Webhooks;
using AutoMapper;
using IdentityServer4.Extensions;
using ES.QLBongDa.Auditing.Dto;
using ES.QLBongDa.Authorization.Accounts.Dto;
using ES.QLBongDa.Authorization.Delegation;
using ES.QLBongDa.Authorization.Permissions.Dto;
using ES.QLBongDa.Authorization.Roles;
using ES.QLBongDa.Authorization.Roles.Dto;
using ES.QLBongDa.Authorization.Users;
using ES.QLBongDa.Authorization.Users.Delegation.Dto;
using ES.QLBongDa.Authorization.Users.Dto;
using ES.QLBongDa.Authorization.Users.Importing.Dto;
using ES.QLBongDa.Authorization.Users.Profile.Dto;
using ES.QLBongDa.Chat;
using ES.QLBongDa.Chat.Dto;
using ES.QLBongDa.DynamicEntityProperties.Dto;
using ES.QLBongDa.Editions;
using ES.QLBongDa.Editions.Dto;
using ES.QLBongDa.Friendships;
using ES.QLBongDa.Friendships.Cache;
using ES.QLBongDa.Friendships.Dto;
using ES.QLBongDa.Localization.Dto;
using ES.QLBongDa.MultiTenancy;
using ES.QLBongDa.MultiTenancy.Dto;
using ES.QLBongDa.MultiTenancy.HostDashboard.Dto;
using ES.QLBongDa.MultiTenancy.Payments;
using ES.QLBongDa.MultiTenancy.Payments.Dto;
using ES.QLBongDa.Notifications.Dto;
using ES.QLBongDa.Organizations.Dto;
using ES.QLBongDa.Sessions.Dto;
using ES.QLBongDa.WebHooks.Dto;

namespace ES.QLBongDa
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateOrEditClubDto, Club>().ReverseMap();
            configuration.CreateMap<ClubDto, Club>().ReverseMap();
            configuration.CreateMap<CreateOrEditManagerDto, Manager>().ReverseMap();
            configuration.CreateMap<ManagerDto, Manager>().ReverseMap();

            configuration.CreateMap<CreateOrEditRankingDto, Ranking>().ReverseMap();
            configuration.CreateMap<RankingDto, Ranking>().ReverseMap();
            configuration.CreateMap<CreateOrEditMatchDto, Match>().ReverseMap();
            configuration.CreateMap<MatchDto, Match>().ReverseMap();

            configuration.CreateMap<CreateOrEditPlayerDto, Player>().ReverseMap();
            configuration.CreateMap<PlayerDto, Player>().ReverseMap();

            configuration.CreateMap<CreateOrEditNationDto, Nation>().ReverseMap();
            configuration.CreateMap<NationDto, Nation>().ReverseMap();
            configuration.CreateMap<CreateOrEditVilageDto, Vilage>().ReverseMap();
            configuration.CreateMap<VilageDto, Vilage>().ReverseMap();
            configuration.CreateMap<CreateOrEditStadiumDto, Stadium>().ReverseMap();
            configuration.CreateMap<StadiumDto, Stadium>().ReverseMap();
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();

            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            //Webhooks
            configuration.CreateMap<WebhookSubscription, GetAllSubscriptionsOutput>();
            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOutput>()
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.WebhookName,
                    options => options.MapFrom(l => l.WebhookEvent.WebhookName))
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.Data,
                    options => options.MapFrom(l => l.WebhookEvent.Data));

            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOfWebhookEventOutput>();

            configuration.CreateMap<DynamicProperty, DynamicPropertyDto>().ReverseMap();
            configuration.CreateMap<DynamicPropertyValue, DynamicPropertyValueDto>().ReverseMap();
            configuration.CreateMap<DynamicEntityProperty, DynamicEntityPropertyDto>()
                .ForMember(dto => dto.DynamicPropertyName,
                    options => options.MapFrom(entity => entity.DynamicProperty.DisplayName.IsNullOrEmpty() ? entity.DynamicProperty.PropertyName : entity.DynamicProperty.DisplayName));
            configuration.CreateMap<DynamicEntityPropertyDto, DynamicEntityProperty>();

            configuration.CreateMap<DynamicEntityPropertyValue, DynamicEntityPropertyValueDto>().ReverseMap();

            //User Delegations
            configuration.CreateMap<CreateUserDelegationDto, UserDelegation>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
        }
    }
}