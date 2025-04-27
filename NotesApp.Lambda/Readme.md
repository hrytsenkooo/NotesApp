# NotesApp Lambda Function

This directory contains the **AWS Lambda** function for the **NotesApp** project. The Lambda function is responsible for handling the business logic of the application, specifically CRUD operations for users and notes. It interacts with the **Amazon DynamoDB** tables to store and retrieve data, and it is integrated with **AWS AppSync** for GraphQL operations.

## Overview

The Lambda function in this repository is written in **.NET 8** and handles the following operations:

- **Users CRUD operations**: create, read, update, delete operations for managing user data.
- **Notes CRUD operations**: create, read, update, delete operations for managing note data.
- **GraphQL resolvers**: the Lambda function acts as the data source for GraphQL queries and mutations in AWS AppSync.

The function is triggered by AWS AppSync to process GraphQL requests, execute the necessary logic (e.g., interacting with DynamoDB), and return the results.

## Architecture

The Lambda function interacts with two **DynamoDB** tables:

1. **Users table**:
   - Stores information about users.
   - Has global secondary indexes on `Email` and `Username` for querying.

2. **Notes table**:
   - Stores notes related to users.
   - Has a global secondary index on `UserId` to query notes by user.

The Lambda function handles the following types of GraphQL queries and mutations:

### Queries:
- `getUserById`: fetches a user by their `Id`.
- `getAllUsers`: fetches all users.
- `getNoteById`: fetches a note by its `Id`.
- `getAllNotes`: fetches all notes.
- `getNotesByUserId`: fetches all notes belonging to a specific user.

### Mutations:
- `createUser`: creates a new user.
- `updateUser`: updates an existing user.
- `deleteUser`: deletes a user.
- `createNote`: creates a new note.
- `updateNote`: updates an existing note.
- `deleteNote`: deletes a note.


