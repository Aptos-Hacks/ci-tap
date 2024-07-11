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

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("game")]
        public Game Game { get; set; }
    }

    public class Game
    {
        [JsonProperty("gameId")]
        public string GameId { get; set; }

        [JsonProperty("balance")]
        public int Balance { get; set; }

        [JsonProperty("totalBonus")]
        public float TotalBonus { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("account")]
        public Account Account { get; set; }
    }
}