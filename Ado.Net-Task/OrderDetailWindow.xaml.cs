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
using System.Windows.Shapes;

namespace Ado.Net_Task
{
    /// <summary>
    /// Interaction logic for OrderDetailWindow.xaml
    /// </summary>
    public partial class OrderDetailWindow : Window
    {
        public OrderDetailWindow()
        {
            InitializeComponent();
        }
        public OrderDetailWindow(DataTable dataTable)
        {
            InitializeComponent();
            lbl_quantity.Content = dataTable.Rows[0][0].ToString();
            lbl_discount.Content= dataTable.Rows[0][2].ToString();
            lbl_orderId.Content= dataTable.Rows[0][3].ToString();
            lbl_productId.Content= dataTable.Rows[0][4].ToString();
            lbl_unitPrice.Content= dataTable.Rows[0][1].ToString();

        }
    }
}
