# misyatagim

## Overview

Misyatagim is a comprehensive e-commerce platform designed to showcase and sell a variety of products, with a particular focus on mattresses. The platform is built with a microservices architecture, separating concerns into distinct services for products, comments, and user authentication. This approach ensures scalability, maintainability, and independent development of different platform functionalities.

The backend is implemented using ASP.NET Core, leveraging modern .NET features and best practices. The frontend is developed with Angular, providing a dynamic and responsive user experience. Communication between services is managed through an API Gateway, simplifying client interactions and routing requests to the appropriate backend services.

Key architectural decisions include:

*   **Microservices Architecture:** Each core functionality (Product Management, Commenting, Authentication) is a separate service.
*   **API Gateway:** Ocelot is used to aggregate and route requests to the microservices.
*   **Backend Technologies:** ASP.NET Core for all backend services.
*   **Frontend Technologies:** Angular for the client-side application.
*   **Databases:** SQL Server for Product Service, PostgreSQL for Identity Service, and MongoDB for Comment Service.
*   **Caching:** Redis is employed for caching product data to improve performance.
*   **Asynchronous Communication:** RabbitMQ is used for asynchronous message processing, specifically for comment-related events.
*   **Authentication & Authorization:** JWT (JSON Web Tokens) are used for securing API endpoints, managed by the Identity Service.

## Features

*   **Product Management:**
    *   Create, read, update, and delete products.
    *   Products can be searched and filtered.
    *   Detailed product pages with descriptions, prices, images, and specifications (size, material, color, firmness).
    *   Slug-based URLs for products for SEO-friendly access.
    *   Image uploads for products.
*   **Comment System:**
    *   Users can leave comments on product pages.
    *   Comments are stored in MongoDB.
    *   Asynchronous processing of comments via RabbitMQ.
    *   Ability to delete comments (with appropriate authorization).
*   **User Authentication & Authorization:**
    *   User registration and login.
    *   JWT-based authentication.
    *   Role-based access control (e.g., Admin role for managing products).
    *   Secure handling of user credentials using ASP.NET Core Identity.
*   **API Gateway:**
    *   Single entry point for all client requests.
    *   Routes requests to the appropriate microservices.
    *   Handles cross-cutting concerns like CORS.
*   **Frontend User Experience:**
    *   Responsive design for various devices.
    *   Intuitive navigation and product browsing.
    *   Admin dashboard for product and comment management.
    *   Interactive elements and clear feedback to the user.

## Project Structure

