# Telldus Live Temperature Sensor Fetcher

Detta exempel (en Azure Functions) hämtar sensordata var 15:e minut via Telldus Live (cloud-tjänst) och mailar informationen till önskad e-postaddress.

För att ändra inställingar lokalt vid utveckling, ändra i `local.settings.json`. För att ändra inställingar vid körning i Azure, skapa Environment-variabler via App Settings.

Denna Azure Function använder sig av [Wolfberry's Telldus Live NuGet](https://github.com/wolfberry-ab/telldus-live-dotnet).

Koden beskrivs mer utförligt i artikeln [Nybörjarguide: Läs av din temperatursensor via Telldus Live's molntjänst](https://medium.com/@pappster/nyb%C3%B6rjarguide-l%C3%A4s-av-din-temperatursensor-via-telldus-lives-molntj%C3%A4nst-8eed6ad55f39).

