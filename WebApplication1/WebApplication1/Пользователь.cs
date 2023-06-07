using System;
using System.Collections.Generic;

namespace WebApplication1;

public partial class Пользователь
{
    public int Idпользователя { get; set; }

    public string Имя { get; set; } = null!;

    public string Фамилия { get; set; } = null!;

    public string Логин { get; set; } = null!;

    public string Пароль { get; set; } = null!;

    public virtual ICollection<Запись> Записьs { get; } = new List<Запись>();
}
