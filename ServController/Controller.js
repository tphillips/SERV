var map;
var trafficLayer;
var weatherLayer;
var bikeIcon = "atv.png";
var homeIcon = "home-2.png";
var hospIcon = "hospital-building.png";
var pinIcon = "pin.png";
var poiIcon = "group-2.png";
var selectMessage = "Please select a Rider / Driver first";
var selectedRider;
var geocoder = new google.maps.Geocoder();
var notesVisible = false;
var trafficVisible = true;
var weatherVisible = false;
var logVisible = false;
var riders = new Array();
var pois = new Array();
var wayPointMarkers = new Array();
var wayPoints = new Array();
var calls = new Array();
var callIndex = 0;

var locations = [
	"NBS Tooting",
	"Kent",
	"Sussex",
	"St Peter's",
	"Frimley Park",
	"St Thomas's",
	"RSCH",
	"Royal Surrey",
	"Hooley Handover",
	"Tooting",
	"Holy Cross, Haslemere",
	"Maidstone",
	"William Harvey",
	"Colindale",
	"Kings",
	"Medway",
	"Haywards Heath",
	"Kent & Canterbury",
	"Farningham",
	"New Malden",
	"Darent Valley",
	"Guys",
	"Princess Royal",
	"Worthing",
	"St George's",
	"Penbury",
	"QEQM",
	"ESH",
	"East Surrey",
	"Redhill Aerodrome",
	"KSAA"
];

var consignments = [
	"1 x Blood",
	"2 x Blood",
	"3 x Blood",
	"4 x Blood",
	"1 x Platelets",
	"2 x Platelets",
	"3 x Platelets",
	"4 x Platelets",
	"1 x Sample",
	"1 x Other",
	"2 x Sample",
	"2 x Other",
	"1 x Blood, 1 x Platelets",
	"1 x FPP",
	"2 x FPP",
	"3 x FPP",
	"4 x FPP"
];

$(function() 
{
	$(".menu").menu();
	$(".menu").menu("collapse");
	$(".button").button();
	$("#addRiderDialog").dialog({
		show: { effect: "clip", duration: 200 },
		hide: { effect: "slide", duration: 200 },
		autoOpen: false
	});
	$("#locationDialog").dialog({
		show: { effect: "clip", duration: 200 },
		hide: { effect: "slide", duration: 200 },
		autoOpen: false
	});
	$("#logCallDialog").dialog({
		show: { effect: "clip", duration: 200 },
		hide: { effect: "slide", duration: 200 },
		autoOpen: false
	});
	$("#helpDialog").dialog({
		width:550,
		show: { effect: "clip", duration: 200 },
		hide: { effect: "slide", duration: 200 },
		autoOpen: false
	});
	$("#routeDialog").dialog({
		width:550,
		show: { effect: "clip", duration: 200 },
		hide: { effect: "slide", duration: 200 },
		autoOpen: false
	});
	$(".locationAC").autocomplete({source: locations });
	$(".consigmentAC").autocomplete({source: consignments });
	initialize();
	addPOIs();
	window.onbeforeunload = function(){ return 'You will lose all rider positions and logging' };
	window.setTimeout('$("#menuDiv").slideDown();', 2000);
	window.setTimeout('showHelp()', 3000);
	window.setTimeout('showHideNotes()', 4000);
});

function initialize() 
{
	var mapOptions = {
		center: new google.maps.LatLng(51.501974479325135,-0.13295898437502274),
		zoom: 10,
		mapTypeId: google.maps.MapTypeId.ROADMAP
	};
	map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
	trafficLayer = new google.maps.TrafficLayer();
	trafficLayer.setMap(map);
	weatherLayer = new google.maps.weather.WeatherLayer({
		temperatureUnits: google.maps.weather.TemperatureUnit.CELSIUS
	});
	//weatherLayer.setMap(map);
}

function showHideTraffic()
{
	if (trafficVisible)
	{
		trafficLayer.setMap(null);
	}
	else
	{
		trafficLayer.setMap(map);
	}
	trafficVisible = !trafficVisible;
}

function showHideWeather()
{
	if (weatherVisible)
	{
		weatherLayer.setMap(null);
	}
	else
	{
		weatherLayer.setMap(map);
	}
	weatherVisible = !weatherVisible;
}

function showLoadRouteFile()
{
	$("#routeDialog").dialog("open");
}

