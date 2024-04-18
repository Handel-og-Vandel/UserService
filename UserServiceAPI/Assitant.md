# Helpful prompts for generating artifacts

## User Schema

```text
generate a json schema using naming conventions defined at schema.org. The schema shall define an object called User and contain the following fields: user identification, users given name, users surname, email, department, login and password.
```

---

## POCO User class

``` text
generate a C# POCO class based on above schema with BSON annotations
```

---

## Repository Interface

``` text
Generate a C# interface class with the essential CRUD operations for the User class above
```

---

## MongoDB implementation

```text
Implement the above interface as a C# class that connects to a mongodb database.
```

---

## MVC Controller

``` text
Generate the HTTP endpoint in a C# MVC controller for accessing the CRUD operations defined in the above interface
```

---