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
	/// Логика взаимодействия для LoginBox.xaml
	/// </summary>
	public partial class LoginBox : UserControl
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
			DependencyProperty.Register("Box", typeof(string), typeof(LoginBox), new PropertyMetadata("",
				new PropertyChangedCallback(BoxChanged)));

		private static void BoxChanged(DependencyObject depObj,
			DependencyPropertyChangedEventArgs args)
		{ }
		#endregion

		#region HitTextProperty
		public string HitText
		{
			get { return (string)GetValue(HitTextProperty); }
			set { SetValue(HitTextProperty, value); }
		}

		// Using a DependencyProperty as the backing store for CatalogBg.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty HitTextProperty =
			DependencyProperty.Register("HitText", typeof(string), typeof(LoginBox), new PropertyMetadata("",
				new PropertyChangedCallback(HitTextChanged)));

		private static void HitTextChanged(DependencyObject depObj,
			DependencyPropertyChangedEventArgs args)
		{ }
		#endregion

		public LoginBox()
		{
			InitializeComponent();
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			Box = textBox.Text;
		}
	}
}
