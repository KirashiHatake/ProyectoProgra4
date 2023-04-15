using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Proyecto2.Models
{
    public class Empleados
    {
        [PrimaryKey, AutoIncrement]

        public int IdEmpleado { get; set; }
        [MaxLength(20)]
        public string Nombres { get; set; }
        [MaxLength(40)]
        public string Apellidos { get; set; }
        [MaxLength(40)]
        public int Edad { get; set; }
        [MaxLength(40)]
        public string Sexo { get; set; }
        [MaxLength(20)]
        public string Direccion { get; set; }
        [MaxLength(70)]
        public string Telefono { get; set; }

    }
}
