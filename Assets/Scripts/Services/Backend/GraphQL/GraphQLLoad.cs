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
      balance,
      totalBonus
    }
  }
}
")]
        Zero
    }

    public static async Task<LoadResponseSchema> LoadAsync(Query query, LoadInput input, HandleGraphQLException HandleGraphQLException = null)
    {
        var queryString = EnumUtility.GetDescription(query);
        var responseType = await GraphQLUtility.QueryAsync<LoadInput, LoadResponseType>(Constants.Urls.Backend.graphQLUrl, queryString, input, HandleGraphQLException);
        return responseType.LoadResponseSchema;
    }
}
