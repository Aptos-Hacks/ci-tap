using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleGraphQL;

using UnityEngine;

public delegate void HandleGraphQLException(Exception ex);
public static class GraphQLUtility
{
    class Variables<TInput> where TInput : class, new()
    {
        [JsonProperty("input")]
        public TInput Input { get; set; }
    }
    public static async Task<TResponseType> QueryAsync<TInput, TResponseType>(
        string url,
        string query,
        TInput input,
        HandleGraphQLException HandleGraphQLException = null
        ) where TInput : class, new() where TResponseType : class, new()
    {
        try
        {
            var client = new GraphQLClient(url);

            var request = new Request
            {
                Query = query,
                Variables = new Variables<TInput>()
                {
                    Input = input
                }
            };

            var responseType = new TResponseType();
            var response = await client.Send(() => responseType, request);

            return response.Data;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);

            HandleGraphQLException(ex);
            return null;
        }
    }
}