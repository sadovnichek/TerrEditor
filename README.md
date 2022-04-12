# TerrEditor Landscape Editor
## Авторы
Капкаев Виктор КН-204
Мелешин Демид КН-204
Моложавых Мария КН-201
Кондратов Данил КН-201

## Проблематика
Данное приложение поможет при планировании сада. TerrEditor - простой в использовании 2D редактор ландшафта. Для работы нужно брать и перетаскивать растения, цветы, мебель в ваш чертеж. Доступны инструменты для работы с объектами - поворот, масштабирование и т.д.

## Устройство архитектуры приложения
Приложение состоит из следующих сборок:
TerrEditor\UI ...
TerrEditor\Application ...
TerrEditor\Domain - место нахождения классов предметной области
TerrEditor\Infrastructure - инфраструктура

DI Container ...

## Точки расширения
* Добавление новых объектов дизайна
* Возможность описания нового формата сохранения - для совместимости с другими редакторами.