```
.
├── README.md
└── backend/
│   ├── APIGateway/                 # Ocelot API Gateway
│   │   ├── APIGateway.csproj
│   │   ├── APIGateway.http
│   │   ├── Program.cs
│   │   ├── appsettings.Development.json
│   │   ├── appsettings.json
│   │   ├── ocelot.json             # Ocelot configuration
│   │   └── Properties/
│   │       └── launchSettings.json
│   ├── CommentService/             # Service for managing comments (MongoDB)
│   │   ├── CommentService.csproj
│   │   ├── CommentService.http
│   │   ├── Program.cs
│   │   ├── appsettings.Development.json
│   │   ├── appsettings.json
│   │   ├── Application/
│   │   │   └── Commands/
│   │   │   │   ├── AddCommentCommand.cs
│   │   │   │   └── DeleteCommentCommand.cs
│   │   │   └── Handlers/
│   │   │   │   ├── AddCommentCommandHandler.cs
│   │   │   │   ├── DeleteCommentCommandHandler.cs
│   │   │   │   ├── GetCommentsQueryHandler.cs
│   │   │   │   └── Interfaces/
│   │   │   │       └── ICommentRepository.cs
│   │   │   │   └── Queries/
│   │   │   │       └── GetCommentsQuery.cs
│   │   │   └── Domain/
│   │   │   │   └── Entitites/
│   │   │   │       └── Comment.cs
│   │   │   └── Infrastructure/
│   │   │   │   └── Data/
│   │   │   │       └── MongoDbContext.cs
│   │   │   │   └── Repositories/
│   │   │   │       └── CommentRepository.cs
│   │   │   │   └── Services/
│   │   │   │       └── RabbitMQProducer.cs
│   │   │   └── Properties/
│   │   │       └── launchSettings.json
│   │   │   └── WebAPI/
│   │   │       └── Controllers/
│   │   │           └── CommentController.cs
│   ├── IdentityService/            # Service for user authentication (PostgreSQL)
│   │   ├── Config.cs
│   │   ├── IdentityService.csproj
│   │   ├── IdentityService.http
│   │   ├── Program.cs
│   │   ├── appsettings.Development.json
│   │   ├── appsettings.json
│   │   ├── tempkey.jwk
│   │   ├── Application/
│   │   │   └── Commands/
│   │   │   │   ├── LoginCommand.cs
│   │   │   │   └── RegisterCommand.cs
│   │   │   └── Handlers/
│   │   │   │   ├── LoginCommandHandler.cs
│   │   │   │   ├── RegisterCommandHandler.cs
│   │   │   │   └── Interfaces/
│   │   │   │       └── IUserRepository.cs
│   │   │   └── Domain/
│   │   │   │   └── Entities/
│   │   │   │       └── User.cs
│   │   │   └── Infrastructure/
│   │   │   │   └── Data/
│   │   │   │       └── IdentityDbContext.cs
│   │   │   │   └── Repositories/
│   │   │   │       └── UserRepository.cs
│   │   │   └── Migrations/
│   │   │       ├── ... (Database migration files)
│   │   │   └── Properties/
│   │   │       └── launchSettings.json
│   │   │   └── WebAPI/
│   │   │       └── Controllers/
│   │   │           └── AuthController.cs
│   ├── ProductService/             # Service for managing products (SQL Server/PostgreSQL, Redis)
│   │   ├── ProductService.csproj
│   │   ├── ProductService.http
│   │   ├── Program.cs
│   │   ├── appsettings.Development.json
│   │   ├── appsettings.json
│   │   ├── Application/
│   │   │   └── Commands/
│   │   │   │   ├── CreateProductCommand.cs
│   │   │   │   ├── DeleteProductCommand.cs
│   │   │   │   └── UpdateProductCommand.cs
│   │   │   └── Handlers/
│   │   │   │   ├── CreateProductCommandHandler.cs
│   │   │   │   ├── DeleteProductCommandHandler.cs
│   │   │   │   ├── GetProductBySlugQueryHandler.cs
│   │   │   │   ├── GetProductsQueryHandler.cs
│   │   │   │   ├── UpdateProductCommandHandler.cs
│   │   │   │   └── Interfaces/
│   │   │   │       └── IProductRepository.cs
│   │   │   │   └── Queries/
│   │   │   │       ├── GetProductBySlugQuery.cs
│   │   │   │       └── GetProductsQuery.cs
│   │   │   └── Domain/
│   │   │   │   └── Entitites/
│   │   │   │       └── Product.cs
│   │   │   └── Infrastructure/
│   │   │   │   └── Data/
│   │   │   │       └── ProductDbContext.cs
│   │   │   │   └── Repositories/
│   │   │   │       └── ProductRepository.cs
│   │   │   │   └── Services/
│   │   │   │       ├── RedisService.cs
│   │   │   │       └── Slugify.cs
│   │   │   └── Migrations/
│   │   │       ├── ... (Database migration files)
│   │   │   └── Properties/
│   │   │       └── launchSettings.json
│   │   │   └── WebAPI/
│   │   │       └── Controllers/
│   │   │           └── ProductController.cs
└── frontend/
    └── misyatagim-angular/         # Angular Frontend Application
        ├── .editorconfig
        ├── README.md
        ├── angular.json
        ├── package.json
        ├── tsconfig.app.json
        ├── tsconfig.json
        ├── tsconfig.spec.json
        ├── public/
        └── src/
            ├── index.html
            ├── main.ts
            ├── styles.css
            └── app/
                ├── app-routing.module.ts
                ├── app.component.css
                ├── app.component.html
                ├── app.component.spec.ts
                ├── app.component.ts
                ├── app.config.ts
                ├── app.routes.ts
                ├── admin/
                │   ├── admin-routing.module.ts
                │   ├── admin.component.css
                │   ├── admin.component.ts
                │   ├── admin.module.ts
                │   ├── comment-list/
                │   │   ├── comment-list.component.css
                │   │   ├── comment-list.component.html
                │   │   ├── comment-list.component.spec.ts
                │   │   └── comment-list.component.ts
                │   ├── product-form/
                │   │   ├── product-form.component.css
                │   │   ├── product-form.component.html
                │   │   ├── product-form.component.spec.ts
                │   │   └── product-form.component.ts
                │   └── product-list/
                │       ├── product-list.component.css
                │       ├── product-list.component.html
                │       ├── product-list.component.spec.ts
                │       └── product-list.component.ts
                ├── directives/
                │   ├── highlight.directive.spec.ts
                │   └── highlight.directive.ts
                ├── guards/
                │   ├── auth.guard.spec.ts
                │   └── auth.guard.ts
                ├── models/
                │   ├── comment.model.ts
                │   └── product.model.ts
                ├── pages/
                │   ├── home/
                │   │   ├── home.component.css
                │   │   ├── home.component.html
                │   │   ├── home.component.spec.ts
                │   │   └── home.component.ts
                │   ├── login/
                │   │   ├── login.component.css
                │   │   ├── login.component.html
                │   │   └── login.component.ts
                │   ├── product-detail/
                │   │   ├── product-detail.component.css
                │   │   ├── product-detail.component.html
                │   │   ├── product-detail.component.spec.ts
                │   │   └── product-detail.component.ts
                │   ├── product-list/
                │   │   ├── product-list.component.css
                │   │   ├── product-list.component.html
                │   │   ├── product-list.component.spec.ts
                │   │   └── product-list.component.ts
                │   └── register/
                │       ├── register.component.css
                │       ├── register.component.html
                │       └── register.component.ts
                ├── pipes/
                │   ├── truncate.pipe.spec.ts
                │   └── truncate.pipe.ts
                └── services/
                    ├── auth.service.spec.ts
                    ├── auth.service.ts
                    ├── comment.service.ts
                    ├── custom-router.service.ts
                    ├── product.service.ts
                    └── shared/
                        ├── material.module.ts
                        └── delete-confirmation-dialog/
                            ├── delete-confirmation-dialog.component.css
                            ├── delete-confirmation-dialog.component.html
                            └── delete-confirmation-dialog.component.ts
```

