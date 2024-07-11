using Newtonsoft.Json;
using System.ComponentModel;
using System.Threading.Tasks;
public static class GraphQLLoad
{
    public class LoadInput
    {
        [JsonProperty("address")]
        public string Address { get; set; }
    }

    public class LoadResponseSchema
    {
        [JsonProperty("game")]
        public Entities.Game Game { get; set; }
    }

    public class LoadResponseType
    {
        [JsonProperty("load")]
        public LoadResponseSchema LoadResponseSchema { get; set; }
    }

    public enum Query
    {
        [Description(@"query Load($input: LoadInput!) {
  load(input: $input) {
    game {
      accountId,
      balance
    }
  }
}
")]
        Zero
    }

    public static async Task<LoadResponseSchema> LoadAsync(Query query, LoadInput input, HandleGraphQLException HandleGraphQLException = null)
    {
        var queryString = EnumUtility.GetDescription(query);
        var type = await GraphQLUtility.Query<LoadInput, LoadResponseType>(Constants.Services.Backend.graphQLUrl, queryString, input, HandleGraphQLException);
        return type.LoadResponseSchema;
    }
}
