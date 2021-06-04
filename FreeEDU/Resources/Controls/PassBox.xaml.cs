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

namespace FreeEDU.Resources.Controls
{
	/// <summary>
	/// Логика взаимодействия для PassBox.xaml
	/// </summary>
	public partial class PassBox : UserControl
	{
		#region BoxProperty
		public string Box
		{
			get { return (string)GetValue(BoxProperty); }
			set 
			{ 
				SetValue(BoxProperty, value);

				hitBox.Visibility = (Box == string.Empty) ? Visibility.Visible : Visibility.Hidden;
			}
		}

		// Using a DependencyProperty as the backing store for CatalogBg.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty BoxProperty =
			DependencyProperty.Register("Box", typeof(string), typeof(PassBox), new PropertyMetadata("",
				new PropertyChangedCallback(BoxChanged)));

		private static void BoxChanged(DependencyObject depObj,
			DependencyPropertyChangedEventArgs args) { }
		#endregion

		public PassBox()
		{
			InitializeComponent();
		}

		private void passBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			PasswordBox passwordBox = sender as PasswordBox;
			Box = passwordBox.Password; 
		}
	}
}
