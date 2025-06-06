namespace Lefish.Common.Validation;

public static class MaxLengths
{
    public static class Common
    {
        // Maximum lengths are used both to define the database
        // table column widths, as well as to validate user input.

        public const int DbString = 4000;

        // A fallback max length. There's a soft limit of 4000 UTF16 chars
        // (8000 bytes) in MS SQL Server, for column type nvarchar, 
        // after which data storage fans out to additional pages.

        public const int DbSuperSize = 16_000;

        public static class Email
        {
            public const int Address = 70;
        }

        public static class Http
        {
            public const int Method = 20;
            public const int Url = 255;
            public const int UserAgent = 150;
        }

        public static class IpAddress
        {
            public const int IPv4 = 15;
            public const int IPv6 = 45;
        }

        public static class Mime
        {
            public const int ContentType = 255;
        }

        public static class Password
        {
            public const int Clear = 70;
            public const int Hash = 128;
        }

        public static class Person
        {
            public const int Name = 40;
        }

        public static class Phone
        {
            public const int Number = 20;
        }


        public static class Postal
        {
            public const int Address = 50;
            public const int ZipCode = 10;
            public const int City = 25;
        }
    }

    public static class Domain
    {
        public static class DataResult
        {
            public const int JsonData = Common.DbSuperSize;
        }

        public static class EmailAccount
        {
            public const int FromName = Common.Person.Name;
            public const int FromAddress = Common.Email.Address;
            public const int ReplyToName = Common.Person.Name;
            public const int ReplyToAddress = Common.Email.Address;
            public const int Password = 100;
            public const int SmtpHost = 100;
        }

        public static class EmailAttachment
        {
            public const int Name = 100;
            public const int ContentType = Common.Mime.ContentType;
        }

        public static class EmailImage
        {
            public const int Name = 100;
            public const int ContentType = Common.Mime.ContentType;
        }

        public static class EmailMessage
        {
            public const int ToName = Common.Person.Name;
            public const int ToAddress = Common.Email.Address;
            
            public const int Subject = 300;

            public const int HtmlBody = Common.DbSuperSize;
            public const int TextBody = Common.DbSuperSize;
        }

        public static class EmailTemplate
        {
            public const int Subject = 300;

            public const int HtmlBody = Common.DbSuperSize;
            public const int TextBody = Common.DbSuperSize;
        }

        public static class Instruction
        {
            public const int Name = 50;

            public const int Script = Common.DbSuperSize;
        }

        public static class InstructionSet
        {
            public const int Name = 50;
        }

        public static class PayloadPage
        {
            public const int Name = 50;

            public const int Html = Common.DbSuperSize;
        }

        public static class PageScript
        {
            public const int Name = 50;

            public const int Script = Common.DbSuperSize;
        }

        public static class Target
        {
            public const int Name = Common.Person.Name;
            public const int Address = Common.Email.Address;
        }

        public static class TargetInstruction
        {
            public const int Name = 50;

            public const int Script = Common.DbSuperSize;
        }
    }
}
