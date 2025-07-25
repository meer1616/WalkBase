﻿using System.Text.Json;

namespace Authentication.Contracts
{
    public class Error
    {
        public string? StatusCode { get; set; }
        public string? Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
