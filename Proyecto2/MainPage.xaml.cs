using Proyecto2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Proyecto2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnregistrar_Clicked(object sender, EventArgs e)
        {
            if (validardatos())
            {
                Empleados emple = new Empleados
                {
                    Nombres = txtnombres.Text,
                    Apellidos = txtapellidos.Text,
                    Edad = int.Parse(txtedad.Text),
                    Sexo = txtsexo.Text,
                    Direccion = txtdireccion.Text,
                    Telefono= txttelefono.Text, 
                };
                await App.SQLiteDB.GuardarEmpeladoAsync(emple);
                LimpiarControles();
                await DisplayAlert("Registro", "Se ha guardado de forma exitosa el registro", "Aceptar");
                llenarDatos();
            }
            else
            {
                await DisplayAlert("Advertencia","Debe de llenar todos los campos","Aceptar");
            }
        }

        public async void llenarDatos()
        {
            var empleadoList = await App.SQLiteDB.GetEmpleadosAsync();
            if (empleadoList != null)
            {
                listempleados.ItemsSource = empleadoList;
            }
        }

        public bool validardatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtnombres.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtapellidos.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtedad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtsexo.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtdireccion.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txttelefono.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private async void btnactualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdEmpleado.Text))
            {
                Empleados empleado = new Empleados()
                {
                    IdEmpleado = Convert.ToInt32(txtIdEmpleado.Text),
                    Nombres = txtnombres.Text,
                    Apellidos = txtapellidos.Text,
                    Edad = Convert.ToInt32(txtedad.Text),
                    Sexo = txtsexo.Text,
                    Direccion = txtdireccion.Text,
                    Telefono = txttelefono.Text,
                };
                await App.SQLiteDB.GuardarEmpeladoAsync(empleado);
                await DisplayAlert("Registro", "Se actualizo de manera exitosa", "Aceptar");
                
                LimpiarControles();
                txtIdEmpleado.IsVisible = false;
                btnactualizar.IsVisible = false;
                btnregistrar.IsVisible = true;
                llenarDatos();
            }
        }

        private async void listempleados_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Empleados)e.SelectedItem;
            btnregistrar.IsVisible = false;
            txtIdEmpleado.IsVisible = true;
            btnactualizar.IsVisible = true;
            btneliminar.IsVisible = true;
            btncancelar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.IdEmpleado.ToString()))
            {
                var empleado = await App.SQLiteDB.GetEmpleadosByIdAsync(obj.IdEmpleado);

                if (empleado != null)
                {
                    txtIdEmpleado.Text = empleado.IdEmpleado.ToString();
                    txtnombres.Text = empleado.Nombres;
                    txtapellidos.Text = empleado.Apellidos;
                    txtedad.Text = empleado.Edad.ToString();
                    txtsexo.Text = empleado.Sexo;
                    txtdireccion.Text = empleado.Direccion;
                    txttelefono.Text = empleado.Telefono;
                }
            }
        }

        public void LimpiarControles()
        {
            txtIdEmpleado.Text = "";
            txtnombres.Text = "";
            txtapellidos.Text = "";
            txtedad.Text = "";
            txtsexo.Text = "";
            txtdireccion.Text = "";
            txttelefono.Text = "";

            txtIdEmpleado.IsVisible = false;
            btnactualizar.IsVisible = false;
            btnregistrar.IsVisible = true;       

        }

        private async void btneliminar_Clicked(object sender, EventArgs e)
        {
            var empleado = await App.SQLiteDB.GetEmpleadosByIdAsync(Convert.ToInt32(txtIdEmpleado.Text));

            if (empleado != null)
            {
                await App.SQLiteDB.DeleteEmpleadoAsync(empleado);
                await DisplayAlert("Empleado", "Se ha eliminado los datos de forma exitosa", "Aceptar");
                LimpiarControles();
                llenarDatos();
                txtIdEmpleado.IsVisible = false;
                btnactualizar.IsVisible = false;
                btnregistrar.IsVisible = true;
                btneliminar.IsVisible = false;
            }
        }

        private void btncancelar_Clicked(object sender, EventArgs e)
        {
            LimpiarControles();
            btnactualizar.IsVisible = false;
            btnregistrar.IsVisible = true;
            btneliminar.IsVisible = false;
            btncancelar.IsVisible = false;
            
        }

        private void btnlist_Clicked(object sender, EventArgs e)
        {
            llenarDatos();
            btnlist.IsVisible = false;
        }
    }
}
