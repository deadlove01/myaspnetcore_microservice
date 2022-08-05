docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d



dotnet ef migrations add initdb -s Order.API/Order.API.csproj -p Order.Infrastructure/Order.Infrastructure.csproj -c OrderContext

