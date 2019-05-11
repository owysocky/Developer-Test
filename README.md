# Junior Web Development Test

## Description 
This is a junior web developement test for Provoke Solutions. It is a simple Azure Functions app. It pings the [Open Weather Map](https://openweathermap.org) API and it is meant to return the data. As you'll see, there are a few problems with its functionality. It's up to you to fix this.

## Problems
1) This application is throwing 401 Unauthorized errors soon after it starts. Please investigate and fix the issue first.
1) The application is currently not storing any of the data when its scheduled tasks run. If you hit the Get endpoint, you'll notice no data being returned. 
1) You may also notice that if you hit the get-data endpoint in the browser, it is returning XML. Ideally, this would return JSON in a browser.
1) We would like to add a new feature. We need a new endpoint that adds to our list of cities so that the next time our scheduler runs, we can get data from multiple cities.
1) The scheduler is running too fast. Please slow it down to run every 5 minutes.

## Setup and Requirements
1) You will need Visual Studio 2017 or Visual Studio 2019 with the Azure Development workload. 
1) You may need to installed the Microsoft Azure Storage Emulator. If it is not included in the Azure Development workload, you can find installation information [here](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator).