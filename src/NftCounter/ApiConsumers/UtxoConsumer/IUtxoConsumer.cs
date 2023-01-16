namespace NftCounter.Api.ApiConsumers.NftCountConsumer;

public interface IUtxoConsumer
{
    Task<long> GetTokensCount(string address, string policyId);
}
