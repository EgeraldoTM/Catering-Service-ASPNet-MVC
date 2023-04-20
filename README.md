# Catering-Service-ASPNet-MVC

ASP.Net Core MVC project (.Net 6)

### Scenario

The company offers its employees inhouse catering. Every day a menu is
prepared and displayed. The employees are free to place orders up to 11 o’clock every
morning. Lunch is served no sooner than 13:00, and placing the orders beforehand allows the
cooks to understand how much of each food to prepare.

### Details

* The Core layer is the main layer which the other projects depend to. It contains mainly the 
domain models, Data Transfer Objects for these models and the interfaces which
define the contracts that different layers communicate with each other without knowing the
the implementation details. The Core define the business logic.

* The Infrastructure layer contains data access and persistence logic. This layer is 
responsible for communicating with external systems like the database.
In this project, there are classes that implement the repository pattern, which 
provides an abstraction layer over data access to make the code more testable and 
maintainable.

* The Web layer is built on top of the Core and Infrastructure projects. It contains
the Controllers, Views, ViewModels, static javasScript and Css files, configuration
files. The Program.cs is also located in this project

By separating concerns into different projects, this solution follows the principles
of Clean Architecture and promotes separation of concerns and modularity.
