using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BD_client
{
    /// <summary>
    /// Логика взаимодействия для WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {


        public WindowLogin windowLogin;

        public WindowLogin()
        {
            InitializeComponent();
            texBoxDBString.Text = ConfigurationManager.ConnectionStrings["BD_client.Properties.Settings.MyBDConnectionString"].ConnectionString;

        }
        UserModel user;
        private void LoginTry(object sender, RoutedEventArgs e)
        {
            if (texBoxLogin.Text.Length > 0) // проверяем введён ли логин     
            {
                if (PasswordBox.Password.Length > 0) // проверяем введён ли пароль         
                {             // ищем в базе данных пользователя с такими данными     
                    SqlConnection connection;
                    string connectionString;
                    string sql = "SELECT * FROM Users where login = '" + texBoxLogin.Text + "' AND [password] = '" + PasswordBox.Password + "'";
                    try
                    {
                    connectionString = ConfigurationManager.ConnectionStrings["BD_client.Properties.Settings.MyBDConnectionString"].ConnectionString;                 
                    connection = new SqlConnection(connectionString);
                        SqlCommand command = new SqlCommand(sql, connection);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable result = new DataTable();
                        connection.Open();
                        adapter.Fill(result);
                        if (result.Rows.Count > 0) // если такая запись существует       
                        {

                            user = new UserModel();
                            user.Id = (int)result.Rows[0].ItemArray[0];
                            user.Role = (int)result.Rows[0].ItemArray[1];
                            user.Login = (string)result.Rows[0].ItemArray[2];

                            MainWindow main = new MainWindow(user);
                            main.Show();
                            this.Close();


                        }
                        else MessageBox.Show("Пользователь не найден"); // выводим ошибку  
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Не удалось присоедениться к базе данных");
                    }

               

                   
                }
                else MessageBox.Show("Введите пароль"); // выводим ошибку    
            }
            else MessageBox.Show("Введите логин"); // выводим ошибку 
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (chb.IsChecked==false) {
                var settings = ConfigurationManager.ConnectionStrings["BD_client.Properties.Settings.MyBDConnectionString"];

                var fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                fi.SetValue(settings, false);
                settings.ConnectionString = texBoxDBString.Text; ;

                //ConfigurationManager.ConnectionStrings["default"].ConnectionString = texBoxDBString.Text;
                labelbd.Visibility = Visibility.Visible;
            }
        
        }

       

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            texBoxDBString.IsEnabled = true;
        }
    }
}
