var builder = DistributedApplication.CreateBuilder(args);

var sqlServerPassword = builder.AddParameter("SqlServerPassword", secret: true);

var sql = builder.AddSqlServer("sql", password: sqlServerPassword, port: 11880);
sql.WithImageTag("latest");
sql.WithDataVolume();

var airlinesSystemDatabase = sql.AddDatabase(
    name: "AirlinesConnectionDbName",
    databaseName: "FlightReservationDb"
);

builder.AddProject<Projects.FlightReservationSystem_Api>("flightreservationsystem-api")
       .WithReference(airlinesSystemDatabase)
       .WaitFor(airlinesSystemDatabase);

builder.AddProject<Projects.WebApp>("webapp")
       .WithReference(airlinesSystemDatabase)
       .WaitFor(airlinesSystemDatabase);

builder.Build().Run();