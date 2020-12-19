using System;
using System.Collections.Generic;
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
        BD_client.MyBDDataSetTableAdapters.Type_ClTableAdapter myBDDataSetType_ClTableAdapter;
        BD_client.MyBDDataSetTableAdapters.ClientTableAdapter myBDDataSetClientTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Сontract_tableTableAdapter myBDDataSetСontract_tableTableAdapter;
        BD_client.MyBDDataSetTableAdapters.Con_pointTableAdapter myBDDataSetCon_pointTableAdapter;
        BD_client.MyBDDataSetTableAdapters.AdressTableAdapter myBDDataSetAdressTableAdapter;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            myBDDataSet = ((BD_client.MyBDDataSet)(this.FindResource("myBDDataSet")));
            // Загрузить данные в таблицу Type_Cl. Можно изменить этот код как требуется.
            myBDDataSetType_ClTableAdapter = new BD_client.MyBDDataSetTableAdapters.Type_ClTableAdapter();
            myBDDataSetType_ClTableAdapter.Fill(myBDDataSet.Type_Cl);
            System.Windows.Data.CollectionViewSource type_ClViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("type_ClViewSource")));
            type_ClViewSource.View.MoveCurrentToFirst();

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

            // Загрузить данные в таблицу Region. Можно изменить этот код как требуется.
            BD_client.MyBDDataSetTableAdapters.RegionTableAdapter myBDDataSetRegionTableAdapter = new BD_client.MyBDDataSetTableAdapters.RegionTableAdapter();
            myBDDataSetRegionTableAdapter.Fill(myBDDataSet.Region);
            System.Windows.Data.CollectionViewSource regionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("regionViewSource")));
            regionViewSource.View.MoveCurrentToFirst();

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

            }
          
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
