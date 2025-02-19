# A ASP.NET+HTMX sample web app.

# Table of contents

Url routing

Use razor view as HTMX response

Layout

Stored procedure and generate c# class by DbSharpApplication

TagHelper

Integrate TypeScript

Calendar feature with FlatPickr

Websocket communication by SignalR


# Setup

Create database by SMSS to your local pc or Azure.

Create table and stored procedure to database by executing TodoTable.sql.

Create config file as below.

C:\DevConfig\aspnet-htmx-todo-sample.json

```
{
  "ConnectionString": "Server=.;Database=aspnet_htmx_todo_sample;Trusted_Connection=Yes;Encrypt=True;TrustServerCertificate=True;Connection Timeout=120;"
}
```







