using GraphQL;
using GraphQL.Client.Abstractions;
using NftCounter.Api.ApiConsumers.UtxoConsumer.ResponseTypes;
using NftCounter.Api.Infrastructure.Exceptions;

namespace NftCounter.Api.ApiConsumers.NftCountConsumer;

public sealed class UtxoConsumer : IUtxoConsumer
{
    private readonly IGraphQLClient _graphQLClient;

    public UtxoConsumer(IGraphQLClient graphQLClient)
    {
        _graphQLClient = graphQLClient;
    }

    /// <summary>
    /// Gets the number of NFTs by address and policyId
    /// </summary>
    /// <param name="address"></param>
    /// <param name="policyId"></param>
    /// <returns>NFTs count</returns>
    /// <exception cref="GraphQlException"></exception>
    public async Task<long> GetTokensCount(string address, string policyId)
    {
        //gets all utxos locked at the address where transaction output contains the given policyId
        var query = new GraphQLRequest
        {
            //To improve this part of the code, it would be possible to use a strongly typed query builder.
            //However, it is redundant in the case of only one query
            Query = @"query getUtxosByAddressAndPolicyId($address: String!, $policyId: Hash28Hex!) {
                          utxos(
                            where: {
                              _and:{
                                address: { _eq: $address }
                                tokens: {
                                  asset: {
                                    policyId: { _eq: $policyId}
                                  }
                                }
                              }
                           }
                          ) {
                           tokens {
                              transactionOutput {
                                tokens {
                                  quantity
                                  asset{
                                    policyId
                                    assetId
                                  }
                                }
                              }
                            }
                          }
                   }",
            Variables = new { address = address, policyId = policyId }
        };

        var response = await _graphQLClient.SendQueryAsync<UtxoResponseType>(query);
        if (response.Errors?.Length > 0)
        {
            throw new GraphQlException(response.Errors);
        }

        long totalCount = 0;

        //filter out all transaction outputs with different policyId
        foreach (var utxo in response.Data.Utxos)
        {
            totalCount += utxo.Tokens[0].TransactionOutput.Tokens.Where(t => t.Asset.PolicyId == policyId).Sum(t => ParseQuantity(t.Quantity));
        }

        return totalCount;
    }

    private long ParseQuantity(string quantityString) {
        if (!long.TryParse(quantityString, out var quantity)) {
            return 0;
        }
        return quantity;
    }
}