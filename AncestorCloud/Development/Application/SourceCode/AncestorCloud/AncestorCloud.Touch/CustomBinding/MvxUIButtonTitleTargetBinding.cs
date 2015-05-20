using System;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding.Bindings.Target;
using UIKit;
using Cirrious.MvvmCross.Binding;

namespace AncestorCloud.Touch
{
	public class MvxUIButtonTitleTargetBinding : MvxConvertingTargetBinding
	{
		protected UIButton Button
		{
			get { return base.Target as UIButton; }
		}

		public MvxUIButtonTitleTargetBinding(UIButton button)
			: base(button)
		{
			if (button == null)
			{
				MvxBindingTrace.Trace(MvxTraceLevel.Error, "Error - UIButton is null in MvxUIButtonTitleTargetBinding");
			}
		}

		public override MvxBindingMode DefaultMode
		{
			get { return MvxBindingMode.OneWay; }
		}

		public override System.Type TargetType
		{
			get { return typeof (string); }
		}

		protected override void SetValueImpl(object target, object value)
		{
			((UIButton)target).SetTitle(value as string, UIControlState.Normal);
		}
	}
}

