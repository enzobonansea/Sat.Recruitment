# Refactor

## Methodology

`UsersController.CreateUser` has a lot of code and many responsibilities, I will split it into several cohesive objects following SRP and then these objects will collaborate in a decoupled way. All the new code will be implemented using TDD.

## Steps

1. `User` class definition and usage have a lot of things to improve:

- It has public setters. This goes against a Domain-Driven Design, it breaks encapsulation and results into anemic objects.
- It is instantiated using a default constructor and then the properties are set using the public setters. That's bad because of the above bullet and because constructors should return instances both full and valid, and the default constructor does not comply with this.
- That's the reason why I added a constructor that returns a full and valid instance using all the logic of the controller
- By the way, I puted the class in another object
