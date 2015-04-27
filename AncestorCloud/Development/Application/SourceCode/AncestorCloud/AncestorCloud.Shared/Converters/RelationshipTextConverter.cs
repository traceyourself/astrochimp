using System;
using Cirrious.CrossCore.Converters;

namespace AncestorCloud.Shared
{
	public class RelationshipTextConverter : MvxValueConverter<String,String>
	{
		protected override String Convert(String value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return "("+value+")";
		}
	}
}


