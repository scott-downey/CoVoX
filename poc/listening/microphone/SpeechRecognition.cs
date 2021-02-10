﻿using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace SpeechToTextFromMic
{
    public class SpeechRecognition
    {
        public static async Task<(string Text, string DetectedLanguage)> RecognizeSpeech(string subscriptionKey, string region)
        {
            var config = SpeechConfig.FromSubscription(subscriptionKey, region);
            var autoDetectSourceLanguageConfig = AutoDetectSourceLanguageConfig.FromOpenRange();
            var recognizer = new SpeechRecognizer(config, autoDetectSourceLanguageConfig);
            var result = await recognizer.RecognizeOnceAsync();

            if (result.Reason == ResultReason.Canceled)
                throw new Exception("Api Communication Failure");

            var detectedLanguage = AutoDetectSourceLanguageResult.FromResult(result).Language;
            return (result.Text, detectedLanguage);
        }

        public static async Task<(string Text, string DetectedLanguage)> RecognizeSpeech(string subscriptionKey, string region, AutoDetectSourceLanguageConfig autoDetectSourceLanguageConfig)
        {
            var config = SpeechConfig.FromSubscription(subscriptionKey, region);
            var recognizer = new SpeechRecognizer(config, autoDetectSourceLanguageConfig);
            var result = await recognizer.RecognizeOnceAsync();

            if (result.Reason == ResultReason.Canceled)
                throw new Exception("Api Communication Failure");

            var detectedLanguage = AutoDetectSourceLanguageResult.FromResult(result).Language;
            return (result.Text, detectedLanguage);
        }

        public static async Task RecognizeSpeechContinuous(string subscriptionKey, string region, AutoDetectSourceLanguageConfig autoDetectSourceLanguageConfig)
        {
            var config = SpeechConfig.FromSubscription(subscriptionKey, region);

            var recognizer = new SpeechRecognizer(config, autoDetectSourceLanguageConfig);
            
            await recognizer.StartContinuousRecognitionAsync();
            
            recognizer.Recognized += (s, e) =>
            {
                if (e.Result.Reason == ResultReason.RecognizedSpeech)
                {
                    var textResult = e.Result.Text;

                    var autoDetectSourceLanguageResult = AutoDetectSourceLanguageResult.FromResult(e.Result);

                    var fromLanguage = autoDetectSourceLanguageResult.Language;

                    Console.WriteLine($"Recognized '{fromLanguage}': {textResult}");
                }
            };

            recognizer.Canceled += (s, e) => { Console.WriteLine("Recognizing cancelled"); };

            recognizer.Recognizing += (s, e) =>
            {
                if (e.Result.Reason == ResultReason.RecognizingSpeech) Console.WriteLine("Recognizing...");
            };

            recognizer.SpeechStartDetected += (s, e) => { Console.WriteLine("SpeechStartDetected"); };

            recognizer.SpeechEndDetected += (s, e) => { Console.WriteLine("SpeechEndDetected"); };
        }
    }
}
