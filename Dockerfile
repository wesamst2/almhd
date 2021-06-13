FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine as base

# Copy everything else and build
COPY ./ /opt/Almhd
WORKDIR /opt/Almhd

RUN ["dotnet","publish","./src/Almhd/Almhd.csproj","-o","./outputs" ]

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine as run
COPY --from=base /opt/Almhd/outputs /opt/Almhd/outputs
WORKDIR /opt/Almhd/outputs
ENTRYPOINT ["dotnet", "Almhd.dll"]
