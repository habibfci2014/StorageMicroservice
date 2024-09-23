File Storage Service

Overview 
The File Storage Service is a robust and scalable API built using .NET Core. 
This service allows users to upload, retrieve, update, and delete files efficiently. 
It follows the principles of Clean Architecture to ensure maintainability, testability, and separation of concerns.

Features 
Upload Files: Supports uploading files with validation. 
Retrieve Files: Get file details and download links. 
Update File Metadata: Update descriptions and other file information. 
Delete Files: Remove files from storage securely. 
Paged File Listing: Retrieve a list of files with pagination support. 
Architecture This project adheres to the Clean Architecture principles:

Domain Layer: Contains business logic and domain entities. 
Infrastructure Layer: Handles external dependencies like database and file storage services (e.g., AWS S3). 
Presentation Layer: The Web API layer that exposes endpoints to clients. 
Technology Stack Framework: .NET Core 6.0 
Database: SQL server database 
File Storage: AWS S3 
Testing Framework: xUnit with Moq for unit testing
