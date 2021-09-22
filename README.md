# SchoolLogin - SDK 3.1.12

Code		without <>
===========
dotnet new webapi -n <filename>		//make a webapi
code -r <filename>					//open it on vs code


program.cs / program class
===
- application entry point

Startup.cs
===
- configure
- important
  setup request pipeline
  middleware
- ConfigureSevices
 dependency injection
 - AddSingleton	- same for every request
 - AddScoped	- created once per client request
 - Transient	- new instance created every time

Appsetting.json
===
- additional config
- connection string

Launch setting.json
===
- profile when start app, start on the url
- "aspnetcore_environment" : "env" settingup environment

Package for connecting webapi to sql
===
- microsoft.entityframeworkcore
- microsoft.entityframeworkcore.design
- microsoft.entityframeworkcore.sqlserver

Install ef entityframework
===
- dotnet tool install --global dotnet-ef
- migrations = instruction give to db to create db schema
- up -> update stuff
- down-> redo/rollback
- dotnet ef database update

For mapping automaticly
===
- automapper.extensions.microsoft.dependencyinjection

For patch
===
- Microsoft.AspNetCore.JsonPatch
- Microsoft.AspNetCore.Mvc.NewtonsoftJson

Test with xunit
===
make a folder
SimpleAPi -> SimpleApiSln, src, test
SimpleApi/src -> SimpleApi
SimpleApi/src/SimpleApi -> (your api)
SimpleApi/test -> use "dotnet new xunit -n simpleapi.test"
SimpleAPi -> make sln "dotnet new sln --name simpleapiSln"
SimpleAPi -> use "dotnet sln .\SchoolLoginSln.sln add .\src\SchoolLogin\ .\test\SchoolLogin.Test\" to connecting
simpleapi -> dotnet add .\test\SchoolLogin.Test\SchoolLogin.Test.csproj reference .\src\SchoolLogin\SchoolLogin.csp add reference on test




Patch Syntax
===
[
	{
		"op":"replace",
		"path":"/howto",
		"value":"something new"
	}
]

[
  {
    "op": "add",		//operation
    "path": "/customerName",	//which element
    "value": "Barry"		//value
  },
  {
    "op": "add",
    "path": "/orders/-",
    "value": {
      "orderName": "Order2",
      "orderType": null
    }
  }
]


Patch Operation
===
- add	Add a property or array element. For existing property: set value.
- remove	Remove a property or array element.
- replace	Same as remove followed by add at same location.
- move	Same as remove from source followed by add to destination using value from source.
- copy	Same as add to destination using value from source.
- test	Return success status code if value at path = provided value.


Null Handling
===
arguementnullexception -> repo			//null handling			

Status code					
===
- (1 _ _) information responses
- (2 _ _) Successful responses
- (3 _ _) Redirection messages
- (4 _ _) Client Error Responses
- (5 _ _) Server Error Responses
- (200 ok) The request has succeeded
- (204 no Content) request succeeded and dont want to return body/content-length {only header}
- (201 Created) POST succeeded

Profile
===
profile -> mapping			//API mappings to send traffic to your APIs through your custom domain name.

Appsetting vs Appsetting.development vs Appsetting.production
===
appsetting vs appsetting.development vs appsetting.production //syntax {appsetting.environment.json} environment to switch between

bin and obj
===
bin		//contain compiled assemblies(.dll files) for custom ASP.NET controls
obj		// store temporary object files and other files used in order to create the final binary during the compilation process.
program.cs //create a host for the web application
startup.cs //Services required by the app are configured.
configure //handle environment
configureservices //inject mapping , constructor injection

