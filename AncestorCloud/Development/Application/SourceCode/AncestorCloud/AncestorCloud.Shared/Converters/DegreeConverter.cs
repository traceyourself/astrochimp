using System;
using Cirrious.CrossCore.Converters;

namespace AncestorCloud.Shared
{
	public class DegreeConverter : MvxValueConverter<int,String>
	{
		protected override String Convert(int value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value.ToString()+"º";
		}
	}
}