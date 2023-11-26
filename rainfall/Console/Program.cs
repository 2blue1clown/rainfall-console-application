// See https://aka.ms/new-console-template for more information
using DataService;
using FileService;
using DataService.Models;

// Dependency Injection
var fileReader = new FileReader();
var processor = new RainfallProcessor();
var reporter = new RainfallReporter(processor);

var app = new App<RainfallData, DeviceData, RainfallReport>(args, fileReader, reporter);