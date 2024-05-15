using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Note.Basic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       



        //对象拷贝
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CopyDemo.Copy1();//浅拷贝。
            CopyDemo.Copy2();//深拷贝。
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _02TypeInfo.Run();
        }

        /// <summary>
        /// 面向对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            _03Object.Run();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            _04Delegate.Run();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            _05event.Run();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            _06Thread.Run();
        }


        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            _07Task.Run();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            _08Generic.Run();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            _10Reflection.Run();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            _11Attribute.Run();
        }
    }
}