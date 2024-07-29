
# <p align="center">Blogging Platform</p>
  
simple blogging platform API using .NET Core, MS
SQL, and Entity Framework. The API allow users to create accounts, create blog posts,comment on posts, and follow other users
##  Features    
1.**User Management**
- Create accounts with username, email, and password.
Follow other users.
2. **Blog Posts**
 - Create, read, update, and delete blog posts.
- List posts (Sort by date descending, Search with title and author).
 - Each post have a title, content, creation date, and be associated with an author.
2. **Comments**
- Leave comments on blog posts.
 - Comments  include the commenter’s name, email, and the comment text.
3. **Followers**
- Track user follow relationships 
## Technologies Used
- [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api): A rich framework for building web apps and APIs using the MVC design pattern.
- [SQL Server](https://www.microsoft.com/en-us/sql-server/): A proprietary relational database management system developed by Microsoft.
- [Entity Framework (EF) Core](https://learn.microsoft.com/en-us/ef/core/): An object-relational mapping (ORM) framework developed by Microsoft.
- [Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity): an API that manages users, passwords, profile data, roles, claims, tokens, email confirmation, and more.
- [AutoMapper](https://automapper.org/): object-object mapper. Takes out all of the fuss of mapping one object to another.
## Architecture and Patterns
- **N-Tier Architecture**: layering for maintainability.
- **Repository Pattern**: Abstraction layer over data access.
- ## Installation

To run project locally, follow these steps:

1. **Clone the repository:**
   
   ```
   git clone https://github.com/yasmeena1999/SingleClic.git
   ```
   
3. **Navigate to the project directory:**
   
   ```
   cd SingleClic
   ```
   4. **Install dependencies:**
   
   ```
   dotnet restore
   ```
   
5. **Create the database using Entity Framework Core:**
   
   ```
   dotnet ef database update
   ```
   
7. **Build and Run:**
   
   ```
   dotnet build
   dotnet run
