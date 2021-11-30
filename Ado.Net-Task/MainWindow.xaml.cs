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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ado.Net_Task
{
    
    public partial class MainWindow : Window
    {
        SqlConnection conn = null;
        DataTable dataTable = null;
        SqlDataAdapter dataAdapter = null;
        public MainWindow()
        {
            InitializeComponent();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString);
            ReadData();
        }

        public void ReadData()
        {
            dataAdapter = new SqlDataAdapter("Select OrderId From Orders", conn);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            for (int i = 0; i < dataTable.Rows.Count; i++) cmx_Order.Items.Add(dataTable.Rows[i][0]);

            dataAdapter = new SqlDataAdapter(@"Select Orders.OrderID,Customers.CompanyName,ProductName From Orders INNER JOIN Customers
                                            on Orders.CustomerID=Customers.CustomerID INNER JOIN [Order Details]
                                            on Orders.OrderID=[Order Details].OrderID INNER JOIN Products
                                            on [Order Details].ProductID=Products.ProductID", conn);
            cmx_Order.SelectedIndex = 0;
        }

        private void cmx_Order_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataAdapter = new SqlDataAdapter(@"Select Customers.CompanyName,ProductName From Orders INNER JOIN Customers
                                               on Orders.CustomerID=Customers.CustomerID INNER JOIN [Order Details]
                                               on Orders.OrderID=[Order Details].OrderID INNER JOIN Products
                                               on [Order Details].ProductID=Products.ProductID
                                               Where Orders.OrderID="+cmx_Order.SelectedItem.ToString()+"", conn);
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            datagrid.ItemsSource = dataTable.AsDataView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedItem == null) return;
            DataRowView Data = (DataRowView)datagrid.SelectedItem;

            dataAdapter = new SqlDataAdapter(@"Select [Order Details].Quantity,[Order Details].UnitPrice,[Order Details].Discount,[Order Details].OrderID,[Order Details].ProductID
                                               From Orders INNER JOIN Customers
                                               on Orders.CustomerID=Customers.CustomerID INNER JOIN [Order Details]
                                               on Orders.OrderID=[Order Details].OrderID INNER JOIN Products
                                               on [Order Details].ProductID=Products.ProductID
                                               Where Products.ProductName = @P", conn);
            dataAdapter.SelectCommand.Parameters.AddWithValue("P", Data.Row[1].ToString());
            dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            OrderDetailWindow orderDetail = new OrderDetailWindow(dataTable);
            orderDetail.ShowDialog();

        }
       
    }
}
