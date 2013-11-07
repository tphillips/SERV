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
		
		public Table<Calendar> Calendar
		{
			get
			{
				return this.GetTable <Calendar>();
			}
		}
		
		public Table<Duty> Duty
		{
			get
			{
				return this.GetTable <Duty>();
			}
		}
		
		public Table<FleetVehicle> FleetVehicle
		{
			get
			{
				return this.GetTable <FleetVehicle>();
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
		
		public Table<MemberDuty> MemberDuty
		{
			get
			{
				return this.GetTable <MemberDuty>();
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
		
		public Table<MemberType> MemberType
		{
			get
			{
				return this.GetTable <MemberType>();
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
		
		public Table<RunLog> RunLog
		{
			get
			{
				return this.GetTable <RunLog>();
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
	}
	
	[Table(Name="SERV.Availability")]
	public partial class Availability : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _availabilityID;
		
		private System.Nullable<sbyte> _available;
		
		private System.Nullable<int> _dayNo;
		
		private System.Nullable<int> _eveningNo;
		
		private EntitySet<Member> _member;
		
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
			_member = new EntitySet<Member>(new Action<Member>(this.Member_Attach), new Action<Member>(this.Member_Detach));
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
		
		#region Children
		[Association(Storage="_member", OtherKey="AvailabilityID", ThisKey="AvailabilityID", Name="fk_Member_Availability1")]
		[DebuggerNonUserCode()]
		public EntitySet<Member> Member
		{
			get
			{
				return this._member;
			}
			set
			{
				this._member = value;
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
		private void Member_Attach(Member entity)
		{
			this.SendPropertyChanging();
			entity.Availability = this;
		}
		
		private void Member_Detach(Member entity)
		{
			this.SendPropertyChanging();
			entity.Availability = null;
		}
		#endregion
	}
	
	[Table(Name="SERV.Calendar")]
	public partial class Calendar : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _calendarID;
		
		private int _dutyID;
		
		private EntityRef<Duty> _duty = new EntityRef<Duty>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCalendarIDChanged();
		
		partial void OnCalendarIDChanging(int value);
		
		partial void OnDutyIDChanged();
		
		partial void OnDutyIDChanging(int value);
		#endregion
		
		
		public Calendar()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_calendarID", Name="CalendarID", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CalendarID
		{
			get
			{
				return this._calendarID;
			}
			set
			{
				if ((_calendarID != value))
				{
					this.OnCalendarIDChanging(value);
					this.SendPropertyChanging();
					this._calendarID = value;
					this.SendPropertyChanged("CalendarID");
					this.OnCalendarIDChanged();
				}
			}
		}
		
		[Column(Storage="_dutyID", Name="DutyID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int DutyID
		{
			get
			{
				return this._dutyID;
			}
			set
			{
				if ((_dutyID != value))
				{
					if (_duty.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDutyIDChanging(value);
					this.SendPropertyChanging();
					this._dutyID = value;
					this.SendPropertyChanged("DutyID");
					this.OnDutyIDChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_duty", OtherKey="DutyID", ThisKey="DutyID", Name="fk_Calendar_Duty1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Duty Duty
		{
			get
			{
				return this._duty.Entity;
			}
			set
			{
				if (((this._duty.Entity == value) == false))
				{
					if ((this._duty.Entity != null))
					{
						Duty previousDuty = this._duty.Entity;
						this._duty.Entity = null;
						previousDuty.Calendar.Remove(this);
					}
					this._duty.Entity = value;
					if ((value != null))
					{
						value.Calendar.Add(this);
						_dutyID = value.DutyID;
					}
					else
					{
						_dutyID = default(int);
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
	
	[Table(Name="SERV.Duty")]
	public partial class Duty : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _duty1;
		
		private int _dutyID;
		
		private EntitySet<Calendar> _calendar;
		
		private EntitySet<MemberDuty> _memberDuty;
		
		private EntitySet<RunLog> _runLog;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnDuty1Changed();
		
		partial void OnDuty1Changing(string value);
		
		partial void OnDutyIDChanged();
		
		partial void OnDutyIDChanging(int value);
		#endregion
		
		
		public Duty()
		{
			_calendar = new EntitySet<Calendar>(new Action<Calendar>(this.Calendar_Attach), new Action<Calendar>(this.Calendar_Detach));
			_memberDuty = new EntitySet<MemberDuty>(new Action<MemberDuty>(this.MemberDuty_Attach), new Action<MemberDuty>(this.MemberDuty_Detach));
			_runLog = new EntitySet<RunLog>(new Action<RunLog>(this.RunLog_Attach), new Action<RunLog>(this.RunLog_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_duty1", Name="Duty", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Duty1
		{
			get
			{
				return this._duty1;
			}
			set
			{
				if (((_duty1 == value) == false))
				{
					this.OnDuty1Changing(value);
					this.SendPropertyChanging();
					this._duty1 = value;
					this.SendPropertyChanged("Duty1");
					this.OnDuty1Changed();
				}
			}
		}
		
		[Column(Storage="_dutyID", Name="DutyID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int DutyID
		{
			get
			{
				return this._dutyID;
			}
			set
			{
				if ((_dutyID != value))
				{
					this.OnDutyIDChanging(value);
					this.SendPropertyChanging();
					this._dutyID = value;
					this.SendPropertyChanged("DutyID");
					this.OnDutyIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_calendar", OtherKey="DutyID", ThisKey="DutyID", Name="fk_Calendar_Duty1")]
		[DebuggerNonUserCode()]
		public EntitySet<Calendar> Calendar
		{
			get
			{
				return this._calendar;
			}
			set
			{
				this._calendar = value;
			}
		}
		
		[Association(Storage="_memberDuty", OtherKey="DutyID", ThisKey="DutyID", Name="fk_Member_Duty_Duty1")]
		[DebuggerNonUserCode()]
		public EntitySet<MemberDuty> MemberDuty
		{
			get
			{
				return this._memberDuty;
			}
			set
			{
				this._memberDuty = value;
			}
		}
		
		[Association(Storage="_runLog", OtherKey="DutyID", ThisKey="DutyID", Name="fk_RunLog_Duty1")]
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
		private void Calendar_Attach(Calendar entity)
		{
			this.SendPropertyChanging();
			entity.Duty = this;
		}
		
		private void Calendar_Detach(Calendar entity)
		{
			this.SendPropertyChanging();
			entity.Duty = null;
		}
		
		private void MemberDuty_Attach(MemberDuty entity)
		{
			this.SendPropertyChanging();
			entity.Duty = this;
		}
		
		private void MemberDuty_Detach(MemberDuty entity)
		{
			this.SendPropertyChanging();
			entity.Duty = null;
		}
		
		private void RunLog_Attach(RunLog entity)
		{
			this.SendPropertyChanging();
			entity.Duty = this;
		}
		
		private void RunLog_Detach(RunLog entity)
		{
			this.SendPropertyChanging();
			entity.Duty = null;
		}
		#endregion
	}
	
	[Table(Name="SERV.FleetVehicle")]
	public partial class FleetVehicle : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _fleetVehicleID;
		
		private int _memberID;
		
		private EntityRef<Member> _member = new EntityRef<Member>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnFleetVehicleIDChanged();
		
		partial void OnFleetVehicleIDChanging(int value);
		
		partial void OnMemberIDChanged();
		
		partial void OnMemberIDChanging(int value);
		#endregion
		
		
		public FleetVehicle()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_fleetVehicleID", Name="FleetVehicleID", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int FleetVehicleID
		{
			get
			{
				return this._fleetVehicleID;
			}
			set
			{
				if ((_fleetVehicleID != value))
				{
					this.OnFleetVehicleIDChanging(value);
					this.SendPropertyChanging();
					this._fleetVehicleID = value;
					this.SendPropertyChanged("FleetVehicleID");
					this.OnFleetVehicleIDChanged();
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
		
		#region Parents
		[Association(Storage="_member", OtherKey="MemberID", ThisKey="MemberID", Name="fk_FleetVehicle_Member1", IsForeignKey=true)]
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
						previousMember.FleetVehicle.Remove(this);
					}
					this._member.Entity = value;
					if ((value != null))
					{
						value.FleetVehicle.Add(this);
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
		
		private sbyte _hospital;
		
		private string _lat;
		
		private string _lng;
		
		private string _location1;
		
		private int _locationID;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnBloodBankChanged();
		
		partial void OnBloodBankChanging(sbyte value);
		
		partial void OnChangeoverChanged();
		
		partial void OnChangeoverChanging(sbyte value);
		
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
		
		private int _memberTypeID;
		
		private string _mobileNumber;
		
		private string _nextOfKin;
		
		private string _nextOfKinAddress;
		
		private string _nextOfKinPhone;
		
		private string _notes;
		
		private string _occupation;
		
		private string _postCode;
		
		private System.Nullable<System.DateTime> _riderAssesmentPassDate;
		
		private string _town;
		
		private EntitySet<FleetVehicle> _fleetVehicle;
		
		private EntitySet<MemberTag> _memberTag;
		
		private EntitySet<MemberDuty> _memberDuty;
		
		private EntitySet<Message> _message;
		
		private EntitySet<User> _user;
		
		private EntityRef<Availability> _availability = new EntityRef<Availability>();
		
		private EntityRef<MemberStatus> _memberStatus = new EntityRef<MemberStatus>();
		
		private EntityRef<MemberType> _memberType = new EntityRef<MemberType>();
		
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
		
		partial void OnMemberTypeIDChanged();
		
		partial void OnMemberTypeIDChanging(int value);
		
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
			_fleetVehicle = new EntitySet<FleetVehicle>(new Action<FleetVehicle>(this.FleetVehicle_Attach), new Action<FleetVehicle>(this.FleetVehicle_Detach));
			_memberTag = new EntitySet<MemberTag>(new Action<MemberTag>(this.MemberTag_Attach), new Action<MemberTag>(this.MemberTag_Detach));
			_memberDuty = new EntitySet<MemberDuty>(new Action<MemberDuty>(this.MemberDuty_Attach), new Action<MemberDuty>(this.MemberDuty_Detach));
			_message = new EntitySet<Message>(new Action<Message>(this.Message_Attach), new Action<Message>(this.Message_Detach));
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
					if (_availability.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
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
					if (_memberStatus.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMemberStatusIDChanging(value);
					this.SendPropertyChanging();
					this._memberStatusID = value;
					this.SendPropertyChanged("MemberStatusID");
					this.OnMemberStatusIDChanged();
				}
			}
		}
		
		[Column(Storage="_memberTypeID", Name="MemberTypeID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int MemberTypeID
		{
			get
			{
				return this._memberTypeID;
			}
			set
			{
				if ((_memberTypeID != value))
				{
					if (_memberType.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMemberTypeIDChanging(value);
					this.SendPropertyChanging();
					this._memberTypeID = value;
					this.SendPropertyChanged("MemberTypeID");
					this.OnMemberTypeIDChanged();
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
		[Association(Storage="_fleetVehicle", OtherKey="MemberID", ThisKey="MemberID", Name="fk_FleetVehicle_Member1")]
		[DebuggerNonUserCode()]
		public EntitySet<FleetVehicle> FleetVehicle
		{
			get
			{
				return this._fleetVehicle;
			}
			set
			{
				this._fleetVehicle = value;
			}
		}
		
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
		
		[Association(Storage="_memberDuty", OtherKey="MemberID", ThisKey="MemberID", Name="fk_Member_Duty_Member1")]
		[DebuggerNonUserCode()]
		public EntitySet<MemberDuty> MemberDuty
		{
			get
			{
				return this._memberDuty;
			}
			set
			{
				this._memberDuty = value;
			}
		}
		
		[Association(Storage="_message", OtherKey="RecipientMemberID", ThisKey="MemberID", Name="fk_Message_Member1")]
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
		
		#region Parents
		[Association(Storage="_availability", OtherKey="AvailabilityID", ThisKey="AvailabilityID", Name="fk_Member_Availability1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Availability Availability
		{
			get
			{
				return this._availability.Entity;
			}
			set
			{
				if (((this._availability.Entity == value) == false))
				{
					if ((this._availability.Entity != null))
					{
						Availability previousAvailability = this._availability.Entity;
						this._availability.Entity = null;
						previousAvailability.Member.Remove(this);
					}
					this._availability.Entity = value;
					if ((value != null))
					{
						value.Member.Add(this);
						_availabilityID = value.AvailabilityID;
					}
					else
					{
						_availabilityID = null;
					}
				}
			}
		}
		
		[Association(Storage="_memberStatus", OtherKey="MemberStatusID", ThisKey="MemberStatusID", Name="fk_Member_MemberStatus1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public MemberStatus MemberStatus
		{
			get
			{
				return this._memberStatus.Entity;
			}
			set
			{
				if (((this._memberStatus.Entity == value) == false))
				{
					if ((this._memberStatus.Entity != null))
					{
						MemberStatus previousMemberStatus = this._memberStatus.Entity;
						this._memberStatus.Entity = null;
						previousMemberStatus.Member.Remove(this);
					}
					this._memberStatus.Entity = value;
					if ((value != null))
					{
						value.Member.Add(this);
						_memberStatusID = value.MemberStatusID;
					}
					else
					{
						_memberStatusID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_memberType", OtherKey="MemberTypeID", ThisKey="MemberTypeID", Name="fk_Member_MemberType1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public MemberType MemberType
		{
			get
			{
				return this._memberType.Entity;
			}
			set
			{
				if (((this._memberType.Entity == value) == false))
				{
					if ((this._memberType.Entity != null))
					{
						MemberType previousMemberType = this._memberType.Entity;
						this._memberType.Entity = null;
						previousMemberType.Member.Remove(this);
					}
					this._memberType.Entity = value;
					if ((value != null))
					{
						value.Member.Add(this);
						_memberTypeID = value.MemberTypeID;
					}
					else
					{
						_memberTypeID = default(int);
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
		private void FleetVehicle_Attach(FleetVehicle entity)
		{
			this.SendPropertyChanging();
			entity.Member = this;
		}
		
		private void FleetVehicle_Detach(FleetVehicle entity)
		{
			this.SendPropertyChanging();
			entity.Member = null;
		}
		
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
		
		private void MemberDuty_Attach(MemberDuty entity)
		{
			this.SendPropertyChanging();
			entity.Member = this;
		}
		
		private void MemberDuty_Detach(MemberDuty entity)
		{
			this.SendPropertyChanging();
			entity.Member = null;
		}
		
		private void Message_Attach(Message entity)
		{
			this.SendPropertyChanging();
			entity.Member = this;
		}
		
		private void Message_Detach(Message entity)
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
	
	[Table(Name="SERV.Member_Duty")]
	public partial class MemberDuty : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _dutyID;
		
		private int _memberDutyID;
		
		private int _memberID;
		
		private EntityRef<Duty> _duty = new EntityRef<Duty>();
		
		private EntityRef<Member> _member = new EntityRef<Member>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnDutyIDChanged();
		
		partial void OnDutyIDChanging(int value);
		
		partial void OnMemberDutyIDChanged();
		
		partial void OnMemberDutyIDChanging(int value);
		
		partial void OnMemberIDChanged();
		
		partial void OnMemberIDChanging(int value);
		#endregion
		
		
		public MemberDuty()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_dutyID", Name="DutyID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int DutyID
		{
			get
			{
				return this._dutyID;
			}
			set
			{
				if ((_dutyID != value))
				{
					if (_duty.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDutyIDChanging(value);
					this.SendPropertyChanging();
					this._dutyID = value;
					this.SendPropertyChanged("DutyID");
					this.OnDutyIDChanged();
				}
			}
		}
		
		[Column(Storage="_memberDutyID", Name="Member_DutyID", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int MemberDutyID
		{
			get
			{
				return this._memberDutyID;
			}
			set
			{
				if ((_memberDutyID != value))
				{
					this.OnMemberDutyIDChanging(value);
					this.SendPropertyChanging();
					this._memberDutyID = value;
					this.SendPropertyChanged("MemberDutyID");
					this.OnMemberDutyIDChanged();
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
		
		#region Parents
		[Association(Storage="_duty", OtherKey="DutyID", ThisKey="DutyID", Name="fk_Member_Duty_Duty1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Duty Duty
		{
			get
			{
				return this._duty.Entity;
			}
			set
			{
				if (((this._duty.Entity == value) == false))
				{
					if ((this._duty.Entity != null))
					{
						Duty previousDuty = this._duty.Entity;
						this._duty.Entity = null;
						previousDuty.MemberDuty.Remove(this);
					}
					this._duty.Entity = value;
					if ((value != null))
					{
						value.MemberDuty.Add(this);
						_dutyID = value.DutyID;
					}
					else
					{
						_dutyID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_member", OtherKey="MemberID", ThisKey="MemberID", Name="fk_Member_Duty_Member1", IsForeignKey=true)]
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
						previousMember.MemberDuty.Remove(this);
					}
					this._member.Entity = value;
					if ((value != null))
					{
						value.MemberDuty.Add(this);
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
	
	[Table(Name="SERV.MemberStatus")]
	public partial class MemberStatus : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _memberStatus1;
		
		private int _memberStatusID;
		
		private EntitySet<Member> _member;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnMemberStatus1Changed();
		
		partial void OnMemberStatus1Changing(string value);
		
		partial void OnMemberStatusIDChanged();
		
		partial void OnMemberStatusIDChanging(int value);
		#endregion
		
		
		public MemberStatus()
		{
			_member = new EntitySet<Member>(new Action<Member>(this.Member_Attach), new Action<Member>(this.Member_Detach));
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
		
		#region Children
		[Association(Storage="_member", OtherKey="MemberStatusID", ThisKey="MemberStatusID", Name="fk_Member_MemberStatus1")]
		[DebuggerNonUserCode()]
		public EntitySet<Member> Member
		{
			get
			{
				return this._member;
			}
			set
			{
				this._member = value;
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
		private void Member_Attach(Member entity)
		{
			this.SendPropertyChanging();
			entity.MemberStatus = this;
		}
		
		private void Member_Detach(Member entity)
		{
			this.SendPropertyChanging();
			entity.MemberStatus = null;
		}
		#endregion
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
	
	[Table(Name="SERV.MemberType")]
	public partial class MemberType : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _memberType1;
		
		private int _memberTypeID;
		
		private EntitySet<Member> _member;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnMemberType1Changed();
		
		partial void OnMemberType1Changing(string value);
		
		partial void OnMemberTypeIDChanged();
		
		partial void OnMemberTypeIDChanging(int value);
		#endregion
		
		
		public MemberType()
		{
			_member = new EntitySet<Member>(new Action<Member>(this.Member_Attach), new Action<Member>(this.Member_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_memberType1", Name="MemberType", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MemberType1
		{
			get
			{
				return this._memberType1;
			}
			set
			{
				if (((_memberType1 == value) == false))
				{
					this.OnMemberType1Changing(value);
					this.SendPropertyChanging();
					this._memberType1 = value;
					this.SendPropertyChanged("MemberType1");
					this.OnMemberType1Changed();
				}
			}
		}
		
		[Column(Storage="_memberTypeID", Name="MemberTypeID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int MemberTypeID
		{
			get
			{
				return this._memberTypeID;
			}
			set
			{
				if ((_memberTypeID != value))
				{
					this.OnMemberTypeIDChanging(value);
					this.SendPropertyChanging();
					this._memberTypeID = value;
					this.SendPropertyChanged("MemberTypeID");
					this.OnMemberTypeIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_member", OtherKey="MemberTypeID", ThisKey="MemberTypeID", Name="fk_Member_MemberType1")]
		[DebuggerNonUserCode()]
		public EntitySet<Member> Member
		{
			get
			{
				return this._member;
			}
			set
			{
				this._member = value;
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
		private void Member_Attach(Member entity)
		{
			this.SendPropertyChanging();
			entity.MemberType = this;
		}
		
		private void Member_Detach(Member entity)
		{
			this.SendPropertyChanging();
			entity.MemberType = null;
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
		
		private int _recipientMemberID;
		
		private string _sentDate;
		
		private EntityRef<Member> _member = new EntityRef<Member>();
		
		private EntityRef<MessageType> _messageType = new EntityRef<MessageType>();
		
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
		
		partial void OnRecipientMemberIDChanging(int value);
		
		partial void OnSentDateChanged();
		
		partial void OnSentDateChanging(string value);
		#endregion
		
		
		public Message()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_message1", Name="Message", DbType="varchar(45)", AutoSync=AutoSync.Never)]
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
		
		[Column(Storage="_messageID", Name="MessageID", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
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
		
		[Column(Storage="_recipient", Name="Recipient", DbType="varchar(45)", AutoSync=AutoSync.Never)]
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
		
		[Column(Storage="_recipientMemberID", Name="RecipientMemberID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int RecipientMemberID
		{
			get
			{
				return this._recipientMemberID;
			}
			set
			{
				if ((_recipientMemberID != value))
				{
					if (_member.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRecipientMemberIDChanging(value);
					this.SendPropertyChanging();
					this._recipientMemberID = value;
					this.SendPropertyChanged("RecipientMemberID");
					this.OnRecipientMemberIDChanged();
				}
			}
		}
		
		[Column(Storage="_sentDate", Name="SentDate", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string SentDate
		{
			get
			{
				return this._sentDate;
			}
			set
			{
				if (((_sentDate == value) == false))
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
		[Association(Storage="_member", OtherKey="MemberID", ThisKey="RecipientMemberID", Name="fk_Message_Member1", IsForeignKey=true)]
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
						previousMember.Message.Remove(this);
					}
					this._member.Entity = value;
					if ((value != null))
					{
						value.Message.Add(this);
						_recipientMemberID = value.MemberID;
					}
					else
					{
						_recipientMemberID = default(int);
					}
				}
			}
		}
		
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
	
	[Table(Name="SERV.RunLog")]
	public partial class RunLog : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private System.Nullable<System.DateTime> _dateTime;
		
		private int _dutyID;
		
		private int _runLogID;
		
		private EntityRef<Duty> _duty = new EntityRef<Duty>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnDateTimeChanged();
		
		partial void OnDateTimeChanging(System.Nullable<System.DateTime> value);
		
		partial void OnDutyIDChanged();
		
		partial void OnDutyIDChanging(int value);
		
		partial void OnRunLogIDChanged();
		
		partial void OnRunLogIDChanging(int value);
		#endregion
		
		
		public RunLog()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_dateTime", Name="DateTime", DbType="timestamp", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> DateTime
		{
			get
			{
				return this._dateTime;
			}
			set
			{
				if ((_dateTime != value))
				{
					this.OnDateTimeChanging(value);
					this.SendPropertyChanging();
					this._dateTime = value;
					this.SendPropertyChanged("DateTime");
					this.OnDateTimeChanged();
				}
			}
		}
		
		[Column(Storage="_dutyID", Name="DutyID", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int DutyID
		{
			get
			{
				return this._dutyID;
			}
			set
			{
				if ((_dutyID != value))
				{
					if (_duty.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDutyIDChanging(value);
					this.SendPropertyChanging();
					this._dutyID = value;
					this.SendPropertyChanged("DutyID");
					this.OnDutyIDChanged();
				}
			}
		}
		
		[Column(Storage="_runLogID", Name="RunLogID", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
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
		
		#region Parents
		[Association(Storage="_duty", OtherKey="DutyID", ThisKey="DutyID", Name="fk_RunLog_Duty1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Duty Duty
		{
			get
			{
				return this._duty.Entity;
			}
			set
			{
				if (((this._duty.Entity == value) == false))
				{
					if ((this._duty.Entity != null))
					{
						Duty previousDuty = this._duty.Entity;
						this._duty.Entity = null;
						previousDuty.RunLog.Remove(this);
					}
					this._duty.Entity = value;
					if ((value != null))
					{
						value.RunLog.Add(this);
						_dutyID = value.DutyID;
					}
					else
					{
						_dutyID = default(int);
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
}
