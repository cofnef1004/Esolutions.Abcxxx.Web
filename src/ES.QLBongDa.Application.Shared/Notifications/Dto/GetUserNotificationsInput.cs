using System;
using Abp.Notifications;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}