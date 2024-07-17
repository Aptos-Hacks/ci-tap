using Newtonsoft.Json;
using System;

public static class Entities
{
    public class Account
    {
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("balance")]
        public int Balance { get; set; }

        [JsonProperty("totalBonus")]
        public float TotalBonus { get; set; }

        [JsonProperty("autoTapperLevel")]
        public int AutoTapperLevel { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}