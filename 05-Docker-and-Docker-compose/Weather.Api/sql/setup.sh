# Wait for SQL Server to be started and then run the sql script
./wait-for-it.sh localhost:1433  --timeout=0 --strict -- sleep 10s && \
/opt/mssql-tools/bin/sqlcmd -S localhost -i InitializeDatabase.sql -U sa -P "$SA_PASSWORD"