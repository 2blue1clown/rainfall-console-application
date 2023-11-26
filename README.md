# rainfall-console-application
A .NET console application that takes in data from a folder then determines some averages and an average trend.
The brief for this project is below. It was given to me as a coding interview in which I did a terrible job. This repo is a better version where I try to fix my [earlier mistakes](#first-attempt-feedback).
While improving my solution I explored using:
- mulitple .NET projects in a solution
- .NET Console Applications
- LINQ
- File IO
- [SOLID programming principles](#how-i-used-solid-principles)

## The Problem: Fuzion Inc. Manages a flood detection programme. 
They have devices in the field that take rainfall readings which are sent to main office via ftp as a .csv file.
They would like a simple console UI that reads in the rainfall readings from a folder and shows the average rainfall over the last 4 hours for each device, whether it is green, amber or red, and whether the average rainfall trend is increasing or decreasing. The thresholds being:
- Green: average rainfall for last 4 hrs < 10mm
- Amber: average rainfall for last 4 hrs < 15mm
- Red: average rainfall for last 4 hrs >= 15mm or any reading in the last 4 hrs > 30mm
### Data:
You are provided with: 
1. The list of devices (csv)
2. The last 2 sets of data files received (csv)
For the purpose of development & testing assume that the last timestamp across all data files is the current time, so if the last time in all data files is 3pm, assume the current time is 3pm

### Solution requirements
The solution should be written in C# although if you only have java / javascript skills we’ll take either of these. The
solution should use no external libraries. There is one exception - you may use your choice of CSV reader. There are a few available. Our suggestion is https://joshclose.github.io/CsvHelper/. It should have a console front end. It is your choice how simple or complex you make it including if and how you choose to store data.
If you have time you are welcome to provide additional features however we are looking for a solution that correctly solves the problem above and shows evidence of good coding practices e.g. unit testing, SOLID, clean code and design over complexity of what the application can do.

## First Attempt Feedback
Here is the feedback I recieved for my abysmal first attempt. I have tried to fix this in this attempt.

- There's no real structure
- Very limited use of classes
- Only 1 function defined, and is not actually used as he ran out of time.
- A lack of consistency with coding style - makes it harder to read then it should be considering how simple it is.
- Also breaks a lot of standard coding conventions, which can be trained out.
- No error checking - the slightest bit of invalid input makes it crash.
- Logic bug - instead of calculating average rainfall, calc's rainfall per hour and called it an average.
- No tests
- Had a small attempt at the trend, but didn't seem to get very far.
- Output is ok, a little bare bones.
- Technical knowledge seems ok, but could do with some training.

## Second Attempt Feedback (looking at commit f9e0061)
I showed a friend of mine my code to get the following feedback.
- still does not use many of the SOLID programming principles.
- the solution over does it with the number of projects... this increases the build times and is just a bit unnessercy. I should just use classes to seperate concerns.
- don't make properties public just so that I can test them. This is a SIN! Instead make another class where that function/property is public and give that to the class where it was originally private. This has the advantage of forcing loosly coupled code.
- (minor) in my core service I am losing the advantage of IEnumerables by putting putting the entire enumerable in a dictionary. Instead I should make another enumerable that has the properties that I want. That way if someone just wants to get the first few elements then they can without having to load the entire thing.
## How I used SOLID principles

### Single Responsibility Principle
Each class shoule do one thing and one thing only. 
#### Implementaion
- FileService is responsible for reading data in from files.
- Console is responsible for recieving inputs and giving the outputs.
- App is responsible for passing data around the services.
- ReportService is responsible for generating a Report data for each device
- DataService responsible for processing the data
- Program is responsible for the dependancy injection

### Open-Closed Principle
Classes should be open to extension but closed to modification. We should be able to add new functionality without having to change any of the existing classes.
#### Implementation
I'm not gonna lie this one was pretty hard for me. I tried to think about scenarios where the requirements might change... and if I would have to change existing classes.

1. Maybe the business wants to also show reports from the data processed differently but with the same report
  - I would have to implement some kind of system to handle processing multiple ways at the same time... but I wouldnt have to change the current processor.
  - I would implement a new class that implements the IProcessor interface. Add this to the multiple processor system.

2. Maybe the business wanted to process a different type of data but have the same report:
  - I would need to implement a new model, processor, and reporter and report model but none of what currently exists would have to change.

3. Maybe the business wants to add more aspects to the report, ie the median and the mode rainfall.
- I would have to change the existing processor and report files.


### Liskov Substitution Principle
TODO
### Interface Segregation Principle
TOOD
### Dependency Inversion Principle
TOOD
