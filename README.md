# Telldus Live Temperature Sensor Fetcher

Detta exempel (en Azure Functions) hämtar sensordata var 15:e minut via Telldus Live (cloud-tjänst) och mailar informationen till önskad e-postaddress.

För att ändra inställingar lokalt vid utveckling, ändra i `local.settings.json`. För att ändra inställingar vid körning i Azure, skapa Environment-variabler via App Settings.

Denna Azure Function använder sig av [Wolfberry's Telldus Live NuGet](https://github.com/wolfberry-ab/telldus-live-dotnet).
