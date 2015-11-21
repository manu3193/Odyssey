using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GUIOdyssey.DAL.Persistence.Models;
using GUIOdyssey.DAL.Persistence.Repositories;
using GUIOdyssey.LogicLayer;
using GUIOdyssey.LogicLayer.ObjectModels;

namespace GUIOdyssey.View
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        
        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Imagen1.Source = new BitmapImage(new Uri("Image/audi.png", UriKind.Relative));
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            Imagen1.Source = new BitmapImage(new Uri("Image/mu.png", UriKind.Relative));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            CrearCuenta nuevaCuenta = new CrearCuenta();

            OdysseyCloudAPIConsumer APIConsumer = new OdysseyCloudAPIConsumer();

            //Se trata de crear el nuevo usuario
            UserInfo userToAuth = APIConsumer.CreateUser(new UserInfo() { Nickname = "NewUser123", Password = "nuevousuario",Name ="NombreNuevoUsuario" }).Result;

            //Si el usuario que se retorna no es nulo
            if (!userToAuth.UserId.Equals(new Guid()))
            {
                SessionManager.Instance.Nickname = userToAuth.Nickname;
                SessionManager.Instance.Name = userToAuth.Name;
                SessionManager.Instance.UserId = userToAuth.UserId;
            }

            nuevaCuenta.Show();
            this.Close();
        }


     

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LibraryManager lm=new LibraryManager();
            OdysseyCloudAPIConsumer APIConsumer = new OdysseyCloudAPIConsumer();

            //Se obtiene logueo de usuario
            UserInfo userToAuth = APIConsumer.GetUserAuth(new UserInfo() {Nickname= "kevuo", Password = "elcharlatan"}).Result;

            //Si el usuario que se retorna no es nulo
            if (!userToAuth.UserId.Equals(new Guid()))
            {
                SessionManager.Instance.Nickname = userToAuth.Nickname;
                SessionManager.Instance.Name = userToAuth.Name;
                SessionManager.Instance.UserId = userToAuth.UserId;
            }


            //Inicializa la biblioteca de usuario
            lm.InitializeLibrary();
            //Importa carpeta de usuario a la biblioteca musical local
            lm.ImportSongsToLibrary(@"C:\Users\Manuel\Desktop\majesco");
            //Sincroniza Biblioteca con Cloud
            lm.SyncUserLibrary();


            Form1 principal = new Form1(lm);
            this.Close();
            principal.ShowDialog();
            
        }
    }
}
