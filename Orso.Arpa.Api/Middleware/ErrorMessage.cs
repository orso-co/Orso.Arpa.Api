using System.Collections.Generic;

namespace Orso.Arpa.Api.Middleware
{
    internal class ErrorMessage
    {
        public string title { get; set; }
        public string description { get; set; }
        public int status { get; set; }
        public Dictionary<string, string[]> errors { get; set; } = new Dictionary<string, string[]>();
    }
}
