﻿using System;
using System.Collections.Generic;

namespace DotNet_Design_Patterns.Chapter4.Adapter
{
    public interface IExternalService
    {
        public string Url { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        void Get(string queryString);
        void Post(object body);
    }
    public class ExternalService : IExternalService
    {
        public string Url { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public void Get(string queryString)
            => Console.WriteLine($"Getting data from: {Url}?{queryString}");
        public void Post(object body)
            => Console.WriteLine($"Posting data to: {Url}");
    }
    public class APIGatewayProxy
    {
        public string BaseUrl { get; set; }
        public void Invoke(string action, object parameters, object body,
                           string verb, Dictionary<string, string> headers)
            => Console.WriteLine($"Invoking {verb} {action} from {BaseUrl}");
    }
    public class ServiceAdapter : IExternalService
    {
        public string Url { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public void Get(string queryString)
        {
            var proxy = new APIGatewayProxy() { BaseUrl = Url[..Url.LastIndexOf("/")] };
            proxy.Invoke(Url[(Url.LastIndexOf("/") + 1)..], queryString, null, "GET", Headers);
        }
        public void Post(object body)
        {
            var proxy = new APIGatewayProxy() { BaseUrl = Url[..Url.LastIndexOf("/")] };
            proxy.Invoke(Url[(Url.LastIndexOf("/") + 1)..], null, body, "POST", Headers);
        }
    }

    /*
     * Pluggable Adapter
        public class Adaptee
        {
            public void Print(string message) => Console.WriteLine(message);
        }
        public class Target
        {
            public void Show(string input) => Console.WriteLine(input);
        }
        public class Adapter : Adaptee
        {
            public Action<string> Request;
            public Adapter(Adaptee adaptee) => Request = adaptee.Print;
            public Adapter(Target target) => Request = target.Show;
        }
    */

}
