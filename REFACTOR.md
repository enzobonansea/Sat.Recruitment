# Refactor

## Methodology

`UsersController.CreateUser` had a lot of code and many responsibilities. I split it into several cohesive objects following SRP (Single responsibility principle) and then these objects collaborated in a decoupled way. All the new code added was implemented using TDD (Test-Driven Development). Furthermore, the code was organized in an hexagonal architecture with the following layers:

- Controllers: here we have the HTTP Adapters
- Application: here we have the use cases
- Domain: here we have the domain model
- Infrastructure: here we have the persistence

Note that Domain is the central layer, meaning that all the others depend on it, allowing a Domain-Driven Design.

## Steps

1. `User` class definition and usage had a lot of things to improve:

- It had public setters. This goes against a Domain-Driven Design, it breaks encapsulation and results into anemic objects.
- It was instantiated using a default constructor and then the properties were set using the public setters. That's bad because of the above bullet and because constructors should return instances both full and valid, and the default constructor does not comply with this.
- That's the reason why I added a constructor that returns a full and valid instance using all the logic of the controller
- By the way, I put the class in another object

2. I detected a performance problem: instead of awaiting tasks, the code used `Task.Result` property; this property blocks the thread until the task had completed. It is preferable to use `await Task` because it produces non-blocking code. The main problem was this line: `var line = reader.ReadLineAsync().Result;` because it blocked the thread until we read all the users from secondary storage (in this case a .txt file but it should be a database too), producing I/O bound code; this means that the upper bound of the performance is the I/O speed, which is much slower than CPU. Using `var line = await reader.ReadLineAsync();` we produce CPU-bound and non-blocking code, resulting in a great performance improvement.

3. `User.IsDuplicated` logic was encapsulated in a new method created with TDD.

4. Controller was also highly coupled with the persistence mechanism. I applied a Repository Pattern for hiding the details of how the data is retrieved from the underlying data source. I used Dependency Injection and I inject an interface in order to apply DIP (Dependency inversion principle).

5. The creation logic (i.e. if a user is duplicated, return an error. Otherwise, create it) was isolated in a use case created with TDD.

6. The validation logic (i.e. name, email, address & phone not null) was moved to a new class called CreateUserRequestValidator created with TDD and whose unique responsibility is asserting parameters correctness.

7. Test cases added to `UserControllerTests` for covering all request/response scenarios.

8. Assertions concerns were added to user's parameters.

9. I made a polymorphic hierarchy of `UserType`.

10. I used a polymorphic method call to `UserType.GetMoney` on `UserType.Money` property getter.

11. I created an `Email` object with all the normalization logic

- The following test assertion

```
email = new Email("some.thing+123@gmail.com");
Assert.Equal("something@gmail.com", email.Normalize());
```

showed me that there was a bug because the old email normalization logic converts 'some.thing+123@gmail.com' into 'something+@gmail.com' because it uses `atIndex` after doing `string.Replace(".", "")` and this method call changes the string length making `atIndex` invalid. Now, the new logic ignores everything after '+' symbol and **then** replaces "." with "".
