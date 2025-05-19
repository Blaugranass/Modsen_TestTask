# Modsen_TestTask
This is test task for Modsen Software development company on trainee position
# О проекте
Library - приложение, для управления работы с книгами. Есть роль пользователя, который может брать книги из библиотеки, искать книги по жанру\автору\названию и просматривать книги, которые он уже взял, также есть Администратор, который работает со всеми книгами (создает, удаляет, редактирует), также управляет авторами.
# Технологический стек
- **C# / .Net 9**
- **Asp.Net Core web API**
- **Entity Framework Core**
- **PostgreSQL**
- **FluentValidation**
- **AutoMapper**
- **Docker, Docker Compose**
- **XUnit**
# Запуск через Docker Compose
 ## Требования
- **Docker**
- **Docker Compose**

Создайте файл  .env, в котором укажите необходимые данные для подключения к базе данных, а также secret key для генерации JWT токена
```
TOKENS_SECRET=your_secret_key
DB_HOST=db
DB_PORT=5432 
POSTGRES_PASSWORD=......
POSTGRES_USER=......
POSTGRES_DB=.......
CONNECTION_STRING=Host=${DB_HOST};Port=${DB_PORT};Database=${POSTGRES_DB};Username=${POSTGRES_USER};Password=${POSTGRES_PASSWORD};
```
## Запуск
Перейдите в директорию, где расположен docker-compose.yml и воспользуйтесь командой
 ```
docker-compose up --build
 ```
