
A netstandard2 client library for accessing the Squidex REST API; built on top of Refit.

nuget : https://www.nuget.org/packages/MGNZ.Squidex.Client/

## show me some code

Untill the documentation is sorted; have a good look at the Integration Tests; we have an integration test for most of the functionality on the library.

Importing schema form somewhere (assumed to be on disk); keep in mind that the schema-blob you have on disk has the schema name encoded into it.

```csharp
// import schema
// - see also SquidexSchemaClientIntegrationTests.CreateSchema_EndToEnd

// construct schema client; we use SimpleAccessTokenHttpClientHandler because it will apply your token to all requests
var schemaClient = RestService.For<ISquidexAppSchemaClient>(
  new HttpClient(new SimpleAccessTokenHttpClientHandler(() => "your-token"))
  {
    BaseAddress = "your-squidex-server-base-address"
  });

// get your schema form somewhere (say from the output of ISquidexAppSchemaClient.GetSchema)

// pull it into the aplication
dynamic schema = GetSchemaFromDisk();
string schemaName = Convert.ToString(schema.name);

var importResult = await schemaClient.CreateSchema("application", schema);

// publish it (otherwise you cant use it) - its asumed you got 'schema-name' form somewhere
var publishresult = await schemaClient.PublishSchema("aut", schemaName);


```

Doing stuff with data
```csharp
```



## current focus

2018 Dec 20
Updated to work with assets (images, files etc..); core interface is working, also working on a few extension methods to do things a bit easier.

Nuget release at : https://www.nuget.org/packages/MGNZ.Squidex.Client/0.2.6-unstable.1214

2018 Oct 26
Things are a bit quiet here because I am spending most time working on a command line client that uses this library over at https://github.com/mgnz/mgnz.squidex.cli once that is a bit more polished the intention is to come back here and fix up the remaining issues.

## done bits

- OAuthClient : gets a token (you need to supply a HttpClientHandler to get the key in fligt; see SimpleAccessTokenHttpClientHandler for a sample reference; also see https://github.com/mgnz/mgnz.squidex.client/issues/2 for a case to make this less retarded).
- SchemaClient : CRUD for schema''s technically you can use this as a poor mans 'import' 'export' if you dont care about history. 
- ContentClient : Typesafe QCRUD for schema-content; currently implemneted a 'typesafe' wrapper over your domain (assuming you construct your dto''s using the objects in Model)
- AttachmentClient : CRUD for managing assets / attachments
- Reasonable test coverage; not public yet but Integration testing is done on VSTS and hits a private Squidex instance.

## examples

``` cshrap
// todo : the examples
```

## limitations

- AppClient : mostly functional; depends on getting a token for an 'administrative login' on the Squidex instance; not something supported yet (but if you get a token via some other means you can supply it for this client and it will work).

## todos

- ContentClient : dynamic QCRUD for schema-content; like what we already have but with the raw dynamic objects you get form the server. see https://github.com/mgnz/mgnz.squidex.client/issues/3
- [x] AssetClient : CRUD over the asset api. see https://github.com/mgnz/mgnz.squidex.client/issues/4
