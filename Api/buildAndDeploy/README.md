# Create the Azure DevOps pipeline

This repository uses one multi-stage Azure Pipelines YAML pipeline. It builds and publishes the .NET 8 API, then deploys the same artifact to Azure App Service in order: DEV, QA, PROD. Deployments run only for `main`.

## Create the pipeline

1. In Azure DevOps, open your project and go to **Pipelines > New pipeline**.
2. Select **GitHub** and authorize Azure DevOps if prompted.
3. Select the GitHub repository containing this project.
4. Choose **Existing Azure Pipelines YAML file**.
5. Select the `main` branch and YAML path `/Api/buildAndDeploy/azure-pipelines.yml` (relative to the repository root).
6. Select **Continue**, review the YAML, then choose **Run**. Save the pipeline.

## Configure deployments

Before deploying, create three Azure DevOps Library variable groups: `Api-DEV`, `Api-QA`, and `Api-PROD`. In each group, set:

| Variable | Value |
| --- | --- |
| `azureServiceConnection` | Name of the Azure Resource Manager service connection authorized for the target App Service |
| `appType` | `webApp` for Windows App Service, or `webAppLinux` for Linux App Service |
| `webAppName` | Target App Service name |
| `ConnectionString` | Environment's SQL Server connection string; mark secret |
| `JwtKey` | Environment-specific JWT signing key; mark secret |
| `JwtIssuer` | Expected JWT issuer |
| `JwtAudience` | Expected JWT audience |

Also:

1. Authorize the pipeline to use all three variable groups and the Azure service connections.
2. Create Azure DevOps environments named `DEV`, `QA`, and `PROD`.
3. Add **Approvals and checks** to the `QA` and `PROD` environments to require approval before deployment. The YAML runs stages in DEV -> QA -> PROD order.
4. Ensure each service connection has permission to deploy to its target App Service.

After setup, changes to `main` under `Api/src` or `Api/buildAndDeploy` trigger a build and deployment. Keep connection strings and signing keys in secret variable-group values; do not commit them to the repository.
