using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Datos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<CLIENTE> Clientes { get; }
        public CollectionViewSource ClientesViewSource { get; }
        private InformesEntities Contexto { get; }

        public MainWindow()
        {
            Contexto = new InformesEntities();
            Contexto.CLIENTES.Include("PEDIDOS").Load();
            Clientes = Contexto.CLIENTES.Local;

            ClientesViewSource = new CollectionViewSource
            {
                Source = Clientes
            };
            InitializeComponent();
            ClientesViewSource.Filter += ClientesViewSource_Filter;
            InsertarStackPanel.DataContext = new CLIENTE();
        }

        private void ClientesViewSource_Filter(object sender, FilterEventArgs e)
        {
            CLIENTE c = (CLIENTE)e.Item;
            string filtro = FiltroTextBox.Text;
            if (string.IsNullOrEmpty(filtro))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = c.nombre.Contains(filtro);
            }
        }

        private void ModificarButton_Click(object sender, RoutedEventArgs e)
        {
            Contexto.SaveChanges();
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Contexto.CLIENTES.Remove((CLIENTE)EliminarComboBox.SelectedItem);
            Contexto.SaveChanges();
        }

        private void InsertarButton_Click(object sender, RoutedEventArgs e)
        {
            Contexto.CLIENTES.Add((CLIENTE) InsertarStackPanel.DataContext);
            Contexto.SaveChanges();
            InsertarStackPanel.DataContext = new CLIENTE();
        }

        private void Filtrar_Click(object sender, RoutedEventArgs e)
        {
            ClientesViewSource.View.Refresh();
        }
    }
}
