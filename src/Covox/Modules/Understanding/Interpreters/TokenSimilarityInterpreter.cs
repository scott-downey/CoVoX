﻿using System.Linq;

namespace Covox.Understanding
{
    internal class TokenSimilarityInterpreter : IInterpreter
    {
        public double CalculateMatchScore(string target, string input)
        {
            var targetTokens = target.Split(' ').ToList();
            var inputTokens = targetTokens.Where(token => input.ToLower().Contains(token)).ToList();

            var percentageAmountOfKeywords = inputTokens.Count / targetTokens.Count;

            return percentageAmountOfKeywords;
        }
    }
}
