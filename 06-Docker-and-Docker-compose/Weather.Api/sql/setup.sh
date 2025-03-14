# Wait for SQL Server to be started and then run the sql script
./wait-for-it.sh localhost:1433  --timeout=0 --strict -- sleep 10s && \
/opt/mssql-tools18/bin/sqlcmd -S localhost -i InitializeDatabase.sql -No -U sa -P "$SA_PASSWORD"
