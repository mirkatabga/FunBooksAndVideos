# Table of content

- [Description](#funbooksandvideos)
  * [Requirements](#requirements)
    + [Business requirements](#business-requirements)
    + [Tasks](#tasks)
  * [How to start](#how-to-start)
  * [Testing](#testing)

# Description
**FunBooksAndVideos** is an e-commerce shop where customers can view books and watch online videos. Users 
can have memberships for the book club, the video club or for both clubs (premium).

This is a Code Kata showcase challange that required building an webapi for exposing checkout order functionality.

## Requirements

### Business requirements
- BR1. If the purchase order contains a membership, it has to be activated in the customer account immediately.
- BR2. If the purchase order contains a physical product a shipping slip has to be generated.
### Tasks
- Implement an Object-Oriented model of the system and expose it as REST API(s) utilizing best practices, standards, and guidelines.
- Implement a flexible Purchase Order Processor using good design principles and patterns.
- Implement the above business rules.

## How to start
The fastest way to start the application is by using the following command from the `\src` directory:

`docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d --build`

this command will spinup two containers.

1. *funbooksandvideos.api* which exposes port 8000 E.g: `http://localhost:8000/swagger` endpoint
2. *funbooksandvideosdb* which exposes the default 1433 mssql port and it is availible at Server Name: `localhost` with credentials that could be found in the `\docker-compose.override.yml` file

On application start automatic migrations will be performed and the database schema will be created. Additionally some sample data will be seeded to speed up manual testing.

## Testing

There are unit tests to check some of the functionality and can be found under `src/Tests`. They could be started by invoking the `dotnet test` command with `\src` as a workdir.

Additionally a public postman collection is available. Please note that the Id's in the requests will be different and could be found by connecting to the database. Thank you for checking this repository out.

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/169519-8d0b6663-6657-46f9-bb45-5f3d92488d6b?action=collection%2Ffork&collection-url=entityId%3D169519-8d0b6663-6657-46f9-bb45-5f3d92488d6b%26entityType%3Dcollection%26workspaceId%3Da46fbfa1-09b8-4b21-954e-1cba418f7706)
