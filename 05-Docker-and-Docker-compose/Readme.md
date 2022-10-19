# Asp.Net Core
## Getting start with VSCode
1. Create new project
```
dotnet new webapi -o Weather.Api
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.Seq
```
2. Create launch.json and task.json
* Click debugging tab and click `create a launch.json file`
3. Add Dockerfile to workspace
* ctrl + shift + p
* type: add docker file
* select ASP.Net Core
* select linux
* select port
* select No (Don't create a docker-compose file)
* add dockerServerReadyAction
```
"dockerServerReadyAction": {
    "action": "openExternally",
    "pattern": "\\bNow listening on:\\s+(https?://\\S+)",
    "uriFormat": "%s://localhost:%s/swagger"
}
```
4. Launch with `Docker .Net Code`

> For more information go to [dotnet-docker](https://github.com/dotnet/dotnet-docker)


## Logging with SeriLog
1. Install nuget package
```
dotnet add package Serilog.AspNetCore  
```
2. Update main function
```
public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
```
3. Add useSeriLog() on CreateHostBuilder function
```
public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
```

4. Add sample logging
```
_logging.LogInformation("Generate random weather");
```

5. See Log information in Debug Console

6. Add sink for SeriLog (seq)

7. Start Seq with docker
```
docker pull datalust/seq:latest
docker run --name seq -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest
```

8. Add write to seq on Logger configuration
```
.WriteTo.Seq(serverUrl: "http://host.docker.internal:5341")
```

9. Check logging at [http://localhost:5341](http://localhost:5341)

## Building Image
Build and run image
```
cd to Dockerfile path
docker build -t weather:latest .
docker run -d -p 8080:80 -e ASPNETCORE_ENVIRONMENT=Development --name [CONTAINER_NAME] weather:latest
```

Rename tag
```
docker tag [SOURCE_IMAGE]:[TAG_NAME]
```

Push to Container Registry
```
docker push [DOCKERHUB_USER]/[IMAGE_NAME]:[TAG_NAME]
docker push --all-tags [DOCKERHUB_USER]/[IMAGE_NAME]
```

## Using Docker Compose
Create docker compose
```
docker-compose up -d
docker-compose up [SERVICE_NAME]
```
Delete docker compose
```
docker-compose down
docker-compose down [SERVICE_NAME]
```
Stop docker compose
```
docker-compose stop
docker-compose stop [SERVICE_NAME]
``` 
Start docker compose
```
docker-compose start
docker-compose start [SERVICE_NAME]
```

### Add SeriLog to docker compose
```
seq:
    image: datalust/seq:latest
    ports:
        - "8005:80"
    environment:
        - ACCEPT_EULA=Y
```

### Add MSSQL Database
Pull MSSQL Server
```
docker pull mcr.microsoft.com/mssql/server:2022-latest
```
Check latest version at [hub.docker.com](https://hub.docker.com/_/microsoft-mssql-server)

Update docker compose file
```
sql_server:
    build:
        context: .
        dockerfile: sql.dockerfile
    restart: always
    ports:
        - "1433:1433"
    environment:
        - ACCEPT_EULA=Y
        - SA_PASSWORD=SuperSecretPassword
```

#### Deploy to local environment with Skaffold
* skaffold init
* skaffold init --compose-file docker-compose.yml
* skaffold init --compose-file docker-compose.yml -f skaffold-test.yml
* skaffold init -k .k8s/*.yml

Define build artifacts
```
skaffold init --compose-file docker-compose.yml \
        -a '{"builder": "Docker", "payload": { "path" : "Dockerfile"}, "image": "skaffold-example"}'
```

A local build will update dist and sync it to the container
```
    sync:
        manual:
        - src: "./dist"
          dest: "/usr/share/nginx/html"
    docker:
        dockerfile: "nginx.dev.dockerfile"
```

#### Build and deploy using Skaffold
* skaffold dev
* skaffold run

Real use-case: http://github.com/DanWahlin/Angular-Jumpstart

#### Other tools
* Draft: https://draft.sh
* Tilt: https://tilt.dev
* Garden: https://garden.io
