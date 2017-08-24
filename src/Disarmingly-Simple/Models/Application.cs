using IBM.WatsonDeveloperCloud.SpeechToText.v1;
using IBM.WatsonDeveloperCloud.SpeechToText.v1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Disarmingly_Simple.Models.EnvironmentVariables;

namespace Disarmingly_Simple.Models
{
    public class Application
    {
        public Array words;

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

        public Array GetSpeechRecogEventWithSessionId()
        {
            Stream s = new MemoryStream(File.ReadAllBytes("wwwroot/audio/getYourShitTogether.wav"));
            var result = _speechToText.RecognizeWithSession(this.GetCreateNewSession().SessionId, "audio/wav", s);
            if (result.Results.Count > 0)
            {
                for (int i=0; i < result.Results.Count; i++)
                {
                    result.Results[i].Alternatives[0].Transcript.Split(' ');//fix array splitting and adding to words array. 

                }
            }
            return words;
        }
    }
}
