using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.ComponentModel;

namespace WPF_Adatkotes_DataSet
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow: Window
  {
    dsHallgatók dsHallgatók = new dsHallgatók();
    public MainWindow()
    {      InitializeComponent();
    }
    private void miBeolvas_Click(object sender, RoutedEventArgs e)
    {  // Párbeszédablak létrehozása.
      var ofdMegnyit = new Microsoft.Win32.OpenFileDialog();
      ofdMegnyit.DefaultExt = ".xml"; // Alapértelmezett kiterjesztés
      // Fájlkiterjesztések szűrője
      ofdMegnyit.Filter = "XML dokumentumok (.xml)|*.xml|Minden dokumentum|*.*";
			ofdMegnyit.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
      // Párbeszédablak megjelenítése.
      if (ofdMegnyit.ShowDialog() == true)
      {  try
        {  // XML dokumentum betöltése. 
          dsHallgatók.ReadXml(ofdMegnyit.FileName, XmlReadMode.Auto);
        }
        catch (Exception)
        { MessageBox.Show("Az XML dokumentum betöltése nem sikerült!\r\n "+
					   "Három mintarekorddal feltöltjük az adatbázist.", "Hiba",
            MessageBoxButton.OK, MessageBoxImage.Error);
					Feltölt();
        }
      }
      else
      {
				// Feltöltjük az adatbázist.
	      Feltölt();
      }
	    Application.Current.Properties["dsHallgatók"] = dsHallgatók;
	    miLista.IsEnabled = true;
    }

		private void Feltölt()
		{
			// Létrehozunk három rekordot.
			dsHallgatók.dtHallgatók.AdddtHallgatókRow("Okoska", "Törp", "DERTFBH.KEFO", 3.5, "ot@net.hu");
			dsHallgatók.dtHallgatók.AdddtHallgatókRow("Duli", "Fuli", "DUFTRPH.KEFO", 4.2, "df@net.hu");
			dsHallgatók.dtHallgatók.AdddtHallgatókRow("Papa", "Törp", "PAPTFBH.KEFO", 4.9, "pt@net.hu");
		}

    private void miMent_Click(object sender, RoutedEventArgs e)
    {
      // Párbeszédablak létrehozása.
      var sfdMent = new Microsoft.Win32.SaveFileDialog();
      sfdMent.DefaultExt = ".xml"; // Alapértelmezett kiterjesztés
      // Fájlkiterjesztések szűrője
      sfdMent.Filter = "XML dokumentumok (.xml)|*.xml|Minden dokumentum|*.*";
      // Párbeszédablak megjelenítése.
      if (sfdMent.ShowDialog() == true)
      {
          // XML dokumentum mentése. 
          dsHallgatók.WriteXml(sfdMent.FileName, XmlWriteMode.WriteSchema);
      }
    }

    private void miKilépés_Click(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown(0);
    }

    

		private void miRekordonként_Click(object sender, RoutedEventArgs e)
		{
			dgRács.Visibility = Visibility.Collapsed;
			ucRekordNezet.Visibility = Visibility.Visible;
		}

		private void miÖsszesAdat_Click(object sender, RoutedEventArgs e)
		{
			ucRekordNezet.Visibility = Visibility.Collapsed;
			dgRács.Visibility = Visibility.Visible;
			dgRács.ItemsSource = dsHallgatók.dtHallgatók;
		}
  }
}
