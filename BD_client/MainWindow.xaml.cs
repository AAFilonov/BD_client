using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
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
using Brushes = System.Drawing.Brushes;
using Image = System.Drawing.Image;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using Size = System.Windows.Size;
using Point = System.Windows.Point;
using System.Windows.Forms;
using TextBox = System.Windows.Controls.TextBox;
using DataGrid = System.Windows.Controls.DataGrid;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.MessageBox;
using Application = System.Windows.Application;

namespace BD_client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        UserModel _user;
        public MainWindow( UserModel user)
        {
            

            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
            _user = user;
            
            this.Title += ":" + user.Login;

          
        }
        BD_client.MyBDDataSet myBDDataSet;
        //основные
        BD_client.MyBDDataSetTableAdapters.ClientTableAdapter myBDDataSetClientTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Сontract_tableTableAdapter myBDDataSetСontract_tableTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Con_pointTableAdapter myBDDataSetCon_pointTableAdapter;
        BD_client.MyBDDataSetTableAdapters.AdressTableAdapter myBDDataSetAdressTableAdapter;
        //услуги
        BD_client.MyBDDataSetTableAdapters.Service_NetTableAdapter myBDDataSetService_NetTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Service_TVTableAdapter myBDDataSetService_TVTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Service_OtherTableAdapter myBDDataSetService_OtherTableAdapter;
        //справочники
        BD_client.MyBDDataSetTableAdapters.Type_ClTableAdapter myBDDataSetType_ClTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Tv_channelTableAdapter myBDDataSetTv_channelTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Tv_bundleTableAdapter myBDDataSetTv_bundleTableAdapter;
        BD_client.MyBDDataSetTableAdapters.RegionTableAdapter myBDDataSetRegionTableAdapter;
        //системные
        BD_client.MyBDDataSetTableAdapters.UsersTableAdapter myBDDataSetUsersTableAdapter;

        BD_client.MyBDDataSetTableAdapters.Tv_bundle_channelTableAdapter myBDDataSetTv_bundle_channelTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Tv_Service_bundleTableAdapter myBDDataSetTv_Service_bundleTableAdapter;

        System.Windows.Data.CollectionViewSource clientViewSource;
        System.Windows.Data.CollectionViewSource view_Client_ServicesViewSource;
        System.Windows.Data.CollectionViewSource type_ClViewSource;

        private void RoleRestrictions()
        {
            List<DataGrid> dataGrids = new List<DataGrid>();
            switch (_user.Role)
            {
                case 2: //admin
                    SystemTables.IsEnabled = true;
                    break;
                case 1: //operator
                    dataGrids.Add(type_ClDataGrid);
                    dataGrids.Add(tv_channelDataGrid);
                    dataGrids.Add(tv_bundleDataGrid);
                    dataGrids.Add(tv_bundle_channelDataGrid);


                    foreach (DataGrid dg in dataGrids)
                    {
                        dg.IsReadOnly = true;
                    }
                    break;
                case 0: //user
                    SystemTables.Visibility = Visibility.Hidden;

                    
                    dataGrids.Add(clientDataGrid);
                    dataGrids.Add(con_pointDataGrid);
                    dataGrids.Add(adressDataGrid);
                    dataGrids.Add(сontractDataGrid);
                    dataGrids.Add(service_NetDataGrid);
                    dataGrids.Add(service_NetDataGrid);
                    dataGrids.Add(tv_Service_bundleDataGrid);
                    dataGrids.Add(service_OtherDataGrid);
                 

            


                    foreach (DataGrid dg in dataGrids)
                    {
                        dg.IsReadOnly = true;
                    }



                    var t = FindVisualChildren<Button>(root);
                    foreach (Button b in t)
                    {
                        if (b.Name.Contains("Save") || b.Name.Contains("Delete"))
                            b.IsEnabled = false;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException("User role " + _user.Role);

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RoleRestrictions();
            myBDDataSet = ((BD_client.MyBDDataSet)(this.FindResource("myBDDataSet")));


            // Загрузить данные в таблицу Client. Можно изменить этот код как требуется.
            myBDDataSetClientTableAdapter = new BD_client.MyBDDataSetTableAdapters.ClientTableAdapter();
            myBDDataSetClientTableAdapter.Fill(myBDDataSet.Client);
            clientViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientViewSource")));


            var clients = new ObservableCollection<Client>();
            foreach (DataRow row in myBDDataSet.Client.Rows)
            {
                var obj = new Client();
                obj.id = (int)row["id"];
                obj.type = (int)row["type"];
                if (row["email"] != DBNull.Value)
                    obj.email = (string)row["email"];

                obj.name = (string)row["name"];
                obj.passport_Seria = (string)row["passport_Seria"];
                obj.passport_Num = (string)row["passport_Num"];
                if (row["legal_entity"] != DBNull.Value)
                    obj.legal_entity = (int?)row["legal_entity"];
                clients.Add(obj);
            }

            clientViewSource.Source = clients;

            clientViewSource.Filter += ClientViewSource_Filter;


            clientViewSource.View.MoveCurrentToFirst();
            // Загрузить данные в таблицу Сontract_table. Можно изменить этот код как требуется.
            myBDDataSetСontract_tableTableAdapter = new BD_client.MyBDDataSetTableAdapters.Сontract_tableTableAdapter();
            myBDDataSetСontract_tableTableAdapter.Fill(myBDDataSet.Сontract_table);
            System.Windows.Data.CollectionViewSource сontract_tableViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("сontract_tableViewSource")));
            сontract_tableViewSource.View.MoveCurrentToFirst();
            // Загрузить данные в таблицу Con_point. Можно изменить этот код как требуется.
            myBDDataSetCon_pointTableAdapter = new BD_client.MyBDDataSetTableAdapters.Con_pointTableAdapter();
            myBDDataSetCon_pointTableAdapter.Fill(myBDDataSet.Con_point);
            System.Windows.Data.CollectionViewSource con_pointViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("con_pointViewSource")));
            con_pointViewSource.View.MoveCurrentToFirst();
            // Загрузить данные в таблицу Adress. Можно изменить этот код как требуется.
            myBDDataSetAdressTableAdapter = new BD_client.MyBDDataSetTableAdapters.AdressTableAdapter();
            myBDDataSetAdressTableAdapter.Fill(myBDDataSet.Adress);
            System.Windows.Data.CollectionViewSource adressViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("adressViewSource")));
            adressViewSource.View.MoveCurrentToFirst();


            // Загрузить данные в таблицу View_Client_Services. Можно изменить этот код как требуется.
            BD_client.MyBDDataSetTableAdapters.View_Client_ServicesTableAdapter myBDDataSetView_Client_ServicesTableAdapter = new BD_client.MyBDDataSetTableAdapters.View_Client_ServicesTableAdapter();
            myBDDataSetView_Client_ServicesTableAdapter.Fill(myBDDataSet.View_Client_Services);
            view_Client_ServicesViewSource = (CollectionViewSource)(FindResource("view_Client_ServicesViewSource"));




            view_Client_ServicesViewSource.View.MoveCurrentToFirst();


            // TODO: Добавить сюда код, чтобы загрузить данные в таблицу Input_Natural_person_data.
            // Не удалось создать этот код, поскольку метод myBDDataSetInput_Natural_person_dataTableAdapter.Fill отсутствует или имеет неизвестные параметры.
            BD_client.MyBDDataSetTableAdapters.Input_Natural_person_dataTableAdapter myBDDataSetInput_Natural_person_dataTableAdapter = new BD_client.MyBDDataSetTableAdapters.Input_Natural_person_dataTableAdapter();
            System.Windows.Data.CollectionViewSource input_Natural_person_dataViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("input_Natural_person_dataViewSource")));
            input_Natural_person_dataViewSource.View.MoveCurrentToFirst();
            // Загрузить данные в таблицу View_Client_Info. Можно изменить этот код как требуется.
            BD_client.MyBDDataSetTableAdapters.View_Client_InfoTableAdapter myBDDataSetView_Client_InfoTableAdapter = new BD_client.MyBDDataSetTableAdapters.View_Client_InfoTableAdapter();
            myBDDataSetView_Client_InfoTableAdapter.Fill(myBDDataSet.View_Client_Info);
            System.Windows.Data.CollectionViewSource view_Client_InfoViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("view_Client_InfoViewSource")));
            view_Client_InfoViewSource.View.MoveCurrentToFirst();


            // Загрузить данные в таблицу Service_Net. Можно изменить этот код как требуется.
            myBDDataSetService_NetTableAdapter = new BD_client.MyBDDataSetTableAdapters.Service_NetTableAdapter();
            myBDDataSetService_NetTableAdapter.Fill(myBDDataSet.Service_Net);
            System.Windows.Data.CollectionViewSource service_NetViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("service_NetViewSource")));
            service_NetViewSource.View.MoveCurrentToFirst();
            // Загрузить данные в таблицу Service_TV. Можно изменить этот код как требуется.
            myBDDataSetService_TVTableAdapter = new BD_client.MyBDDataSetTableAdapters.Service_TVTableAdapter();
            myBDDataSetService_TVTableAdapter.Fill(myBDDataSet.Service_TV);
            System.Windows.Data.CollectionViewSource service_TVViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("service_TVViewSource")));
            service_TVViewSource.View.MoveCurrentToFirst();
            // Загрузить данные в таблицу Service_Other. Можно изменить этот код как требуется.
            myBDDataSetService_OtherTableAdapter = new BD_client.MyBDDataSetTableAdapters.Service_OtherTableAdapter();
            myBDDataSetService_OtherTableAdapter.Fill(myBDDataSet.Service_Other);
            System.Windows.Data.CollectionViewSource service_OtherViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("service_OtherViewSource")));
            service_OtherViewSource.View.MoveCurrentToFirst();



            // Загрузить данные в таблицу Tv_channel. Можно изменить этот код как требуется.
            myBDDataSetTv_channelTableAdapter = new BD_client.MyBDDataSetTableAdapters.Tv_channelTableAdapter();
            myBDDataSetTv_channelTableAdapter.Fill(myBDDataSet.Tv_channel);
            System.Windows.Data.CollectionViewSource tv_channelViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tv_channelViewSource")));
            tv_channelViewSource.View.MoveCurrentToFirst();
            // Загрузить данные в таблицу Tv_bundle. Можно изменить этот код как требуется.
            myBDDataSetTv_bundleTableAdapter = new BD_client.MyBDDataSetTableAdapters.Tv_bundleTableAdapter();
            myBDDataSetTv_bundleTableAdapter.Fill(myBDDataSet.Tv_bundle);
            System.Windows.Data.CollectionViewSource tv_bundleViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tv_bundleViewSource")));
            tv_bundleViewSource.View.MoveCurrentToFirst();
            // Загрузить данные в таблицу Type_Cl. Можно изменить этот код как требуется.
            myBDDataSetType_ClTableAdapter = new BD_client.MyBDDataSetTableAdapters.Type_ClTableAdapter();
            myBDDataSetType_ClTableAdapter.Fill(myBDDataSet.Type_Cl);
            type_ClViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("type_ClViewSource")));
            type_ClViewSource.View.MoveCurrentToFirst();
            // Загрузить данные в таблицу Region. Можно изменить этот код как требуется.
            myBDDataSetRegionTableAdapter = new BD_client.MyBDDataSetTableAdapters.RegionTableAdapter();
            myBDDataSetRegionTableAdapter.Fill(myBDDataSet.Region);
            System.Windows.Data.CollectionViewSource regionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("regionViewSource")));
            regionViewSource.View.MoveCurrentToFirst();

            // Загрузить данные в таблицу Users. Можно изменить этот код как требуется.
            myBDDataSetUsersTableAdapter = new BD_client.MyBDDataSetTableAdapters.UsersTableAdapter();
            myBDDataSetUsersTableAdapter.Fill(myBDDataSet.Users);
            System.Windows.Data.CollectionViewSource usersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("usersViewSource")));
            usersViewSource.View.MoveCurrentToFirst();

            // Загрузить данные в таблицу Tv_bundle_channel. Можно изменить этот код как требуется.
            myBDDataSetTv_bundle_channelTableAdapter = new BD_client.MyBDDataSetTableAdapters.Tv_bundle_channelTableAdapter();
            myBDDataSetTv_bundle_channelTableAdapter.Fill(myBDDataSet.Tv_bundle_channel);
            System.Windows.Data.CollectionViewSource tv_bundle_channelViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tv_bundle_channelViewSource")));
            tv_bundle_channelViewSource.View.MoveCurrentToFirst();
            // Загрузить данные в таблицу Tv_Service_bundle. Можно изменить этот код как требуется.
             myBDDataSetTv_Service_bundleTableAdapter = new BD_client.MyBDDataSetTableAdapters.Tv_Service_bundleTableAdapter();
            myBDDataSetTv_Service_bundleTableAdapter.Fill(myBDDataSet.Tv_Service_bundle);
            System.Windows.Data.CollectionViewSource tv_Service_bundleViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tv_Service_bundleViewSource")));
            tv_Service_bundleViewSource.View.MoveCurrentToFirst();

            var type_Cls = new ObservableCollection<Type_Cl>();
            var none = new Type_Cl()
            {
                id = -1,
                type = "None"
            };

            type_Cls.Add(none);
            foreach (DataRow row in myBDDataSet.Type_Cl.Rows)
            {
                var obj = new Type_Cl()
                {
                    id = (int)row["id"],
                    type = (string)row["type"]
                };
                type_Cls.Add(obj);
            }



            filterType.ItemsSource = type_Cls;
            filterType.SelectedValuePath = "id";
            filterType.DisplayMemberPath = "type";
            filterType.SelectedIndex = 0;


        }

  
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_user.Role == 0) return;

          var b  =(Button)sender;
            switch (b.Name)
            {
                case "ClientSave":
                    myBDDataSetClientTableAdapter.Update(myBDDataSet.Client);
                    break;
                case "ContractSave":
                    myBDDataSetСontract_tableTableAdapter.Update(myBDDataSet.Сontract_table);
                    break;
                case "ConPointSave":
                    myBDDataSetCon_pointTableAdapter.Update(myBDDataSet.Con_point);
                    break;
                case "AdressSave":
                    myBDDataSetAdressTableAdapter.Update(myBDDataSet.Adress);
                    break;             
                case "ServiceNetSave":
                    myBDDataSetService_NetTableAdapter.Update(myBDDataSet.Service_Net);
                    break;
                case "ServiceTVSave":
                    myBDDataSetService_TVTableAdapter.Update(myBDDataSet.Service_TV);
                    break;
                case "ServiceOtherSave":
                    myBDDataSetService_OtherTableAdapter.Update(myBDDataSet.Service_Other);
                    break;
                case "Type_ClSave":
                    myBDDataSetType_ClTableAdapter.Update(myBDDataSet.Type_Cl);
                    break;
                case "TV_ChanelSave":
                    myBDDataSetTv_channelTableAdapter.Update(myBDDataSet.Tv_channel);
                    break;
                case "TV_BundleSave":
                    myBDDataSetTv_bundleTableAdapter.Update(myBDDataSet.Tv_bundle);
                    break;
                case "RegionSave":
                    myBDDataSetRegionTableAdapter.Update(myBDDataSet.Region);
                    break;

                case "userSave":
                    myBDDataSetUsersTableAdapter.Update(myBDDataSet.Users );
                    break;
                case "tv_bundle_channelSave":
                    myBDDataSetTv_bundle_channelTableAdapter.Update(myBDDataSet.Tv_bundle_channel);
                    break;
                case "tv_Service_bundleSave":
                    myBDDataSetTv_Service_bundleTableAdapter.Update(myBDDataSet.Tv_Service_bundle);
                    break;
                    




                default:
                    throw new ArgumentOutOfRangeException(b.Name);
            }
          
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_user.Role == 0) return;

            var b = (Button)sender;
            var p = (StackPanel)b.Parent;
            var pp = (Grid)p.Parent;
            var dg_1 = FindVisualChildren<DataGrid>(pp);
            var dg = dg_1.First();

            for (int i = 0; i < dg.SelectedItems.Count; i++)
            {
                var dataRowView = dg.SelectedItems[i] as DataRowView;
                if (dataRowView != null)
                {
                    DataRow dataRow = dataRowView.Row;
                    dataRow.Delete();
                }
            }
            switch (b.Name)
            {
                case "ClientDelete":                   
                    myBDDataSetClientTableAdapter.Update(myBDDataSet.Client);
                    break;
                case "ContractDelete":
               myBDDataSetСontract_tableTableAdapter.Update(myBDDataSet.Сontract_table);
                    break;
                case "ConPointDelete":
                myBDDataSetCon_pointTableAdapter.Update(myBDDataSet.Con_point);
                    break;
                case "AdressDelete":
                  myBDDataSetAdressTableAdapter.Update(myBDDataSet.Adress);
                    break;
                case "ServiceNetDelete":    
                    myBDDataSetService_NetTableAdapter.Update(myBDDataSet.Service_Net);
                    break;
                case "ServiceTVDelete":
          
                    myBDDataSetService_TVTableAdapter.Update(myBDDataSet.Service_TV);
                    break;
                case "ServiceOtherDelete":
           
                    myBDDataSetService_OtherTableAdapter.Update(myBDDataSet.Service_Other);
                    break;
                case "Type_ClSave":
              
                    myBDDataSetType_ClTableAdapter.Update(myBDDataSet.Type_Cl);
                    break;
                case "TV_ChanelSaveDelete":
            
                    myBDDataSetTv_channelTableAdapter.Update(myBDDataSet.Tv_channel);
                    break;
                case "TV_BundleDelete":
      
                    myBDDataSetTv_bundleTableAdapter.Update(myBDDataSet.Tv_bundle);
                    break;
                case "RegionDelete":
                  
                    myBDDataSetRegionTableAdapter.Update(myBDDataSet.Region);
                    break;
                case "UserDelete":
                    myBDDataSetUsersTableAdapter.Update(myBDDataSet.Users);
                    break;
                case "tv_bundle_channelDelete":
                    myBDDataSetTv_bundle_channelTableAdapter.Update(myBDDataSet.Tv_bundle_channel);
                    break;
                case "tv_Service_bundleDelete":
                    myBDDataSetTv_Service_bundleTableAdapter.Update(myBDDataSet.Tv_Service_bundle);
                    break;


                default:
                    throw new ArgumentOutOfRangeException(b.Name);

            }
        }

        private void ClientViewSource_Filter(object sender, FilterEventArgs e)
        {
            var row = ((Client)e.Item).name;
            var text = ClientSearchTextBox.Text;

            e.Accepted = row.Contains(text);
        }


        private void ClientSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clientViewSource.View.Refresh();
        }

        private void filterType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filterType.SelectedIndex!=0)
            {
                try
                {

                    var connectionString = ConfigurationManager.ConnectionStrings["BD_client.Properties.Settings.MyBDConnectionString"].ConnectionString;
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "getClient_by_type";
                    command.Parameters.AddWithValue("@type", filterType.SelectedIndex);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable result = new DataTable();
                    connection.Open();
                    adapter.Fill(result);
                    clientDataGrid.ItemsSource = result.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка:\n"+ ex.Message);
                }
            }
            else
            {
                clientDataGrid.ItemsSource = myBDDataSet.Client;
            }
        }
        private Font PrintFont;
        private void pd_PrintSingle(object sender, PrintPageEventArgs ev)
        {
            float leftMargin = ev.MarginBounds.Left;
            float yPos = ev.MarginBounds.Top;
            string line;


            var dataRowView = clientDataGrid.SelectedItems[0] as DataRowView;
            
            MyBDDataSet.ClientRow row = (MyBDDataSet.ClientRow)dataRowView.Row;


            PrintFont = new Font("Arial",20);
            line = "Информация о клиенте:";
            ev.Graphics.DrawString(line, PrintFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            yPos += PrintFont.GetHeight(ev.Graphics);


            PrintFont = new Font("Arial", 14);
            line = $"ID = {row.id}\r\n" +
                   $"ФИО:= {row.name}\r\n" +
                   $"Паспорт: {row.passport_Seria}  {row.passport_Num} \r\n";
                  


            ev.Graphics.DrawString(line, PrintFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            yPos += PrintFont.GetHeight(ev.Graphics) * 10;

            ev.HasMorePages = false;
        }

        private void PreviewClientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintPreviewDialog preview = new PrintPreviewDialog();
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(pd_PrintSingle);
                preview.Document = pd;
                preview.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(pd_PrintSingle);
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
