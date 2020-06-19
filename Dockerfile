FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /build/
COPY . /build/
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /root/
COPY --from=0 /build/bin/Release/netcoreapp3.1/publish .
ENTRYPOINT ["dotnet", "hn-top.dll"]