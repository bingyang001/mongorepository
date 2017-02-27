@echo on
echo 'start pack'
nuget.exe pack ..\YmtSystem.Domain.IMongodbRepository.csproj
echo 'start upload'
NuGetPackageUploader .