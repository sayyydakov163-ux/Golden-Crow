# Golden Crow

## Описание

API для аутентификации и управления пользователями с автоматической очисткой просроченных токенов через фоновый сервис.

## Возможности

- Регистрация и авторизация пользователей
- JWT-токены для защиты эндпоинтов
- Фоновый сервис (`BackgroundService`) для удаления истекших токенов
- Middleware для проверки авторизации
- Swagger для тестирования API

## Технологии

- ASP.NET Core 8
- Entity Framework Core
- JWT Bearer Authentication
- Swagger/OpenAPI

## Структура проекта
Golden Crow/
├── Controllers/ # Обработка HTTP-запросов
├── Services/ # Бизнес-логика
│ └── BackgroundService # Фоновая очистка токенов
├── Middlewares/ # Проверка авторизации
├── Models/ # Модели данных
├── DTOs/ # Объекты передачи данных
├── Database/ # Контекст БД
├── Properties/ # Настройки запуска
├── Constants.cs # Константы проекта
├── Result.cs # Обертки для ответов
├── Program.cs # Точка входа и DI
└── appsettings.json # Конфигурация

text

## Запуск

1. Откройте `Golden Crow.sln` в Visual Studio
2. Настройте строку подключения в `appsettings.json`
3. Выполните миграции: `dotnet ef database update`
4. Нажмите `F5`

## API Endpoints

Документация доступна после запуска по адресу:
https://localhost:7001/swagger/index.html

text

## Автор

[sayyydakov163-ux](https://github.com/sayyydakov163-ux)