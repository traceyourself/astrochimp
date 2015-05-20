﻿using System;
using Cirrious.CrossCore.Converters;

namespace AncestorCloud.Shared
{
	public class CheckboxConverter : MvxValueConverter<bool,bool>
	{
		protected override bool Convert (bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value = !value;
		}
	}
}

