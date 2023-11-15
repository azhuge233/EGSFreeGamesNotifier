# EGSFreeGamesNotifier

A CLI tool

- Fetch free games info from Epic Games Store.
- Send notifications to Telegram, Bark, Email, QQ, PushPlus(Wechat), DingTalk, PushDeer and Discord.

Demo Telegram Channel [@azhuge233_FreeGames](https://t.me/azhuge233_FreeGames)

## Build

Install dotnet 8.0 SDK first, you can find installation packages/guides [here](https://dotnet.microsoft.com/download).

```shell
git clone https://github.com/azhuge233/EGSFreeGamesNotifier.git
cd EGSFreeGamesNotifier
dotnet publish -c Release -p:PublishDir=/your/path/here -r [win-x64/osx-x64/...] --sc
```

## Usage

Set your telegram bot token and chat ID in config.json.

Check [wiki](https://github.com/azhuge233/EGSFreeGamesNotifier/wiki) for more explanation.

### Repeatedly running

The program will not add while/for loop, it's a scraper. To schedule the program, use cron.d in Linux(macOS) or Task Scheduler in Windows.

## My Free Games Collection

- SteamDB
    - [https://github.com/azhuge233/SteamDB-FreeGames-dotnet](https://github.com/azhuge233/SteamDB-FreeGames-dotnet)(Stop Maintained)
- EpicBundle
    - [https://github.com/azhuge233/EpicBundle-FreeGames-dotnet](https://github.com/azhuge233/EpicBundle-FreeGames-dotnet)
- Indiegala
    - [https://github.com/azhuge233/IndiegalaFreebieNotifier](https://github.com/azhuge233/IndiegalaFreebieNotifier)
- GOG
    - [https://github.com/azhuge233/GOGGiveawayNotifier](https://github.com/azhuge233/GOGGiveawayNotifier)
- Ubisoft
    - [https://github.com/azhuge233/UbisoftGiveawayNotifier](https://github.com/azhuge233/UbisoftGiveawayNotifier)
- PlayStation Plus
    - [https://github.com/azhuge233/PSPlusMonthlyGames-Notifier](https://github.com/azhuge233/PSPlusMonthlyGames-Notifier)
- Reddit Community
    - [https://github.com/azhuge233/RedditFreeGamesNotifier](https://github.com/azhuge233/RedditFreeGamesNotifier)
- Epic Games Store
    - [https://github.com/azhuge233/EGSFreeGamesNotifier](https://github.com/azhuge233/EGSFreeGamesNotifier)
