using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestClient
{
    public partial class frmProducts : Form
    {
        ProductServiceReference.ProductServiceClient client =
            new ProductServiceReference.ProductServiceClient(); //proxy object

        public frmProducts()
        {
            InitializeComponent();
        }

        //FILL GRIDVIEW ON FORM LOAD
        private void frmProducts_Load(object sender, EventArgs e)
        {
            this.LoadAllProducts();
        }

        //SEARCH BY PARTIAL NAME AND DISPLAY IN GRIDVIEW
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtPartialName.Text == "")
            {
                MessageBox.Show("Search text is a required field.", "Entry Error");
                txtPartialName.Focus();
            }
            else
            {
                string searchName = txtPartialName.Text;

                try
                {
                    //get list of matching products
                    ProductServiceReference.Product[] results = client.GetProductsByPartialName(searchName);

                    productDataGridView.DataSource = null;
                    productDataGridView.DataSource = results;
                }
                catch (FaultException ex)
                {
                    if (ex.Code.Name == "Database Error")
                        MessageBox.Show(ex.Reason.ToString(), ex.Code.Name);
                    else if (ex.Code.Name == "Error")
                        MessageBox.Show(ex.Reason.ToString(), ex.Code.Name);
                }
            }
        }

        //RELOAD ALL DATA INTO GRIDVIEW
        private void btnViewAll_Click(object sender, EventArgs e)
        {
            productDataGridView.DataSource = null;
            this.LoadAllProducts();
        }

        //METHOD TO LOAD DATA
        private void LoadAllProducts()
        {
            try
            {
                ProductServiceReference.Product[] products = client.GetAllProducts();

                foreach (ProductServiceReference.Product p in products)
                {
                    productDataGridView.DataSource = products;
                }
            }
            catch (FaultException ex)
            {
                if (ex.Code.Name == "Database Error")
                    MessageBox.Show(ex.Reason.ToString(), ex.Code.Name);
                else if (ex.Code.Name == "Error")
                    MessageBox.Show(ex.Reason.ToString(), ex.Code.Name);
            }
        }

        //CLOSE PROGRAM
        private void btnExit_Click(object sender, EventArgs e)
        {
            client.Close();
            this.Close();
        }

    } //END CLASS
}
