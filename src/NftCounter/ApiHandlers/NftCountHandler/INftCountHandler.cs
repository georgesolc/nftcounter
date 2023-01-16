using NftCounter.Api.Contracts.NftCount;

namespace NftCounter.Api.ApiHandlers.NftCountHandler;
public interface INftCountHandler
{
    Task<IEnumerable<NftCountItemDto>> HandleNftCount(string policyId);
}
