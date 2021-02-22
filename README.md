# The Objective
The goal of the task is to develop a web service (ASP.NET Web API 5.2.x/ASP.NET Core 2.1+ based application) that provides API for searching
a number of closest airports for every pair of cities in a list.

Please use the following endpoint https://homework.appulate.dev/api/Airport/search to get information about city airports.
More information is available at https://homework.appulate.dev/swagger/index.html.

The application you create should accept a list of cities' names.
Every city could have zero or more airports.
The result should be the list of all possible cities pairs. Every city pair must have cities names, airport names and distance.
