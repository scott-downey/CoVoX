﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Covox.Attributes;
using Microsoft.Extensions.Configuration;

namespace Covox
{
    public class Configuration
    {
        [Required]
        public AzureConfiguration AzureConfiguration { get; set; }
        
        [Range(0, 1)]
        public double MatchingThreshold { get; set; } = 0.95;

        [NotEmpty, MustHaveValidLanguages]
        public IReadOnlyList<string> InputLanguages { get; set; }

        public IReadOnlyList<string> HotWords { get; set; }
    }

    public class AzureConfiguration
    {
        [NotEmpty]
        public string SubscriptionKey { get; private init; }

        [NotEmpty]
        public string Region { get; private init; }

        private AzureConfiguration()
        {
        }

        /// <summary>
        /// Creates AzureConfiguration from parameters.
        /// </summary>
        /// <param name="subscriptionKey">Azure Cognitive Services Speech subscription key.</param>
        /// <param name="region">Azure Cognitive Services Speech region.</param>
        public static AzureConfiguration FromSubscription(string subscriptionKey, string region)
        {
            return new()
            {
                SubscriptionKey = subscriptionKey,
                Region = region
            };
        }

        /// <summary>
        /// Reads AzureConfiguration from secrets.json file.
        /// </summary>
        /// <param name="secretsPath">Full path off the secrets.json file.</param>
        /// <returns>AzureConfiguration with values read from secrets.json located in path.</returns>
        public static AzureConfiguration FromFile(string secretsPath = "secrets.json")
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(secretsPath)
                .Build();

            var section = config.GetSection(nameof(AzureConfiguration));
            
            var children = section.GetChildren().ToArray();
            
            var subscriptionKey = children.FirstOrDefault(x => x.Key == "SubscriptionKey")?.Value;
            var region = children.FirstOrDefault(x => x.Key == "Region")?.Value;

            return new AzureConfiguration()
            {
                SubscriptionKey = subscriptionKey,
                Region = region
            };
        }

        /// <summary>
        /// Reads AzureConfiguration from COVOX_AZURE_CONFIG environment variable.
        /// Default: "COVOX_AZURE_CONFIG" variable with values split by ':'
        /// </summary>
        /// <param name="variableName">Environment variable name</param>
        /// <param name="separator">Value separator character</param>
        /// <returns>AzureConfiguration with values read from environment variable.</returns>
        public static AzureConfiguration FromEnvironmentVariable(
            string variableName = "COVOX_AZURE_CONFIG",
            char separator = ':')
        {
            var config = Environment.GetEnvironmentVariable(variableName);

            if (string.IsNullOrEmpty(config) || !config.Contains(separator)) 
                return null;
            
            var tokens = config.Split(separator);
            var subscriptionKey = tokens.FirstOrDefault();
            var region = tokens.LastOrDefault();
                
            return new AzureConfiguration()
            {
                SubscriptionKey = subscriptionKey,
                Region = region
            };
        }
    }
}
