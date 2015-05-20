using System;
using Cirrious.CrossCore.Converters;

namespace AncestorCloud.Shared
{
	public class SlashRemoveConverter : MvxValueConverter<String,String>
	{
		protected override String Convert(String value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string val = value.Replace ("/","");

			return val;
		}
	}
}