WebApi2Book
===========

This repository contains the example source code that accompanies *ASP.NET Web API 2: Building a REST Service from Start to Finish* (ISBN-13: 978-1484201107).

The book is available from Amazon: [http://www.amazon.com/gp/product/1484201108](http://www.amazon.com/gp/product/1484201108 "ASP.NET Web API 2 on Amazon").

The repository contains chapter-specific branches so that you can follow along with the implementation described in the book. The "completed" branch contains the finished solution, including the ASMX-based service and client discussed in Chapter 8.

## Demo: Using the Fiddler Files ##
Fiddler session files are available in the doc directory. Please be sure to modify the port number in the request messages to match that of your own Web API.

## Demo: Using Web API to Process SOAP Messages ##
1. Make sure the following sites are running in iisexpress: `WebApi2Book.Web.Api` and `WebApi2Book.Web.Legacy.Api`.
2. Start the `WebApi2Book.Windows.Legacy.Client` application.
3. Use the application to invoke methods against the legacy SOAP-based service and against the Web.API REST service.

If you examine the client application code you'll notice that the same proxy class is being used for both services (see the `MainWindow.GetServiceClient` method). The only difference is the endpoint being used for the particular service.
