using System;
using System.Collections.Generic;
using System.Data;
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

namespace BD_client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        BD_client.MyBDDataSet myBDDataSet;

        BD_client.MyBDDataSetTableAdapters.ClientTableAdapter myBDDataSetClientTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Сontract_tableTableAdapter myBDDataSetСontract_tableTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Con_pointTableAdapter myBDDataSetCon_pointTableAdapter;
        BD_client.MyBDDataSetTableAdapters.AdressTableAdapter myBDDataSetAdressTableAdapter;

        BD_client.MyBDDataSetTableAdapters.Service_NetTableAdapter myBDDataSetService_NetTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Service_TVTableAdapter myBDDataSetService_TVTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Service_OtherTableAdapter myBDDataSetService_OtherTableAdapter;

        BD_client.MyBDDataSetTableAdapters.Type_ClTableAdapter myBDDataSetType_ClTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Tv_channelTableAdapter myBDDataSetTv_channelTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Tv_bundleTableAdapter myBDDataSetTv_bundleTableAdapter;
        BD_client.MyBDDataSetTableAdapters.RegionTableAdapter myBDDataSetRegionTableAdapter;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            myBDDataSet = ((BD_client.MyBDDataSet)(this.FindResource("myBDDataSet")));
           

            // Загрузить данные в таблицу Client. Можно изменить этот код как требуется.
            myBDDataSetClientTableAdapter = new BD_client.MyBDDataSetTableAdapters.ClientTableAdapter();
            myBDDataSetClientTableAdapter.Fill(myBDDataSet.Client);
            System.Windows.Data.CollectionViewSource clientViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientViewSource")));
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
            System.Windows.Data.CollectionViewSource view_Client_ServicesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("view_Client_ServicesViewSource")));
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
            System.Windows.Data.CollectionViewSource type_ClViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("type_ClViewSource")));
            type_ClViewSource.View.MoveCurrentToFirst();
            // Загрузить данные в таблицу Region. Можно изменить этот код как требуется.
            myBDDataSetRegionTableAdapter = new BD_client.MyBDDataSetTableAdapters.RegionTableAdapter();
            myBDDataSetRegionTableAdapter.Fill(myBDDataSet.Region);
            System.Windows.Data.CollectionViewSource regionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("regionViewSource")));
            regionViewSource.View.MoveCurrentToFirst();
        }



        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
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


           

                default:
                    throw new ArgumentOutOfRangeException(b.Name);
            }
          
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
 
            switch (b.Name)
            {
                case "ClientDelete":
                    for (int i = 0; i < clientDataGrid.SelectedItems.Count; i++)
                    {
                        var dataRowView = clientDataGrid.SelectedItems[i] as DataRowView;
                        if (dataRowView != null)
                        {
                            DataRow dataRow = dataRowView.Row;
                            dataRow.Delete();
                        }
                    }
                    myBDDataSetClientTableAdapter.Update(myBDDataSet.Client);
                    break;
                case "ContractDelete":
                    for (int i = 0; i < сontractDataGrid.SelectedItems.Count; i++)
                    {
                        var dataRowView = сontractDataGrid.SelectedItems[i] as DataRowView;
                        if (dataRowView != null)
                        {
                            DataRow dataRow = dataRowView.Row;
                            dataRow.Delete();
                        }
                    }
                    myBDDataSetСontract_tableTableAdapter.Update(myBDDataSet.Сontract_table);
                    break;
                case "ConPointDelete":
                    for (int i = 0; i < con_pointDataGrid.SelectedItems.Count; i++)
                    {
                        var dataRowView = con_pointDataGrid.SelectedItems[i] as DataRowView;
                        if (dataRowView != null)
                        {
                            DataRow dataRow = dataRowView.Row;
                            dataRow.Delete();
                        }
                    }
                    myBDDataSetCon_pointTableAdapter.Update(myBDDataSet.Con_point);
                    break;
                case "AdressDelete":
                    for (int i = 0; i < adressDataGrid.SelectedItems.Count; i++)
                    {
                        var dataRowView = adressDataGrid.SelectedItems[i] as DataRowView;
                        if (dataRowView != null)
                        {
                            DataRow dataRow = dataRowView.Row;
                            dataRow.Delete();
                        }
                    }
                    myBDDataSetAdressTableAdapter.Update(myBDDataSet.Adress);
                    break;
                case "ServiceNetDelete":
                    for (int i = 0; i < service_NetDataGrid.SelectedItems.Count; i++)
                    {
                        var dataRowView = service_NetDataGrid.SelectedItems[i] as DataRowView;
                        if (dataRowView != null)
                        {
                            DataRow dataRow = dataRowView.Row;
                            dataRow.Delete();
                        }
                    }
                    myBDDataSetService_NetTableAdapter.Update(myBDDataSet.Service_Net);
                    break;
                case "ServiceTVDelete":
                    for (int i = 0; i < service_TVDataGrid.SelectedItems.Count; i++)
                    {
                        var dataRowView = service_TVDataGrid.SelectedItems[i] as DataRowView;
                        if (dataRowView != null)
                        {
                            DataRow dataRow = dataRowView.Row;
                            dataRow.Delete();
                        }
                    }
                    myBDDataSetService_TVTableAdapter.Update(myBDDataSet.Service_TV);
                    break;
                case "ServiceOtherDelete":
                    for (int i = 0; i < service_OtherDataGrid.SelectedItems.Count; i++)
                    {
                        var dataRowView = service_OtherDataGrid.SelectedItems[i] as DataRowView;
                        if (dataRowView != null)
                        {
                            DataRow dataRow = dataRowView.Row;
                            dataRow.Delete();
                        }
                    }
                    myBDDataSetService_OtherTableAdapter.Update(myBDDataSet.Service_Other);
                    break;
                case "Type_ClSave":
                    for (int i = 0; i < type_ClDataGrid.SelectedItems.Count; i++)
                    {
                        var dataRowView = type_ClDataGrid.SelectedItems[i] as DataRowView;
                        if (dataRowView != null)
                        {
                            DataRow dataRow = dataRowView.Row;
                            dataRow.Delete();
                        }
                    }
                    myBDDataSetType_ClTableAdapter.Update(myBDDataSet.Type_Cl);
                    break;
                case "TV_ChanelSaveDelete":
                    for (int i = 0; i < tv_channelDataGrid.SelectedItems.Count; i++)
                    {
                        var dataRowView = tv_channelDataGrid.SelectedItems[i] as DataRowView;
                        if (dataRowView != null)
                        {
                            DataRow dataRow = dataRowView.Row;
                            dataRow.Delete();
                        }
                    }
                    myBDDataSetTv_channelTableAdapter.Update(myBDDataSet.Tv_channel);
                    break;
                case "TV_BundleDelete":
                    for (int i = 0; i < tv_bundleDataGrid.SelectedItems.Count; i++)
                    {
                        var dataRowView = tv_bundleDataGrid.SelectedItems[i] as DataRowView;
                        if (dataRowView != null)
                        {
                            DataRow dataRow = dataRowView.Row;
                            dataRow.Delete();
                        }
                    }
                    myBDDataSetTv_bundleTableAdapter.Update(myBDDataSet.Tv_bundle);
                    break;
                case "RegionDelete":
                    for (int i = 0; i < regionDataGrid.SelectedItems.Count; i++)
                    {
                        var dataRowView = regionDataGrid.SelectedItems[i] as DataRowView;
                        if (dataRowView != null)
                        {
                            DataRow dataRow = dataRowView.Row;
                            dataRow.Delete();
                        }
                    }
                    myBDDataSetRegionTableAdapter.Update(myBDDataSet.Region);
                    break;


                default:
                    throw new ArgumentOutOfRangeException(b.Name);

            }
        }
    }
}
