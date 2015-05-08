using System;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Shared
{
	public class MatchGetPersonMeassage : MvxMessage
	{
		public String persondata;
		public bool isCeleb;

		public MatchGetPersonMeassage (object sender,String persondata,bool celeb) : base(sender)
		{
			this.persondata = persondata;
			this.isCeleb = celeb;
		}
	}
}

