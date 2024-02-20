using System;
using System.Configuration;
using CodingTracker;

var name = ConfigurationManager.AppSettings["name"];
var surname = ConfigurationManager.AppSettings["surname"];


Console.WriteLine($"Hi my name is {name} {surname}");