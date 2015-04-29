// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Message type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace AncestorCloud.Touch
{
	using Cirrious.MvvmCross.Plugins.Messenger;

	/// <summary>
	///    Defines the Message type.
	/// </summary>
	public class MyTableCellTappedMessage : MvxMessage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Message"/> class.
		/// </summary>
		/// <param name="sender">The sender.</param>
		public MyTableCellTappedMessage(object sender)
			: base(sender)
		{
		}
	}
}