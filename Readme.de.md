﻿﻿# ELEMNTViewer


## Beschreibung

**Willkommen Rennradfahrer und andere Radfahrer mit einem Wahoo ELEMNT (oder Bolt, Roam) Bike Computer**

Du kannst Dein Workout analysieren und du kannst detaillierte Informationen über dein Workout mit dieser App auf einem Windows Computer anzeigen (Windows 7 und höhere Versionen).

Dieses Programm kann die *.fit Dateien des Wahoo ELEMNT und ELEMNT Bolt auslesen und grafisch darstellen. Die Session- und Lap-Werte werden tabellarisch angezeigt. In einem OpenStreetMap Fenster kann man die Route des Workout betrachten. Eine Beispiel FIT Datei befindet sich im Example Verzeichnis.

Der einfachste Weg die *.fit Dateien auf den Windows Computer zu bekommen: Richte einen freien DropBox Account ein und verbinde diesen zu der Wahoo App auf deinem Phone. Nun werden die *.fit Dateien zur DropBox kopiert nach jedem Workout. Im Windows Computer muss auch die DropBox installiert werden mit deinem Account. Nun kann man die *.fit Dateien im DropBox Ordner Apps\WahooFitness sehen. Diese Dateien müssen über die App ELEMNTViewer ausgewählt werden.

Auch *.gpx Dateien mit Routen (Tracks) und Wegpunkten können im ElemntViewer ab Version 2.3.0 in einer Landkarte dargestellt werden.

ELEMNTViewer ist Open Source Software, veröffentlicht unter der MIT License.

ELEMNTViewer benutzt die Fit Dll von ThisIsAnt FitSDK zum Dekodieren der *.fit Files

Eine Bildschirmauflösung von mindestens 1024 x 768 muss vorhanden sein.

Das Programm ist mit der kostenlosen Entwicklungsumgebung Microsoft Visual Studio Community in der Programmiersprache C# geschrieben und lauffähig unter dem .NET 4.6.2 Framework oder einer höheren Version.

## Contributions

 ELEMNTViewer verwendet die folgenden third party Komponenten:


| Name                                                         | Author(s)                                            | Lizenz      |
| ------------------------------------------------------------ | ---------------------------------------------------- | ----------- |
| [FitSDK](https://www.thisisant.com/developer/resources/downloads/) | [ThisIsAnt](https://www.thisisant.com/)              | MIT License |
| [Windows Ribbon](https://github.com/harborsiem/WindowsRibbon) | [harborsiem](https://github.com/harborsiem) & andere | MIT License |
| [XAML Map Control](https://github.com/ClemensFischer/XAML-Map-Control) | [ClemensFischer](https://github.com/ClemensFischer) | MS-PL License |
## Themen des ELEMNTViewer Programms

- Session Anzeige 
- Lap Anzeigen 
- HeartRate Zonen 
- Power Zonen 
- grafische Anzeige der aufgezeichneten Werte mit Zoom Funktion. Hierbei kann gewählt werden, ob die gesamte Session oder nur einzelne Laps dargestellt werden. Bei einigen Werten (Power, andere Power spezifische Werte, ...) kann eine Glättung aktiviert werden von 3s bis 30s.
- Die Route des Workout in einer Landkarte anzeigen, speichern der Route als gpx - Datei.

## Installation
Voraussetzung für die Installation des Programms auf einem Windows-System ist das .NET Framework 4.6.2 oder eine höhere Version. Falls es noch nicht auf dem Rechner installiert ist, kann das .NET Framework 4.6.2 oder eine höhere Version von der Microsoft-Seite kostenfrei heruntergeladen und installiert werden.

Man benötigt auch das Microsoft .NET Framework 3.5. In Windows 10 muss man die Systemsteuerung  > Programme  > Programme und Features aufrufen. Auf der linken Seite "Windows-Features aktivieren oder deaktivieren". Es erscheint ein neuer Dialog. Hier ist  ".NET Framework 3.5 (enthält ...)" auszuwählen.

[Microsoft .NET Framework](http://www.microsoft.com/netframework)

Entzippe das Setup von der Releases Seite. Danach erfolgt die Installation des Programms mit der Datei ELEMNTSetup.exe (in früheren Versionen ELEMNTViewer.msi) (Doppelklick im Datei-Explorer). Das Programm ist jetzt im Programmverzeichnis ELEMNTViewer installiert. Über den Eintrag ELEMNTViewer im Startmenu kann das Programm aufgerufen werden.

## aktuelles Setup
#### (in Zipdatei gepacktes Setup)

siehe [Releases](https://github.com/harborsiem/ELEMNTViewer/releases) Seite.


## Bilder vom ELEMNTViewer


![ELEMNTViewer](./Images/Viewer1.jpg)
