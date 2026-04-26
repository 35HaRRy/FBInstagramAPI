# FBInstagramAPI

## Overview
A small console utility that reads a Facebook or Instagram access token and prints posts or media URLs from the Graph APIs.
It is intended as a lightweight way to inspect account content from the command line.

## Related Article
- [Personal story about retrieving all posts from a Facebook or Instagram user](https://medium.com/@hayrihabip/ki%C5%9Fisel-bir-istekten-dolay%C4%B1-instagram-da-ve-facebook-ta-bir-kullanc%C4%B1ya-ait-t%C3%BCm-payla%C5%9F%C4%B1mlar%C4%B1-65a052e77b3f)

## Dependencies
- .NET Core 3.1 SDK/runtime
- ASP.NET Core shared framework components
- `System.ServiceModel.Syndication`

## Setup
1. Install the .NET Core 3.1 SDK/runtime.
2. Restore the project with `dotnet restore`.
3. Review the source if you want to adjust the API endpoint or output format.

## Run
- Start the console app with `dotnet run`.
- Choose the API type and provide the access token when prompted.

## Notes
The app uses the Facebook and Instagram Graph APIs directly, so valid tokens and permissions are required.