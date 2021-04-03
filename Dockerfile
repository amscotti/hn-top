FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /build/
COPY . /build/
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /root/
COPY --from=0 /build/bin/Release/net5.0/publish .
ENTRYPOINT ["dotnet", "hn-top.dll"]