function loadRouteFile()
{
	wayPointMarkers = new Array();
	wayPoints = new Array();
	var route = $("#txtRoute").val();
	var lines = route.split("\n");
	for (var x = 0; x < lines.length; x++)
	{
		var line = lines[x].trim();
		if (line != "")
		{
			var bits = line.split("|");
			if (bits.length>1)
			{
				var lat = "00000000" + bits[1];
				var ln = "00000000" + bits[0];
				lat = lat.replace("-","");
				ln = ln.replace("-","");
				lat = lat.replace("+","");
				ln = ln.replace("+","");
				lat = lat.substring(0,lat.length -5) + "." + lat.substring(lat.length -5);
				ln = ln.substring(0,ln.length -5) + "." + ln.substring(ln.length -5);
				if (bits[1].substring(0,1) == '-') { lat = "-" + lat; }
				if (bits[0].substring(0,1) == '-') { ln = "-" + ln; }
				var latLon = new google.maps.LatLng(lat, ln);
				var marker = new google.maps.Marker({
					position: latLon,
					icon: pinIcon,
					draggable: false,
					title:bits[2],
					map: map,
					animation: google.maps.Animation.DROP
				});
				wayPoints[wayPoints.length] = latLon;
				wayPointMarkers[wayPointMarkers.length] = marker;
			}
		}
	}
	var route = new google.maps.Polyline({
		path: wayPoints,
		strokeColor: "#FF0000",
		strokeOpacity: 1.0,
		strokeWeight: 2
	});
	route.setMap(map);
	$("#routeDialog").dialog("close");
}


function showHideLog()
{
	if (logVisible)
	{
		$("#Calls").slideUp();
	}
	else
	{
		$("#Calls").slideDown();
	}
	logVisible = !logVisible;
}

function showHelp()
{
	$("#helpDialog").dialog("open");
}

function addPOIs()
{
	addHospital(new google.maps.LatLng(51.3191188, -0.7417103000000225), "Frimley Park");
	addHospital(new google.maps.LatLng(51.24102, -0.6074419999999918), "Royal Surrey");
	addHospital(new google.maps.LatLng(51.4278088, -0.17581829999994625), "NBS Tooting");
	addHospital(new google.maps.LatLng(51.37906839999999, -0.5287485000000061), "St Peter's");
	addHospital(new google.maps.LatLng(51.49790789999999, -0.11967070000002877), "St Thomas's");
	addHospital(new google.maps.LatLng(51.50356, -0.08719389999998839), "Guy's");
	addHospital(new google.maps.LatLng(51.2196498, -0.16349900000000162), "East Surrey");
	addHospital(new google.maps.LatLng(51.2161202, -0.1428381000000627), "KSAA Redhill");
	addHospital(new google.maps.LatLng(51.32623253970275, -0.2731786925903634), "Epsom");
	addHospital(new google.maps.LatLng(51.380739, -0.18434300000001258), "St Helier");
	
	addPOI(new google.maps.LatLng(51.29405320883721, -0.15355908459468992), "Hooley Handover");
	addPOI(new google.maps.LatLng(51.3834925498744, 0.22220046298218676), "Farningham Handover");
	addPOI(new google.maps.LatLng(51.32293052945197, -0.7621635074096957), "Camberly Handover");
}

function addPOI(latLon, name)
{
	var marker = new google.maps.Marker({
		position: latLon,
		draggable: false,
		title:name,
		map: map,
		icon: poiIcon,
		animation: google.maps.Animation.DROP
	});
	pois[pois.length] = marker;
}

function addHospital(latLon, name)
{
	var marker = new google.maps.Marker({
		position: latLon,
		draggable: false,
		title:name,
		map: map,
		icon: hospIcon,
		animation: google.maps.Animation.DROP
	});
	pois[pois.length] = marker;
}

function showHideNotes()
{
	if (notesVisible)
	{
		$("#Notes").slideUp();
	}
	else
	{
		$("#Notes").slideDown();
	}
	notesVisible = !notesVisible;
}

function showAddRider()
{
	$("#addRiderDialog").dialog("open");
}

function selectRider(name)
{
	selectedRider = null;
	for (var x = 0; x < riders.length; x++)
	{
		if (riders[x].rider == name)
		{
			selectedRider = riders[x];
			$("#menRider").text(selectedRider.rider + "\r\n" + selectedRider.notes + ' (' + selectedRider.jobs + ' runs)');
		}
	}
}

function markRiderOut()
{
	if (selectedRider == null) { alert(selectMessage); return; }
	selectedRider.home = false;
	selectedRider.jobs ++;
	selectedRider.setIcon(bikeIcon);
	note(selectedRider.rider + ": dispatched");
}

