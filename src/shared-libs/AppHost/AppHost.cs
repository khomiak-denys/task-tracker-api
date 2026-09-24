var builder = DistributedApplication.CreateBuilder(args);



var usersDb = builder.AddPostgres("users-db")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Session)
    .WithPgAdmin()
    .AddDatabase("users-database");

var tasksDb = builder.AddPostgres("tasks-db")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Session)
    .WithPgAdmin()
    .AddDatabase("tasks-database");

builder.AddProject<Projects.Users_API>("users-api")
    .WithReference(usersDb)
    .WaitFor(usersDb);

builder.AddProject<Projects.Tasks_API>("tasks-api")
    .WithReference(tasksDb)
    .WaitFor(tasksDb);

await builder.Build().RunAsync();
