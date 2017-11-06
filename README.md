# IdentityProject
Проект з використанням asp.net identity. У role base основі.
Hangfire, autofac, angularjs, tabledata, ajax.

логін для адміна - alex@mail.ru
пароль - Fi11235813!
Є дві особи адмін, та студент.

При реєстрації по дефолту користувач стає студентом. Адмін у системі є зразу.
Адміну надається можливість бачити список студентів. Та панелька у Hangfire.

Студент, вибирає у кабінеті початок навчання та йому автоматично приходить сповіщення за місяць, тиждень, день о 8 ранку про початок навчання.

Issues:

1) Reset password, forget password, two factor authentification aren't completed.
2) I didn't actually 100% sure of that fact , that DI worked correctly(я пробував прив'язати класи до абстракцій але не зосвім вийшло)
3) Атрибут [Authorize] не працював коректно(чомусь), і я його заміняв відповідними User.Identity.Name, зменшуючи швидкість виконання.
4) Алгоритм , яким я ставлю у чергу дату навчання , є не зовсім оптимальним його треба покращити.
5) Тільки реєстрація зроблена із використанням Angularjs, validation, $http.post().
6) Nlog використанний тільки у деяких контролерах. HomeController, StudentController та трохи AccountController.


PS. (дуже багато непотрібних packages)  
