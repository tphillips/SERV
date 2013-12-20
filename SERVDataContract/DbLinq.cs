
namespace SERVDataContract.DbLinq
{
	using System;
	using System.ComponentModel;
	using System.Data;
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Diagnostics;
	
	
	public partial class SERVDB : DataContext
	{
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		#endregion
		
		
		public SERVDB(string connectionString) : 
				base(connectionString)
		{
			this.OnCreated();
		}
		
		public SERVDB(IDbConnection connection) : 
				base(connection)
		{
			this.OnCreated();
		}
		
		public SERVDB(string connection, MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			this.OnCreated();
		}
		
		public SERVDB(IDbConnection connection, MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			this.OnCreated();
		}
		
		public Table<Availability> Availability
		{
			get
			{
				return this.GetTable <Availability>();
			}
		}
		
		public Table<Location> Location
		{
			get
			{
				return this.GetTable <Location>();
			}
		}
		
		public Table<Member> Member
		{
			get
			{
				return this.GetTable <Member>();
			}
		}
		
		public Table<MemberStatus> MemberStatus
		{
			get
			{
				return this.GetTable <MemberStatus>();
			}
		}
		
		public Table<MemberTag> MemberTag
		{
			get
			{
				return this.GetTable <MemberTag>();
			}
		}
		
		public Table<Message> Message
		{
			get
			{
				return this.GetTable <Message>();
			}
		}
		
		public Table<MessageType> MessageType
		{
			get
			{
				return this.GetTable <MessageType>();
			}
		}
		
		public Table<Product> Product
		{
			get
			{
				return this.GetTable <Product>();
			}
		}
		
		public Table<RawRunLog> RawRunLog
		{
			get
			{
				return this.GetTable <RawRunLog>();
			}
		}
		
		public Table<RunLog> RunLog
		{
			get
			{
				return this.GetTable <RunLog>();
			}
		}
		
		public Table<RunLogProduct> RunLogProduct
		{
			get
			{
				return this.GetTable <RunLogProduct>();
			}
		}
		
		public Table<Tag> Tag
		{
			get
			{
				return this.GetTable <Tag>();
			}
		}
		
		public Table<User> User
		{
			get
			{
				return this.GetTable <User>();
			}
		}
		
		public Table<UserLevel> UserLevel
		{
			get
			{
				return this.GetTable <UserLevel>();
			}
		}
		
		public Table<VehicleType> VehicleType
		{
			get
			{
				return this.GetTable <VehicleType>();
			}
		}
	}
	
	[Table(Name="SERV.Availability")]
	public partial class Availability : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _availabilityID;
		
		private System.Nullable<sbyte> _available;
		
		private System.Nullable<int> _dayNo;
		
		private System.Nullable<int> _eveningNo;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAvailabilityIDChanged();
		
		partial void OnAvailabilityIDChanging(int value);
		
		partial void OnAvailableChanged();
		
		partial void OnAvailableChanging(System.Nullable<sbyte> value);
		
		partial void OnDayNoChanged();
		
		partial void OnDayNoChanging(System.Nullable<int> value);
		
		partial void OnEveningNoChanged();
		
		partial void OnEveningNoChanging(System.Nullable<int> value);
		#endregion
		
		
		public Availability()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_availabilityID", Name="AvailabilityID", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int AvailabilityID
		{
			get
			{
				return this._availabilityID;
			}
			set
			{
				if ((_availabilityID != value))
				{
					this.OnAvailabilityIDChanging(value);
					this.SendPropertyChanging();
					this._availabilityID = value;
					this.SendPropertyChanged("AvailabilityID");
					this.OnAvailabilityIDChanged();
				}
			}
		}
		
