using IBM.WatsonDeveloperCloud.SpeechToText.v1;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Disarmingly_Simple.Models.EnvironmentVariables;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Disarmingly_Simple.Models
{
    public class Application
    {
        public string[] words;
        public SpeechRecognitionEvent result;
        public string[] bgToGreen = { "change", "the", "background", "color", "to", "green" };
        public string[] bgToRed = { "change", "the", "background", "color", "to", "red" };
		public string[] colors = { "red", "green", "blue", "yellow" };
		public string[] numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty" };

        private SpeechToTextService _speechToText = new SpeechToTextService();

        private void SetCredentials()
        {
            _speechToText.SetCredential(EnviroVars.AccountID, EnviroVars.AccountPass);
        }

        public SpeechModelSet GetModelsForSet()
        {
            SetCredentials();
            var models = _speechToText.GetModels();
            return models;
        }
        public Session GetCreateNewSession()
        {
            SetCredentials();
            var sessionResult = _speechToText.CreateSession("en-US_BroadbandModel");
            return sessionResult;
        }

        public RecognizeStatus GetSessionStatus_of_GetCreateNewSession()
        {
            SetCredentials();
            var recognizeStatus = _speechToText.GetSessionStatus(this.GetCreateNewSession().SessionId);
            return recognizeStatus;
        }

        public SpeechRecognitionEvent GetSpeechRecogEventWithSessionId(string fileName)
        {
            Stream s = new MemoryStream(File.ReadAllBytes("wwwroot/audio/" + fileName + ".wav"));
            result = _speechToText.RecognizeWithSession(this.GetCreateNewSession().SessionId, "audio/wav", s);
            return result;
        }

        public Array parseSpeechToTextResult(SpeechRecognitionEvent speechResult)
        {
            if (speechResult.Results.Count > 0)
            {
                for (int i = 0; i < speechResult.Results.Count; i++)
                {
                    words = speechResult.Results[i].Alternatives[0].Transcript.Split(' ');//fix array splitting and adding to words array. 
                }
				//NEED TO ADD ERROR HANDLING FOR TO/TWO AND OTHER SIMILAR ISSUES
            }
            else
            {
                string[] testArray = { "<style>body{background-color:green;}</style>", "aint", "nothing", "here"};
                words = testArray;
            }
            return splitArrayTesting(words);
        }
        public Array splitArrayTesting(string[] chosenArray)
        {
            string[] wordToRemove = { "the", "of", "it", "to", "two" };
            string[] fixedArray = { };
            //string[] codeToReturn = { };
            for(var i = 0; i < wordToRemove.Count(); i ++)
            {
                fixedArray = chosenArray.Where(val => val != wordToRemove[i]).ToArray();
            }
            if(fixedArray.Contains("background"))
            {
                if(fixedArray.Contains("color"))
                {
                    if(fixedArray.Contains("green"))
                    {
                        string[] codeToReturn = { "<style>body{background-color:green;}</style>", "green"};
                        return codeToReturn.ToArray();
                    }
                    else if(fixedArray.Contains("red"))
                    {
                        string[] codeToReturn = { "<style>body{background-color:red;}</style>", "red"};
                        return codeToReturn.ToArray();
                    }
                    else if (fixedArray.Contains("blue"))
                    {
                        string[] codeToReturn = { "<style>body{background-color:blue;}</style>", "blue"};
                        return codeToReturn.ToArray();
                    }
                    else if (fixedArray.Contains("yellow"))
                    {
                        string[] codeToReturn = { "<style>body{background-color:yellow;}</style>", "yellow"};
                        return codeToReturn.ToArray();
                    }
                    else
                    {
                        string[] codeToReturn = { "BEEP BOOP DOES NOT COMPUTE", "maybe speak more clearly or use a primary color"};
                        return codeToReturn.ToArray();
                    }
                }
                return fixedArray;
            }
			else if( fixedArray.Contains("font"))
			{
				if(fixedArray.Contains("size"))
				{
					foreach(var number in numbers)
					{
						if(fixedArray.Contains(number))
						{
							
							string[] codeToReturn = { "<style>body{font-size:"+(Array.IndexOf(numbers, number)+1)+"px;}</style>", number };
							return codeToReturn;
						}
					}
					string[] toReturn = { "", "Failure in font size"};
					return toReturn;
				}
				else if(fixedArray.Contains("color"))
				{
					foreach(string color in colors)
					{
						if(fixedArray.Contains(color))
						{
							string[] codeToReturn = { "<style>body{color:"+color+";}</style>}", "changed font color to "+color};
						}
						else
						{
							string[] codeToReturn = { "", "Incapable of returning this color yet!  ..Oh yeah beep and boop.. still a abot" };
						}
					}
					string[] toReturn = { "", "Failure in font color" };
					return toReturn;
				}
				else
				{
					string[] toReturn = { "", "Failure in font" };
					return toReturn;
				}
			}
			else
			{
				return fixedArray;
			}
        }
    }
}
