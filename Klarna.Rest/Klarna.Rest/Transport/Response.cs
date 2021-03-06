﻿#region Copyright Header
//-----------------------------------------------------------------------
// <copyright file="Response.cs" company="Klarna AB">
//     Copyright 2014 Klarna AB
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------
#endregion
namespace Klarna.Rest.Transport
{
    using System;
    using System.Net;
    using Newtonsoft.Json;

    /// <summary>
    /// HTTP response data class.
    /// </summary>
    internal class Response : IResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Response" /> class.
        /// </summary>
        /// <param name="status">the response status</param>
        /// <param name="headers">the response headers</param>
        /// <param name="payload">the response data</param>
        public Response(HttpStatusCode status, WebHeaderCollection headers, string payload)
        {
            this.Status = status;
            this.Headers = headers;
            this.Payload = payload;
        }

        /// <summary>
        /// Gets the response status.
        /// </summary>
        public HttpStatusCode Status { get; private set; }

        /// <summary>
        /// Gets the response headers.
        /// </summary>
        public WebHeaderCollection Headers { get; private set; }

        /// <summary>
        /// Gets the raw response payload.
        /// </summary>
        public string Payload { get; private set; }

        /// <summary>
        /// Gets the JSON data.
        /// </summary>
        /// <typeparam name="T">generic data object</typeparam>
        /// <returns>the generic data object</returns>
        public T Data<T>()
        {
            return JsonConvert.DeserializeObject<T>(this.Payload);
        }
    }
}
