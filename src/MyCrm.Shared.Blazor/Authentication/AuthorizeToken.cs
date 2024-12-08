using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCrm.Shared.Blazor.Authentication
{

    public sealed class AccessTokenResponse
    {
        public string Token_Type { get; } = "Bearer";

        public required string Access_Token { get; init; }

        public required long Expires_In { get; init; }

    }

}