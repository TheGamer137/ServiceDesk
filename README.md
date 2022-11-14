# ServiceDesk
Тестовое задание от ЕРЦ
## Задача:
Разработать web-сервис для взаимодействия с заказчиками с помощью поставленных задач
## Стек:
* БД SQLite
* EntityFramework
* MVC
## Общие требования сервису:
* Наличие рабочих мест Заказчика и Исполнителя
* Наличие авторизации (доступ к соответствующему рабочему месту только авторизированному
пользователю)
* Возможность просмотра списка задач, а также подробной информации по каждой задаче
* Возможность фильтрации задач.
* Возможность поиска задач по ключевым словам
* Возможность оставлять комментарии в задаче
## Сложности:
* В процессе создания никак не мог провести миграцию в бд с Identity поэтому пришлось обойтись без этого
* Из-за этого я убил кучу времени и не успел реализовать вьюшки
* В теории код должен работать, но проверить его у меня времени не было