## Getting Started

To build and run the Misyatagim project, follow these steps:

### Prerequisites

*   .NET SDK (version 8.0 or higher)
*   Node.js and npm (or yarn)
*   Docker (for databases and RabbitMQ, recommended)

### Backend Setup

1.  **Navigate to the backend directory:**
    ```bash
    cd backend
    ```

2.  **Start the microservices:**
    You can start each service individually or use a tool like Docker Compose if you have it set up for your databases and RabbitMQ.

    *   **API Gateway:**
        ```bash
        cd APIGateway
        dotnet run
        ```
        (Runs on `http://localhost:5000`)

    *   **Identity Service:**
        ```bash
        cd IdentityService
        dotnet run
        ```
        (Runs on `http://localhost:5005`)

    *   **Product Service:**
        ```bash
        cd ProductService
        dotnet run
        ```
        (Runs on `http://localhost:5001`)

    *   **Comment Service:**
        ```bash
        cd CommentService
        dotnet run
        ```
        (Runs on `http://localhost:5003`)

    **Note:** Ensure your database connection strings in `appsettings.json` files are correctly configured for PostgreSQL, SQL Server, and MongoDB. For RabbitMQ, ensure it's running on `localhost:5672`.

### Frontend Setup

1.  **Navigate to the frontend directory:**
    ```bash
    cd ../frontend/misyatagim-angular
    ```

2.  **Install dependencies:**
    ```bash
    npm install
    ```

3.  **Start the Angular development server:**
    ```bash
    ng serve
    ```
    (Runs on `http://localhost:4200/`)

### Running Migrations (If needed)

If you are setting up the databases from scratch, you will need to run the Entity Framework Core migrations for the Identity and Product services.

1.  **For Identity Service (PostgreSQL):**
    ```bash
    cd backend/IdentityService
    dotnet ef database update
    ```

2.  **For Product Service (SQL Server/PostgreSQL):**
    ```bash
    cd backend/ProductService
    dotnet ef database update
    ```

### Accessing the Application

Once all services are running, you can access the application through your browser:

*   **Frontend:** `http://localhost:4200/`
*   **API Gateway Swagger:** `http://localhost:5000/swagger` (for API Gateway endpoints, though most are proxied)
*   **Individual Service Swagger:** You can access the Swagger UI for each service directly if needed (e.g., `http://localhost:5001/swagger` for Product Service).

## License

This project is licensed under the GNU GPL v3.0 license. See the [LICENSE](LICENSE) file for more details.
