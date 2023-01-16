namespace NftCounter.Api.ApiConsumers.UtxoConsumer.ResponseTypes;

public sealed record UtxoResponseType (Utxo[] Utxos);

public sealed record Utxo(Tokens[] Tokens);

public sealed record Tokens(TransactionOutput TransactionOutput);

public sealed record TransactionOutput(TransactionOutputItem[] Tokens);

public sealed record TransactionOutputItem(Asset Asset, string Quantity);

public sealed record Asset(string PolicyId);


