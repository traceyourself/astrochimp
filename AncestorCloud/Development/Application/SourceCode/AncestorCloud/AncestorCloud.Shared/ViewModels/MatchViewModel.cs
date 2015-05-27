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
		private readonly IReachabilityService _reachabilityService;


		public MatchViewModel(IReachabilityService reachabilty)
		{
			
			_matchService = Mvx.Resolve<IMatchService> ();	
			_databaseService = Mvx.Resolve<IDatabaseService> ();

			_alert = Mvx.Resolve<IAlert> ();


			_matcherMessenger = Mvx.Resolve<IMvxMessenger>();
			matcherChosenToken = _matcherMessenger.Subscribe<MatchGetPersonMeassage>(message => {
				HandleSelectedPerson(message);
			});

			_reachabilityService = reachabilty;
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

		#region get Userdata method
		public LoginModel GetUserData()
		{
			LoginModel data = new LoginModel ();
			try{
				data = _databaseService.GetLoginDetails ();
			}catch(Exception e){
			}
			return data;
		}
		#endregion


		#region friendList
		public void ShowFriendList()
		{
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
		public String FirstPersonImageUrl{ get; set;}
		public String SecondPersonImageUrl{ get; set;}
		public String FirstPersonTag{ get; set;}
		public String SecondPersonTag{ get; set;}
		public String FirstPersonImageName{ get; set;}
		public String SecondPersonImageName{ get; set;}
		#endregion

		#region Ogfn holders
		public String FirstPersonOgfn{ get; set;}
		public String SecondPersonOgfn{ get; set;}

		#endregion

		#region MATCH Service
		public async void MatchService()
		{
			if (Validate ()) {

				if (_reachabilityService.IsNetworkNotReachable ()) {
					Mvx.Resolve<IAlert>().ShowAlert (AlertConstant.INTERNET_ERROR_MESSAGE, AlertConstant.INTERNET_ERROR);
				}

				LoginModel data = _databaseService.GetLoginDetails ();

				ResponseModel<RelationshipFindResult> result = await _matchService.Match (data.Value,FirstPersonOgfn,SecondPersonOgfn);//"747510545", "747227929");

				if (result.Status == ResponseStatus.OK) {
					if (result.Content.Found) {
//						if (!Mvx.CanResolve<IAndroidService> ()) 
//						{
//							Close();
//						}

						var matchString = Mvx.Resolve<IMvxJsonConverter> ().SerializeObject (result.Content);

						ShowViewModel<RelationshipMatchDetailViewModel> (new RelationshipMatchDetailViewModel.DetailParameter { MatchResult = matchString ,FirstPersonUrl = FirstPersonImageUrl,SecondPersonUrl = SecondPersonImageUrl,FirstPersonName= FirstPersonImageName,SecondPersonName= SecondPersonImageName,FirstPersonTag = FirstPersonTag, SecondPersonTag=SecondPersonTag});
					} else 
					{
						//TODO: Show No Match Screen
						_alert.ShowAlert(AlertConstant.MATCH_ERROR_MESSAGE, AlertConstant.MATCH_ERROR);
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

				if (FirstPersonCeleb != null) {
					FirstPersonOgfn = FirstPersonCeleb.OGFN;
					FirstPersonImageUrl = FirstPersonCeleb.Img;
					FirstPersonImageName = FirstPersonCeleb.GivenNames;
					FirstPersonTag = FirstPersonCeleb.Tag;

				} else if (FirstPersonPeople != null) {
					FirstPersonOgfn = FirstPersonPeople.IndiOgfn;
					FirstPersonImageUrl = FirstPersonPeople.ProfilePicURL;
					FirstPersonTag = FirstPersonPeople.Tag;
					FirstPersonImageName = FirstPersonPeople.FirstName;
					if(FirstPersonImageName == null){
						FirstPersonImageName = FirstPersonPeople.Email;
					}
				}


				if (SecondPersonCeleb == null && SecondPersonPeople == null) {
					_alert.ShowAlert(AlertConstant.MATCH_SEC_PERSON_ERROR_MESSAGE,AlertConstant.MATCH_SEC_PERSON_ERROR);
					isValid = false;
				} else {
					if(SecondPersonCeleb != null){
						SecondPersonOgfn = SecondPersonCeleb.OGFN;
						SecondPersonImageUrl = SecondPersonCeleb.Img;
						SecondPersonTag = SecondPersonCeleb.Tag;
						SecondPersonImageName = SecondPersonCeleb.GivenNames;
					}else if(SecondPersonPeople != null){
						SecondPersonOgfn = SecondPersonPeople.IndiOgfn;
						SecondPersonImageUrl = SecondPersonPeople.ProfilePicURL;
						SecondPersonTag = SecondPersonPeople.Tag;
						SecondPersonImageName = SecondPersonPeople.FirstName;
						if(SecondPersonImageName == null){
							SecondPersonImageName = SecondPersonPeople.Email;
						}
					}
				}
			}else{
				_alert.ShowAlert(AlertConstant.MATCH_FIRST_PERSON_MESSAGE,AlertConstant.MATCH_FIRST_PERSON_ERROR);
				isValid = false;
			}
			return isValid;
		}
		#endregion

		#region ProfilePic
		public void ShowProfilePicModel()
		{
			ShowViewModel<ProfilePicViewModel> (new ProfilePicViewModel.DetailParameter { FromSignUp = false });
		}
		#endregion

	}
}

