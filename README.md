# Scoped Filters

MassTransit supports [scoped filters](https://masstransit-project.com/advanced/middleware/scoped.html), allowing access to the same container scope as consumers as well as message producers, like those within ASP.NET controllers, etc.

This sample shows how to manage a scoped class, `Token`, and use it to pass values between an ASP.NET controller action and MassTransit send/publish middleware filters.

Simply open it up and run it, and add a header to the request (using Postman, CURL, whatever) called "Token" with any string value to get a response.
