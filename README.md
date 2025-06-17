# shynvtech Website

The Website for shynvtech

## Deployment

To deploy the website, you can use the following command:

```bash
azd auth login --scope https://management.azure.com//.default

azd init

azd up
```

```text
SUCCESS: Your app is ready for the cloud!
Run azd up to provision and deploy your app to Azure.
Run azd add to add new Azure components to your project.
Run azd infra gen to generate IaC for your project to disk, allowing you to manually manage it.
See ./next-steps.md for more information on configuring your app.
```
