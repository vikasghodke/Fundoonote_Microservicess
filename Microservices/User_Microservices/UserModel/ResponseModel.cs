﻿namespace User_Microservices.UserModel
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; } = "";

        public T? Data { get; set; }
    }
}