		[Column(Storage="_available", Name="Available", DbType="tinyint(1)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<sbyte> Available
		{
			get
			{
				return this._available;
			}
			set
			{
				if ((_available != value))
				{
					this.OnAvailableChanging(value);
					this.SendPropertyChanging();
					this._available = value;
					this.SendPropertyChanged("Available");
					this.OnAvailableChanged();
				}
			}
		}
		
		[Column(Storage="_dayNo", Name="DayNo", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> DayNo
		{
			get
			{
				return this._dayNo;
			}
			set
			{
				if ((_dayNo != value))
				{
					this.OnDayNoChanging(value);
					this.SendPropertyChanging();
					this._dayNo = value;
					this.SendPropertyChanged("DayNo");
					this.OnDayNoChanged();
				}
			}
		}
		
		[Column(Storage="_eveningNo", Name="EveningNo", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> EveningNo
		{
			get
			{
				return this._eveningNo;
			}
			set
			{
				if ((_eveningNo != value))
				{
					this.OnEveningNoChanging(value);
					this.SendPropertyChanging();
					this._eveningNo = value;
					this.SendPropertyChanged("EveningNo");
					this.OnEveningNoChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="SERV.Location")]
	public partial class Location : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private sbyte _bloodBank;
		
		private sbyte _changeover;
		
		private sbyte _enabled;
		
		private sbyte _hospital;
		
		private string _lat;
		
		private string _lng;
		
		private string _location1;
		
		private int _locationID;
		
		private EntitySet<RunLog> _runLog;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnBloodBankChanged();
		
		partial void OnBloodBankChanging(sbyte value);
		
		partial void OnChangeoverChanged();
		
		partial void OnChangeoverChanging(sbyte value);
		
		partial void OnEnabledChanged();
		
		partial void OnEnabledChanging(sbyte value);
		
		partial void OnHospitalChanged();
		
		partial void OnHospitalChanging(sbyte value);
		
		partial void OnLatChanged();
		
		partial void OnLatChanging(string value);
		
		partial void OnLngChanged();
		
		partial void OnLngChanging(string value);
		
		partial void OnLocation1Changed();
		
		partial void OnLocation1Changing(string value);
		
		partial void OnLocationIDChanged();
		
		partial void OnLocationIDChanging(int value);
		#endregion
		
		
		public Location()
		{
			_runLog = new EntitySet<RunLog>(new Action<RunLog>(this.RunLog_Attach), new Action<RunLog>(this.RunLog_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_bloodBank", Name="BloodBank", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public sbyte BloodBank
		{
			get
			{
				return this._bloodBank;
			}
			set
			{
				if ((_bloodBank != value))
				{
					this.OnBloodBankChanging(value);
					this.SendPropertyChanging();
					this._bloodBank = value;
					this.SendPropertyChanged("BloodBank");
					this.OnBloodBankChanged();
				}
			}
		}
		
		[Column(Storage="_changeover", Name="Changeover", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public sbyte Changeover
		{
			get
			{
				return this._changeover;
			}
			set
			{
				if ((_changeover != value))
				{
					this.OnChangeoverChanging(value);
					this.SendPropertyChanging();
					this._changeover = value;
					this.SendPropertyChanged("Changeover");
					this.OnChangeoverChanged();
				}
			}
		}
		
		[Column(Storage="_enabled", Name="Enabled", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public sbyte Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if ((_enabled != value))
				{
					this.OnEnabledChanging(value);
					this.SendPropertyChanging();
					this._enabled = value;
					this.SendPropertyChanged("Enabled");
					this.OnEnabledChanged();
				}
			}
		}
		
		[Column(Storage="_hospital", Name="Hospital", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public sbyte Hospital
		{
			get
			{
				return this._hospital;
			}
			set
			{
				if ((_hospital != value))
				{
					this.OnHospitalChanging(value);
					this.SendPropertyChanging();
					this._hospital = value;
					this.SendPropertyChanged("Hospital");
					this.OnHospitalChanged();
				}
			}
		}
		
		[Column(Storage="_lat", Name="Lat", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Lat
		{
			get
			{
				return this._lat;
			}
			set
			{
				if (((_lat == value) == false))
				{
					this.OnLatChanging(value);
					this.SendPropertyChanging();
					this._lat = value;
					this.SendPropertyChanged("Lat");
					this.OnLatChanged();
				}
			}
		}
		
		[Column(Storage="_lng", Name="Lng", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Lng
		{
			get
			{
				return this._lng;
			}
			set
			{
				if (((_lng == value) == false))
				{
					this.OnLngChanging(value);
					this.SendPropertyChanging();
					this._lng = value;
					this.SendPropertyChanged("Lng");
					this.OnLngChanged();
				}
			}
		}
		
		[Column(Storage="_location1", Name="Location", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Location1
		{
			get
			{
				return this._location1;
			}
			set
			{
				if (((_location1 == value) == false))
				{
					this.OnLocation1Changing(value);
					this.SendPropertyChanging();
					this._location1 = value;
					this.SendPropertyChanged("Location1");
					this.OnLocation1Changed();
				}
			}
		}
		
		[Column(Storage="_locationID", Name="LocationID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int LocationID
		{
			get
			{
				return this._locationID;
			}
			set
			{
				if ((_locationID != value))
				{
					this.OnLocationIDChanging(value);
					this.SendPropertyChanging();
					this._locationID = value;
					this.SendPropertyChanged("LocationID");
					this.OnLocationIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_runLog", OtherKey="DeliverToLocationID", ThisKey="LocationID", Name="fk_RunLog_Location1")]
		[DebuggerNonUserCode()]
		public EntitySet<RunLog> RunLog
		{
			get
			{
				return this._runLog;
			}
			set
			{
				this._runLog = value;
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void RunLog_Attach(RunLog entity)
		{
			this.SendPropertyChanging();
			entity.Location = this;
		}
		
		private void RunLog_Detach(RunLog entity)
		{
			this.SendPropertyChanging();
			entity.Location = null;
		}
		#endregion
	}
	
	[Table(Name="SERV.Member")]
	public partial class Member : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _address1;
		
		private string _address2;
		
		private string _address3;
		
		private System.Nullable<System.DateTime> _adQualPassDate;
		
		private string _adQualType;
		
		private System.Nullable<int> _availabilityID;
		
		private string _bikeType;
		
		private System.Nullable<int> _birthYear;
		
		private string _carType;
		
		private string _county;
		
		private string _emailAddress;
		
		private string _firstName;
		
		private string _homeNumber;
		
		private System.Nullable<System.DateTime> _joinDate;
		
		private System.Nullable<System.DateTime> _lastGdpgmpdAte;
		
		private string _lastName;
		
		private System.Nullable<System.DateTime> _leaveDate;
		
		private System.Nullable<sbyte> _legalConfirmation;
		
		private int _memberID;
		
		private int _memberStatusID;
		
		private string _mobileNumber;
		
		private string _nextOfKin;
		
		private string _nextOfKinAddress;
		
		private string _nextOfKinPhone;
		
		private string _notes;
		
		private string _occupation;
		
		private string _postCode;
		
		private System.Nullable<System.DateTime> _riderAssesmentPassDate;
		
		private string _town;
		
		private EntitySet<MemberTag> _memberTag;
		
		private EntitySet<RunLog> _runLog;
		
		private EntitySet<User> _user;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAddress1Changed();
		
		partial void OnAddress1Changing(string value);
		
		partial void OnAddress2Changed();
		
		partial void OnAddress2Changing(string value);
		
		partial void OnAddress3Changed();
		
		partial void OnAddress3Changing(string value);
		
		partial void OnAdQualPassDateChanged();
		
		partial void OnAdQualPassDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnAdQualTypeChanged();
		
		partial void OnAdQualTypeChanging(string value);
		
		partial void OnAvailabilityIDChanged();
		
		partial void OnAvailabilityIDChanging(System.Nullable<int> value);
		
		partial void OnBikeTypeChanged();
		
		partial void OnBikeTypeChanging(string value);
		
		partial void OnBirthYearChanged();
		
		partial void OnBirthYearChanging(System.Nullable<int> value);
		
		partial void OnCarTypeChanged();
		
		partial void OnCarTypeChanging(string value);
		
		partial void OnCountyChanged();
		
		partial void OnCountyChanging(string value);
		
		partial void OnEmailAddressChanged();
		
		partial void OnEmailAddressChanging(string value);
		
		partial void OnFirstNameChanged();
		
		partial void OnFirstNameChanging(string value);
		
		partial void OnHomeNumberChanged();
		
		partial void OnHomeNumberChanging(string value);
		
		partial void OnJoinDateChanged();
		
		partial void OnJoinDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnLastGdpgmpdAteChanged();
		
		partial void OnLastGdpgmpdAteChanging(System.Nullable<System.DateTime> value);
		
		partial void OnLastNameChanged();
		
		partial void OnLastNameChanging(string value);
		
		partial void OnLeaveDateChanged();
		
		partial void OnLeaveDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnLegalConfirmationChanged();
		
		partial void OnLegalConfirmationChanging(System.Nullable<sbyte> value);
		
		partial void OnMemberIDChanged();
		
		partial void OnMemberIDChanging(int value);
		
		partial void OnMemberStatusIDChanged();
		
		partial void OnMemberStatusIDChanging(int value);
		
		partial void OnMobileNumberChanged();
		
		partial void OnMobileNumberChanging(string value);
		
		partial void OnNextOfKinChanged();
		
		partial void OnNextOfKinChanging(string value);
		
		partial void OnNextOfKinAddressChanged();
		
		partial void OnNextOfKinAddressChanging(string value);
		
		partial void OnNextOfKinPhoneChanged();
		
		partial void OnNextOfKinPhoneChanging(string value);
		
		partial void OnNotesChanged();
		
		partial void OnNotesChanging(string value);
		
		partial void OnOccupationChanged();
		
		partial void OnOccupationChanging(string value);
		
		partial void OnPostCodeChanged();
		
		partial void OnPostCodeChanging(string value);
		
		partial void OnRiderAssesmentPassDateChanged();
		
		partial void OnRiderAssesmentPassDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnTownChanged();
		
		partial void OnTownChanging(string value);
		#endregion
		
		
		public Member()
		{
			_memberTag = new EntitySet<MemberTag>(new Action<MemberTag>(this.MemberTag_Attach), new Action<MemberTag>(this.MemberTag_Detach));
			_runLog = new EntitySet<RunLog>(new Action<RunLog>(this.RunLog_Attach), new Action<RunLog>(this.RunLog_Detach));
			_user = new EntitySet<User>(new Action<User>(this.User_Attach), new Action<User>(this.User_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_address1", Name="Address1", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Address1
		{
			get
			{
				return this._address1;
			}
			set
			{
				if (((_address1 == value) == false))
				{
					this.OnAddress1Changing(value);
					this.SendPropertyChanging();
					this._address1 = value;
					this.SendPropertyChanged("Address1");
					this.OnAddress1Changed();
				}
			}
		}
		
		[Column(Storage="_address2", Name="Address2", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Address2
		{
			get
			{
				return this._address2;
			}
			set
			{
				if (((_address2 == value) == false))
				{
					this.OnAddress2Changing(value);
					this.SendPropertyChanging();
					this._address2 = value;
					this.SendPropertyChanged("Address2");
					this.OnAddress2Changed();
				}
			}
		}
		
		[Column(Storage="_address3", Name="Address3", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Address3
		{
			get
			{
				return this._address3;
			}
			set
			{
				if (((_address3 == value) == false))
				{
					this.OnAddress3Changing(value);
					this.SendPropertyChanging();
					this._address3 = value;
					this.SendPropertyChanged("Address3");
					this.OnAddress3Changed();
				}
			}
		}
		
		[Column(Storage="_adQualPassDate", Name="AdQualPassDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> AdQualPassDate
		{
			get
			{
				return this._adQualPassDate;
			}
			set
			{
				if ((_adQualPassDate != value))
				{
					this.OnAdQualPassDateChanging(value);
					this.SendPropertyChanging();
					this._adQualPassDate = value;
					this.SendPropertyChanged("AdQualPassDate");
					this.OnAdQualPassDateChanged();
				}
			}
		}
		
		[Column(Storage="_adQualType", Name="AdQualType", DbType="varchar(15)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string AdQualType
		{
			get
			{
				return this._adQualType;
			}
			set
			{
				if (((_adQualType == value) == false))
				{
					this.OnAdQualTypeChanging(value);
					this.SendPropertyChanging();
					this._adQualType = value;
					this.SendPropertyChanged("AdQualType");
					this.OnAdQualTypeChanged();
				}
			}
		}
		
		[Column(Storage="_availabilityID", Name="AvailabilityID", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> AvailabilityID
		{
			get
			{
				return this._availabilityID;
			}
			set
			{
				if ((_availabilityID != value))
				{
					this.OnAvailabilityIDChanging(value);
					this.SendPropertyChanging();
					this._availabilityID = value;
					this.SendPropertyChanged("AvailabilityID");
					this.OnAvailabilityIDChanged();
				}
			}
		}
		
		[Column(Storage="_bikeType", Name="BikeType", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string BikeType
		{
			get
			{
				return this._bikeType;
			}
			set
			{
				if (((_bikeType == value) == false))
				{
					this.OnBikeTypeChanging(value);
					this.SendPropertyChanging();
					this._bikeType = value;
					this.SendPropertyChanged("BikeType");
					this.OnBikeTypeChanged();
				}
			}
		}
		
		[Column(Storage="_birthYear", Name="BirthYear", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> BirthYear
		{
			get
			{
				return this._birthYear;
			}
			set
			{
				if ((_birthYear != value))
				{
					this.OnBirthYearChanging(value);
					this.SendPropertyChanging();
					this._birthYear = value;
					this.SendPropertyChanged("BirthYear");
					this.OnBirthYearChanged();
				}
			}
		}
		
		[Column(Storage="_carType", Name="CarType", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CarType
		{
			get
			{
				return this._carType;
			}
			set
			{
				if (((_carType == value) == false))
				{
					this.OnCarTypeChanging(value);
					this.SendPropertyChanging();
					this._carType = value;
					this.SendPropertyChanged("CarType");
					this.OnCarTypeChanged();
				}
			}
		}
		
		[Column(Storage="_county", Name="County", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string County
		{
			get
			{
				return this._county;
			}
			set
			{
				if (((_county == value) == false))
				{
					this.OnCountyChanging(value);
					this.SendPropertyChanging();
					this._county = value;
					this.SendPropertyChanged("County");
					this.OnCountyChanged();
				}
			}
		}
		
		[Column(Storage="_emailAddress", Name="EmailAddress", DbType="varchar(60)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string EmailAddress
		{
			get
			{
				return this._emailAddress;
			}
			set
			{
				if (((_emailAddress == value) == false))
				{
					this.OnEmailAddressChanging(value);
					this.SendPropertyChanging();
					this._emailAddress = value;
					this.SendPropertyChanged("EmailAddress");
					this.OnEmailAddressChanged();
				}
			}
		}
		
		[Column(Storage="_firstName", Name="FirstName", DbType="varchar(45)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string FirstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				if (((_firstName == value) == false))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._firstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[Column(Storage="_homeNumber", Name="HomeNumber", DbType="varchar(12)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HomeNumber
		{
			get
			{
				return this._homeNumber;
			}
			set
			{
				if (((_homeNumber == value) == false))
				{
					this.OnHomeNumberChanging(value);
					this.SendPropertyChanging();
					this._homeNumber = value;
					this.SendPropertyChanged("HomeNumber");
					this.OnHomeNumberChanged();
				}
			}
		}
		
		[Column(Storage="_joinDate", Name="JoinDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> JoinDate
		{
			get
			{
				return this._joinDate;
			}
			set
			{
				if ((_joinDate != value))
				{
					this.OnJoinDateChanging(value);
					this.SendPropertyChanging();
					this._joinDate = value;
					this.SendPropertyChanged("JoinDate");
					this.OnJoinDateChanged();
				}
			}
		}
		
		[Column(Storage="_lastGdpgmpdAte", Name="LastGDPGMPDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> LastGdpgmpdAte
		{
			get
			{
				return this._lastGdpgmpdAte;
			}
			set
			{
				if ((_lastGdpgmpdAte != value))
				{
					this.OnLastGdpgmpdAteChanging(value);
					this.SendPropertyChanging();
					this._lastGdpgmpdAte = value;
					this.SendPropertyChanged("LastGdpgmpdAte");
					this.OnLastGdpgmpdAteChanged();
				}
			}
		}
		
		[Column(Storage="_lastName", Name="LastName", DbType="varchar(45)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string LastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				if (((_lastName == value) == false))
				{
					this.OnLastNameChanging(value);
					this.SendPropertyChanging();
					this._lastName = value;
					this.SendPropertyChanged("LastName");
					this.OnLastNameChanged();
				}
			}
		}
		
		[Column(Storage="_leaveDate", Name="LeaveDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> LeaveDate
		{
			get
			{
				return this._leaveDate;
			}
			set
			{
				if ((_leaveDate != value))
				{
					this.OnLeaveDateChanging(value);
					this.SendPropertyChanging();
					this._leaveDate = value;
					this.SendPropertyChanged("LeaveDate");
					this.OnLeaveDateChanged();
				}
			}
		}
		
		[Column(Storage="_legalConfirmation", Name="LegalConfirmation", DbType="tinyint(1)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<sbyte> LegalConfirmation
		{
			get
			{
				return this._legalConfirmation;
			}
			set
			{
				if ((_legalConfirmation != value))
				{
					this.OnLegalConfirmationChanging(value);
					this.SendPropertyChanging();
					this._legalConfirmation = value;
					this.SendPropertyChanged("LegalConfirmation");
					this.OnLegalConfirmationChanged();
				}
			}
		}
		
		[Column(Storage="_memberID", Name="MemberID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int MemberID
		{
			get
			{
				return this._memberID;
			}
			set
			{
				if ((_memberID != value))
				{
					this.OnMemberIDChanging(value);
					this.SendPropertyChanging();
					this._memberID = value;
					this.SendPropertyChanged("MemberID");
					this.OnMemberIDChanged();
				}
			}
		}
		
		[Column(Storage="_memberStatusID", Name="MemberStatusID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int MemberStatusID
		{
			get
			{
				return this._memberStatusID;
			}
			set
			{
				if ((_memberStatusID != value))
				{
					this.OnMemberStatusIDChanging(value);
					this.SendPropertyChanging();
					this._memberStatusID = value;
					this.SendPropertyChanged("MemberStatusID");
					this.OnMemberStatusIDChanged();
				}
			}
		}
		
		[Column(Storage="_mobileNumber", Name="MobileNumber", DbType="varchar(12)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string MobileNumber
		{
			get
			{
				return this._mobileNumber;
			}
			set
			{
				if (((_mobileNumber == value) == false))
				{
					this.OnMobileNumberChanging(value);
					this.SendPropertyChanging();
					this._mobileNumber = value;
					this.SendPropertyChanged("MobileNumber");
					this.OnMobileNumberChanged();
				}
			}
		}
		
		[Column(Storage="_nextOfKin", Name="NextOfKin", DbType="varchar(80)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string NextOfKin
		{
			get
			{
				return this._nextOfKin;
			}
			set
			{
				if (((_nextOfKin == value) == false))
				{
					this.OnNextOfKinChanging(value);
					this.SendPropertyChanging();
					this._nextOfKin = value;
					this.SendPropertyChanged("NextOfKin");
					this.OnNextOfKinChanged();
				}
			}
		}
		
		[Column(Storage="_nextOfKinAddress", Name="NextOfKinAddress", DbType="varchar(200)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string NextOfKinAddress
		{
			get
			{
				return this._nextOfKinAddress;
			}
			set
			{
				if (((_nextOfKinAddress == value) == false))
				{
					this.OnNextOfKinAddressChanging(value);
					this.SendPropertyChanging();
					this._nextOfKinAddress = value;
					this.SendPropertyChanged("NextOfKinAddress");
					this.OnNextOfKinAddressChanged();
				}
			}
		}
		
		[Column(Storage="_nextOfKinPhone", Name="NextOfKinPhone", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string NextOfKinPhone
		{
			get
			{
				return this._nextOfKinPhone;
			}
			set
			{
				if (((_nextOfKinPhone == value) == false))
				{
					this.OnNextOfKinPhoneChanging(value);
					this.SendPropertyChanging();
					this._nextOfKinPhone = value;
					this.SendPropertyChanged("NextOfKinPhone");
					this.OnNextOfKinPhoneChanged();
				}
			}
		}
		
		[Column(Storage="_notes", Name="Notes", DbType="varchar(400)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Notes
		{
			get
			{
				return this._notes;
			}
			set
			{
				if (((_notes == value) == false))
				{
					this.OnNotesChanging(value);
					this.SendPropertyChanging();
					this._notes = value;
					this.SendPropertyChanged("Notes");
					this.OnNotesChanged();
				}
			}
		}
		
		[Column(Storage="_occupation", Name="Occupation", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Occupation
		{
			get
			{
				return this._occupation;
			}
			set
			{
				if (((_occupation == value) == false))
				{
					this.OnOccupationChanging(value);
					this.SendPropertyChanging();
					this._occupation = value;
					this.SendPropertyChanged("Occupation");
					this.OnOccupationChanged();
				}
			}
		}
		
		[Column(Storage="_postCode", Name="PostCode", DbType="varchar(10)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string PostCode
		{
			get
			{
				return this._postCode;
			}
			set
			{
				if (((_postCode == value) == false))
				{
					this.OnPostCodeChanging(value);
					this.SendPropertyChanging();
					this._postCode = value;
					this.SendPropertyChanged("PostCode");
					this.OnPostCodeChanged();
				}
			}
		}
		
		[Column(Storage="_riderAssesmentPassDate", Name="RiderAssesmentPassDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> RiderAssesmentPassDate
		{
			get
			{
				return this._riderAssesmentPassDate;
			}
			set
			{
				if ((_riderAssesmentPassDate != value))
				{
					this.OnRiderAssesmentPassDateChanging(value);
					this.SendPropertyChanging();
					this._riderAssesmentPassDate = value;
					this.SendPropertyChanged("RiderAssesmentPassDate");
					this.OnRiderAssesmentPassDateChanged();
				}
			}
		}
		
		[Column(Storage="_town", Name="Town", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Town
		{
			get
			{
				return this._town;
			}
			set
			{
				if (((_town == value) == false))
				{
					this.OnTownChanging(value);
					this.SendPropertyChanging();
					this._town = value;
					this.SendPropertyChanged("Town");
					this.OnTownChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_memberTag", OtherKey="MemberID", ThisKey="MemberID", Name="fk_Member_Capability_Member1")]
		[DebuggerNonUserCode()]
		public EntitySet<MemberTag> MemberTag
		{
			get
			{
				return this._memberTag;
			}
			set
			{
				this._memberTag = value;
			}
		}
		
		[Association(Storage="_runLog", OtherKey="RiderMemberID", ThisKey="MemberID", Name="fk_RunLog_Member1")]
		[DebuggerNonUserCode()]
		public EntitySet<RunLog> RunLog
		{
			get
			{
				return this._runLog;
			}
			set
			{
				this._runLog = value;
			}
		}
		
		[Association(Storage="_user", OtherKey="MemberID", ThisKey="MemberID", Name="fk_User_Member")]
		[DebuggerNonUserCode()]
		public EntitySet<User> User
		{
			get
			{
				return this._user;
			}
			set
			{
				this._user = value;
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void MemberTag_Attach(MemberTag entity)
		{
			this.SendPropertyChanging();
			entity.Member = this;
		}
		
		private void MemberTag_Detach(MemberTag entity)
		{
			this.SendPropertyChanging();
			entity.Member = null;
		}
		
		private void RunLog_Attach(RunLog entity)
		{
			this.SendPropertyChanging();
			entity.Member = this;
		}
		
		private void RunLog_Detach(RunLog entity)
		{
			this.SendPropertyChanging();
			entity.Member = null;
		}
		
		private void User_Attach(User entity)
		{
			this.SendPropertyChanging();
			entity.Member = this;
		}
		
		private void User_Detach(User entity)
		{
			this.SendPropertyChanging();
			entity.Member = null;
		}
		#endregion
	}
	
	[Table(Name="SERV.MemberStatus")]
	public partial class MemberStatus : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _memberStatus1;
		
		private int _memberStatusID;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnMemberStatus1Changed();
		
		partial void OnMemberStatus1Changing(string value);
		
		partial void OnMemberStatusIDChanged();
		
		partial void OnMemberStatusIDChanging(int value);
		#endregion
		
		
		public MemberStatus()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_memberStatus1", Name="MemberStatus", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MemberStatus1
		{
			get
			{
				return this._memberStatus1;
			}
			set
			{
				if (((_memberStatus1 == value) == false))
				{
					this.OnMemberStatus1Changing(value);
					this.SendPropertyChanging();
					this._memberStatus1 = value;
					this.SendPropertyChanged("MemberStatus1");
					this.OnMemberStatus1Changed();
				}
			}
		}
		
		[Column(Storage="_memberStatusID", Name="MemberStatusID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int MemberStatusID
		{
			get
			{
				return this._memberStatusID;
			}
			set
			{
				if ((_memberStatusID != value))
				{
					this.OnMemberStatusIDChanging(value);
					this.SendPropertyChanging();
					this._memberStatusID = value;
					this.SendPropertyChanged("MemberStatusID");
					this.OnMemberStatusIDChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="SERV.Member_Tag")]
	public partial class MemberTag
	{
		
		private int _memberID;
		
		private int _tagID;
		
		private EntityRef<Tag> _tag = new EntityRef<Tag>();
		
		private EntityRef<Member> _member = new EntityRef<Member>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnMemberIDChanged();
		
		partial void OnMemberIDChanging(int value);
		
		partial void OnTagIDChanged();
		
		partial void OnTagIDChanging(int value);
		#endregion
		
		
		public MemberTag()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_memberID", Name="MemberID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int MemberID
		{
			get
			{
				return this._memberID;
			}
			set
			{
				if ((_memberID != value))
				{
					if (_member.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMemberIDChanging(value);
					this._memberID = value;
					this.OnMemberIDChanged();
				}
			}
		}
		
		[Column(Storage="_tagID", Name="TagID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int TagID
		{
			get
			{
				return this._tagID;
			}
			set
			{
				if ((_tagID != value))
				{
					if (_tag.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnTagIDChanging(value);
					this._tagID = value;
					this.OnTagIDChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_tag", OtherKey="TagID", ThisKey="TagID", Name="fk_Member_Capability_Capability1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Tag Tag
		{
			get
			{
				return this._tag.Entity;
			}
			set
			{
				if (((this._tag.Entity == value) == false))
				{
					if ((this._tag.Entity != null))
					{
						Tag previousTag = this._tag.Entity;
						this._tag.Entity = null;
						previousTag.MemberTag.Remove(this);
					}
					this._tag.Entity = value;
					if ((value != null))
					{
						value.MemberTag.Add(this);
						_tagID = value.TagID;
					}
					else
					{
						_tagID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_member", OtherKey="MemberID", ThisKey="MemberID", Name="fk_Member_Capability_Member1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Member Member
		{
			get
			{
				return this._member.Entity;
			}
			set
			{
				if (((this._member.Entity == value) == false))
				{
					if ((this._member.Entity != null))
					{
						Member previousMember = this._member.Entity;
						this._member.Entity = null;
						previousMember.MemberTag.Remove(this);
					}
					this._member.Entity = value;
					if ((value != null))
					{
						value.MemberTag.Add(this);
						_memberID = value.MemberID;
					}
					else
					{
						_memberID = default(int);
					}
				}
			}
		}
		#endregion
	}
	
	[Table(Name="SERV.Message")]
	public partial class Message : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _message1;
		
		private int _messageID;
		
		private int _messageTypeID;
		
		private string _recipient;
		
		private System.Nullable<int> _recipientMemberID;
		
		private int _senderUserID;
		
		private System.DateTime _sentDate;
		
		private EntityRef<MessageType> _messageType = new EntityRef<MessageType>();
		
		private EntityRef<User> _user = new EntityRef<User>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnMessage1Changed();
		
		partial void OnMessage1Changing(string value);
		
		partial void OnMessageIDChanged();
		
		partial void OnMessageIDChanging(int value);
		
		partial void OnMessageTypeIDChanged();
		
		partial void OnMessageTypeIDChanging(int value);
		
		partial void OnRecipientChanged();
		
		partial void OnRecipientChanging(string value);
		
		partial void OnRecipientMemberIDChanged();
		
		partial void OnRecipientMemberIDChanging(System.Nullable<int> value);
		
		partial void OnSenderUserIDChanged();
		
		partial void OnSenderUserIDChanging(int value);
		
		partial void OnSentDateChanged();
		
		partial void OnSentDateChanging(System.DateTime value);
		#endregion
		
		
		public Message()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_message1", Name="Message", DbType="varchar(1000)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Message1
		{
			get
			{
				return this._message1;
			}
			set
			{
				if (((_message1 == value) == false))
				{
					this.OnMessage1Changing(value);
					this.SendPropertyChanging();
					this._message1 = value;
					this.SendPropertyChanged("Message1");
					this.OnMessage1Changed();
				}
			}
		}
		
		[Column(Storage="_messageID", Name="MessageID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int MessageID
		{
			get
			{
				return this._messageID;
			}
			set
			{
				if ((_messageID != value))
				{
					this.OnMessageIDChanging(value);
					this.SendPropertyChanging();
					this._messageID = value;
					this.SendPropertyChanged("MessageID");
					this.OnMessageIDChanged();
				}
			}
		}
		
		[Column(Storage="_messageTypeID", Name="MessageTypeID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int MessageTypeID
		{
			get
			{
				return this._messageTypeID;
			}
			set
			{
				if ((_messageTypeID != value))
				{
					if (_messageType.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMessageTypeIDChanging(value);
					this.SendPropertyChanging();
					this._messageTypeID = value;
					this.SendPropertyChanged("MessageTypeID");
					this.OnMessageTypeIDChanged();
				}
			}
		}
		
		[Column(Storage="_recipient", Name="Recipient", DbType="varchar(4000)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Recipient
		{
			get
			{
				return this._recipient;
			}
			set
			{
				if (((_recipient == value) == false))
				{
					this.OnRecipientChanging(value);
					this.SendPropertyChanging();
					this._recipient = value;
					this.SendPropertyChanged("Recipient");
					this.OnRecipientChanged();
				}
			}
		}
		
		[Column(Storage="_recipientMemberID", Name="RecipientMemberID", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> RecipientMemberID
		{
			get
			{
				return this._recipientMemberID;
			}
			set
			{
				if ((_recipientMemberID != value))
				{
					this.OnRecipientMemberIDChanging(value);
					this.SendPropertyChanging();
					this._recipientMemberID = value;
					this.SendPropertyChanged("RecipientMemberID");
					this.OnRecipientMemberIDChanged();
				}
			}
		}
		
		[Column(Storage="_senderUserID", Name="SenderUserID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int SenderUserID
		{
			get
			{
				return this._senderUserID;
			}
			set
			{
				if ((_senderUserID != value))
				{
					if (_user.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnSenderUserIDChanging(value);
					this.SendPropertyChanging();
					this._senderUserID = value;
					this.SendPropertyChanged("SenderUserID");
					this.OnSenderUserIDChanged();
				}
			}
		}
		
		[Column(Storage="_sentDate", Name="SentDate", DbType="datetime", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime SentDate
		{
			get
			{
				return this._sentDate;
			}
			set
			{
				if ((_sentDate != value))
				{
					this.OnSentDateChanging(value);
					this.SendPropertyChanging();
					this._sentDate = value;
					this.SendPropertyChanged("SentDate");
					this.OnSentDateChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_messageType", OtherKey="MessageTypeID", ThisKey="MessageTypeID", Name="fk_Message_MessageType1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public MessageType MessageType
		{
			get
			{
				return this._messageType.Entity;
			}
			set
			{
				if (((this._messageType.Entity == value) == false))
				{
					if ((this._messageType.Entity != null))
					{
						MessageType previousMessageType = this._messageType.Entity;
						this._messageType.Entity = null;
						previousMessageType.Message.Remove(this);
					}
					this._messageType.Entity = value;
					if ((value != null))
					{
						value.Message.Add(this);
						_messageTypeID = value.MessageTypeID;
					}
					else
					{
						_messageTypeID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_user", OtherKey="UserID", ThisKey="SenderUserID", Name="fk_Message_User1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public User User
		{
			get
			{
				return this._user.Entity;
			}
			set
			{
				if (((this._user.Entity == value) == false))
				{
					if ((this._user.Entity != null))
					{
						User previousUser = this._user.Entity;
						this._user.Entity = null;
						previousUser.Message.Remove(this);
					}
					this._user.Entity = value;
					if ((value != null))
					{
						value.Message.Add(this);
						_senderUserID = value.UserID;
					}
					else
					{
						_senderUserID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="SERV.MessageType")]
	public partial class MessageType : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _messageType1;
		
		private int _messageTypeID;
		
		private EntitySet<Message> _message;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnMessageType1Changed();
		
		partial void OnMessageType1Changing(string value);
		
		partial void OnMessageTypeIDChanged();
		
		partial void OnMessageTypeIDChanging(int value);
		#endregion
		
		
		public MessageType()
		{
			_message = new EntitySet<Message>(new Action<Message>(this.Message_Attach), new Action<Message>(this.Message_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_messageType1", Name="MessageType", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MessageType1
		{
			get
			{
				return this._messageType1;
			}
			set
			{
				if (((_messageType1 == value) == false))
				{
					this.OnMessageType1Changing(value);
					this.SendPropertyChanging();
					this._messageType1 = value;
					this.SendPropertyChanged("MessageType1");
					this.OnMessageType1Changed();
				}
			}
		}
		
		[Column(Storage="_messageTypeID", Name="MessageTypeID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int MessageTypeID
		{
			get
			{
				return this._messageTypeID;
			}
			set
			{
				if ((_messageTypeID != value))
				{
					this.OnMessageTypeIDChanging(value);
					this.SendPropertyChanging();
					this._messageTypeID = value;
					this.SendPropertyChanged("MessageTypeID");
					this.OnMessageTypeIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_message", OtherKey="MessageTypeID", ThisKey="MessageTypeID", Name="fk_Message_MessageType1")]
		[DebuggerNonUserCode()]
		public EntitySet<Message> Message
		{
			get
			{
				return this._message;
			}
			set
			{
				this._message = value;
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void Message_Attach(Message entity)
		{
			this.SendPropertyChanging();
			entity.MessageType = this;
		}
		
		private void Message_Detach(Message entity)
		{
			this.SendPropertyChanging();
			entity.MessageType = null;
		}
		#endregion
	}
	
	[Table(Name="SERV.Product")]
	public partial class Product : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private sbyte _enabled;
		
		private string _product1;
		
		private int _productID;
		
		private EntitySet<RunLogProduct> _runLogProduct;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnEnabledChanged();
		
		partial void OnEnabledChanging(sbyte value);
		
		partial void OnProduct1Changed();
		
		partial void OnProduct1Changing(string value);
		
		partial void OnProductIDChanged();
		
		partial void OnProductIDChanging(int value);
		#endregion
		
		
		public Product()
		{
			_runLogProduct = new EntitySet<RunLogProduct>(new Action<RunLogProduct>(this.RunLogProduct_Attach), new Action<RunLogProduct>(this.RunLogProduct_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_enabled", Name="Enabled", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public sbyte Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if ((_enabled != value))
				{
					this.OnEnabledChanging(value);
					this.SendPropertyChanging();
					this._enabled = value;
					this.SendPropertyChanged("Enabled");
					this.OnEnabledChanged();
				}
			}
		}
		
		[Column(Storage="_product1", Name="Product", DbType="varchar(100)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Product1
		{
			get
			{
				return this._product1;
			}
			set
			{
				if (((_product1 == value) == false))
				{
					this.OnProduct1Changing(value);
					this.SendPropertyChanging();
					this._product1 = value;
					this.SendPropertyChanged("Product1");
					this.OnProduct1Changed();
				}
			}
		}
		
		[Column(Storage="_productID", Name="ProductID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ProductID
		{
			get
			{
				return this._productID;
			}
			set
			{
				if ((_productID != value))
				{
					this.OnProductIDChanging(value);
					this.SendPropertyChanging();
					this._productID = value;
					this.SendPropertyChanged("ProductID");
					this.OnProductIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_runLogProduct", OtherKey="ProductID", ThisKey="ProductID", Name="fk_RunLog_Product_Product1")]
		[DebuggerNonUserCode()]
		public EntitySet<RunLogProduct> RunLogProduct
		{
			get
			{
				return this._runLogProduct;
			}
			set
			{
				this._runLogProduct = value;
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void RunLogProduct_Attach(RunLogProduct entity)
		{
			this.SendPropertyChanging();
			entity.Product = this;
		}
		
		private void RunLogProduct_Detach(RunLogProduct entity)
		{
			this.SendPropertyChanging();
			entity.Product = null;
		}
		#endregion
	}
	
	[Table(Name="SERV.RawRunLog")]
	public partial class RawRunLog : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private System.Nullable<System.DateTime> _callDate;
		
		private string _callTime;
		
		private string _collectFrom;
		
		private string _collectTime;
		
		private string _collectTime2;
		
		private string _consignment;
		
		private string _controller;
		
		private string _deliveryTime;
		
		private string _destination;
		
		private string _notes;
		
		private int _rawRunLogID;
		
		private string _rider;
		
		private string _urgency;
		
		private string _vehicle;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCallDateChanged();
		
		partial void OnCallDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnCallTimeChanged();
		
		partial void OnCallTimeChanging(string value);
		
		partial void OnCollectFromChanged();
		
		partial void OnCollectFromChanging(string value);
		
		partial void OnCollectTimeChanged();
		
		partial void OnCollectTimeChanging(string value);
		
		partial void OnCollectTime2Changed();
		
		partial void OnCollectTime2Changing(string value);
		
		partial void OnConsignmentChanged();
		
		partial void OnConsignmentChanging(string value);
		
		partial void OnControllerChanged();
		
		partial void OnControllerChanging(string value);
		
		partial void OnDeliveryTimeChanged();
		
		partial void OnDeliveryTimeChanging(string value);
		
		partial void OnDestinationChanged();
		
		partial void OnDestinationChanging(string value);
		
		partial void OnNotesChanged();
		
		partial void OnNotesChanging(string value);
		
		partial void OnRawRunLogIDChanged();
		
		partial void OnRawRunLogIDChanging(int value);
		
		partial void OnRiderChanged();
		
		partial void OnRiderChanging(string value);
		
		partial void OnUrgencyChanged();
		
		partial void OnUrgencyChanging(string value);
		
		partial void OnVehicleChanged();
		
		partial void OnVehicleChanging(string value);
		#endregion
		
		
		public RawRunLog()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_callDate", Name="CallDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CallDate
		{
			get
			{
				return this._callDate;
			}
			set
			{
				if ((_callDate != value))
				{
					this.OnCallDateChanging(value);
					this.SendPropertyChanging();
					this._callDate = value;
					this.SendPropertyChanged("CallDate");
					this.OnCallDateChanged();
				}
			}
		}
		
		[Column(Storage="_callTime", Name="CallTime", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CallTime
		{
			get
			{
				return this._callTime;
			}
			set
			{
				if (((_callTime == value) == false))
				{
					this.OnCallTimeChanging(value);
					this.SendPropertyChanging();
					this._callTime = value;
					this.SendPropertyChanged("CallTime");
					this.OnCallTimeChanged();
				}
			}
		}
		
		[Column(Storage="_collectFrom", Name="CollectFrom", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CollectFrom
		{
			get
			{
				return this._collectFrom;
			}
			set
			{
				if (((_collectFrom == value) == false))
				{
					this.OnCollectFromChanging(value);
					this.SendPropertyChanging();
					this._collectFrom = value;
					this.SendPropertyChanged("CollectFrom");
					this.OnCollectFromChanged();
				}
			}
		}
		
		[Column(Storage="_collectTime", Name="CollectTime", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CollectTime
		{
			get
			{
				return this._collectTime;
			}
			set
			{
				if (((_collectTime == value) == false))
				{
					this.OnCollectTimeChanging(value);
					this.SendPropertyChanging();
					this._collectTime = value;
					this.SendPropertyChanged("CollectTime");
					this.OnCollectTimeChanged();
				}
			}
		}
		
		[Column(Storage="_collectTime2", Name="CollectTime2", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string CollectTime2
		{
			get
			{
				return this._collectTime2;
			}
			set
			{
				if (((_collectTime2 == value) == false))
				{
					this.OnCollectTime2Changing(value);
					this.SendPropertyChanging();
					this._collectTime2 = value;
					this.SendPropertyChanged("CollectTime2");
					this.OnCollectTime2Changed();
				}
			}
		}
		
		[Column(Storage="_consignment", Name="Consignment", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Consignment
		{
			get
			{
				return this._consignment;
			}
			set
			{
				if (((_consignment == value) == false))
				{
					this.OnConsignmentChanging(value);
					this.SendPropertyChanging();
					this._consignment = value;
					this.SendPropertyChanged("Consignment");
					this.OnConsignmentChanged();
				}
			}
		}
		
		[Column(Storage="_controller", Name="Controller", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Controller
		{
			get
			{
				return this._controller;
			}
			set
			{
				if (((_controller == value) == false))
				{
					this.OnControllerChanging(value);
					this.SendPropertyChanging();
					this._controller = value;
					this.SendPropertyChanged("Controller");
					this.OnControllerChanged();
				}
			}
		}
		
		[Column(Storage="_deliveryTime", Name="DeliveryTime", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string DeliveryTime
		{
			get
			{
				return this._deliveryTime;
			}
			set
			{
				if (((_deliveryTime == value) == false))
				{
					this.OnDeliveryTimeChanging(value);
					this.SendPropertyChanging();
					this._deliveryTime = value;
					this.SendPropertyChanged("DeliveryTime");
					this.OnDeliveryTimeChanged();
				}
			}
		}
		
		[Column(Storage="_destination", Name="Destination", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Destination
		{
			get
			{
				return this._destination;
			}
			set
			{
				if (((_destination == value) == false))
				{
					this.OnDestinationChanging(value);
					this.SendPropertyChanging();
					this._destination = value;
					this.SendPropertyChanged("Destination");
					this.OnDestinationChanged();
				}
			}
		}
		
		[Column(Storage="_notes", Name="Notes", DbType="varchar(2000)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Notes
		{
			get
			{
				return this._notes;
			}
			set
			{
				if (((_notes == value) == false))
				{
					this.OnNotesChanging(value);
					this.SendPropertyChanging();
					this._notes = value;
					this.SendPropertyChanged("Notes");
					this.OnNotesChanged();
				}
			}
		}
		
		[Column(Storage="_rawRunLogID", Name="RawRunLogID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RawRunLogID
		{
			get
			{
				return this._rawRunLogID;
			}
			set
			{
				if ((_rawRunLogID != value))
				{
					this.OnRawRunLogIDChanging(value);
					this.SendPropertyChanging();
					this._rawRunLogID = value;
					this.SendPropertyChanged("RawRunLogID");
					this.OnRawRunLogIDChanged();
				}
			}
		}
		
		[Column(Storage="_rider", Name="Rider", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Rider
		{
			get
			{
				return this._rider;
			}
			set
			{
				if (((_rider == value) == false))
				{
					this.OnRiderChanging(value);
					this.SendPropertyChanging();
					this._rider = value;
					this.SendPropertyChanged("Rider");
					this.OnRiderChanged();
				}
			}
		}
		
		[Column(Storage="_urgency", Name="Urgency", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Urgency
		{
			get
			{
				return this._urgency;
			}
			set
			{
				if (((_urgency == value) == false))
				{
					this.OnUrgencyChanging(value);
					this.SendPropertyChanging();
					this._urgency = value;
					this.SendPropertyChanged("Urgency");
					this.OnUrgencyChanged();
				}
			}
		}
		
		[Column(Storage="_vehicle", Name="Vehicle", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Vehicle
		{
			get
			{
				return this._vehicle;
			}
			set
			{
				if (((_vehicle == value) == false))
				{
					this.OnVehicleChanging(value);
					this.SendPropertyChanging();
					this._vehicle = value;
					this.SendPropertyChanged("Vehicle");
					this.OnVehicleChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="SERV.RunLog")]
	public partial class RunLog : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _boxes;
		
		private System.Nullable<System.DateTime> _callDateTime;
		
		private int _callFromLocationID;
		
		private System.Nullable<System.DateTime> _collectDateTime;
		
		private int _collectionLocationID;
		
		private int _controllerMemberID;
		
		private System.DateTime _createDate;
		
		private int _createdByUserID;
		
		private System.Nullable<System.DateTime> _deliverDateTime;
		
		private int _deliverToLocationID;
		
		private string _description;
		
		private System.Nullable<System.DateTime> _dutyDate;
		
		private int _finalDestinationLocationID;
		
		private System.Nullable<System.DateTime> _homeSafeDateTime;
		
		private sbyte _isTransfer;
		
		private string _notes;
		
		private int _originLocationID;
		
		private int _riderMemberID;
		
		private int _runLogID;
		
		private int _urgency;
		
		private int _vehicleTypeID;
		
		private EntitySet<RunLogProduct> _runLogProduct;
		
		private EntityRef<Location> _location = new EntityRef<Location>();
		
		private EntityRef<Member> _member = new EntityRef<Member>();
		
		private EntityRef<User> _user = new EntityRef<User>();
		
		private EntityRef<VehicleType> _vehicleType = new EntityRef<VehicleType>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnBoxesChanged();
		
		partial void OnBoxesChanging(int value);
		
		partial void OnCallDateTimeChanged();
		
		partial void OnCallDateTimeChanging(System.Nullable<System.DateTime> value);
		
		partial void OnCallFromLocationIDChanged();
		
		partial void OnCallFromLocationIDChanging(int value);
		
		partial void OnCollectDateTimeChanged();
		
		partial void OnCollectDateTimeChanging(System.Nullable<System.DateTime> value);
		
		partial void OnCollectionLocationIDChanged();
		
		partial void OnCollectionLocationIDChanging(int value);
		
		partial void OnControllerMemberIDChanged();
		
		partial void OnControllerMemberIDChanging(int value);
		
		partial void OnCreateDateChanged();
		
		partial void OnCreateDateChanging(System.DateTime value);
		
		partial void OnCreatedByUserIDChanged();
		
		partial void OnCreatedByUserIDChanging(int value);
		
		partial void OnDeliverDateTimeChanged();
		
		partial void OnDeliverDateTimeChanging(System.Nullable<System.DateTime> value);
		
		partial void OnDeliverToLocationIDChanged();
		
		partial void OnDeliverToLocationIDChanging(int value);
		
		partial void OnDescriptionChanged();
		
		partial void OnDescriptionChanging(string value);
		
		partial void OnDutyDateChanged();
		
		partial void OnDutyDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnFinalDestinationLocationIDChanged();
		
		partial void OnFinalDestinationLocationIDChanging(int value);
		
		partial void OnHomeSafeDateTimeChanged();
		
		partial void OnHomeSafeDateTimeChanging(System.Nullable<System.DateTime> value);
		
		partial void OnIsTransferChanged();
		
		partial void OnIsTransferChanging(sbyte value);
		
		partial void OnNotesChanged();
		
		partial void OnNotesChanging(string value);
		
		partial void OnOriginLocationIDChanged();
		
		partial void OnOriginLocationIDChanging(int value);
		
		partial void OnRiderMemberIDChanged();
		
		partial void OnRiderMemberIDChanging(int value);
		
		partial void OnRunLogIDChanged();
		
		partial void OnRunLogIDChanging(int value);
		
		partial void OnUrgencyChanged();
		
		partial void OnUrgencyChanging(int value);
		
		partial void OnVehicleTypeIDChanged();
		
		partial void OnVehicleTypeIDChanging(int value);
		#endregion
		
		
		public RunLog()
		{
			_runLogProduct = new EntitySet<RunLogProduct>(new Action<RunLogProduct>(this.RunLogProduct_Attach), new Action<RunLogProduct>(this.RunLogProduct_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_boxes", Name="Boxes", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Boxes
		{
			get
			{
				return this._boxes;
			}
			set
			{
				if ((_boxes != value))
				{
					this.OnBoxesChanging(value);
					this.SendPropertyChanging();
					this._boxes = value;
					this.SendPropertyChanged("Boxes");
					this.OnBoxesChanged();
				}
			}
		}
		
		[Column(Storage="_callDateTime", Name="CallDateTime", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CallDateTime
		{
			get
			{
				return this._callDateTime;
			}
			set
			{
				if ((_callDateTime != value))
				{
					this.OnCallDateTimeChanging(value);
					this.SendPropertyChanging();
					this._callDateTime = value;
					this.SendPropertyChanged("CallDateTime");
					this.OnCallDateTimeChanged();
				}
			}
		}
		
		[Column(Storage="_callFromLocationID", Name="CallFromLocationID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CallFromLocationID
		{
			get
			{
				return this._callFromLocationID;
			}
			set
			{
				if ((_callFromLocationID != value))
				{
					this.OnCallFromLocationIDChanging(value);
					this.SendPropertyChanging();
					this._callFromLocationID = value;
					this.SendPropertyChanged("CallFromLocationID");
					this.OnCallFromLocationIDChanged();
				}
			}
		}
		
		[Column(Storage="_collectDateTime", Name="CollectDateTime", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> CollectDateTime
		{
			get
			{
				return this._collectDateTime;
			}
			set
			{
				if ((_collectDateTime != value))
				{
					this.OnCollectDateTimeChanging(value);
					this.SendPropertyChanging();
					this._collectDateTime = value;
					this.SendPropertyChanged("CollectDateTime");
					this.OnCollectDateTimeChanged();
				}
			}
		}
		
		[Column(Storage="_collectionLocationID", Name="CollectionLocationID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CollectionLocationID
		{
			get
			{
				return this._collectionLocationID;
			}
			set
			{
				if ((_collectionLocationID != value))
				{
					this.OnCollectionLocationIDChanging(value);
					this.SendPropertyChanging();
					this._collectionLocationID = value;
					this.SendPropertyChanged("CollectionLocationID");
					this.OnCollectionLocationIDChanged();
				}
			}
		}
		
		[Column(Storage="_controllerMemberID", Name="ControllerMemberID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ControllerMemberID
		{
			get
			{
				return this._controllerMemberID;
			}
			set
			{
				if ((_controllerMemberID != value))
				{
					this.OnControllerMemberIDChanging(value);
					this.SendPropertyChanging();
					this._controllerMemberID = value;
					this.SendPropertyChanged("ControllerMemberID");
					this.OnControllerMemberIDChanged();
				}
			}
		}
		
		[Column(Storage="_createDate", Name="CreateDate", DbType="timestamp", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime CreateDate
		{
			get
			{
				return this._createDate;
			}
			set
			{
				if ((_createDate != value))
				{
					this.OnCreateDateChanging(value);
					this.SendPropertyChanging();
					this._createDate = value;
					this.SendPropertyChanged("CreateDate");
					this.OnCreateDateChanged();
				}
			}
		}
		
		[Column(Storage="_createdByUserID", Name="CreatedByUserID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CreatedByUserID
		{
			get
			{
				return this._createdByUserID;
			}
			set
			{
				if ((_createdByUserID != value))
				{
					if (_user.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCreatedByUserIDChanging(value);
					this.SendPropertyChanging();
					this._createdByUserID = value;
					this.SendPropertyChanged("CreatedByUserID");
					this.OnCreatedByUserIDChanged();
				}
			}
		}
		
		[Column(Storage="_deliverDateTime", Name="DeliverDateTime", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> DeliverDateTime
		{
			get
			{
				return this._deliverDateTime;
			}
			set
			{
				if ((_deliverDateTime != value))
				{
					this.OnDeliverDateTimeChanging(value);
					this.SendPropertyChanging();
					this._deliverDateTime = value;
					this.SendPropertyChanged("DeliverDateTime");
					this.OnDeliverDateTimeChanged();
				}
			}
		}
		
		[Column(Storage="_deliverToLocationID", Name="DeliverToLocationID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int DeliverToLocationID
		{
			get
			{
				return this._deliverToLocationID;
			}
			set
			{
				if ((_deliverToLocationID != value))
				{
					if (_location.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDeliverToLocationIDChanging(value);
					this.SendPropertyChanging();
					this._deliverToLocationID = value;
					this.SendPropertyChanged("DeliverToLocationID");
					this.OnDeliverToLocationIDChanged();
				}
			}
		}
		
		[Column(Storage="_description", Name="Description", DbType="varchar(300)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (((_description == value) == false))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_dutyDate", Name="DutyDate", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> DutyDate
		{
			get
			{
				return this._dutyDate;
			}
			set
			{
				if ((_dutyDate != value))
				{
					this.OnDutyDateChanging(value);
					this.SendPropertyChanging();
					this._dutyDate = value;
					this.SendPropertyChanged("DutyDate");
					this.OnDutyDateChanged();
				}
			}
		}
		
		[Column(Storage="_finalDestinationLocationID", Name="FinalDestinationLocationID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int FinalDestinationLocationID
		{
			get
			{
				return this._finalDestinationLocationID;
			}
			set
			{
				if ((_finalDestinationLocationID != value))
				{
					this.OnFinalDestinationLocationIDChanging(value);
					this.SendPropertyChanging();
					this._finalDestinationLocationID = value;
					this.SendPropertyChanged("FinalDestinationLocationID");
					this.OnFinalDestinationLocationIDChanged();
				}
			}
		}
		
		[Column(Storage="_homeSafeDateTime", Name="HomeSafeDateTime", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> HomeSafeDateTime
		{
			get
			{
				return this._homeSafeDateTime;
			}
			set
			{
				if ((_homeSafeDateTime != value))
				{
					this.OnHomeSafeDateTimeChanging(value);
					this.SendPropertyChanging();
					this._homeSafeDateTime = value;
					this.SendPropertyChanged("HomeSafeDateTime");
					this.OnHomeSafeDateTimeChanged();
				}
			}
		}
		
		[Column(Storage="_isTransfer", Name="IsTransfer", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public sbyte IsTransfer
		{
			get
			{
				return this._isTransfer;
			}
			set
			{
				if ((_isTransfer != value))
				{
					this.OnIsTransferChanging(value);
					this.SendPropertyChanging();
					this._isTransfer = value;
					this.SendPropertyChanged("IsTransfer");
					this.OnIsTransferChanged();
				}
			}
		}
		
		[Column(Storage="_notes", Name="Notes", DbType="varchar(600)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Notes
		{
			get
			{
				return this._notes;
			}
			set
			{
				if (((_notes == value) == false))
				{
					this.OnNotesChanging(value);
					this.SendPropertyChanging();
					this._notes = value;
					this.SendPropertyChanged("Notes");
					this.OnNotesChanged();
				}
			}
		}
		
		[Column(Storage="_originLocationID", Name="OriginLocationID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int OriginLocationID
		{
			get
			{
				return this._originLocationID;
			}
			set
			{
				if ((_originLocationID != value))
				{
					this.OnOriginLocationIDChanging(value);
					this.SendPropertyChanging();
					this._originLocationID = value;
					this.SendPropertyChanged("OriginLocationID");
					this.OnOriginLocationIDChanged();
				}
			}
		}
		
		[Column(Storage="_riderMemberID", Name="RiderMemberID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RiderMemberID
		{
			get
			{
				return this._riderMemberID;
			}
			set
			{
				if ((_riderMemberID != value))
				{
					if (_member.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRiderMemberIDChanging(value);
					this.SendPropertyChanging();
					this._riderMemberID = value;
					this.SendPropertyChanged("RiderMemberID");
					this.OnRiderMemberIDChanged();
				}
			}
		}
		
		[Column(Storage="_runLogID", Name="RunLogID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RunLogID
		{
			get
			{
				return this._runLogID;
			}
			set
			{
				if ((_runLogID != value))
				{
					this.OnRunLogIDChanging(value);
					this.SendPropertyChanging();
					this._runLogID = value;
					this.SendPropertyChanged("RunLogID");
					this.OnRunLogIDChanged();
				}
			}
		}
		
		[Column(Storage="_urgency", Name="Urgency", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Urgency
		{
			get
			{
				return this._urgency;
			}
			set
			{
				if ((_urgency != value))
				{
					this.OnUrgencyChanging(value);
					this.SendPropertyChanging();
					this._urgency = value;
					this.SendPropertyChanged("Urgency");
					this.OnUrgencyChanged();
				}
			}
		}
		
		[Column(Storage="_vehicleTypeID", Name="VehicleTypeID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int VehicleTypeID
		{
			get
			{
				return this._vehicleTypeID;
			}
			set
			{
				if ((_vehicleTypeID != value))
				{
					if (_vehicleType.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnVehicleTypeIDChanging(value);
					this.SendPropertyChanging();
					this._vehicleTypeID = value;
					this.SendPropertyChanged("VehicleTypeID");
					this.OnVehicleTypeIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_runLogProduct", OtherKey="RunLogID", ThisKey="RunLogID", Name="fk_RunLog_Product_RunLog1")]
		[DebuggerNonUserCode()]
		public EntitySet<RunLogProduct> RunLogProduct
		{
			get
			{
				return this._runLogProduct;
			}
			set
			{
				this._runLogProduct = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_location", OtherKey="LocationID", ThisKey="DeliverToLocationID", Name="fk_RunLog_Location1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Location Location
		{
			get
			{
				return this._location.Entity;
			}
			set
			{
				if (((this._location.Entity == value) == false))
				{
					if ((this._location.Entity != null))
					{
						Location previousLocation = this._location.Entity;
						this._location.Entity = null;
						previousLocation.RunLog.Remove(this);
					}
					this._location.Entity = value;
					if ((value != null))
					{
						value.RunLog.Add(this);
						_deliverToLocationID = value.LocationID;
					}
					else
					{
						_deliverToLocationID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_member", OtherKey="MemberID", ThisKey="RiderMemberID", Name="fk_RunLog_Member1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Member Member
		{
			get
			{
				return this._member.Entity;
			}
			set
			{
				if (((this._member.Entity == value) == false))
				{
					if ((this._member.Entity != null))
					{
						Member previousMember = this._member.Entity;
						this._member.Entity = null;
						previousMember.RunLog.Remove(this);
					}
					this._member.Entity = value;
					if ((value != null))
					{
						value.RunLog.Add(this);
						_riderMemberID = value.MemberID;
					}
					else
					{
						_riderMemberID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_user", OtherKey="UserID", ThisKey="CreatedByUserID", Name="fk_RunLog_User1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public User User
		{
			get
			{
				return this._user.Entity;
			}
			set
			{
				if (((this._user.Entity == value) == false))
				{
					if ((this._user.Entity != null))
					{
						User previousUser = this._user.Entity;
						this._user.Entity = null;
						previousUser.RunLog.Remove(this);
					}
					this._user.Entity = value;
					if ((value != null))
					{
						value.RunLog.Add(this);
						_createdByUserID = value.UserID;
					}
					else
					{
						_createdByUserID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_vehicleType", OtherKey="VehicleTypeID", ThisKey="VehicleTypeID", Name="fk_RunLog_VehicleType1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public VehicleType VehicleType
		{
			get
			{
				return this._vehicleType.Entity;
			}
			set
			{
				if (((this._vehicleType.Entity == value) == false))
				{
					if ((this._vehicleType.Entity != null))
					{
						VehicleType previousVehicleType = this._vehicleType.Entity;
						this._vehicleType.Entity = null;
						previousVehicleType.RunLog.Remove(this);
					}
					this._vehicleType.Entity = value;
					if ((value != null))
					{
						value.RunLog.Add(this);
						_vehicleTypeID = value.VehicleTypeID;
					}
					else
					{
						_vehicleTypeID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void RunLogProduct_Attach(RunLogProduct entity)
		{
			this.SendPropertyChanging();
			entity.RunLog = this;
		}
		
		private void RunLogProduct_Detach(RunLogProduct entity)
		{
			this.SendPropertyChanging();
			entity.RunLog = null;
		}
		#endregion
	}
	
	[Table(Name="SERV.RunLog_Product")]
	public partial class RunLogProduct : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _productID;
		
		private int _quantity;
		
		private int _runLogID;
		
		private int _runLogProductID;
		
		private EntityRef<Product> _product = new EntityRef<Product>();
		
		private EntityRef<RunLog> _runLog = new EntityRef<RunLog>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnProductIDChanged();
		
		partial void OnProductIDChanging(int value);
		
		partial void OnQuantityChanged();
		
		partial void OnQuantityChanging(int value);
		
		partial void OnRunLogIDChanged();
		
		partial void OnRunLogIDChanging(int value);
		
		partial void OnRunLogProductIDChanged();
		
		partial void OnRunLogProductIDChanging(int value);
		#endregion
		
		
		public RunLogProduct()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_productID", Name="ProductID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ProductID
		{
			get
			{
				return this._productID;
			}
			set
			{
				if ((_productID != value))
				{
					if (_product.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnProductIDChanging(value);
					this.SendPropertyChanging();
					this._productID = value;
					this.SendPropertyChanged("ProductID");
					this.OnProductIDChanged();
				}
			}
		}
		
		[Column(Storage="_quantity", Name="Quantity", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Quantity
		{
			get
			{
				return this._quantity;
			}
			set
			{
				if ((_quantity != value))
				{
					this.OnQuantityChanging(value);
					this.SendPropertyChanging();
					this._quantity = value;
					this.SendPropertyChanged("Quantity");
					this.OnQuantityChanged();
				}
			}
		}
		
		[Column(Storage="_runLogID", Name="RunLogID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RunLogID
		{
			get
			{
				return this._runLogID;
			}
			set
			{
				if ((_runLogID != value))
				{
					if (_runLog.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRunLogIDChanging(value);
					this.SendPropertyChanging();
					this._runLogID = value;
					this.SendPropertyChanged("RunLogID");
					this.OnRunLogIDChanged();
				}
			}
		}
		
		[Column(Storage="_runLogProductID", Name="RunLog_ProductID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RunLogProductID
		{
			get
			{
				return this._runLogProductID;
			}
			set
			{
				if ((_runLogProductID != value))
				{
					this.OnRunLogProductIDChanging(value);
					this.SendPropertyChanging();
					this._runLogProductID = value;
					this.SendPropertyChanged("RunLogProductID");
					this.OnRunLogProductIDChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_product", OtherKey="ProductID", ThisKey="ProductID", Name="fk_RunLog_Product_Product1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Product Product
		{
			get
			{
				return this._product.Entity;
			}
			set
			{
				if (((this._product.Entity == value) == false))
				{
					if ((this._product.Entity != null))
					{
						Product previousProduct = this._product.Entity;
						this._product.Entity = null;
						previousProduct.RunLogProduct.Remove(this);
					}
					this._product.Entity = value;
					if ((value != null))
					{
						value.RunLogProduct.Add(this);
						_productID = value.ProductID;
					}
					else
					{
						_productID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_runLog", OtherKey="RunLogID", ThisKey="RunLogID", Name="fk_RunLog_Product_RunLog1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public RunLog RunLog
		{
			get
			{
				return this._runLog.Entity;
			}
			set
			{
				if (((this._runLog.Entity == value) == false))
				{
					if ((this._runLog.Entity != null))
					{
						RunLog previousRunLog = this._runLog.Entity;
						this._runLog.Entity = null;
						previousRunLog.RunLogProduct.Remove(this);
					}
					this._runLog.Entity = value;
					if ((value != null))
					{
						value.RunLogProduct.Add(this);
						_runLogID = value.RunLogID;
					}
					else
					{
						_runLogID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="SERV.Tag")]
	public partial class Tag : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _tag1;
		
		private int _tagID;
		
		private EntitySet<MemberTag> _memberTag;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnTag1Changed();
		
		partial void OnTag1Changing(string value);
		
		partial void OnTagIDChanged();
		
		partial void OnTagIDChanging(int value);
		#endregion
		
		
		public Tag()
		{
			_memberTag = new EntitySet<MemberTag>(new Action<MemberTag>(this.MemberTag_Attach), new Action<MemberTag>(this.MemberTag_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_tag1", Name="Tag", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Tag1
		{
			get
			{
				return this._tag1;
			}
			set
			{
				if (((_tag1 == value) == false))
				{
					this.OnTag1Changing(value);
					this.SendPropertyChanging();
					this._tag1 = value;
					this.SendPropertyChanged("Tag1");
					this.OnTag1Changed();
				}
			}
		}
		
		[Column(Storage="_tagID", Name="TagID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int TagID
		{
			get
			{
				return this._tagID;
			}
			set
			{
				if ((_tagID != value))
				{
					this.OnTagIDChanging(value);
					this.SendPropertyChanging();
					this._tagID = value;
					this.SendPropertyChanged("TagID");
					this.OnTagIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_memberTag", OtherKey="TagID", ThisKey="TagID", Name="fk_Member_Capability_Capability1")]
		[DebuggerNonUserCode()]
		public EntitySet<MemberTag> MemberTag
		{
			get
			{
				return this._memberTag;
			}
			set
			{
				this._memberTag = value;
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void MemberTag_Attach(MemberTag entity)
		{
			this.SendPropertyChanging();
			entity.Tag = this;
		}
		
		private void MemberTag_Detach(MemberTag entity)
		{
			this.SendPropertyChanging();
			entity.Tag = null;
		}
		#endregion
	}
	
	[Table(Name="SERV.User")]
	public partial class User : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private System.Nullable<System.DateTime> _lastLoginDate;
		
		private int _memberID;
		
		private string _passwordHash;
		
		private int _userID;
		
		private int _userLevelID;
		
		private EntitySet<Message> _message;
		
		private EntitySet<RunLog> _runLog;
		
		private EntityRef<Member> _member = new EntityRef<Member>();
		
		private EntityRef<UserLevel> _userLevel = new EntityRef<UserLevel>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnLastLoginDateChanged();
		
		partial void OnLastLoginDateChanging(System.Nullable<System.DateTime> value);
		
		partial void OnMemberIDChanged();
		
		partial void OnMemberIDChanging(int value);
		
		partial void OnPasswordHashChanged();
		
		partial void OnPasswordHashChanging(string value);
		
		partial void OnUserIDChanged();
		
		partial void OnUserIDChanging(int value);
		
		partial void OnUserLevelIDChanged();
		
		partial void OnUserLevelIDChanging(int value);
		#endregion
		
		
		public User()
		{
			_message = new EntitySet<Message>(new Action<Message>(this.Message_Attach), new Action<Message>(this.Message_Detach));
			_runLog = new EntitySet<RunLog>(new Action<RunLog>(this.RunLog_Attach), new Action<RunLog>(this.RunLog_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_lastLoginDate", Name="LastLoginDate", DbType="timestamp", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> LastLoginDate
		{
			get
			{
				return this._lastLoginDate;
			}
			set
			{
				if ((_lastLoginDate != value))
				{
					this.OnLastLoginDateChanging(value);
					this.SendPropertyChanging();
					this._lastLoginDate = value;
					this.SendPropertyChanged("LastLoginDate");
					this.OnLastLoginDateChanged();
				}
			}
		}
		
		[Column(Storage="_memberID", Name="MemberID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int MemberID
		{
			get
			{
				return this._memberID;
			}
			set
			{
				if ((_memberID != value))
				{
					if (_member.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMemberIDChanging(value);
					this.SendPropertyChanging();
					this._memberID = value;
					this.SendPropertyChanged("MemberID");
					this.OnMemberIDChanged();
				}
			}
		}
		
		[Column(Storage="_passwordHash", Name="PasswordHash", DbType="varchar(45)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string PasswordHash
		{
			get
			{
				return this._passwordHash;
			}
			set
			{
				if (((_passwordHash == value) == false))
				{
					this.OnPasswordHashChanging(value);
					this.SendPropertyChanging();
					this._passwordHash = value;
					this.SendPropertyChanged("PasswordHash");
					this.OnPasswordHashChanged();
				}
			}
		}
		
		[Column(Storage="_userID", Name="UserID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int UserID
		{
			get
			{
				return this._userID;
			}
			set
			{
				if ((_userID != value))
				{
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._userID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[Column(Storage="_userLevelID", Name="UserLevelID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int UserLevelID
		{
			get
			{
				return this._userLevelID;
			}
			set
			{
				if ((_userLevelID != value))
				{
					if (_userLevel.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserLevelIDChanging(value);
					this.SendPropertyChanging();
					this._userLevelID = value;
					this.SendPropertyChanged("UserLevelID");
					this.OnUserLevelIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_message", OtherKey="SenderUserID", ThisKey="UserID", Name="fk_Message_User1")]
		[DebuggerNonUserCode()]
		public EntitySet<Message> Message
		{
			get
			{
				return this._message;
			}
			set
			{
				this._message = value;
			}
		}
		
		[Association(Storage="_runLog", OtherKey="CreatedByUserID", ThisKey="UserID", Name="fk_RunLog_User1")]
		[DebuggerNonUserCode()]
		public EntitySet<RunLog> RunLog
		{
			get
			{
				return this._runLog;
			}
			set
			{
				this._runLog = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_member", OtherKey="MemberID", ThisKey="MemberID", Name="fk_User_Member", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Member Member
		{
			get
			{
				return this._member.Entity;
			}
			set
			{
				if (((this._member.Entity == value) == false))
				{
					if ((this._member.Entity != null))
					{
						Member previousMember = this._member.Entity;
						this._member.Entity = null;
						previousMember.User.Remove(this);
					}
					this._member.Entity = value;
					if ((value != null))
					{
						value.User.Add(this);
						_memberID = value.MemberID;
					}
					else
					{
						_memberID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_userLevel", OtherKey="UserLevelID", ThisKey="UserLevelID", Name="fk_User_UserLevel1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public UserLevel UserLevel
		{
			get
			{
				return this._userLevel.Entity;
			}
			set
			{
				if (((this._userLevel.Entity == value) == false))
				{
					if ((this._userLevel.Entity != null))
					{
						UserLevel previousUserLevel = this._userLevel.Entity;
						this._userLevel.Entity = null;
						previousUserLevel.User.Remove(this);
					}
					this._userLevel.Entity = value;
					if ((value != null))
					{
						value.User.Add(this);
						_userLevelID = value.UserLevelID;
					}
					else
					{
						_userLevelID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void Message_Attach(Message entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void Message_Detach(Message entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
		
		private void RunLog_Attach(RunLog entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void RunLog_Detach(RunLog entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
		#endregion
	}
	
	[Table(Name="SERV.UserLevel")]
	public partial class UserLevel : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _userLevel1;
		
		private int _userLevelID;
		
		private EntitySet<User> _user;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnUserLevel1Changed();
		
		partial void OnUserLevel1Changing(string value);
		
		partial void OnUserLevelIDChanged();
		
		partial void OnUserLevelIDChanging(int value);
		#endregion
		
		
		public UserLevel()
		{
			_user = new EntitySet<User>(new Action<User>(this.User_Attach), new Action<User>(this.User_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_userLevel1", Name="UserLevel", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string UserLevel1
		{
			get
			{
				return this._userLevel1;
			}
			set
			{
				if (((_userLevel1 == value) == false))
				{
					this.OnUserLevel1Changing(value);
					this.SendPropertyChanging();
					this._userLevel1 = value;
					this.SendPropertyChanged("UserLevel1");
					this.OnUserLevel1Changed();
				}
			}
		}
		
		[Column(Storage="_userLevelID", Name="UserLevelID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int UserLevelID
		{
			get
			{
				return this._userLevelID;
			}
			set
			{
				if ((_userLevelID != value))
				{
					this.OnUserLevelIDChanging(value);
					this.SendPropertyChanging();
					this._userLevelID = value;
					this.SendPropertyChanged("UserLevelID");
					this.OnUserLevelIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_user", OtherKey="UserLevelID", ThisKey="UserLevelID", Name="fk_User_UserLevel1")]
		[DebuggerNonUserCode()]
		public EntitySet<User> User
		{
			get
			{
				return this._user;
			}
			set
			{
				this._user = value;
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void User_Attach(User entity)
		{
			this.SendPropertyChanging();
			entity.UserLevel = this;
		}
		
		private void User_Detach(User entity)
		{
			this.SendPropertyChanging();
			entity.UserLevel = null;
		}
		#endregion
	}
	
	[Table(Name="SERV.VehicleType")]
	public partial class VehicleType : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private sbyte _enabled;
		
		private string _vehicleType1;
		
		private int _vehicleTypeID;
		
		private EntitySet<RunLog> _runLog;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnEnabledChanged();
		
		partial void OnEnabledChanging(sbyte value);
		
		partial void OnVehicleType1Changed();
		
		partial void OnVehicleType1Changing(string value);
		
		partial void OnVehicleTypeIDChanged();
		
		partial void OnVehicleTypeIDChanging(int value);
		#endregion
		
		
		public VehicleType()
		{
			_runLog = new EntitySet<RunLog>(new Action<RunLog>(this.RunLog_Attach), new Action<RunLog>(this.RunLog_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_enabled", Name="Enabled", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public sbyte Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if ((_enabled != value))
				{
					this.OnEnabledChanging(value);
					this.SendPropertyChanging();
					this._enabled = value;
					this.SendPropertyChanged("Enabled");
					this.OnEnabledChanged();
				}
			}
		}
		
		[Column(Storage="_vehicleType1", Name="VehicleType", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string VehicleType1
		{
			get
			{
				return this._vehicleType1;
			}
			set
			{
				if (((_vehicleType1 == value) == false))
				{
					this.OnVehicleType1Changing(value);
					this.SendPropertyChanging();
					this._vehicleType1 = value;
					this.SendPropertyChanged("VehicleType1");
					this.OnVehicleType1Changed();
				}
			}
		}
		
		[Column(Storage="_vehicleTypeID", Name="VehicleTypeID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int VehicleTypeID
		{
			get
			{
				return this._vehicleTypeID;
			}
			set
			{
				if ((_vehicleTypeID != value))
				{
					this.OnVehicleTypeIDChanging(value);
					this.SendPropertyChanging();
					this._vehicleTypeID = value;
					this.SendPropertyChanged("VehicleTypeID");
					this.OnVehicleTypeIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_runLog", OtherKey="VehicleTypeID", ThisKey="VehicleTypeID", Name="fk_RunLog_VehicleType1")]
		[DebuggerNonUserCode()]
		public EntitySet<RunLog> RunLog
		{
			get
			{
				return this._runLog;
			}
			set
			{
				this._runLog = value;
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void RunLog_Attach(RunLog entity)
		{
			this.SendPropertyChanging();
			entity.VehicleType = this;
		}
		
		private void RunLog_Detach(RunLog entity)
		{
			this.SendPropertyChanging();
			entity.VehicleType = null;
		}
		#endregion
	}
}
