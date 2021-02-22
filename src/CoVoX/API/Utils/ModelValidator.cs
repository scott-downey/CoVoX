﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Utils
{
    public static class ModelValidator
    {
        public static List<ValidationResult> ValidateModel(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, ctx, results, true);

            return results;
        }
    }
}
