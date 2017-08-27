# Digital-ease

Capstone for Epicodus C#/.NET.<br>
This is an idea for a hands free web-developement app. Using speech-to-text technology, the user will be able to interact with the app and build/style a simple website.

## Specs/Wishlist

| Sections of developement | Implemented? |
|---|---|
| API call using curl or postman | Postman - X |
| api call using RestSharp | NO WAY |
| use the Watson Nuget Packages to successfully transcribe speech to text | X |
| find a way for a user to record then save audio directly from the page | X |
| output text on page | X |
| build corpus for code-specific words/sentences | O |
| build keyword file | O |
| build keyword references | O |
| build code snippets that link to a keyword | O |
| parse returned json for transcript  | X |
| split json parse into array | O |
| parse array for keywords | O |
| change view based on code-snippets | O |

<hr>

## Project Build

Download from the [repo](https://github.com/GrapeSalad/Digital-ease)

Open Viual Studio 2015 and choose the Open Project option.

Navigate to the directory in which you downloaded the repo and open the project file.

*Packages*

Nuget Packages to get:
* IBM.WatsonDeveloperCloud.SpeechToText.v1 - [documentation](https://github.com/watson-developer-cloud/dotnet-standard-sdk/tree/development/src/IBM.WatsonDeveloperCloud.SpeechToText.v1)
* Newtonsoft.Json

### Migration

Navigate to `....\project-name\src\projectname`.
Run this command in your terminal: `dotnet ef database update`.

### Data Entry

Open [SSMS](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms), Log in to the default database, Find the open file button in the tool ribbon, navigate to and double click on the file named Data_For_Migration in the SQL_Queries folder.

### Running the Server

After installation and Migration, press f5 or the play button in the toolbar to run the server and have the site appear in your default browser.

## Known Bugs

This don't even work yet broh. Barely even started, broh.

## Technologies Utilized

* C#
* Visual Studio
* ASP.NET MVC
* ASP.NET
* CSS
* IBM's Watson Speech-to-Text [API](https://www.ibm.com/watson/services/speech-to-text/)
* [Postman](https://www.getpostman.com/)
* [CameraMouse](http://www.cameramouse.org/index.html)
* [Knockout](http://knockoutjs.com/)

## Credits

* David Wilson