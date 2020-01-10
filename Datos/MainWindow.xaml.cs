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
        private InformesEntities Contexto { get; }

        public MainWindow()
        {
            Contexto = new InformesEntities();
            Contexto.CLIENTES.Load();
            Clientes = Contexto.CLIENTES.Local;
            InitializeComponent();
            InsertarStackPanel.DataContext = new CLIENTE();
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
    }
}
