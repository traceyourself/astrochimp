// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Message type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using AncestorCloud.Shared;


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
		public MyTableCellTappedMessage(object sender,People famMember)
			: base(sender)
		{
			FamilyMember = famMember;
		}


		public People FamilyMember{ get; set;}
	}
}