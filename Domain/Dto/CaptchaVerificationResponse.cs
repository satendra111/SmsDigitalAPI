using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CaptchaVerificationResponse
    {
        public bool Success { get; set; }

        [JsonProperty("challenge_ts")]
        public DateTime ChallengeTimestamp { get; set; }
        public string Hostname { get; set; }

        [JsonProperty("error-codes")]
        public string[] Errorcodes { get; set; }

    }
}
