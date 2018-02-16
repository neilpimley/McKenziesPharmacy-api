<img src="https://github.com/neilpimley/McKenziesPharmacy-ui/raw/master/src/assets/images/cross.png" height="30" /> McKenzies Pharmacy - Prescription Re-ordering Service
================================

This repository contains the source code of the API and associate tests for a sample Repeat Prescription Re-ordering Application designed for McKenzies Pharmacy and used by myself to try out new technologies.

- API Documentation: https://mckenziespharmacy.azurewebsites.net/swagger/
- Application: https://mckenziespharmacy.netlify.com/

## Source Code

- Frontend: https://github.com/neilpimley/McKenziesPharmacy-ui
- Core Packages: https://github.com/neilpimley/McKenziesPharmacy-core
- Back office: https://github.com/neilpimley/McKenziesPharmacy-dispensing
- Database: https://github.com/neilpimley/McKenziesPharmacy-database

## System Components

- Website Api (.NET Core / WebApi project / Swagger definitiion)
- Core Services ( .NET Core 2.0, EF Core - services / respositories / models)
- Database (DB deployment project)
- Tests (XUnit / integraton)

## Applicatoin settings required in azurewebsites

In the local dev environment use "manage user secrets" in Visual Studio and add the following to secrets.json
```json
{
    "ConnectionStrings": {
        "Entities": ""
    },
    "ServiceSettings": {
        "SendGridApiKey": "",
        "TwilloAccountSid": "",
        "TwilloAuthToken": "",
        "TwilloNumber": "",
        "GetAddressApiKey": "",
        "AllowedPostcodes": "BT1,BT2,BT3,BT4,BT5,BT6,BT7,BT8,BT9,BT10,BT11,BT12,BT13,BT14,BT15,BT16,BT17",
        "Auth0Domain": "",
        "Auth0ClientID": "",
        "Auth0ApiIdentifier": "",
        "Auth0ClientSecret": ""
    }
}
```
If hosted in Azure add the application settings below
```
ConnectionStrings:Entities

ServiceSettings:SendGridApiKey
ServiceSettings:TwilloAccountSid
ServiceSettings:TwilloAuthToken
ServiceSettings:TwilloNumber
ServiceSettings:GetAddressApiKey
ServiceSettings:AllowedPostcodes
ServiceSettings:Auth0Domain
ServiceSettings:Auth0ClientID
ServiceSettings:Auth0ApiIdentifier
ServiceSettings:Auth0ClientSecret
```
## Docker
To run in docker 
```
> docker build -t mckenzies .
> docker run -d -p 8080:80 --name pharmacyapi mckenzies
```

