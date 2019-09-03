using System;
using System.Runtime.Serialization;

namespace WebApplication1.Shared
{
	[DataContract]
	public class Buster75User
	{
		public enum UserStatusFlag
		{
			Unknown = 0,
			Created = 1,
			Verified = 2,
			ValidLicence = 4,
			ExpiredLicence = 8,
			Blocked = 512,
			DemoLicence = 1024,
			RemovedGDPR = 2048
		}

		public enum UserBlockedStatus
		{
			NotSet = 0,
			TooManyFailedLogins = 1,
			UserRequest = 2,
			BySystem = 4,
			Fraud = 8,
			RemovedGDPR = 128
		}

		public class Roles
		{
			public const string User = "User";
			public const string InvalidLicence = "InvalidLicence";
		}

		[DataMember]
		public Guid? UserId { get; set; }
		[DataMember]
		public string Username { get; set; }        // same as Email usually
		[DataMember]
		public string Password { get; set; }
		[DataMember]
		public string Email { get; set; }           // same as Username usually
		[DataMember]
		public DateTime DateOfBirth { get; set; }
		[DataMember]
		public DateTime RegisterDate { get; set; }
		[DataMember]
		public DateTime LicenceValidUntil { get; set; }
		[DataMember]
		public DateTime LastLogin { get; set; }
		[DataMember]
		public int Status { get; set; }
		[DataMember]
		public DateTime VerifiedDate { get; set; }
		[DataMember]
		public string FirstName { get; set; }
		[DataMember]
		public string LastName { get; set; }
		[DataMember]
		public int MobilePhoneCountryCode { get; set; }
		[DataMember]
		public string MobilePhoneNr { get; set; }
		[DataMember]
		public bool NewsletterEmail { get; set; }
		[DataMember]
		public bool NewsletterSMS { get; set; }
		[DataMember]
		public bool NotifyByEmail { get; set; }
		[DataMember]
		public bool NotifyBySMS { get; set; }
		[DataMember]
		public UserBlockedStatus BlockedStatus { get; set; }
		[DataMember]
		public string BlockedReason { get; set; }
		[DataMember]
		public string AlternativeEmail { get; set; }
		[DataMember]
		public string ExternalAuthenticator { get; set; }       // Google, Facebook, Microsoft, ... 
		[DataMember]
		public string ExternalAuthenticatorData { get; set; }       // Json , or seperated string of values..

		[DataMember]
		public string UsernameHash { get; set; }
		[DataMember]
		public string EmailHash { get; set; }
		[DataMember]
		public string MobileHash { get; set; }

		// not serialized
		public bool IsVerified { get => this.HasStatus(UserStatusFlag.Verified); }
		public bool LicenceValid { get => this.HasStatus(UserStatusFlag.ValidLicence) && !this.HasStatus(UserStatusFlag.ExpiredLicence); }
		public bool IsBlocked { get => this.HasStatus(UserStatusFlag.Blocked); }
		public string MobileNrString { get => this.MobilePhoneCountryCode + this.MobilePhoneNr.ToUpper().Trim(); }

		public Buster75User()
		{ }

		public Buster75User Copy()
		{
			return (Buster75User)this.MemberwiseClone();
		}

		public bool HasStatus(UserStatusFlag pFlag)
		{
			return (this.Status & (int)pFlag) == (int)pFlag;
		}
		public void SetStatus(UserStatusFlag pFlag)
		{
			this.Status |= (int)pFlag;
		}
		public void RemoveStatus(UserStatusFlag pFlag)
		{
			this.Status &= ~(int)pFlag;
		}

	}
}
