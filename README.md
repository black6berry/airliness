# Курсовой проект Авиалинии
Цель курсового проекта подсчет суммы проданных билетов на авиаперелеты
# Api 
Api доступно по адресу - https://private-public.site/Rodnoy/airlines-api

Ссылка на API Postman - https://elements.getpostman.com/redirect?entityId=20457530-e2bf0a0b-8c01-4a27-a2df-4c08a05bf6fa&entityType=collection


Доступные методы:

#### GET
- plane (возвращает самолеты)
- plane/{id} (возвращает самолет)
- routes (возвращает маршруты)
- ticketsSold/ (возвращает проданные билеты)
- sign-in (в параметрах приниает Login и Password) 
пример https://private-public.site/Rodnoy/airlines-api/sign-in?Login=8&Password=8
- captains (возвращает всех пилотов)

#### POST 

JSON объекты передаются через Body->row если вы используете Postman
- create-user
```
{
    "Firstname": "Jhon",
    "Lastname": "Doe",
    "Patronymic": "Smoth",
    "PasportNumber": "212333",
    "Address": "this",
    "PhoneNumber": "8800553535",
    "Role": "Пилот",
    "PilotExperience": "555"
}
```
- delete-user{id}
- del-user
```
{
    "Id":"84"
}
```
- update-user
```
{
    "Id": "99",
    "Firstname":"No name",
    "Lastname": "No name",
    "Patronymic": "No name",
    "PasportNumber": "000000",
    "Address": "No name",
    "PhoneNumber": "00000000000",
    "Role": "Пилот",
    "PilotExperience": "none"

}
```
- create-plane
```
{
    "BortNumber": "test5",
    "Model": "test5",
    "CreationDate": "test5",
    "YearsOfUse": "test5",
    "ReadFly": "0",
    "MaxPassenger": "test5"
}
```
- add-ticket
```
{
    "Firstname": "No name",
    "Lastname": "No name",
    "Patronymic": "No name",
    "PasportNumber": "000000",
    "DepartureAirport": "Москва (Внуково)",
    "ArrivalAirport": "Сочи (Адлер)",
    "TimeTripMinute": "230",
    "Price": "29985.00",
    "DateTimeDeparture": "1679124268",
    "BortNumber": "B7572001",
    "SeatNumber": "A01"
}
```

Роли доступные в программе:

1) Пользователь (Логин-8 Пароль-8)
- Оформление билета
2) Админ (Логин-1 Пароль-1)
- создание пользавателя с ролью "Капитан"
- изменение данных пользователя с ролью "Капитан"
- удаление пользователя с ролью "Капитан"

# Экраны программы

Авторизация: <p align="center"><img src="WpfAirliness/img/Auth.jpg"/></p>
Главная страница Администратора <p align="center"><img src="WpfAirliness/img/WindowAdminMain.jpg"/></p>

# Видео с работой программы

Добавление билета

![Video](WpfAirliness/video/AddTIcket2_video.mov) 
- Если видео не отображается, то оно лежит по этому пути WpfAirliness/video/AddTIcket2

Создание, редактирование, удаление записей

![Video](WpfAirliness/video/Crud-captain2_video.MOV)
- Если видео не отображается, то оно лежит по этому пути WpfAirliness/video/Crud-captain2
