using NftCounter.Api.ApiHandlers.NftCountHandler;

namespace NftCounter.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void RegisterEndpoints(this WebApplication app) {
        app.MapGet("/api/policies/{policyId}/nft-count/", async (string policyId, INftCountHandler handler) =>
        {
            return Results.Ok(await handler.HandleNftCount(policyId));
        })
        .WithOpenApi();
    }
}
