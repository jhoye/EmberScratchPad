# Ember ScratchPad API

This is a BFF API for an Ember.js client. It implements the JSON:API specification for consumption by the Ember Data ORM with minimal configuration (see *JsonProperty* attributes on the properties of *Models* classes - they use kebob-casing following the spec).

## Set Up and Run

1. Restore NuGet packages
2. CLI command: `dotnet run` or debug (F5 in VS Code)
3. CLI command: `dotnet dev-certs https --trust` (to run HTTPS okay for localhost in certain browsers)

The application should run locally at: https://localhost:5001