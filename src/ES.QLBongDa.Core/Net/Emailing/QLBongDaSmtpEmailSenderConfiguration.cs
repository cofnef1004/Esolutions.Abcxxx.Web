﻿using Abp.Configuration;
using Abp.Net.Mail;
using Abp.Net.Mail.Smtp;
using Abp.Runtime.Security;

namespace ES.QLBongDa.Net.Emailing
{
    public class QLBongDaSmtpEmailSenderConfiguration : SmtpEmailSenderConfiguration
    {
        public QLBongDaSmtpEmailSenderConfiguration(ISettingManager settingManager) : base(settingManager)
        {

        }

        public override string Password => SimpleStringCipher.Instance.Decrypt(GetNotEmptySettingValue(EmailSettingNames.Smtp.Password));
    }
}