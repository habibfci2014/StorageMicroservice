<h2>File Storage Service</h2>

<p>Overview</p>
The File Storage Service is a robust and scalable API built using .NET Core.</br> 
This service allows users to upload, retrieve, update, and delete files efficiently. </br> 
It follows the principles of Clean Architecture to ensure maintainability, testability, and separation of concerns.</br> 
</br> 
<p>Features</p> 
Upload Files: Supports uploading files with validation. </br> 
Retrieve Files: Get file details and download links. </br> 
Update File Metadata: Update descriptions and other file information.</br>  
Delete Files: Remove files from storage securely. </br> 
Paged File Listing: Retrieve a list of files with pagination support. </br> 
Architecture This project adheres to the Clean Architecture principles:</br> 
</br> 
Domain Layer: Contains business logic and domain entities. </br> 
Infrastructure Layer: Handles external dependencies like database and file storage services (e.g., AWS S3). </br> 
Presentation Layer: The Web API layer that exposes endpoints to clients. </br> 
Technology Stack Framework: .NET Core 6.0 </br> 
Database: SQL server database </br> 
File Storage: AWS S3 </br> 
Testing Framework: xUnit with Moq for unit testing</br> 
