# Bloggy

Restful API backend for a mini blog engine. Features include token-based
authentication (using JWTs + Authorization headers), CORS, and swagger. The
application architecture is vertically sliced and with CQRS (via MediatR).

Technology
----------
* ASP.NET Core 2.0
* PostgreSQL
* Entity Framework Core 2.0 
* AutoMapper
* MediatR
* FluentValidation
* CSharpFunctionalExtensions
* Swashbuckle

Endpoints
---------

### Tags
| Method    | URI                                   | Action                                    |
|-----------|---------------------------------------|-------------------------------------------|
| `GET`     | `/api/tags`                           | `Retrieve all tags`                       |

### Categories
| Method    | URI                                   | Action                                    |
|-----------|---------------------------------------|-------------------------------------------|
| `GET`     | `/api/categories`                     | `Retrieve all categories`                 |

### Posts
| Method    | URI                                   | Action                                    |
|-----------|---------------------------------------|-------------------------------------------|
| `GET`     | `/api/posts`                          | `Retrieve all posts`                      |
| `GET`     | `/api/posts/{pid}`                    | `Retrieve post`                           |
| `POST`    | `/api/posts/`                         | `Create post`                             |
| `PUT`     | `/api/posts/{pid}`                    | `Edit post`                               |
| `DELETE`  | `/api/posts/{pid}`                    | `Delete post`                             |
| `GET`     | `/api/posts/{pid}/comments`           | `Retrieve all post comments`              |
| `GET`     | `/api/posts/{pid}/comments/{id}`      | `Retrieve post comment`                   |
| `POST`    | `/api/posts/{pid}/comments`           | `Create post comment`                     |
| `PUT`     | `/api/posts/{pid}/comments/{id}`      | `Edit post comment`                       |
| `DELETE`  | `/api/posts/{pid}/comments/{id}`      | `Delete post comment`                     |

### Users
| Method    | URI                                   | Action                                    |
|-----------|---------------------------------------|-------------------------------------------|
| `GET`     | `/api/users/{username}`               | `Retrieve user details`                   |
| `PUT`     | `/api/users`                          | `Edit (logged in) user profile`           |

### Auth
| Method     | URI                                   | Action                                   |
|------------|---------------------------------------|------------------------------------------|
| `POST`     | `/auth/register`                      | `Register User`                          |
| `POST`     | `/auth/login`                         | `Login User`                             |

Sample Usage
---------------
`http post localhost:5000/auth/register email=user@example.com username=user password=password`

```
{
    "email": "user@example.com", 
    "username": "user"
}
```

`http post localhost:5000/auth/login email=user@email.com password=password`
```
{
    "email": "user@example.com", 
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyNSIsImp0aSI6Ijc2MDY3M2Q2LTYxYjEtNDc1ZC1hOWM4LTExMjRlOTRjNWIwMCIsImlhdCI6MTUyMjk2ODczNCwibmJmIjoxNTIyOTY4NzM0LCJleHAiOjE1MjI5NjkwMzQsImlzcyI6Imlzc3VlciIsImF1ZCI6ImF1ZGllbmNlIn0.QDvtbxjkJTUUObB4Bw6nNtepqhi-UivaA4_2I5ufX4k", 
    "username": "user"
}
```

`http localhost:5000/api/users` (unprotected endpoint)

```
[
    {
        "author": "lazyprogrammer", 
        "body": "Statically typed functional programming languages like F# encourage a very different way of thinking about types.", 
        "category": "Software Architecture", 
        "comments": [
            {
                "author": {
                    "id": 2, 
                    "username": "codemonkey"
                }, 
                "body": "wonderful", 
                "createdDate": "2018-02-28T00:00:00", 
                "id": 5
            }, 
            {
                "author": {
                    "id": 1, 
                    "username": "lazyprogrammer"
                }, 
                "body": "I'm glad you enjoyed it", 
                "createdDate": "2018-04-12T00:00:00", 
                "id": 4
            }, 
            {
                "author": {
                    "id": 2, 
                    "username": "codemonkey"
                }, 
                "body": "This is very helpful, thanks!", 
                "createdDate": "2018-03-21T00:00:00", 
                "id": 1
            }
        ], 
        "createdDate": "2018-02-11T00:00:00", 
        "id": 1, 
        "tags": [
            "FSharp", 
            "DDD"
        ], 
        "title": "Domain Driven Design"
    },
...
```

`http --auth-type=jwt --auth="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyNSIsImp0aSI6Ijc2MDY3M2Q2LTYxYjEtNDc1ZC1hOWM4LTExMjRlOTRjNWIwMCIsImlhdCI6MTUyMjk2ODczNCwibmJmIjoxNTIyOTY4NzM0LCJleHAiOjE1MjI5NjkwMzQsImlzcyI6Imlzc3VlciIsImF1ZCI6ImF1ZGllbmNlIn0.QDvtbxjkJTUUObB4Bw6nNtepqhi-UivaA4_2I5ufX4k" 
post localhost:5000/api/posts title="Event sourcing, CQRS, and Stream Processing" body="Event sourciing as an application architecture pattern is rising in popularity..." categoryId:=1 tags:='["software", "architecture"]'`

```
{
    "author": "user", 
    "body": "Event sourciing as an application architecture pattern is rising in popularity...", 
    "category": "Software Architecture", 
    "createdDate": "2018-04-06T01:31:12.3492258Z", 
    "id": 17, 
    "tags": [
        "software", 
        "architecture"
    ], 
    "title": "Event sourcing, CQRS, and Stream Processing"
}
```

Run
---

With docker:
```
docker-compose build
docker-compose up
Go to http://localhost:5000 and visit one of the above endpoints (or /swagger)
```

Alternatively, you will need the .NET Core 2.0 SDK. If you have the SDK installed,
then open `appsettings.json` and point the connection string to your server.

```
dotnet restore
dotnet ef database update
dotnet run
Go to http://localhost:5000 and visit one of the above endpoints (or /swagger)
```

Run
---
Figure out how to test commands involving user context
