[![Build Status](https://github.com/edwinprakashgmailcom/legendary-succotash/workflows/companiesapi%20Build%20and%20Test/badge.svg)](https://github.com/edwinprakashgmailcom/legendary-succotash/actions)

# ASP.NET Core Reference Api Application

## Running the application

After cloning or downloading you should be able to run it immediately. 

 **CLI** Just go to the src/Api folder in a terminal window and run `dotnet run` from there.

 **Visual Studio** Just F5 to build and run.

Now you should be able to browse to `http://localhost:5029/swagger` to see the specifications and try it out.  


## Running the application using Docker

You can run the application by running these commands from the root folder (where the .sln file is located):

```
docker-compose build
docker-compose up
```

Now you should be able to browse to `http://localhost:5029/swagger` to see the specifications and try it out.  

You can also run the application by using the instructions located in the `Dockerfile` file in the root of the Api project. Again, run these commands from the root of the solution (where the .sln file is located).
