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
| split json parse into array | X |
| parse array for keywords | X |
| change view based on code-snippets | X |
| loading gif/img while user waits | X |

<hr>

## Project Build

Download from the [repo](https://github.com/GrapeSalad/Digital-ease)

Open Viual Studio 2015 and choose the Open Project option.

Navigate to the directory in which you downloaded the repo and open the project file.

*Packages*

Nuget Packages to ensure are installed:
* IBM.WatsonDeveloperCloud.SpeechToText.v1 - [documentation](https://github.com/watson-developer-cloud/dotnet-standard-sdk/tree/development/src/IBM.WatsonDeveloperCloud.SpeechToText.v1)
* Newtonsoft.Json

Watch for Reference Restoration errors, you may need to add `System.Runtime.4.1.0` to the project.

### Running the Server

After installation , press f5 or the play button in the toolbar to run the server and have the site appear in your default browser.

## Known Bugs

Speech to text is buggy. Its almost impossible for a human to fully understand what another human says every time, so lets not assume the computer knows either.
Need to account for much more variation, which will help.

## Technologies Utilized

* C#
* Visual Studio
* ASP.NET MVC
* ASP.NET
* Javascript
* JQuery
* CSS
* IBM's Watson Speech-to-Text [API](https://www.ibm.com/watson/services/speech-to-text/)
* [Postman](https://www.getpostman.com/)
* [CameraMouse](http://www.cameramouse.org/index.html)
* [Knockout](http://knockoutjs.com/)

## Credits

* David Wilson - main project and idea
* see comments in Javascript files
