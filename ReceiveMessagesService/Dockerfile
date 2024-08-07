#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ReceiveMessagesService.csproj", "."]
RUN dotnet restore "./ReceiveMessagesService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ReceiveMessagesService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReceiveMessagesService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReceiveMessagesService.dll"]