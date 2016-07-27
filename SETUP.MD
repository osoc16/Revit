# SETUP

## Frontend 

You need to have Node and Node Package Manager (NPM) installed to run this project[Node and NPM](https://docs.npmjs.com/getting-started/installing-node).

Run the following commands to solve package dependencies
```
npm install
```
After which you can surf to the project URL in your local webserver.

The application accesses an api hosted on Azure by default but should you want to host the API yourself, the ASP.Net Web API solution is included under the 'Backend' Folder. More explanation on the API itself can be found in the Backend section.

To change the address the angular application makes calls to, change the `apiBaseUrl` variable value found in <a href="https://github.com/osoc16/Revit/blob/master/Source/Frontend/app/services/revitService.js">here</a>

## Backend
