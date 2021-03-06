﻿using GUIOdyssey.LogicLayer;
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
            SessionManager instance = SessionManager.Instance;
            instance.UserId = new Guid();
            LibraryManager lm = new LibraryManager();
            //lm.ImportSongsToLibrary("C:\\Users\\Manuel\\Desktop\\mudi");
            instance.UserId=Guid.Parse("1bbe27bd-164f-4798-9e15-6f4fb2f4bbab");
            instance.Nickname = "Majesco";
           // lm = new LibraryManager();
            //lm.ImportSongsToLibrary("C:\\Users\\Manuel\\Desktop\\mudi1");
            lm.InitializeLibrary();
            //OdysseyCloudAPIConsumer ocac =new OdysseyCloudAPIConsumer();
            //var a =ocac.GetUserAuth(new UserInfo() {Nickname = "manzumbado", Password = "cacabubu"}); 
            //Console.WriteLine(a);
            FileManager fm= new FileManager();
            fm.GetUserPathToOdysseyMusic();
            string reslt =fm.uploadFile(@"C:\Users\Manuel\Desktop\music\09 Eclipse.mp3");
            string result2 = fm.downloadFile(reslt);
            CrearCuenta nuevaCuenta = new CrearCuenta();
            nuevaCuenta.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OdysseyCloudAPIConsumer clientApiConsumer =new OdysseyCloudAPIConsumer();
            Guid? userID = clientApiConsumer.GetUserAuth(new UserInfo() {Nickname = "manzumbado", Password = "cacabubu"}).Result;
            Console.WriteLine(userID.ToString());
            MainWindow principal = new MainWindow();
            principal.Show();
            this.Close();
        }
    }
}
