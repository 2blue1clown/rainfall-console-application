# rainfall-console-application
A .NET console application that takes in data from a folder then determines some averages and an average trend.
The breif for this project is below. It was given to me as a coding interview in which I did a terrible job. This repo is a better version where I try to fix my earlier mistakes.
While improving my solution I explored using:
- mulitple projects in a solution
- LINQ
- File IO

## The Problem: Fuzion Inc. Manages a flood detection programme. 
They have devices in the field that take rainfall readings which are sent to main office via ftp as a .csv file.
They would like a simple console UI that reads in the rainfall readings from a folder and shows the average rainfall over the last 4 hours for each device, whether it is green, amber or red, and whether the average rainfall trend is increasing or decreasing. The thresholds being:
Green: average rainfall for last 4 hrs < 10mm
Amber: average rainfall for last 4 hrs < 15mm
Red: average rainfall for last 4 hrs >= 15mm or any reading in the last 4 hrs > 30mm
### Data:
You are provided with:  The list of devices (csv)  The last 2 sets of data files received (csv)
For the purpose of development & testing assume that the last timestamp across all data files is the current time, so if the last time in all data files is 3pm, assume the current time is 3pm

### Solution requirements
The solution should be written in C# although if you only have java / javascript skills we’ll take either of these. The
solution should use no external libraries. There is one exception - you may use your choice of CSV reader. There are a few available. Our suggestion is https://joshclose.github.io/CsvHelper/. It should have a console front end. It is your choice how simple or complex you make it including if and how you
choose to store data.
If you have time you are welcome to provide additional features however we are looking for a solution that correctly solves the problem above and shows evidence of good coding practices e.g. unit testing, SOLID1
, clean code and
design over complexity of what the application can do.

## Feedback
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
