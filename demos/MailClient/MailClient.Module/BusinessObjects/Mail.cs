﻿using System;
using System.Collections.Generic;
using System.Text;

using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace MailClient.Module.BusinessObjects
{
    [Persistent("Mails")]
    public class Mail : MailBaseObjectId
    {
        // 50000 chosen to be explicit to allow enough size to avoid truncation, yet stay beneath the MySql row size limit of ~65K
        // apparently anything over 4K converts to nvarchar(max) on SqlServer
        private const int textSizeIndexable = 50000;

        public Mail(Session session) : base(session) { }

        [RuleRequiredField(DefaultContexts.Save)]
        public MailAccount? Account { get; set; }

        public MailDirection Direction { get; set; } = MailDirection.Inbound;

        [Persistent("From")]
        [Size(textSizeIndexable)]
        [Indexed]
        public string? From { get; set; }

        [Persistent("To")]
        [Size(textSizeIndexable)]
        [Indexed]
        public string? To { get; set; }

        [Persistent("CC")]
        [Size(textSizeIndexable)]
        [Indexed]
        public string? CC { get; set; }

        [Persistent("BCC")]
        [Size(textSizeIndexable)]
        [Indexed]
        public string? BCC { get; set; }

        [Persistent("Subject")]
        [Size(textSizeIndexable)]
        [Indexed]
        public string? Subject { get; set; }

        [Persistent("Sent")]
        public DateTime? Sent { get; set; }

        [Persistent("TextBody")]
        [Size(SizeAttribute.Unlimited)]
        public string? TextBody { get; set; }

        [Persistent("HtmlBody")]
        [Size(SizeAttribute.Unlimited)]
        public string? HtmlBody { get; set; }
    }

    public enum MailDirection
    {
        Outbound = 1,
        Inbound = 2,
    }
}
