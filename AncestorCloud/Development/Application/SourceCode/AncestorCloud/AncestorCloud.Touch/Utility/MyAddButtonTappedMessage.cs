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
	public class MyAddButtonTappedMessage : MvxMessage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Message"/> class.
		/// </summary>
		/// <param name="sender">The sender.</param>
		public MyAddButtonTappedMessage(object sender,Celebrity famMember)
			: base(sender)
		{
			FamilyMember = famMember;
		}


		public Celebrity FamilyMember{ get; set;}
	}
}