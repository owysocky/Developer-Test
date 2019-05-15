# Junior Web Development Test

## Description 
This is a junior web developement test for Provoke Solutions. It is a simple Azure Functions app consisting of a timer function that pulls data from the [OpenWeatherMap API](https://openweathermap.org) and stores it, and two http trigger functions that surface that data. In this development version of the app, the captured data is stored locally in the emulated version of Azure Table Storage. 

## Setup and Requirements
1) You need Visual Studio 2017 or Visual Studio 2019 with the Azure Development workload to run the app in development mode.  
   * [Visual Studio Community Edition download](https://visualstudio.microsoft.com/free-developer-offers/)
   * [Adding workloads to Visual Studio](https://github.com/MicrosoftDocs/visualstudio-docs/blob/master/docs/install/modify-visual-studio.md)
1) Microsoft Azure Storage Emulator to run the local storage configuration. If it does not come included with the Azure Development workload, you can find installation information [here](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator).
1) Not required, but the [Microsoft Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/) will allow you to view and manipulate values stored on your local machine. You shouldn't need to do this for the purpose of this exercise, but the tools are there.
   * The app's storage will be at Local and Attached > Storage Accounts > Tables > WeatherTable

To setup the project, clone it down into a local directory, and open the .sln file with Visual Studio. That should be all the setup you need.

## Running the app

The app should be runnable immediately from within Visual Studio, either f5 to run or the "run" button.

As you'll see, there are a few problems with its functionality. It's up to you to fix this.

## Problems
1) This application is throwing 401 Unauthorized errors soon after it starts. Please investigate and fix the issue first.
1) The application is currently not storing any of the data when its scheduled tasks run. If you hit the Get endpoint, you'll notice no data being returned. 
1) You may also notice that if you hit the get-data endpoint in the browser, it is returning XML. Ideally, this would return JSON in a browser.
1) The scheduler is running too fast. Please slow it down to run every 5 minutes (note: you may speed this back up during you development, just ensure that it is set at 5 minutes when you submit your final code).

Once you've fixed these immediate issues, the app should:
* Get data from the open weather API every 5 minutes and store it locally
* Return all or parts of that data on the two exposed endpoints, /api/get-data and /api/get-most-recent

## Additional Features
1) We would like to add a new feature to the API layer: we need a new endpoint that adds to our list of cities so that the next time our scheduler runs, we can get data from multiple cities.
    * Should be an endpoint that accepts a city name in the form of a string and adds it to the list of cities that the app checks
1) The app needs a front-end layer. The front end doesn't have to look amazing, but it should be clean and display the following functionality:
    * A section containing an input and a button that allows users to type in a city name and then on button click add it to the list of cities that the backend retrieves data for.
    * A display section showing the most recent weather data for each city stored in the app's database (i.e. the results of the get-most-recent endpoint). This section should update itself every couple of minutes to stay current with the backend's data checks
      * Should display at a minimum: the location name (city name), the main weather name and description, the temperature, and an image indicative of the weather
    * An expandable archives section that displays all the data collected by the app
      * Each city should have its own sub-section
      * City sub-sections should be ordered alphabetically
      * Weather data should be ordered by the time of measurement, with more recent data appearing nearer the top
      * Full section should not always be visible and should be able to be hidden/shown through UI

Again, this front end can be whatever you feel like making: plain JS and HTML, React, Angular, whatever.

## Submitting your work
To submit your work, please fork this repository first and make your changes in your own repository. You can then open a pull request for changes for us to review.

Good luck!
