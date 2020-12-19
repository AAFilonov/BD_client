using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
         
        }

        private void LoginTry(object sender, RoutedEventArgs e)
        {
            if (texBoxLogin.Text.Length > 0) // проверяем введён ли логин     
            {
                if (PasswordBox.Password.Length > 0) // проверяем введён ли пароль         
                {             // ищем в базе данных пользователя с такими данными     
                    string sql = "SELECT * FROM Users where login = '" + texBoxLogin.Text + "' AND [password] = '" + PasswordBox.Password + "'";
                    var connectionString = ConfigurationManager.ConnectionStrings["BD_client.Properties.Settings.MyBDConnectionString"].ConnectionString;
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable result = new DataTable();
                    connection.Open();
                    adapter.Fill(result);
                    if (result.Rows.Count > 0) // если такая запись существует       
                    {
                      
                        UserModel user = new UserModel();
                        user.Id = (int)result.Rows[0].ItemArray[0];
                        user.Role = (int) result.Rows[0].ItemArray[1];
                        user.Login = (string) result.Rows[0].ItemArray[2];
       
                        MainWindow mainWindow = new MainWindow(user);
                        mainWindow.Show();
                        this.Close();
               
                    }
                    else MessageBox.Show("Пользователь не найден"); // выводим ошибку  
                }
                else MessageBox.Show("Введите пароль"); // выводим ошибку    
            }
            else MessageBox.Show("Введите логин"); // выводим ошибку 
        }
    }
}
