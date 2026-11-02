# Azure DevOps API deployment

The API uses two YAML pipelines from the GitHub-backed Azure DevOps project:

1. Create a pipeline named **Api-CI** and select `Api/build/ci.yml`. It builds the .NET 8 API on `main`, then publishes the `api` artifact.
2. Create a pipeline named **Api-CD** and select `Api/deploy/deploy.yml`. It starts when **Api-CI** publishes a successful run from `main`, then deploys that same artifact to DEV, QA, and PROD in order.

## Azure DevOps setup

- Create three Azure DevOps variable groups named `Api-DEV`, `Api-QA`, and `Api-PROD`. In each group, define:
  - `azureServiceConnection`: the Azure Resource Manager service connection authorized for that environment.
  - `appType`: `webApp` for a Windows App Service or `webAppLinux` for a Linux App Service.
  - `webAppName`: the App Service name.
  - `ConnectionString`: that environment's SQL Server connection string; mark it secret.
  - `JwtKey`: a unique, strong signing key for that environment; mark it secret.
  - `JwtIssuer` and `JwtAudience`: the expected token issuer and audience.
- Authorize **Api-CD** to use all three variable groups and service connections.
- Create Azure DevOps environments named `DEV`, `QA`, and `PROD`.
- Add an approval check to the `QA` and `PROD` environments in Azure DevOps (**Pipelines > Environments > Approvals and checks**). The YAML enforces the DEV → QA → PROD order; the environment checks pause before QA and PROD until approved.
- Ensure each service connection has permission to deploy to its target App Service.

The pipeline updates the App Service settings for the connection string and JWT configuration on every deployment. Keep credentials and signing keys in secret variable-group values; do not commit them to the repository.
