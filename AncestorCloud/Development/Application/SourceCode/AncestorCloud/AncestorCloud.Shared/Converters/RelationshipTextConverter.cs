using System;
using Cirrious.CrossCore.Converters;

namespace AncestorCloud.Shared
{
	public class RelationshipTextConverter : MvxValueConverter<String,String>
	{
		string _gender;

		public RelationshipTextConverter(string gender)
		{
			_gender = gender;
		}

		protected override String Convert(String value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (! value.ToLower ().Equals (AppConstant.Sibling_comparison.ToLower ())) {
				if (_gender.Equals (AppConstant.MALE))
					return "(Father)";
				else if (_gender.Equals (AppConstant.FEMALE))
					return "(Mother)";
			}

			return "(" + value + ")";
		}
	}
}