function markRiderHome()
{
	if (selectedRider == null) { alert(selectMessage); return; }
	selectedRider.home = true;
	selectedRider.setIcon(homeIcon);
	note(selectedRider.rider + ": home");
}

function addRider(name, at, notes)
{
	geocoder.geocode( { 'address': at}, function(results, status) 
	{
		if (status == google.maps.GeocoderStatus.OK) 
		{
			var marker = new google.maps.Marker({
				position: results[0].geometry.location,
				rider: name,
				draggable: true,
				home: true,
				homeLocation: at,
				title:name,
				notes:notes,
				jobs: 0,
				map: map,
				icon: homeIcon,
				animation: google.maps.Animation.DROP
			});
			google.maps.event.addListener(marker, 'click', function() 
			{
				selectRider(marker.title);
			});
			google.maps.event.addListener(marker, 'dragend', function() 
			{
				selectRider(marker.title);
				note(selectedRider.rider + ": location set to: " + selectedRider.position);
				if (selectedRider.home)
				{
					markRiderOut();
				}
			});
			$("#txtNewRiderName").val("");
			$("#txtNewRiderLoc").val("");
			$("#txtNewRiderNotes").val("");
			$("#addRiderDialog").dialog("close");
			riders[riders.length] = marker;
			selectRider(marker.title);
			note("Rider added: " + marker.title + " @ " + marker.homeLocation + " - " + marker.notes);
		} 
		else 
		{
			alert("Google maps could not find a match for the location " + at + ". Try again using non SERV specific terms.");
			return null;
		}
	});
}

function showUpdateRiderLocation(at)
{
	if (selectedRider == null) { alert(selectMessage); return; }
	$("#locationDialog").dialog("open");
}

function showLogCall()
{
	$("#logCallDialog").dialog("open");
}

function Call() // Call "class"
{
	this.index = 0;
	this.CallDate = "";
	this.CallTime = "";
	this.Destination = "";
	this.FinalDestination = "";
	this.CollectFrom = "";
	this.Consignment = "";
	this.Rider = "";
	this.Controller = "";
	this.PickupTime = "";
	this.DropOffTime = "";
	this.TransportMethod = "";
	this.UrgencyClass = 0;
	this.Notes = "";
	
	this.log = function()
	{
		var now = new Date();
		$("#call_" + this.index + "_txtDate").val(this.CallDate);
		$("#call_" + this.index + "_txtTime").val(this.CallTime);
		$("#call_" + this.index + "_txtDest").val(this.Destination);
		$("#call_" + this.index + "_txtCon").val(this.Consignment);
		$("#call_" + this.index + "_txtFrom").val(this.CollectFrom);
		$("#call_" + this.index + "_txtUrgency").val(this.UrgencyClass);
		$("#call_" + this.index + "_txtController").val(this.Controller);
		var notes = this.FinalDestination != "" ? 
			"For " + this.FinalDestination + ". " + this.Notes :
			this.Notes;
		$("#call_" + this.index + "_txtNotes").val(notes);
	};
}

function logCall()
{
	var now = new Date();
	var call = new Call();
	call.index = callIndex;
	call.CallDate = now.format("dd/mm/yyyy");
	call.CallTime = now.format("HH:MM");
	call.CollectFrom = $('#txtCallColFrom').val();
	call.Destination = $('#txtCallDestination').val();
	call.Consignment = $('#txtCallConsign').val();
	call.Controller = $('#txtCallController').val();
	call.Notes = $('#txtCallNotes').val();
	call.FinalDestination = $('#txtCallFinalDest').val();
	call.UrgencyClass = $('#txtCallUrgency').val();
	call.log();
	calls[callIndex] = call;
	callIndex ++;
	
	note('Call from ' + $('#txtCallDestination').val() + ' needing ' + $('#txtCallConsign').val() + 
		' from '  + $('#txtCallColFrom').val() + '. Deliver to ' + $('#txtCallDestination').val() + ' - ' + $('#txtCallNotes').val());
		
	$('#txtCallColFrom').val("");
	$('#txtCallConsign').val("");
	$('#txtCallDestination').val("");
	$('#txtCallNotes').val("");
	
	$("#logCallDialog").dialog("close");
	
	$("#Calls").slideUp();
	$("#call_" + (callIndex -1)).slideDown();
	$('#Calls').animate({ height: 'show', opacity: 0.8 }, 300);
	logVisible = true;
}

