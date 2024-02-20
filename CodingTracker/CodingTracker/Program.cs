using System;
using System.Configuration;

var name = ConfigurationManager.AppSettings["name"];
var surname = ConfigurationManager.AppSettings["surname"];


Console.WriteLine($"Hi my name is {name} {surname}");