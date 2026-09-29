var builder = DistributedApplication.CreateBuilder(args);



var usersDb = builder.AddPostgres("users-db")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Session)
    .WithPgAdmin()
    .AddDatabase("UsersDb");

var tasksDb = builder.AddPostgres("tasks-db")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Session)
    .WithPgAdmin()
    .AddDatabase("TasksDb");

var usersApi = builder.AddProject<Projects.Users_API>("users-api")
    .WithReference(usersDb)
    .WaitFor(usersDb);

var tasksApi = builder.AddProject<Projects.Tasks_API>("tasks-api")
    .WithReference(tasksDb)
    .WaitFor(tasksDb);

var gateway = builder.AddProject<Projects.gateway>("gateway")
    .WithReference(usersApi)
    .WithReference(tasksApi)
    .WaitFor(tasksApi)
    .WaitFor(usersApi);

await builder.Build().RunAsync();
