# Science Archive Project

<p align="center">
  <img src="docs/assets/docs-images/science-archive.logo.png" alt="Title image" height="175px"/>
</p>

<p align="center">
  Welcome to my pet project of Science Archive! The idea of this project is to create web-service allowing people to create and share worldwide their knowledges of any science kind.
</p>

## Project Architecture

Whole project consists of the following services which are stored in separated directories:

- `ScienceArchive.Server` - main backend service. It was created following Clean Architecture principles and powered by .NET platform. All business logic is stored there;
- `science-archive-web` - client-side application, which is created with Angular as Single Page Application (SPA);
- `ScienceArchive.DB` - it is database project. All SQL and NoSQL scripts are stored here. There are two DBMS used in this project - PostgreSQL for main data and ClickHouse for logs;
- `doc-store-api` - web API created with Go language. It is used to work with file storage: upload, download etc;
- `telegram-bot-api` - this is a web API service for interacting with Telegram API to send notifications to users, help to find information in service (articles, authors etc);
- `ScienceArchive.Mobile` - test mobile application created with .NET MAUI.

## Common Backend Services Architecture

Consider backend UML Class Diagram below:

![Backend Diagram](./docs/assets/backend/science-archive-backend-diagram.jpg)

## Database Entity-Relationships Diagram (ERD)

Consider a template of database structure:

![Database Diagram. ERD](./docs/assets/backend/database-diagram.jpg)
