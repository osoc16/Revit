# SETUP

## Frontend 

### Angular
You need to have Node and Node Package Manager (NPM) installed to run this project [Node and NPM](https://docs.npmjs.com/getting-started/installing-node).

Run the following commands to solve package dependencies
```
npm install
```
After which you can surf to the project URL in your local webserver.

The application accesses an api hosted on Azure by default but should you want to host the API yourself, the ASP.Net Web API solution is included under the 'Backend' Folder. More explanation on the API itself can be found in the Backend section.

To change the address the angular application makes calls to, change the `apiBaseUrl` variable value found in <a href="https://github.com/osoc16/Revit/blob/master/Source/Frontend/app/services/revitService.js">here</a>

### SASS
The styling of the application is based on sass. <a href="http://gulpjs.com/">Gulp</a> was used for the compilation of scss files. Install it and run  `gulp` command in the 'Frontend' folder to get the automation task started.

## Backend

1) launch the visual studio project
2) edit the Web.config file to add your <connectionStrings>
3) in the DataContext.cs : change the base name
4) compile the project
5) publish the api to your favorite host
