using System;

namespace Learun.Application.WeChat
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field , AllowMultiple = false, Inherited = false)]
    public class LengthAttribute : System.Attribute, IVerifyAttribute
    {
        int MinLength { get; set; }

        int MaxLength { get; set; }

        string Message { get; set; }

        public LengthAttribute(int minLength, int maxLength)
        {
            MinLength = minLength;
            MaxLength = maxLength;
            Message = string.Format("字符串长度应在{0}到{1}之间",minLength,maxLength);
        }
        public LengthAttribute(int minLength, int maxLength,string message)
        {
            MinLength = minLength;
            MaxLength = maxLength;
            Message = string.Format(message, minLength, maxLength);
        }

        public bool Verify(Type type,object obj,out string message)
        {
            message = "";

            if (type == typeof(string) && obj != null)
            {
                if ((obj as string).Length > MaxLength || (obj as string).Length < MinLength)
                {
                    message = Message;
                    return false;
                }
            }

            return true;
        }
    }
}
