FROM mcr.microsoft.com/mssql/server:2022-latest

ARG PROJECT_DIR=/tmp/devdatabase
RUN mkdir -p $PROJECT_DIR
WORKDIR $PROJECT_DIR
COPY sql/InitializeDatabase.sql ./
COPY sql/wait-for-it.sh ./
COPY sql/entrypoint.sh ./
COPY sql/setup.sh ./

USER root
RUN sh -c "chmod +x ./*.* && chown mssql ./*.*"

USER mssql
EXPOSE 1433
CMD ["/bin/bash", "entrypoint.sh"]
# CMD sh -c "./wait-for-it.sh localhost:1433 --timeout=0 --strict -- sleep 5s && /opt/mssql-tools/bin/sqlcmd -S localhost -i InitializeDatabase.sql -U sa -P $SA_PASSWORD"
