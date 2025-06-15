using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public class Driver
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}

