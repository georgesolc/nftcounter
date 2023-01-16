# NFT Counter

Application to get the number of NFTs locked at an address by policyid. To extract information from the blockchain, the application uses [Cardano GraphQL](https://github.com/input-output-hk/cardano-graphql).

The application leverages minimal API and all exceptions are handled and logged by [ApiExceptionMiddleware](./src/NftCounter/Middlewares/ApiExceptionMiddleware.cs).

The addresses on which the NFT count is calculated are stored in config files and binded to the models. When the application starts, the configurations are validated.

## Run local environment
**The application requires [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) to run.**

To run local version of application run command line and navigate to the *src/NftCounter* folder. Then run `dotnet run` command. 

Endpoint for getting NFTs count is available at [localhost:5201/api/policies/{policyID}/nft-count](http://localhost:5201/api/policies/{policyID}/nft-count). 
Swagger documentation is available at [localhost:5201](http://localhost:5201).