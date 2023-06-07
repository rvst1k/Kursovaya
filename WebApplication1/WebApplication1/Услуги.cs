using System;
using System.Collections.Generic;

namespace WebApplication1;

public partial class Услуги
{
    public int Idуслуги { get; set; }

    public string Название { get; set; } = null!;

    public string Описание { get; set; } = null!;

    public decimal Цена { get; set; }

    public byte[]? Изображение { get; set; }

    public virtual ICollection<Запись> Записьs { get; } = new List<Запись>();
}
