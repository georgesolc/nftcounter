using Microsoft.Extensions.Options;
using NftCounter.Api.ApiConsumers.NftCountConsumer;
using NftCounter.Api.Contracts.NftCount;

namespace NftCounter.Api.ApiHandlers.NftCountHandler;

public sealed class NftCountHandler : INftCountHandler
{
    private readonly IUtxoConsumer _utxoAggregateConsumer;
    private readonly Options.StoreOptions _storeOptions;
    public NftCountHandler(IUtxoConsumer utxoAggregateConsumer, 
        IOptions<Options.StoreOptions> config) {
        _utxoAggregateConsumer = utxoAggregateConsumer;
        _storeOptions = config.Value;
    }

    public async Task<IEnumerable<NftCountItemDto>> HandleNftCount(string policyId) {
        List<NftCountItemDto> itemsList = new List<NftCountItemDto>();
        foreach (var store in _storeOptions.Stores) {
            var result = await _utxoAggregateConsumer.GetTokensCount(store.ScriptAddress, policyId);

            itemsList.Add(new NftCountItemDto(
                store.Name,
                result
            ));
        }

        return itemsList;
    }
}

