using System;
using System.Collections.Generic;

namespace WebApplication1;

public partial class Запись
{
    public int Idзаписи { get; set; }

    public int Idпользователя { get; set; }

    public int Idуслуги { get; set; }

    public string Телефон { get; set; } = null!;

    public DateTime? Дата { get; set; }

    public string? Время { get; set; }

    public string? Статус { get; set; }

    public virtual Пользователь IdпользователяNavigation { get; set; } = null!;

    public virtual Услуги IdуслугиNavigation { get; set; } = null!;
}
