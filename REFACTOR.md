# Refactor

## Methodology

All the new code will be implemented using TDD.

## Steps

1. `UsersController.CreateUser` has a lot of code and many responsibilities, I will split it into several cohesive objects following SRP and then these objects will collaborate in a decoupled way.
