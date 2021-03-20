using System;
using System.Collections.Generic;

namespace ScratchPad.JsonApi
{
    public class JsonApiException : Exception
    {
        public enum StatusCodes
        {
            BadRequest = 400,
            NotFound = 404,
            InternalServerError = 500
        }

        public readonly List<string> Errors;

        public readonly StatusCodes StatusCode;

        public JsonApiException(List<string> errors, StatusCodes statusCode = StatusCodes.InternalServerError)
        {
            Errors = errors;
            StatusCode = statusCode;
        }

        public JsonApiException(string error, StatusCodes statusCode = StatusCodes.InternalServerError)
        {
            Errors = new List<string> { error };
            StatusCode = statusCode;
        }
    }
}