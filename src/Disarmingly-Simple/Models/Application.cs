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
					if (fixedArray.Contains("dark"))
					{
						if(fixedArray.Contains("cyan"))
						{
							string[] codeToReturn = { "<style>body{background-color:darkcyan;}</style>", "darkcyan" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("blue"))
						{
							string[] codeToReturn = { "<style>body{background-color:darkblue;}</style>", "darkblue" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("goldenrod"))
						{
							string[] codeToReturn = { "<style>body{background-color:darkgoldenrod;}</style>", "darkgoldenrod" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("grey") || fixedArray.Contains("gray"))
						{
							string[] codeToReturn = { "<style>body{background-color:darkgrey;}</style>", "darkgrey" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("green"))
						{
							string[] codeToReturn = { "<style>body{background-color:darkgreen;}</style>", "darkgreen" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("khaki"))
						{
							string[] codeToReturn = { "<style>body{background-color:darkkhaki;}</style>", "darkkhaki" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("magenta"))
						{
							string[] codeToReturn = { "<style>body{background-color:darkmagenta;}</style>", "darkmagenta" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("olive") && fixedArray.Contains("green"))
						{
							string[] codeToReturn = { "<style>body{background-color:darkolivegreen;}</style>", "darkolivegreen" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("orange"))
						{
							string[] codeToReturn = { "<style>body{background-color:darkorange;}</style>", "darkorange" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("orchid") || (fixedArray.Contains("ore") && fixedArray.Contains("kid")) || (fixedArray.Contains("or") && fixedArray.Contains("kid")))
						{
							string[] codeToReturn = { "<style>body{background-color:darkorchid;}</style>", "darkorchid" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("red"))
						{
							string[] codeToReturn = { "<style>body{background-color:darkred;}</style>", "darkred" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("salmon"))
						{
							string[] codeToReturn = { "<style>body{background-color:darksalmon;}</style>", "darksalmon" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("seagreen") || ((fixedArray.Contains("sea") || fixedArray.Contains("see")) && fixedArray.Contains("green")))
						{
							string[] codeToReturn = { "<style>body{background-color:darkseagreen;}</style>", "darkseagreen" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("slate"))
						{
							if(fixedArray.Contains("blue"))
							{
								string[] codeToReturn = { "<style>body{background-color:darkslateblue;}</style>", "darkslateblue" };
								return codeToReturn.ToArray();
							}
							else if(fixedArray.Contains("grey") || fixedArray.Contains("gray"))
							{
								string[] codeToReturn = { "<style>body{background-color:darkslategrey;}</style>", "darkslategrey" };
								return codeToReturn.ToArray();
							}
							else
							{
								string[] codeToReturn = { "", "Needs either blue or grey!" };
								return codeToReturn.ToArray();
							}
						}
						else if (fixedArray.Contains("turquoise"))
						{
							string[] codeToReturn = { "<style>body{background-color:darkturquoise;}</style>", "darkturquoise" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("violet"))
						{
							string[] codeToReturn = { "<style>body{background-color:darkviolet;}</style>", "darkviolet" };
							return codeToReturn.ToArray();
						}
						else
						{
							string[] codeToReturn = { "", "Please see the documentation for visual studio's native colors." };
							return codeToReturn.ToArray();
						}
					}
					else if(fixedArray.Contains("deep"))
					{
						if (fixedArray.Contains("pink"))
						{
							string[] codeToReturn = { "<style>body{background-color:darkpink;}</style>", "darkpink" };
							return codeToReturn.ToArray();
						}
						else if (fixedArray.Contains("skyblue") || (fixedArray.Contains("sky") && fixedArray.Contains("blue")))
						{
							string[] codeToReturn = { "<style>body{background-color:darkskyblue;}</style>", "darkskyblue" };
							return codeToReturn.ToArray();
						}
						else
						{
							string[] codeToReturn = { "", "Needs pink or skyblue" };
							return codeToReturn.ToArray();
						}
					}
                    else if(fixedArray.Contains("alice") && fixedArray.Contains("blue"))
                    {
                        string[] codeToReturn = { "<style>body{background-color:aliceblue;}</style>", "Alice Blue"};
                        return codeToReturn.ToArray();
                    }
					else if (fixedArray.Contains("antique")&&fixedArray.Contains("white"))
					{
						string[] codeToReturn = { "<style>body{background-color:antiquewhite;}</style>", "AntiqueWhite" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("aquamarine") || (fixedArray.Contains("aqua") && fixedArray.Contains("marine")))
					{
						string[] codeToReturn = { "<style>body{background-color:aquamarine;}</style>", "Aquamarine" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("aqua"))
					{
						string[] codeToReturn = { "<style>body{background-color:aqua;}</style>", "aqua" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("azure"))
					{
						string[] codeToReturn = { "<style>body{background-color:azure;}</style>", "azure" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("beige"))
					{
						string[] codeToReturn = { "<style>body{background-color:beige;}</style>", "beige" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("bisque"))
					{
						string[] codeToReturn = { "<style>body{background-color:bisque;}</style>", "bisque" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("black"))
					{
						string[] codeToReturn = { "<style>body{background-color:black;}</style>", "black" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("blanched") && fixedArray.Contains("almond"))
					{
						string[] codeToReturn = { "<style>body{background-color:blanchedalmond;}</style>", "Blanched Almond" };
						return codeToReturn.ToArray();
					}
                    else if (fixedArray.Contains("blue") && fixedArray.Contains("violet"))
                    {
                        string[] codeToReturn = { "<style>body{background-color:blueviolet;}</style>", "Blue Violet"};
                        return codeToReturn.ToArray();
                    }
					else if (fixedArray.Contains("cadetblue") || (fixedArray.Contains("cadet") && fixedArray.Contains("blue")))
					{
						string[] codeToReturn = { "<style>body{background-color:cadetblue;}</style>", "cadetblue" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("cornflower") || (fixedArray.Contains("cornflower") && fixedArray.Contains("blue")) || (fixedArray.Contains("corn") && fixedArray.Contains("flower") && fixedArray.Contains("blue")))
					{
						string[] codeToReturn = { "<style>body{background-color:cornflowerblue;}</style>", "cornflowerblue" };
						return codeToReturn.ToArray();
					}

					else if (fixedArray.Contains("midnightblue") || (fixedArray.Contains("midnight") && fixedArray.Contains("blue")))
					{
						string[] codeToReturn = { "<style>body{background-color:midnightblue;}</style>", "midnightblue" };
						return codeToReturn.ToArray();
					}

					else if (fixedArray.Contains("royal") && fixedArray.Contains("blue"))
					{
						string[] codeToReturn = { "<style>body{background-color:royalblue;}</style>", "royalblue" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("blue"))
					{
						string[] codeToReturn = { "<style>body{background-color:blue;}</style>", "blue" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("burlywood") || (fixedArray.Contains("burly") && fixedArray.Contains("wood")))
					{
						string[] codeToReturn = { "<style>body{background-color:burlywood;}</style>", "BurlyWood" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("chartreuse") || (fixedArray.Contains("chart") && fixedArray.Contains("ruse")))
					{
						string[] codeToReturn = { "<style>body{background-color:chartreuse;}</style>", "chartreuse" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("chocolate"))
					{
						string[] codeToReturn = { "<style>body{background-color:chocolate;}</style>", "chocolate" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("coral"))
					{
						string[] codeToReturn = { "<style>body{background-color:coral;}</style>", "coral" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("cornsilk") || (fixedArray.Contains("corn") && fixedArray.Contains("silk")))
					{
						string[] codeToReturn = { "<style>body{background-color:cornsilk;}</style>", "cornsilk" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("crimson"))
					{
						string[] codeToReturn = { "<style>body{background-color:crimson;}</style>", "crimson" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("cyan"))
					{
						string[] codeToReturn = { "<style>body{background-color:cyan;}</style>", "cyan" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("dim") && (fixedArray.Contains("grey") || fixedArray.Contains("gray")))
					{
						string[] codeToReturn = { "<style>body{background-color:dimgrey;}</style>", "dimgrey" };
						return codeToReturn.ToArray();
					}
					else if ((fixedArray.Contains("dodger") || (fixedArray.Contains("dodge") && (fixedArray.Contains("er") || fixedArray.Contains("her")))) && fixedArray.Contains("blue"))
					{
						string[] codeToReturn = { "<style>body{background-color:dodgerblue;}</style>", "dodgerblue" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("firebrick") || (fixedArray.Contains("fire") && fixedArray.Contains("brick")))
					{
						string[] codeToReturn = { "<style>body{background-color:firebrick;}</style>", "firebrick" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("floral") && fixedArray.Contains("white"))
					{
						string[] codeToReturn = { "<style>body{background-color:floralwhite;}</style>", "floralwhite" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("forest") && fixedArray.Contains("green"))
					{
						string[] codeToReturn = { "<style>body{background-color:forestgreen;}</style>", "forestgreen" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("fuchsia"))
					{
						string[] codeToReturn = { "<style>body{background-color:fuchsia;}</style>", "fuchsia" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("gainsboro") || (fixedArray.Contains("gains") && (fixedArray.Contains("boro") || fixedArray.Contains("borough") || fixedArray.Contains("borrow"))))
					{
						string[] codeToReturn = { "<style>body{background-color:gainsboro;}</style>", "gainsboro" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("ghost") && fixedArray.Contains("white"))
					{
						string[] codeToReturn = { "<style>body{background-color:ghostwhite;}</style>", "ghostwhite" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("gold"))
					{
						string[] codeToReturn = { "<style>body{background-color:gold;}</style>", "gold" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("goldenrod") || (fixedArray.Contains("golden") && fixedArray.Contains("rod")))
					{
						string[] codeToReturn = { "<style>body{background-color:goldenrod;}</style>", "goldenrod" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("grey") || fixedArray.Contains("gray"))
					{
						string[] codeToReturn = { "<style>body{background-color:grey;}</style>", "grey" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("green") && fixedArray.Contains("yellow"))
					{
						string[] codeToReturn = { "<style>body{background-color:greenyellow;}</style>", "greenyellow" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("lawn") && fixedArray.Contains("green"))
					{
						string[] codeToReturn = { "<style>body{background-color:lawngreen;}</style>", "lawngreen" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("lime") && fixedArray.Contains("green"))
					{
						string[] codeToReturn = { "<style>body{background-color:limegreen;}</style>", "limegreen" };
						return codeToReturn.ToArray();
					}
					else if ((fixedArray.Contains("sea") || fixedArray.Contains("see")) && fixedArray.Contains("green"))
					{
						string[] codeToReturn = { "<style>body{background-color:seagreen;}</style>", "seagreen" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("green"))
					{
						string[] codeToReturn = { "<style>body{background-color:green;}</style>", "green" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("honeydew") || (fixedArray.Contains("honey") && (fixedArray.Contains("dew") || fixedArray.Contains("do"))))
					{
						string[] codeToReturn = { "<style>body{background-color:honeydew;}</style>", "honeydew" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("hotpink") || (fixedArray.Contains("hot") && fixedArray.Contains("pink")))
					{
						string[] codeToReturn = { "<style>body{background-color:hotpink;}</style>", "hotpink" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("indianred") || (fixedArray.Contains("indian") && (fixedArray.Contains("read") || fixedArray.Contains("red"))))
					{
						string[] codeToReturn = { "<style>body{background-color:indianred;}</style>", "indianred" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("indigo"))
					{
						string[] codeToReturn = { "<style>body{background-color:indigo;}</style>", "indigo" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("ivory"))
					{
						string[] codeToReturn = { "<style>body{background-color:ivory;}</style>", "ivory" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("khaki"))
					{
						string[] codeToReturn = { "<style>body{background-color:khaki;}</style>", "khaki" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("lavenderblush") || (fixedArray.Contains("lavender") && fixedArray.Contains("blush")))
					{
						string[] codeToReturn = { "<style>body{background-color:lavenderblush;}</style>", "lavenderblush" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("lavender"))
					{
						string[] codeToReturn = { "<style>body{background-color:lavender;}</style>", "lavender" };
						return codeToReturn.ToArray();
					}

					else if (fixedArray.Contains("lemon") && fixedArray.Contains("chiffon"))
					{
						string[] codeToReturn = { "<style>body{background-color:lemonchiffon;}</style>", "lemonchiffon" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("light"))
					{
						if(fixedArray.Contains("blue"))
						{
							string[] codeToReturn = { "<style>body{background-color:lightblue;}</style>", "lightblue" };
							return codeToReturn.ToArray();
						}

						else if (fixedArray.Contains("coral"))
						{
							string[] codeToReturn = { "<style>body{background-color:lightcoral;}</style>", "lightcoral" };
							return codeToReturn.ToArray();
						}

						else if (fixedArray.Contains("cyan"))
						{
							string[] codeToReturn = { "<style>body{background-color:lightcyan;}</style>", "lightcyan" };
							return codeToReturn.ToArray();
						}

						else if (fixedArray.Contains("golden") && fixedArray.Contains("rod"))
						{
							string[] codeToReturn = { "<style>body{background-color:lightgoldenrodyellow;}</style>", "lightgoldenrodyellow" };
							return codeToReturn.ToArray();
						}

						else if (fixedArray.Contains("green"))
						{
							string[] codeToReturn = { "<style>body{background-color:lightgreen;}</style>", "lightgreen" };
							return codeToReturn.ToArray();
						}

						else if (fixedArray.Contains("grey") || fixedArray.Contains("gray"))
						{
							string[] codeToReturn = { "<style>body{background-color:lightgrey;}</style>", "lightgrey" };
							return codeToReturn.ToArray();
						}

						else if (fixedArray.Contains("pink"))
						{
							string[] codeToReturn = { "<style>body{background-color:lightpink;}</style>", "lightpink" };
							return codeToReturn.ToArray();
						}

						else if (fixedArray.Contains("salmon"))
						{
							string[] codeToReturn = { "<style>body{background-color:lighsalmon;}</style>", "lighsalmon" };
							return codeToReturn.ToArray();
						}

						else if (fixedArray.Contains("yellow"))
						{
							string[] codeToReturn = { "<style>body{background-color:lightyellow;}</style>", "lightyellow" };
							return codeToReturn.ToArray();
						}
					}
					else if (fixedArray.Contains("lime"))
					{
						string[] codeToReturn = { "<style>body{background-color:lime;}</style>", "lime" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("linen"))
					{
						string[] codeToReturn = { "<style>body{background-color:linen;}</style>", "linen" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("magenta"))
					{
						string[] codeToReturn = { "<style>body{background-color:magenta;}</style>", "magenta" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("maroon"))
					{
						string[] codeToReturn = { "<style>body{background-color:maroon;}</style>", "maroon" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("mint") && fixedArray.Contains("cream"))
					{
						string[] codeToReturn = { "<style>body{background-color:mintcream;}</style>", "mintcream" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("misty") && fixedArray.Contains("rose"))
					{
						string[] codeToReturn = { "<style>body{background-color:mistyrose;}</style>", "mistyrose" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("moccasin"))
					{
						string[] codeToReturn = { "<style>body{background-color:moccasin;}</style>", "moccasin" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("navajo"))
					{
						string[] codeToReturn = { "<style>body{background-color:navajowhite;}</style>", "navajowhite" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("navy"))
					{
						string[] codeToReturn = { "<style>body{background-color:navy;}</style>", "navy" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("old") && fixedArray.Contains("lace"))
					{
						string[] codeToReturn = { "<style>body{background-color:oldlace;}</style>", "oldlace" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("olive"))
					{
						string[] codeToReturn = { "<style>body{background-color:olive;}</style>", "olive" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("red") && fixedArray.Contains("orange"))
					{
						string[] codeToReturn = { "<style>body{background-color:orangered;}</style>", "orangered" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("orange"))
					{
						string[] codeToReturn = { "<style>body{background-color:orange;}</style>", "orange" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("orchid"))
					{
						string[] codeToReturn = { "<style>body{background-color:orchid;}</style>", "orchid" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("peach") && fixedArray.Contains("puff"))
					{
						string[] codeToReturn = { "<style>body{background-color:peachpuff;}</style>", "peachpuff" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("peru"))
					{
						string[] codeToReturn = { "<style>body{background-color:peru;}</style>", "peru" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("plum"))
					{
						string[] codeToReturn = { "<style>body{background-color:plum;}</style>", "plum" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("powderblue") || (fixedArray.Contains("powder") && fixedArray.Contains("blue")))
					{
						string[] codeToReturn = { "<style>body{background-color:powderblue;}</style>", "powderblue" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("purple"))
					{
						string[] codeToReturn = { "<style>body{background-color:purple;}</style>", "purple" };
						return codeToReturn.ToArray();
					}
					else if(fixedArray.Contains("red"))
                    {
                        string[] codeToReturn = { "<style>body{background-color:red;}</style>", "red"};
                        return codeToReturn.ToArray();
                    }
					else if (fixedArray.Contains("rosy") && fixedArray.Contains("brown"))
					{
						string[] codeToReturn = { "<style>body{background-color:rosybrown;}</style>", "rosybrown" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("saddle") && fixedArray.Contains("brown"))
					{
						string[] codeToReturn = { "<style>body{background-color:saddlebrown;}</style>", "saddlebrown" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("salmon"))
					{
						string[] codeToReturn = { "<style>body{background-color:salmon;}</style>", "salmon" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("sandy") && fixedArray.Contains("brown"))
					{
						string[] codeToReturn = { "<style>body{background-color:yellow;}</style>", "yellow" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("sienna"))
                    {
                        string[] codeToReturn = { "<style>body{background-color:sienna;}</style>", "sienna"};
                        return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("silver"))
					{
						string[] codeToReturn = { "<style>body{background-color:silver;}</style>", "silver" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("snow"))
					{
						string[] codeToReturn = { "<style>body{background-color:snow;}</style>", "snow" };
						return codeToReturn.ToArray();
					}

					else if (fixedArray.Contains("tan"))
					{
						string[] codeToReturn = { "<style>body{background-color:tan;}</style>", "tan" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("teal"))
					{
						string[] codeToReturn = { "<style>body{background-color:teal;}</style>", "teal" };
						return codeToReturn.ToArray();
					}

					else if (fixedArray.Contains("thistle"))
					{
						string[] codeToReturn = { "<style>body{background-color:thistle;}</style>", "thistle" };
						return codeToReturn.ToArray();
					}

					else if (fixedArray.Contains("tomato"))
					{
						string[] codeToReturn = { "<style>body{background-color:tomato;}</style>", "tomato" };
						return codeToReturn.ToArray();
					}

					else if (fixedArray.Contains("turquiose"))
					{
						string[] codeToReturn = { "<style>body{background-color:turquiose;}</style>", "turquiose" };
						return codeToReturn.ToArray();
					}

					else if (fixedArray.Contains("violet"))
					{
						string[] codeToReturn = { "<style>body{background-color:violet;}</style>", "violet" };
						return codeToReturn.ToArray();
					}

					else if (fixedArray.Contains("wheat"))
					{
						string[] codeToReturn = { "<style>body{background-color:wheat;}</style>", "wheat" };
						return codeToReturn.ToArray();
					}

					else if (fixedArray.Contains("white"))
					{
						string[] codeToReturn = { "<style>body{background-color:white;}</style>", "white" };
						return codeToReturn.ToArray();
					}

					else if (fixedArray.Contains("yellow"))
					{
						string[] codeToReturn = { "<style>body{background-color:yellow;}</style>", "yellow" };
						return codeToReturn.ToArray();
					}
					else if (fixedArray.Contains("pink"))
					{
						string[] codeToReturn = { "<style>body{background-color:pink;}</style>", "pink" };
						return codeToReturn.ToArray();
					}
					else
                    {
                        string[] codeToReturn = { "BEEP BOOP DOES NOT COMPUTE", "See Visual Studio's Documentation on color names"};
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
				return words;
			}
        }
    }
}
