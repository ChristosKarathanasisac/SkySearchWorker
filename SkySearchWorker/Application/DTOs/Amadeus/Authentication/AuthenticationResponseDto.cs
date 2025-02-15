using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.DTOs.Amadeus.Authentication
{
    public class AuthenticationResponseDto
    {
        public string? type { get; set; }
        public string? username { get; set; }
        public string? application_name { get; set; }
        public string? client_id { get; set; }
        public string? token_type { get; set; }
        public string? access_token { get; set; }
        public int expires_in { get; set; }
        public string? state { get; set; }
        public string? scope { get; set; }
    }
}
