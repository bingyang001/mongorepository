@echo on
echo 'start pack'
nuget.exe pack ..\YmtSystem.Repository.Mongodb.csproj
echo 'start upload'
NuGetPackageUploader .