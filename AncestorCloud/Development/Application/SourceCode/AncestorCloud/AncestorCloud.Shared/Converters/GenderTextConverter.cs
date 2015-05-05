using System;
using Cirrious.CrossCore.Converters;

namespace AncestorCloud.Shared
{
	public class GenderTextConverter : MvxValueConverter<string, int>
	{
		protected override int Convert (string value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			int gender = 0;

			if (value == null)
				return gender;

			if(value.Equals("female") || value.Equals("Female"))
			{
				gender = 1;
			}

			return gender;
		}

		protected override string ConvertBack (int value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string gender = "Male";

			switch (value)
			{
				case 1:
					gender = "Female";
					break;
			}

			return gender;
		}
	}
}

