dotnet publish NRG.Greeter.App\NRG.Greeter.App.csproj -c Release -r win-x64 -o NRG.Greeter.Installations\
tar -acf NRG.Greeter.Installations\App\app.zip -C NRG.Greeter.Installations *.*
rmdir /s /q NRG.Greeter.Installations