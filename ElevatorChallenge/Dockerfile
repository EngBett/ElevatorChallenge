FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ElevatorChallenge/ElevatorChallenge.csproj", "ElevatorChallenge/"]
RUN dotnet restore "ElevatorChallenge/ElevatorChallenge.csproj"
COPY . .
WORKDIR "/src/ElevatorChallenge"
RUN dotnet build "ElevatorChallenge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElevatorChallenge.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElevatorChallenge.dll"]
