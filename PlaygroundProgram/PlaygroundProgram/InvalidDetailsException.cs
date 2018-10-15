using System;

namespace PlaygroundProgram
{
    public class InvalidDetailsException : Exception
    {
        public InvalidDetailsException(string message, string detailName) : base(message)
        {
            DetailName = detailName;
        }
        public string DetailName { get; set; }
    }
}