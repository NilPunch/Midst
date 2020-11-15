# Midst
Небольшой проект, созданный для Brackeys Game Jam 2020.2 в течение недели.

Был создан совместно с Даниилом Панкевичем (GitHub - [NilPunch](https://github.com/NilPunch))

Ссылка на проект на Itch.io: [Midst](https://itch.io/jam/brackeys-4/rate/724712)
## Видимые сейчас ошибки:
* Нарушение принципов SOLID ( в частности, SRP и DIP)
* Повторное использование кода
* Неоптимизированный UI
* Наличие выводов Debug.Log без оборачивания в #if #endif
* Использование [RequireComponent] и GetComponent<> вместо сериализации
* Использование FindObjectsOfType<>
