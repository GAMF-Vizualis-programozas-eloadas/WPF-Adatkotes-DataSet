using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPF_Adatkotes_DataSet
{
	/// <summary>
	/// Interaction logic for ucRekordNezet.xaml
	/// </summary>
	public partial class ucRekordNezet : UserControl
	{
		private dsHallgatók dsHallgatók;
		public ucRekordNezet()
		{
			InitializeComponent();
		}
		private void btLe_Click(object sender, RoutedEventArgs e)
		{
			ICollectionView cv =
				CollectionViewSource.GetDefaultView(dsHallgatók.dtHallgatók);
			// Ha van hova visszalépni.
			if (cv.CurrentPosition > 0)
				cv.MoveCurrentToPrevious();
			else
				// Ha nem (azaz az aktuális az első), akkor ugrás az utolsóra.
				cv.MoveCurrentToLast();
		}

		private void btFel_Click(object sender, RoutedEventArgs e)
		{
			ICollectionView cv = CollectionViewSource.GetDefaultView(dsHallgatók.dtHallgatók);
			// Ha még van elem a listában akkor előre lépünk
			if (cv.CurrentPosition < dsHallgatók.dtHallgatók.Rows.Count - 1)
				cv.MoveCurrentToNext();
			else
				// Ha nem (azaz az aktuális az utolsó), akkor ugrás az elsőre
				cv.MoveCurrentToFirst();
		}

		private void btLegkisebbÁtlag_Click(object sender, RoutedEventArgs e)
		{
			// Átlag alapján növekvő sorrendbe rendezi a listaelemeket
			dsHallgatók.dtHallgatók.DefaultView.Sort =
				"GöngyölítettÁtlag ASC";
			ICollectionView cv =
				CollectionViewSource.GetDefaultView(dsHallgatók.dtHallgatók);
			// Ugrás a lista első elemére
			cv.MoveCurrentToFirst();
		}

		private void btLegnagyobbÁtlag_Click(object sender, RoutedEventArgs e)
		{
			// Átlag alapján növekvő sorrendbe rendezi a listaelemeket
			dsHallgatók.dtHallgatók.DefaultView.Sort =
				"GöngyölítettÁtlag ASC";
			ICollectionView cv =
				CollectionViewSource.GetDefaultView(dsHallgatók.dtHallgatók);
			// Ugrás a lista utolsó elemére
			cv.MoveCurrentToLast();
		}

		private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if(!IsVisible) return;
			dsHallgatók = 
				Application.Current.Properties["dsHallgatók"] as dsHallgatók;
			grRács.DataContext = dsHallgatók.dtHallgatók;
		}
	}
}
