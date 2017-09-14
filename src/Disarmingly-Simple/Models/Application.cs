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
    public class Application//most returns are structured as string arrays, keep this in mind when creating methods to crunch and return values.
    {
        public string[] words;
        public SpeechRecognitionEvent result;
        public string[] bgToGreen = { "change", "the", "background", "color", "to", "green" };
        public string[] bgToRed = { "change", "the", "background", "color", "to", "red" };
		public string[] colors = { "red", "green", "blue", "yellow" };
		public string[] numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty" };//can refactor this in order to reduce code. need to account for 22 == twenty-two.

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
                string[] testArray = { "Nothing to record", "Please, re-record your statement", "and stand close to mic"};
                words = testArray;
            }
            return splitArrayTesting(words);
        }

		public string[] fixArray(string[] array)//is this necessary?
		{
			string[] wordToRemove = { "the", "of", "it", "to", "two", "too", "", "that" };
			string[] fixedArray = { };
			//string[] codeToReturn = { };
			for (var i = 0; i < wordToRemove.Count(); i++)
			{
				fixedArray = array.Where(val => val != wordToRemove[i]).ToArray();
			}
			return fixedArray;
		}
        public Array splitArrayTesting(string[] chosenArray)//Simple changes to bg/font color, font size
        {
			string[] fixedArray = fixArray(chosenArray);
			if(fixedArray.Contains("change"))
			{
				if (fixedArray.Contains("background"))
				{
					if(fixedArray.Contains("color"))
					{
						string color = GetColor(fixedArray)[0];
						if (GetColor(fixedArray).Length == 1)
						{
							string[] returnArray = { "<style>body{background-color:" + color + ";}</style>", "Background color changed to " + color };
							return returnArray;
						}
						else
						{
							string[] returnArray = { "", GetColor(fixedArray)[1] };
							return returnArray;
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
								string[] codeToReturn = { "<style>body{font-size:"+(Array.IndexOf(numbers, number)+1)+"px;}</style>", number };//Gets index of the number in the numbers array
								return codeToReturn;
							}
						}
						string[] toReturn = { "", "Failure in font size"};
						return toReturn;
					}
					else if(fixedArray.Contains("color"))
					{
						string color = GetColor(fixedArray)[0];
						if (GetColor(fixedArray).Length == 1)
						{
							string[] returnArray = { "<style>body{color:" + color + "!important;}</style>", "Font color changed to "+color };
							return returnArray;
						}
						else
						{
							string[] returnArray = { "", GetColor(fixedArray)[1] };
							return returnArray;
						}
					}
					else
					{
						string[] toReturn = { "", "Failure in font" };
						return toReturn;
					}
				}
				else
				{
					return words;
				}
			}
			else if(fixedArray.Contains("create"))//CREATE PARAGRAPH
			{
				if(fixedArray.Contains("paragraph"))
				{
					if(fixedArray.Contains("says"))
					{
						//List<string> returnArray = new List<string>(){ };
						List<string> testArray = new List<string>() { "<p>" };
						for(var i = (Array.IndexOf(fixedArray, "says")+1); i<=fixedArray.Length;i++)
						{
							if(i < fixedArray.Length)
							{
								testArray.Add(fixedArray[i].ToString());
							}
							else
							{
								testArray.Add("</p>");
							}
						}

						string[] returnArray = { String.Join(" ", testArray), String.Join(" ", fixedArray)};
						//returnArray.Add(fixedArray.ToString());
						return returnArray;
					}
					else
					{
						return words;
					}
				}
				else
				{
					return words;
				}
			}
			else
			{
				return words;
			}
        }

		public string[] GetColor(string[] chosenArray)//Well, this one gets the color
		{
			if (chosenArray.Contains("dark"))
			{
				if (chosenArray.Contains("cyan"))
				{
					string[] codeToReturn = { "darkcyan" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("blue"))
				{
					string[] codeToReturn = { "darkblue" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("goldenrod"))
				{
					string[] codeToReturn = { "darkgoldenrod" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("grey") || chosenArray.Contains("gray"))
				{
					string[] codeToReturn = { "darkgrey" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("green"))
				{
					string[] codeToReturn = { "darkgreen" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("khaki"))
				{
					string[] codeToReturn = { "darkkhaki" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("magenta"))
				{
					string[] codeToReturn = { "darkmagenta" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("olive") && chosenArray.Contains("green"))
				{
					string[] codeToReturn = { "darkolivegreen" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("orange"))
				{
					string[] codeToReturn = { "darkorange" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("orchid") || (chosenArray.Contains("ore") && chosenArray.Contains("kid")) || (chosenArray.Contains("or") && (chosenArray.Contains("kid") || chosenArray.Contains("could") || chosenArray.Contains("did"))) || (chosenArray.Contains("her") && chosenArray.Contains("or") && chosenArray.Contains("get")) || chosenArray.Contains("work"))
				{
					string[] codeToReturn = { "darkorchid" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("red"))
				{
					string[] codeToReturn = { "darkred" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("salmon"))
				{
					string[] codeToReturn = { "darksalmon" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("seagreen") || ((chosenArray.Contains("sea") || chosenArray.Contains("see")) && chosenArray.Contains("green")))
				{
					string[] codeToReturn = { "darkseagreen" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("slate"))
				{
					if (chosenArray.Contains("blue"))
					{
						string[] codeToReturn = { "darkslateblue" };
						return codeToReturn.ToArray();
					}
					else if (chosenArray.Contains("grey") || chosenArray.Contains("gray"))
					{
						string[] codeToReturn = { "darkslategrey" };
						return codeToReturn.ToArray();
					}
					else
					{
						string[] codeToReturn = { "error" };
						return codeToReturn.ToArray();
					}
				}
				else if (chosenArray.Contains("turquoise"))
				{
					string[] codeToReturn = { "darkturquoise" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("violet"))
				{
					string[] codeToReturn = { "darkviolet" };
					return codeToReturn.ToArray();
				}
				else
				{
					string[] codeToReturn = { "", "Please see the documentation for visual studio's native colors." };
					return codeToReturn.ToArray();
				}
			}
			else if (chosenArray.Contains("deep"))
			{
				if (chosenArray.Contains("pink"))
				{
					string[] codeToReturn = { "darkpink" };
					return codeToReturn.ToArray();
				}
				else if (chosenArray.Contains("skyblue") || (chosenArray.Contains("sky") && chosenArray.Contains("blue")))
				{
					string[] codeToReturn = { "darkskyblue" };
					return codeToReturn.ToArray();
				}
				else
				{
					string[] codeToReturn = { "", "Needs pink or skyblue" };
					return codeToReturn.ToArray();
				}
			}
			else if ((chosenArray.Contains("alice") || chosenArray.Contains("Dallas") || (chosenArray.Contains("how") && chosenArray.Contains("lists"))) && chosenArray.Contains("blue"))
			{
				string[] codeToReturn = { "aliceblue" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("antique") && chosenArray.Contains("white"))
			{
				string[] codeToReturn = { "antiqueWhite" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("aquamarine") || (chosenArray.Contains("aqua") && chosenArray.Contains("marine")))
			{
				string[] codeToReturn = { "aquamarine" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("aqua"))
			{
				string[] codeToReturn = { "aqua" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("azure"))
			{
				string[] codeToReturn = { "azure" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("beige"))
			{
				string[] codeToReturn = { "beige" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("bisque"))
			{
				string[] codeToReturn = { "bisque" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("black"))
			{
				string[] codeToReturn = { "black" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("blanched") && chosenArray.Contains("almond"))
			{
				string[] codeToReturn = { "Blanched Almond" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("blue") && chosenArray.Contains("violet"))
			{
				string[] codeToReturn = { "Blue Violet" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("cadetblue") || (chosenArray.Contains("cadet") && chosenArray.Contains("blue")))
			{
				string[] codeToReturn = { "cadetblue" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("cornflower") || (chosenArray.Contains("cornflower") && chosenArray.Contains("blue")) || (chosenArray.Contains("corn") && chosenArray.Contains("flower") && chosenArray.Contains("blue")))
			{
				string[] codeToReturn = { "cornflowerblue" };
				return codeToReturn.ToArray();
			}

			else if (chosenArray.Contains("midnightblue") || (chosenArray.Contains("midnight") && chosenArray.Contains("blue")))
			{
				string[] codeToReturn = { "midnightblue" };
				return codeToReturn.ToArray();
			}

			else if (chosenArray.Contains("royal") && chosenArray.Contains("blue"))
			{
				string[] codeToReturn = { "royalblue" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("blue"))
			{
				string[] codeToReturn = { "blue" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("burlywood") || (chosenArray.Contains("burly") && chosenArray.Contains("wood")))
			{
				string[] codeToReturn = { "BurlyWood" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("chartreuse") || (chosenArray.Contains("chart") && chosenArray.Contains("ruse")))
			{
				string[] codeToReturn = { "chartreuse" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("chocolate"))
			{
				string[] codeToReturn = { "chocolate" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("coral"))
			{
				string[] codeToReturn = { "coral" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("cornsilk") || (chosenArray.Contains("corn") && chosenArray.Contains("silk")))
			{
				string[] codeToReturn = { "cornsilk" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("crimson"))
			{
				string[] codeToReturn = {"crimson" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("cyan"))
			{
				string[] codeToReturn = { "cyan" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("dim") && (chosenArray.Contains("grey") || chosenArray.Contains("gray")))
			{
				string[] codeToReturn = { "dimgrey" };
				return codeToReturn.ToArray();
			}
			else if ((chosenArray.Contains("dodger") || (chosenArray.Contains("dodge") && (chosenArray.Contains("er") || chosenArray.Contains("her")))) && chosenArray.Contains("blue"))
			{
				string[] codeToReturn = { "dodgerblue" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("firebrick") || (chosenArray.Contains("fire") && chosenArray.Contains("brick")))
			{
				string[] codeToReturn = { "firebrick" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("floral") && chosenArray.Contains("white"))
			{
				string[] codeToReturn = { "floralwhite" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("forest") && chosenArray.Contains("green"))
			{
				string[] codeToReturn = { "forestgreen" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("fuchsia"))
			{
				string[] codeToReturn = { "fuchsia" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("gainsboro") || (chosenArray.Contains("gains") && (chosenArray.Contains("boro") || chosenArray.Contains("borough") || chosenArray.Contains("borrow"))))
			{
				string[] codeToReturn = { "gainsboro" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("ghost") && chosenArray.Contains("white"))
			{
				string[] codeToReturn = { "ghostwhite" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("gold"))
			{
				string[] codeToReturn = { "gold" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("goldenrod") || (chosenArray.Contains("golden") && chosenArray.Contains("rod")))
			{
				string[] codeToReturn = { "goldenrod" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("grey") || chosenArray.Contains("gray"))
			{
				string[] codeToReturn = { "grey" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("green") && chosenArray.Contains("yellow"))
			{
				string[] codeToReturn = { "greenyellow" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("lawn") && chosenArray.Contains("green"))
			{
				string[] codeToReturn = { "lawngreen" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("lime") && chosenArray.Contains("green"))
			{
				string[] codeToReturn = { "limegreen" };
				return codeToReturn.ToArray();
			}
			else if ((chosenArray.Contains("sea") || chosenArray.Contains("see")) && chosenArray.Contains("green"))
			{
				string[] codeToReturn = { "seagreen" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("green"))
			{
				string[] codeToReturn = { "green" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("honeydew") || (chosenArray.Contains("honey") && (chosenArray.Contains("dew") || chosenArray.Contains("do"))))
			{
				string[] codeToReturn = { "honeydew" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("hotpink") || (chosenArray.Contains("hot") && chosenArray.Contains("pink")))
			{
				string[] codeToReturn = { "hotpink" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("indianred") || (chosenArray.Contains("indian") && (chosenArray.Contains("read") || chosenArray.Contains("red"))))
			{
				string[] codeToReturn = { "indianred" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("indigo"))
			{
				string[] codeToReturn = { "indigo" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("ivory"))
			{
				string[] codeToReturn = { "ivory" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("khaki"))
			{
				string[] codeToReturn = { "khaki" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("lavenderblush") || (chosenArray.Contains("lavender") && chosenArray.Contains("blush")))
			{
				string[] codeToReturn = { "lavenderblush" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("lavender"))
			{
				string[] codeToReturn = { "lavender" };
				return codeToReturn.ToArray();
			}

			else if (chosenArray.Contains("lemon") && chosenArray.Contains("chiffon"))
			{
				string[] codeToReturn = { "lemonchiffon" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("light"))
			{
				if (chosenArray.Contains("blue"))
				{
					string[] codeToReturn = { "lightblue" };
					return codeToReturn.ToArray();
				}

				else if (chosenArray.Contains("coral"))
				{
					string[] codeToReturn = { "lightcoral" };
					return codeToReturn.ToArray();
				}

				else if (chosenArray.Contains("cyan"))
				{
					string[] codeToReturn = { "lightcyan" };
					return codeToReturn.ToArray();
				}

				else if (chosenArray.Contains("golden") && chosenArray.Contains("rod"))
				{
					string[] codeToReturn = { "lightgoldenrodyellow" };
					return codeToReturn.ToArray();
				}

				else if (chosenArray.Contains("green"))
				{
					string[] codeToReturn = { "lightgreen" };
					return codeToReturn.ToArray();
				}

				else if (chosenArray.Contains("grey") || chosenArray.Contains("gray"))
				{
					string[] codeToReturn = { "lightgrey" };
					return codeToReturn.ToArray();
				}

				else if (chosenArray.Contains("pink"))
				{
					string[] codeToReturn = { "lightpink" };
					return codeToReturn.ToArray();
				}

				else if (chosenArray.Contains("salmon"))
				{
					string[] codeToReturn = { "lighsalmon" };
					return codeToReturn.ToArray();
				}

				else if (chosenArray.Contains("yellow"))
				{
					string[] codeToReturn = { "lightyellow" };
					return codeToReturn.ToArray();
				}
			}
			else if (chosenArray.Contains("lime"))
			{
				string[] codeToReturn = { "lime" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("linen"))
			{
				string[] codeToReturn = { "linen" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("magenta"))
			{
				string[] codeToReturn = { "magenta" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("maroon"))
			{
				string[] codeToReturn = { "maroon" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("mint") && chosenArray.Contains("cream"))
			{
				string[] codeToReturn = { "mintcream" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("misty") && chosenArray.Contains("rose"))
			{
				string[] codeToReturn = { "mistyrose" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("moccasin"))
			{
				string[] codeToReturn = { "moccasin" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("navajo"))
			{
				string[] codeToReturn = { "navajowhite" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("navy"))
			{
				string[] codeToReturn = { "navy" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("old") && chosenArray.Contains("lace"))
			{
				string[] codeToReturn = { "oldlace" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("olive"))
			{
				string[] codeToReturn = { "olive" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("red") && chosenArray.Contains("orange"))
			{
				string[] codeToReturn = { "orangered" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("orange"))
			{
				string[] codeToReturn = { "orange" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("orchid"))
			{
				string[] codeToReturn = { "orchid" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("peach") && chosenArray.Contains("puff"))
			{
				string[] codeToReturn = { "peachpuff" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("peru"))
			{
				string[] codeToReturn = { "peru" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("plum"))
			{
				string[] codeToReturn = { "plum" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("powderblue") || (chosenArray.Contains("powder") && chosenArray.Contains("blue")))
			{
				string[] codeToReturn = { "powderblue" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("purple"))
			{
				string[] codeToReturn = { "purple" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("red"))
			{
				string[] codeToReturn = { "red" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("rosy") && chosenArray.Contains("brown"))
			{
				string[] codeToReturn = { "rosybrown" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("saddle") && chosenArray.Contains("brown"))
			{
				string[] codeToReturn = { "saddlebrown" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("salmon"))
			{
				string[] codeToReturn = { "salmon" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("sandy") && chosenArray.Contains("brown"))
			{
				string[] codeToReturn = { "yellow" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("sienna"))
			{
				string[] codeToReturn = { "sienna" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("silver"))
			{
				string[] codeToReturn = { "silver" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("snow"))
			{
				string[] codeToReturn = { "snow" };
				return codeToReturn.ToArray();
			}

			else if (chosenArray.Contains("tan"))
			{
				string[] codeToReturn = { "tan" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("teal"))
			{
				string[] codeToReturn = { "teal" };
				return codeToReturn.ToArray();
			}

			else if (chosenArray.Contains("thistle"))
			{
				string[] codeToReturn = { "thistle" };
				return codeToReturn.ToArray();
			}

			else if (chosenArray.Contains("tomato"))
			{
				string[] codeToReturn = { "tomato" };
				return codeToReturn.ToArray();
			}

			else if (chosenArray.Contains("turquiose"))
			{
				string[] codeToReturn = { "turquiose" };
				return codeToReturn.ToArray();
			}

			else if (chosenArray.Contains("violet"))
			{
				string[] codeToReturn = { "violet" };
				return codeToReturn.ToArray();
			}

			else if (chosenArray.Contains("wheat"))
			{
				string[] codeToReturn = { "wheat" };
				return codeToReturn.ToArray();
			}

			else if (chosenArray.Contains("white"))
			{
				string[] codeToReturn = { "white" };
				return codeToReturn.ToArray();
			}

			else if (chosenArray.Contains("yellow"))
			{
				string[] codeToReturn = { "yellow" };
				return codeToReturn.ToArray();
			}
			else if (chosenArray.Contains("pink"))
			{
				string[] codeToReturn = { "pink" };
				return codeToReturn.ToArray();
			}
			else
			{
				string[] codeToReturn = { "BEEP BOOP DOES NOT COMPUTE", "Error finding the name, potential fix: re-record | " + String.Join(" ", chosenArray)};
				return codeToReturn.ToArray();
			}
			return chosenArray;
		}

		public string[] CreateForm(string[] chosenArray)
		{
			string[] formCreation = {"<form>" };
			if(chosenArray.Contains("input") && (chosenArray.Contains("field") || chosenArray.Contains("fields")))
			{
				if(chosenArray.Contains("number") || chosenArray.Contains("numbers"))
				{
					int num = Array.IndexOf(chosenArray, "number");//gets the index of the word number in the chosenArray
					foreach (var number in numbers)
					{
						if (Array.IndexOf(chosenArray, number) == (num + 1))//testing to see if chosenArray returns a number before the word number, to see how many number fields the user wants
						{
							for(int i = 0; i<=)//need to get index of number array to get number of number input fields to display
							formCreation.Append();
						}
					}
				}
				else if()//contains text
				{

				}
			}
			else
			{
				string[] fail = {"you done failed this one", "broh" };
				return fail;
			}
		}
    }
}
