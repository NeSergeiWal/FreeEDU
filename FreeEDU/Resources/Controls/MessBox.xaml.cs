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
	/// Логика взаимодействия для MessBox.xaml
	/// </summary>
	public partial class MessBox : UserControl
	{
		#region BoxProperty
		public string Box
		{
			get { return (string)GetValue(BoxProperty); }
			set
			{
				SetValue(BoxProperty, value);
			}
		}

		// Using a DependencyProperty as the backing store for CatalogBg.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty BoxProperty =
			DependencyProperty.Register("Box", typeof(string), typeof(MessBox), new PropertyMetadata(null,
				new PropertyChangedCallback(BoxChanged)));

		private static void BoxChanged(DependencyObject depObj,
			DependencyPropertyChangedEventArgs args)
		{ }
		#endregion

		public MessBox()
		{
			InitializeComponent();
		}
	}
}
