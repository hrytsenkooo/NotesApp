# NotesApp Backend (AWS Lambda & .NET)

This repository contains the backend for the NotesApp project, implemented using **AWS Lambda**, **AWS AppSync** (GraphQL API), and **Amazon DynamoDB** as the database. The backend is built using **.NET 8** and is designed to handle CRUD operations for users and notes. It provides a GraphQL API to interact with users and notes.

## Overview

The backend uses **AWS Lambda** for business logic processing, **AWS AppSync** for providing a GraphQL API, and **Amazon DynamoDB** for storing users and notes.

Additionally, the infrastructure (Lambda functions, DynamoDB tables, and AppSync API) is managed in a separate repository. You can find the repository for the infrastructure [here](https://github.com/hrytsenkooo/NotesAppCdk.git).

---

## Prerequisites

Before you begin, make sure you have the following tools installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [AWS CLI](https://aws.amazon.com/cli/)
- [AWS CDK](https://docs.aws.amazon.com/cdk/latest/guide/work-with-cdk.html)
- [Node.js](https://nodejs.org/) (required for AWS CDK)

Additionally, you will need access to an AWS account and an **IAM user** with permissions to manage Lambda, DynamoDB, and AppSync resources.

---

## Setup and installation

1. **Clone the repository**:

   ```bash
   git clone https://github.com/hrytsenkooo/NotesApp.git
   cd NotesApp

 2. **Install dependencies**:

       This project uses .NET 8 for backend development. Ensure that you have the correct version of .NET installed on your machine

  3. **Configure AWS credentials**:

       Make sure your AWS credentials are set up. You can configure them using the AWS CLI:
      
      ```bash
      aws configure
      
  4. **Restore .NET NuGet packages**:

       Run the following command to restore the necessary dependencies for the .NET application:
      
      ```bash
      dotnet restore
      
  5. **Build the solution**:

       Build the backend solution using the following command:
      
      ```bash
      dotnet build
  6. **Build the solution**:

       If you're working on the Lambda functions (located in NotesApp.Lambda), navigate to the folder and publish it:
      
      ```bash
      cd NotesApp.Lambda
      dotnet publish -c Release -o ./bin/publish

  7. **Compress the Lambda Package**:

       Once the Lambda function is published, compress the package for deployment:
      
      ```bash
      cd bin/publish
      powershell Compress-Archive -Path * -DestinationPath ..\..\..\lambda-package.zip -Force 
      cd ..\..\..

Commands for Windows only. Alternatively, you can always use the zip file, which is also added to this repository
