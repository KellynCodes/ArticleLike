
---

# Like Button API

This is a scalable and resilient **Like Button API** built with **.NET 6**. The API allows users to like articles and tracks the total likes for each article. It is optimized for performance and handles concurrent requests efficiently.

## Features
- **Add a Like**: Increment the like count for an article.
- **Get Like Count**: Retrieve the current total likes for an article.
- **Resiliency**: Handles concurrent clicks safely using distributed caching.
- **Scalability**: Optimized for millions of users and requests.

## Technologies Used
- **.NET 6**: Backend framework  
- **Entity Framework Core**: ORM for database interaction  
- **SQL Server**: Database  
- **Redis Distributed Cache**: For performance optimization and concurrency control  

---

## Database Schema

### Article Table
| Column    | Type   | Description                |
|-----------|--------|----------------------------|
| Id        | int    | Primary Key                |
| Title     | string | Title of the article       |
| Content   | string | Content of the article     |

### Like Table
| Column     | Type | Description                        |
|------------|------|------------------------------------|
| Id         | int  | Primary Key                        |
| ArticleId  | int  | Foreign key referencing Article    |
| CreatedAt  | DateTime | Timestamp of the like         |

---

## Endpoints

### **1. Add Like to an Article**
**POST** `/api/articles/{id}/like`  
- **Description**: Increments the like count for a specific article.  
- **Response**: 
```json
{ "message": "Like added successfully" }
```

### **2. Get Like Count for an Article**
**GET** `/api/articles/{id}/likes`  
- **Description**: Retrieves the total like count for a specific article.  
- **Response**:
```json
{
  "articleId": 1,
  "likes": 100
}
```

---

## Setup and Usage

1. **Clone the repository**:
   ```bash
   git clone https://github.com/kellyncodes/articlelike.git
   cd ArticleLike
   ```

2. **Update Database Configuration**:
   In `appsettings.json`, set the connection string to your SQL Server instance.

3. **Set up Redis**:  
   Make sure Redis is installed and running.  
   If Redis is running on a remote server, configure the server address in `appsettings.json`.

   Example `appsettings.json` configuration:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your-sql-server;Database=LikeButtonDB;Trusted_Connection=True;"
     },
     "Redis": {
       "Connection": "your-redis-server:6379"
     }
   }
   ```

4. **Configure Redis in `Program.cs`**:
   ```csharp
   var builder = WebApplication.CreateBuilder(args);

   // Add Redis caching
   builder.Services.AddStackExchangeRedisCache(options =>
   {
       options.Configuration = builder.Configuration["Redis:Connection"];
       options.InstanceName = "LikeButton:";
   });

   // Add services to the container.
   builder.Services.AddControllers();
   builder.Services.AddDbContext<ApplicationDbContext>();

   var app = builder.Build();
   app.MapControllers();
   app.Run();
   ```

5. **Run Migrations**:
   ```bash
   dotnet ef database update
   ```

6. **Start the API**:
   ```bash
   dotnet run
   ```

7. **Test the Endpoints**: Use tools like Postman or Curl to interact with the API.

---

## Optimization and Scalability

- **Concurrency Handling**: Uses Redis distributed cache to prevent race conditions during concurrent likes.
- **Scaling for High Load**:
  - **Horizontal Scaling**: Supports multiple instances using Redis as a shared cache.
  - **Batching Requests**: Implements delayed writes to reduce database load.
- **Abuse Prevention**: IP rate-limiting middleware can be added to prevent spam clicking.

---

## License
This project is licensed under the MIT License. See `LICENSE` for more details.

---