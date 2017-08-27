﻿using IBM.WatsonDeveloperCloud.SpeechToText.v1;
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
        public Array words;
        public SpeechRecognitionEvent result;

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
            }
            else
            {
                string[] testArray = { "there", "aint", "nothing", "here"};
                words = testArray;
            }
            return words;
        }
    }
}
