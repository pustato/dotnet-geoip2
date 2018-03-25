using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace GeoIP.Responses
{
    public class ApiResponse<TPayload> : IActionResult
    {
        public bool Success { get; set; } = true;

        public string Msg { get; set; } = "OK";

        public TPayload Payload { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; } = StatusCodes.Status200OK;

        public Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(ToString()) { StatusCode = StatusCode };

            return result.ExecuteResultAsync(context);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },
                Formatting = Formatting.Indented
            });
        }

        public static ApiResponse<TPayload> ValidationError(TPayload payload)
        {
            return new ApiResponse<TPayload>
            {
                Success = false,
                Msg = "Validation error",
                Payload = payload,
                StatusCode = StatusCodes.Status406NotAcceptable,
            };
        }
    }
}
