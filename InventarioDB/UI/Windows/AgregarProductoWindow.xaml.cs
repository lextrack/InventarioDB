﻿using InventarioDB.Models;
using System;
using System.Windows;

namespace InventarioDB.UI
{
    public partial class AgregarProductoWindow : Window
    {
        public AgregarProductoWindow()
        {
            InitializeComponent();
        }

        // Manejador de eventos para el botón "Guardar".
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            // Obtén los valores ingresados por el usuario.
            string nombre = NombreTextBox.Text;
            string responsable = ResponsableTextBox.Text;
            int sku;
            decimal valor;
            int cantidad;
            int stock;
            DateTime? fecha = FechaDatePicker.SelectedDate;
            DateTime? hora = HoraTimePicker.Value;

            // Comprueba si se pueden convertir las cadenas de texto a enteros o decimales.
            if (int.TryParse(StockMinimoTextBox.Text, out stock) &&
                int.TryParse(CantidadTextBox.Text, out cantidad) &&
                int.TryParse(SkuTextBox.Text, out sku) &&
                fecha.HasValue && hora.HasValue &&
                decimal.TryParse(ValorTextBox.Text, out valor))
            {
                DateTime fechaYHoraSeleccionada = fecha.Value.Date + hora.Value.TimeOfDay;

                // Crea un nuevo objeto Producto y asigna los valores.
                Producto nuevoProducto = new Producto
                {
                    Nombre = nombre,
                    Responsable = responsable,
                    Sku = sku,
                    Valor = valor,
                    Cantidad = cantidad,
                    StockMinimo = stock,
                    Fecha = fechaYHoraSeleccionada
                };

                // Agrega el nuevo producto a la base de datos utilizando Entity Framework.
                using (var db = new InventarioDbContext())
                {
                    db.Productos.Add(nuevoProducto);
                    db.SaveChanges();
                }

                // Cierra la ventana después de agregar el producto.
                Close();
            }
            else
            {
                // Muestra un mensaje de error si los valores no son válidos o si se dejaron campos en blanco.
                MessageBox.Show("Los valores ingresados no son válidos o has dejado recuadros en blanco.", "Error");
            }
        }

        // Manejador de eventos para el botón "Cancelar".
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            // Cierra la ventana sin guardar el producto.
            Close();
        }
    }
}

