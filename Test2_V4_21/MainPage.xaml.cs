using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Test2_V4_21
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            dtpEnterServiceDate.SelectedDate = DateTime.Now.Date;  //Sets today as a default value for the date picker.
        }
        private void btnPrintInvoice_Click(object sender, RoutedEventArgs e)
        {
            //This code obtains the user input from the four input controls. 
            //Please use these variables for Part B of the test.

            DateTime serviceDate = dtpEnterServiceDate.Date.DateTime;
            string custName = txtEnterCustomerName.Text;
            decimal taxRate = decimal.Parse(txtEnterTaxRate.Text);
            decimal[] allCharges = ConvertToDecimalArray(txtEnterServiceCharges.Text);
            try
            {
                //Code here for Part B of the test!
                if (custName == null)
                {
                    MessageDialog dialog = new MessageDialog("Please Enter Customer name");
                    _ = dialog.ShowAsync();
                }
                else if (taxRate > 0.4m)
                {

                    MessageDialog dialog = new MessageDialog("TaxRate is more than 40%");
                    _ = dialog.ShowAsync();
                }
                else if (allCharges == null)
                {
                    MessageDialog dialog = new MessageDialog("TaxRate is more than 40%");
                    _ = dialog.ShowAsync();
                }
                else
                {
                    try
                    {
                        InvoiceInstance invoice = new InvoiceInstance(custName, serviceDate, allCharges, taxRate);
                        txtInvoiceCompanyName.Text = "Patel Industries";
                        txtInvoiceCustomerName.Text = custName;
                        txtInvoiceTotal.Text = invoice.CalculateTotal(taxRate).ToString();
                        txtInvoiceServiceDate.DataContext = serviceDate.ToString();
                        txtInvoiceServiceCharges.Text = invoice.ListServiceCharges();
                    }
                    catch (Exception es)
                    {
                        MessageDialog dialog = new MessageDialog(es.Message);
                        _ = dialog.ShowAsync();
                    }
                }
                
            }
            catch (Exception es)
            {
                MessageDialog dialog = new MessageDialog(es.Message);
                //MessageDialog message = new MessageDialog(es.InnerException.ToString());
                _ = dialog.ShowAsync();
                //_ = message.ShowAsync();
            }
            //This line of code makes the Display Invoice section visible (the reset button hides it)
            DisplayInvoice.Visibility = Visibility.Visible;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                                                                             //
        //              DO NOT MODIFY ANYTHING BELOW THIS LINE!!                                       //
        //                                                                                             //
        /////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Takes a string which should contain string representations of decimal values separated by commas, spaces, and/or line returns.
        /// Breaks this string into an array of decimal values.
        /// </summary>
        /// <param name="valuesList">The input string.</param>
        /// <returns>An array of decimal values extracted from the input string.</returns>
        private decimal[] ConvertToDecimalArray(string valuesList)
        {
            try
            {
                string[] tempValues = valuesList.Split(new char[] { '\r', '\n', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                decimal[] decValues = Array.ConvertAll(tempValues, Convert.ToDecimal);
                return decValues;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            //Clear/reset the user input control values 
            txtEnterCustomerName.Text = "";
            dtpEnterServiceDate.SelectedDate = DateTime.Now.Date;
            txtEnterTaxRate.Text = "0.10";
            txtEnterServiceCharges.Text = "";

            //Hide the display invoice section from view
            DisplayInvoice.Visibility = Visibility.Collapsed;
        }
    }
}
