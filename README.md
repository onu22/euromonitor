### **Note**

- sql server runs in a docker container
- API is protected by Azure AD
- To view the token being passed, see request header on browser's network tab.

### Test locally

STEPS:

(1. Clone project

(2. Ensure docker-compose is set as default project, to avoid having to change db connection string. Run the project.

(3. If docker is not available, set "Euromonitor.Api" as the default project. Change db connection string (in appsettings.Development) to local sql server. Run project.

(4. Swagger documentation is on https://localhost:9350/swagger/index.html - change port if not running on docker. To test on swagger, Log in with the accessToken by clicking on "Authorize" button. Enter: Bearer <replace_with_accesstoken> .

(5. Open euromonitor-angular, the Front-End project, in your favorite editor and run "npm install" and "npm start". - If the API is not running on docker, change API_URL (in environment.ts) to the correct url.

(6. To login, enter username: euromonitor@pascal2bhotmail.onmicrosoft.com and password: Mama6001
