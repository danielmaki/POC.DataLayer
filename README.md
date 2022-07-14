
# Summary
*This is a POC for fully separating all data related models, operations and tests from the rest of the solution using Entity Framework.*
*Its purpose is to have a generic approach for creating solid and easy-to-create CRUD operations for each Entity (data model).*
*Data models (Entities) are devided into three separate models each with their own scope.*
- *ORM:* a model representing the data in storage
- *Model:* the main model that is used internally throughout the solution
- *DTO:* a model facing the front-end and only containing data visible to the outside
*A data mapper allows for fluent translations between these three models.*
*There is also a generic interface for easy testing of the data models, mappings and CRUD operations using an in memory SQL database.*

## Before running the application follow the steps below:

### Entity Framework: Create database
- Open the Package Manager Console
- Set the POC.DataLayer.Data project as the default project
- Run the following command:
- """Update-Database -s POC.DataLayer.Data"""

## Reminders

### Entity Framework: Add migration
- Open the Package Manager Console
- Set the POC.DataLayer.Data project as the default project
- Run the following command:
"""Add-Migration Fruit_1 -c ApplicationDbContext -s POC.DataLayer.Data"""