function updateRiderLocation(at, home)
{
	if (selectedRider == null) { alert(selectMessage); return; }
	geocoder.geocode( { 'address': at}, function(results, status) 
	{
		if (status == google.maps.GeocoderStatus.OK) 
		{
			$("#info").text(results[0].geometry.location);
			selectedRider.setPosition(results[0].geometry.location);
			note(selectedRider.rider + ": location set to: " + at + '\r\n' + results[0].geometry.location);
			if (selectedRider.home && !home)
			{
				markRiderOut();
			}
			if (home)
			{
				markRiderHome();
			}
		}
		else
		{
			alert("Google maps could not find a match for the location " + at + ". Try again using non SERV specific terms.");
		}
	});
	$('#txtRiderLoc').val("");
	$("#locationDialog").dialog("close");
}

function sendRiderHome()
{
	if (selectedRider == null) { alert(selectMessage); return; }
	updateRiderLocation(selectedRider.homeLocation, true);
}

function removeRider()
{
	if (selectedRider == null) { alert(selectMessage); return; }
	note(selectedRider.name + ": off duty");
	selectedRider.setMap(null);
	selectedRider = null;
	$("#menRider").text("Rider");
}

function note(text)
{
	var now = new Date();
	$("#txtNotes").val($("#txtNotes").val() + "\r\n\r\n" + now.format("dd/mm/yyyy - HH:MM:ss") + ":\r\n" + text);
}

var dateFormat = function () {
	var	token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
		timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
		timezoneClip = /[^-+\dA-Z]/g,
		pad = function (val, len) {
			val = String(val);
			len = len || 2;
			while (val.length < len) val = "0" + val;
			return val;
		};

	// Regexes and supporting functions are cached through closure
	return function (date, mask, utc) {
		var dF = dateFormat;

		// You can't provide utc if you skip other args (use the "UTC:" mask prefix)
		if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
			mask = date;
			date = undefined;
		}

		// Passing date through Date applies Date.parse, if necessary
		date = date ? new Date(date) : new Date;
		if (isNaN(date)) throw SyntaxError("invalid date");

		mask = String(dF.masks[mask] || mask || dF.masks["default"]);

		// Allow setting the utc argument via the mask
		if (mask.slice(0, 4) == "UTC:") {
			mask = mask.slice(4);
			utc = true;
		}

		var	_ = utc ? "getUTC" : "get",
			d = date[_ + "Date"](),
			D = date[_ + "Day"](),
			m = date[_ + "Month"](),
			y = date[_ + "FullYear"](),
			H = date[_ + "Hours"](),
			M = date[_ + "Minutes"](),
			s = date[_ + "Seconds"](),
			L = date[_ + "Milliseconds"](),
			o = utc ? 0 : date.getTimezoneOffset(),
			flags = {
				d:    d,
				dd:   pad(d),
				ddd:  dF.i18n.dayNames[D],
				dddd: dF.i18n.dayNames[D + 7],
				m:    m + 1,
				mm:   pad(m + 1),
				mmm:  dF.i18n.monthNames[m],
				mmmm: dF.i18n.monthNames[m + 12],
				yy:   String(y).slice(2),
				yyyy: y,
				h:    H % 12 || 12,
				hh:   pad(H % 12 || 12),
				H:    H,
				HH:   pad(H),
				M:    M,
				MM:   pad(M),
				s:    s,
				ss:   pad(s),
				l:    pad(L, 3),
				L:    pad(L > 99 ? Math.round(L / 10) : L),
				t:    H < 12 ? "a"  : "p",
				tt:   H < 12 ? "am" : "pm",
				T:    H < 12 ? "A"  : "P",
				TT:   H < 12 ? "AM" : "PM",
				Z:    utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
				o:    (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
				S:    ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
			};

		return mask.replace(token, function ($0) {
			return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
		});
	};
}();

// Some common format strings
dateFormat.masks = {
	"default":      "ddd mmm dd yyyy HH:MM:ss",
	shortDate:      "m/d/yy",
	mediumDate:     "mmm d, yyyy",
	longDate:       "mmmm d, yyyy",
	fullDate:       "dddd, mmmm d, yyyy",
	shortTime:      "h:MM TT",
	mediumTime:     "h:MM:ss TT",
	longTime:       "h:MM:ss TT Z",
	isoDate:        "yyyy-mm-dd",
	isoTime:        "HH:MM:ss",
	isoDateTime:    "yyyy-mm-dd'T'HH:MM:ss",
	isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
	dayNames: [
		"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
		"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
	],
	monthNames: [
		"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
		"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
	]
};

// For convenience...
Date.prototype.format = function (mask, utc) {
	return dateFormat(this, mask, utc);
};
