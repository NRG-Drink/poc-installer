dotnet publish NRG.Greeter.App\NRG.Greeter.App.csproj -c Release -r win-x64 -o NRG.Greeter.Installations\.app
tar -acf NRG.Greeter.Installations\app.zip -C NRG.Greeter.Installations\.app *.*
rmdir /s /q NRG.Greeter.Installations\.app