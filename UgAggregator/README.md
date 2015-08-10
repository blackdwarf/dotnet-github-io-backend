# UgAggregator backend service

## What is this?

This service aggregates near future .NET events from two main sources:
1. [Community Megaphone](http://communitymegaphone.com/)
2. [Meetup](http://www.meetup.com/)

The events are then shown on the main [dotnet.github.io site](http://dotnet.github.io/).

## What is it built on?

It is a simple solution using primarily ASP.NET 5 for the REST APIs. It is also 
configured to run on top of [CoreCLR](https://www.github.com/dotnet/coreclr). 
You can run it on other supported .NET implementations by adding the appropriate 
lines in the *project.json* file. 

It has been developed on Windows using [Visual Studio 2015](http://www.visualstudio.com/). 
In the future

## How do I run it?

In order to run this service, you need several pieces of tech:
1. CoreCLR
2. Supported web server
3. Meetup API access

**TODO: write a more detailed set of instructions here.**

## How do I contribute?

At this time, pull requests are not being accepted, since this is still a 
mostly in-development project and because it might some day be moved to a much 
better repo (repo that makes more sense). 
