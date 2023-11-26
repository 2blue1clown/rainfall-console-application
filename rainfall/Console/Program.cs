// See https://aka.ms/new-console-template for more information
using DataService;
using FileService;
using DataService.Models;

// Dependency Injection
var f = new FileReader();
var p = new RainfallProcessor();
var r = new RainfallReporter(p);

var app = new App<RainfallData, DeviceData, RainfallReport>(args, f, r);