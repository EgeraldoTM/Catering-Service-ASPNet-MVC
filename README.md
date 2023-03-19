# Catering-Service-ASPNet-MVC

ASP.Net Core MVC project (.Net 6)

### Scenario

The company offers its employees inhouse catering. Every day a menu is
prepared and displayed. The employees are free to place orders up to 11 o’clock every
morning. Lunch is served no sooner than 13:00, and placing the orders beforehand allows the
cooks to understand how much of each food to prepare.

### Details
This project is built with Clean Architecture patter with the aim to make the project easily 
testable, maintainable and as little dependent on external dependencies as possible.

The Core layer is the main layer which the other projects depend to. It contains mainly the 
domain models, Data Transfer Objects dependent on these models and the interfaces which
define the contracts that different layers communicate with each other without knowing the
the implementation details.
