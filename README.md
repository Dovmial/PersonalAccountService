# PAService
В основном реализован бэк. Клиент имеет только страницы Create и Index (для остальны конопок view нет).
- т.к. у жильцов может быть несколько счетов, а к счету привязано несколько жильцов - связь между сущностями многие-ко-многим
- реализованы фильтрации, сортировки, пагинации(на уровне запроса)
<img width="618" height="344" alt="image" src="https://github.com/user-attachments/assets/526ab92b-36b9-4cb0-bbdb-a792fca1c0b9" /><br>

- Генерация номера лицевого осуществляется в рамках одной транзакции на уровне базы без лишних запросов.<br>
<img width="785" height="403" alt="image" src="https://github.com/user-attachments/assets/2ed01329-c4e8-433c-a1d6-a9dbfdb3cbf8" />
<br>
<img width="600" height="300" alt="image" src="https://github.com/user-attachments/assets/8d0700e4-977a-46d9-b86e-7bd0078ced3a" />
<hr>сама таблица <br>
<img width="600" height="300" alt="image" src="https://github.com/user-attachments/assets/bc55f32b-87be-41a8-ba7e-0d6ce6d55b2f" />
<br>
- создание нового счета <br>
<img width="928" height="331" alt="image" src="https://github.com/user-attachments/assets/066f43d7-1d6b-44a0-ae83-28a5751a3838" /><br>
