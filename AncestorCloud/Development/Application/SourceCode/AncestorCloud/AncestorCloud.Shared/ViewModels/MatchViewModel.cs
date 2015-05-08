using System;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Shared.ViewModels
{
	public class MatchViewModel:BaseViewModel
	{
		private readonly IMatchService _matchService;
		private readonly IAlert _alert;
		private readonly IDatabaseService _databaseService;
		private MvxSubscriptionToken matcherChosenToken;
		IMvxMessenger _matcherMessenger;
		public Celebrity FirstPersonCeleb,SecondPersonCeleb;
		public People FirstPersonPeople,SecondPersonPeople;


		public MatchViewModel()
		{
			
			_matchService = Mvx.Resolve<IMatchService> ();	
			_databaseService = Mvx.Resolve<IDatabaseService> ();

			_alert = Mvx.Resolve<IAlert> ();


			_matcherMessenger = Mvx.Resolve<IMvxMessenger>();
			matcherChosenToken = _matcherMessenger.Subscribe<MatchGetPersonMeassage>(message => {
				HandleSelectedPerson(message);
			});
		}

		public void HandleSelectedPerson(MatchGetPersonMeassage message)
		{
			if (message.isCeleb) {
				if(WhichImageClicked == 1) {
					IMvxJsonConverter converter = Mvx.Resolve<IMvxJsonConverter>();
					ResponseModel<Celebrity> modelRcvd = converter.DeserializeObject<ResponseModel<Celebrity>>(message.persondata);
					FirstPersonCeleb = modelRcvd.Content;

				}else {
					IMvxJsonConverter converter = Mvx.Resolve<IMvxJsonConverter>();
					ResponseModel<Celebrity> modelRcvd = converter.DeserializeObject<ResponseModel<Celebrity>>(message.persondata);
					SecondPersonCeleb = modelRcvd.Content;
					//Mvx.Trace("celeb name in match view model for sec image: "+SecondPersonCeleb.GivenNames);
				}
			} else {
				if(WhichImageClicked == 1) {
					IMvxJsonConverter converter = Mvx.Resolve<IMvxJsonConverter>();
					ResponseModel<People> modelRcvd = converter.DeserializeObject<ResponseModel<People>>(message.persondata);
					FirstPersonPeople = modelRcvd.Content;
					//Mvx.Trace("People name in match view model for first image: "+FirstPersonPeople.Name);
				}else {
					IMvxJsonConverter converter = Mvx.Resolve<IMvxJsonConverter>();
					ResponseModel<People> modelRcvd = converter.DeserializeObject<ResponseModel<People>>(message.persondata);
					SecondPersonPeople = modelRcvd.Content;
					//Mvx.Trace("People name in match view model for Sec image: "+SecondPersonPeople.Name);
				}		
			}
		}

		public class MatchParameter
		{
			public String MatchUser { get; set;}

		}


		#region Relationship View

		public void ShowRelationshipMatchDetailViewModel()
		{
			//ShowViewModel<RelationshipMatchDetailViewModel> ();
			MatchService();
		}
		#endregion


		#region friendList
		public void ShowFriendList()
		{
			System.Diagnostics.Debug.WriteLine ("Tapped:");
			ShowViewModel<AddFriendViewModel> ();
		}
		#endregion

		#region Close Method
		public void Close()
		{
			UnsubscribeMessages ();
			this.Close(this);
		}
		#endregion

		#region unsubscribe messenger
		public void UnsubscribeMessages ()
		{
			_matcherMessenger.Unsubscribe<MatchGetPersonMeassage> (matcherChosenToken);
		}
		#endregion

		#region Family Call Method
		public void ShowFamilyView()
		{
			ShowViewModel<FamilyViewModel>();
			this.Close(this);
		}
		#endregion

		#region call Reseach help
		public void ShowResearchHelpViewModel()
		{
			ShowViewModel<ResearchHelpViewModel> ();
		}
		#endregion


		#region show past matches
		public void ShowPastMatchesViewModel()
		{
			ShowViewModel<PastMatchesViewModel> ();
		}
		#endregion


		#region logout
		public void Logout()
		{
			ShowViewModel<HomePageViewModel>();
			this.Close(this);
		}
		#endregion

		public void ShowPastMatches()
		{
			ShowViewModel<NoMatchesViewModel> ();
		}


		#region property holding the clicked image 
		public int WhichImageClicked{ get; set;}
		#endregion

		#region Ogfn holders
		public String FirstPersonOgfn{ get; set;}
		public String SecondPersonOgfn{ get; set;}
		#endregion

		#region MATCH Service
		public async void MatchService()
		{
			if (Validate ()) {

				LoginModel data = _databaseService.GetLoginDetails ();

				ResponseModel<RelationshipFindResult> result = await _matchService.Match (data.Value, "747510545", "747227929");

				if (result.Status == ResponseStatus.OK) {
					if (result.Content.Found) {
						Close();
						var matchString = Mvx.Resolve<IMvxJsonConverter> ().SerializeObject (result.Content);

						ShowViewModel<RelationshipMatchDetailViewModel> (new RelationshipMatchDetailViewModel.DetailParameter { MatchResult = matchString });
					} else 
					{
						//TODO: Show No Match Screen
						_alert.ShowAlert("Oops!! No match found. Try again", "Matcher");
					}
				}
			} 
		}
		#endregion


		#region Validation
		public bool Validate()
		{
			bool isValid = true;


			if(FirstPersonCeleb != null || FirstPersonPeople != null){

				if(FirstPersonCeleb != null){
					FirstPersonOgfn = FirstPersonCeleb.OGFN;
				}else if(FirstPersonPeople != null){
					FirstPersonOgfn = FirstPersonPeople.IndiOgfn;
				}


				if (SecondPersonCeleb == null && SecondPersonPeople == null) {
					Alert.ShowAlert("Please Select Second Person to match","Person not selected");
					isValid = false;
				} else {
					if(SecondPersonCeleb != null){
						SecondPersonOgfn = SecondPersonCeleb.OGFN;
					}else if(SecondPersonPeople != null){
						SecondPersonOgfn = SecondPersonPeople.IndiOgfn;
					}
				}
			}else{
				Alert.ShowAlert("Please Select First Person to match","Person not selected");
				isValid = false;
			}
			return isValid;
		}
		#endregion
	}
}

