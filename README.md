# Le fish

An email sending web thing.

## Getting started

### 0. Prepare a domain

- Register a suitable domain name
- Find web hosting with asp.net core support, and email.
- Set up DNS records, MX 
- Prepare the website (SSL, etc)
- Prepare an email account for sending email.

### 1. Prepare the app settings

The is a file named 'appsettings.json' in the Web project, 
with properties mirrored by the UserAccountConfiguration class in the Common project.

Make sure the default values of appsettings.json and its subfiles are 
the desired ones, i.e. allowing registration of an account when running remotely, 
but not allowing registration of an account when deployed in the production environment.

### 2. Prepare the database config

You need to create a file named 'Database.json' in the Config folder of the Web project,
with the expected format.

```
{
  "ConnectionStrings": {
    "Local": " ... ",
    "Remote": " ...",
    "Production": " ..."
  }
}
```

This has to mirror the Solution Configurations, and also the preprocessor directives
of the ConnectionStringFactory class in the Web project. (Yes, this is a bit clunky,
and probably a misnomer.)

And you have to fill in the database connection strings values, 
matching your preexisting production environment (which you have to provide somehow),
and a local environment if you need it.

The purpose of the Remote configuration is primarily to run the app locally against
the database of the producion environment.

Try to verify by some other tool, e.g. SQL Server Management Studio,
that your database connection string(s) work.

### 3. Create the database schema

When starting from an empty database with no schema yet, you can use Visual Studio
to create an intial database migration, which creates all the expected database tables 
and relations for you.

In Visual Studio:
- Delete the Migrations folder in the Persistence project, if there is one.
- Select the Solution Configuration "Remote". (Or Production)
- Right-click the Web project in the Solution Explorer sidebar, and "Set as Startup Project".
- Verify that the name of the Web project is shown in Bold.
- Open the Package Manager Console.
- Select "Persistence" as the "Default project", from the dropdown on the "Package Manager Console" panel.
- Type "Add-Migration FirstMigration", in the package manager console, and run it.
- Inspect the migrations files which are created.
- Type "Update-Database" and run it.
- Try to verify by some other tool, e.g. SQL Server Management Studio, that tables and relations have been created.
- Run the project with the Remote config and verify that the database connection works.

### 4. Prepare the admin account

- Use the Remote config.
- Run the Web project locally against the producation database.
- Visit the page /register-account
- Create your account.
- Verify that you can log in.

## FAQ

### Why so many projects for a simple website?

Years ago I modelled a solution after [Matthew Renze](https://github.com/matthewrenze) /
[clean-architecture-core](https://github.com/matthewrenze/clean-architecture-core) 
and I kept using it. This code is a stripped down version.

### Why not store the database connection string in appsettings.json et al?

That would have been nice, but I'd rather keep it out of source control.
Please excuse the ConnectionStringFactory kludge.

### Why is there a SessionKeys folder in the Web project?

Storing the session keys this way keeps user sessions alive on redeployment of the app.
It works well with my present hosting provider. 
No other reason.
