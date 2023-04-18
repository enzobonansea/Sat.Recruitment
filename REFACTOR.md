# Refactor

## Methodology

`UsersController.CreateUser` had a lot of code and many responsibilities. I split it into several cohesive objects following SRP (Single responsibility principle) and then these objects collaborated in a decoupled way. All the new code added was implemented using TDD (Test-Driven Development). Furthermore, the code was organized in an hexagonal architecture with the following layers:

- Controllers: here we have the HTTP Adapters
- Application: here we have the use cases
- Domain: here we have the domain model
- Infrastructure: here we have the persistence

Note that Domain is the central layer, meaning that all the others depend on it, allowing a Domain-Driven Design.

## Non-breaking changes

The following changes are non-breaking, meaning that will not affect API's clients.

1. `User` class definition and usage had a lot of things to improve:

- It had public setters. This goes against a Domain-Driven Design, it breaks encapsulation and results into anemic objects.
- It was instantiated using a default constructor and then the properties were set using the public setters. That's bad because of the above bullet and because constructors should return instances both full and valid, and the default constructor does not comply with this.
- That's the reason why I added a constructor that returns a full and valid instance using all the logic of the controller
- By the way, I put the class in another object

2. I detected a performance problem: instead of awaiting tasks, the code used `Task.Result` property; this property blocks the thread until the task had completed. It is preferable to use `await Task` because it produces non-blocking code. The main problem was this line: `var line = reader.ReadLineAsync().Result;` because it blocked the thread until we read all the users from secondary storage (in this case a .txt file but it should be a database too), producing I/O bound code; this means that the upper bound of the performance is the I/O speed, which is much slower than CPU. Using `var line = await reader.ReadLineAsync();` we produce CPU-bound and non-blocking code, resulting in a great performance improvement.

3. `User.IsDuplicated` logic was encapsulated in a new method created with TDD.

4. Controller was also highly coupled with the persistence mechanism. I applied a Repository Pattern for hiding the details of how the data is retrieved from the underlying data source. I used Dependency Injection and I inject an interface in order to apply DIP (Dependency inversion principle).

5. The creation logic (i.e. if a user is duplicated, return an error. Otherwise, create it) was isolated in a use case created with TDD.

6. The validation logic (i.e. name, email, address & phone not null) was moved to a new class called UserValidator created with TDD and whose unique responsibility is asserting parameters correctness.

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

## Breaking changes

There are some breaking changes, meaning that our clients need to change their integration with the API. The changes are necessary because the API does not comply with REST because `/create-user` does not provide a meaningful response status code: it always returns a '200 OK' no matter what happened during the creation, and the right way is returning '201 Created' if the user is created or '400 Bad Request' if there was a problem with the user input data.
Since the system is in production, we cannot introduce breaking changes without coordinating with our clients. That's why I used feature flags to introduce meaningful status codes, a breaking change.
Feature flags are a powerful tool that allows decoupling deployment and release since we can deploy the newest code without activating it, so only when the feature flag is activated is the new code work. Until then, the old code is running. Another powerful feature of feature flags (although not used here) is to provide features to only some specific users.
The idea is: keep the old code and reference to it with a feature flag in off, develop the new feature (in this case improve API responses) and reference to it with the feature flag in on, deploy changes, coordinate with clients the breaking change and when all clients are ready, turn on the feature flag. Then, delete the old code and the feature flag keeping only the new code without the feature flag since it is unnecessary.
For simplicity I will store my feature flag in the `app.settings.json` file but in a real scenario I would use a third-party feature management system like 'split.io'.
In this case, after all our clients change their integration with the API we can delete all the code under the "off" branch of the feature flag, the class `Result` because it will be no longer used, the `LegacyUsersControllerTest` and the feature flag itself.
