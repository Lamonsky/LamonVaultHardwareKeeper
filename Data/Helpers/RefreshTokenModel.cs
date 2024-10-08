﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Helpers
{
    public class RefreshTokenModel
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        public RefreshTokenModel(string accessToken) {
            RefreshToken = accessToken;
        }
    }
}
