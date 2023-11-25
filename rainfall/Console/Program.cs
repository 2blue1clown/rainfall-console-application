// See https://aka.ms/new-console-template for more information
using DataService;

// Dependency Injection
var p = new Processor();
var r = new Reporter(p);

var app = new App(args, p, r);