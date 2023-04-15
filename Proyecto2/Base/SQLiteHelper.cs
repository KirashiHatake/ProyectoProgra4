using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Proyecto2.Models;
using System.Threading.Tasks;

namespace Proyecto2.Base
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Empleados>().Wait();
        }

        //El metodo Create y Update
        public Task<int> GuardarEmpeladoAsync(Empleados emple) 
        {
            if (emple.IdEmpleado != 0)
            {
                return db.UpdateAsync(emple);
            }
            else
            {
                return db.InsertAsync(emple);
            }
        }

        //Este metodo para eliminar (Eliminar)
        public Task<int> DeleteEmpleadoAsync (Empleados empleado)
        {
            return db.DeleteAsync(empleado);
        }

        //Este metodo sirve para recuperar los empleados (Read)
        public Task<List<Empleados>> GetEmpleadosAsync()
        {
            return db.Table<Empleados>().ToListAsync();
        }

        //Este metodo sirve para recuperar los empleados por id
        public Task<Empleados> GetEmpleadosByIdAsync(int IdEmpleado)
        {
            return db.Table<Empleados>().Where(a => a.IdEmpleado == IdEmpleado).FirstOrDefaultAsync();
        }


    }
}
