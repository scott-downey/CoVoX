﻿using System;
using System.Text.Json;
using System.Threading.Tasks;
using CustomCommands.Configuration;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Dialog;

namespace CustomCommands
{
    class CustomCommandsClient
    {
        public DialogServiceConnector Recognizer;
        
        public CustomCommandsClient(
            CustomCommandClientConfiguration configuration, 
            AudioConfig audioConfig)
        {
            CreateRecognizer(configuration, audioConfig);
        }

        private void CreateRecognizer(CustomCommandClientConfiguration configuration, AudioConfig audioConfig)
        {
            var customCommandsConfig = CustomCommandsConfig.FromSubscription(
                configuration.AppId, 
                configuration.SubscriptionKey, 
                configuration.Region);
            
            Recognizer = new DialogServiceConnector(customCommandsConfig, audioConfig);
        }

        public async Task Connect()
        {
            Console.WriteLine("connecting..");
            
            await Recognizer.ConnectAsync();
        }
        
        public async Task StartListenForSpeech()
        {
            Console.WriteLine("Now listening for commands: ");
            
            await Recognizer.ListenOnceAsync();
        }
        
        public async Task StartListenForInputs()
        {
            Console.WriteLine("Now listening for input: ");
            
            await Recognizer.ListenOnceAsync();
        }

        public async Task SendActivity(string sentence)
        {
            var activity = new { Text = sentence, Type = "message" };
            var activitySerialized = JsonSerializer.Serialize(activity);
            await Recognizer.SendActivityAsync(activitySerialized);
        }
    }
}
