function DisplayMember(memberId)
{
	callServerSide(
		"Service/Service.asmx/GetMember", 
		"{'memberId':" + memberId + "}",
		function(json)
		{
			$("#lblTitle").text(json.d.FirstName + " " + json.d.LastName);
			$("#txtFirstName").val(json.d.FirstName);
			$("#txtLastName").val(json.d.LastName);
			$("#txtEmail").val(json.d.EmailAddress);
			$("#txtMobile").val(json.d.MobileNumber);
			$("#txtHomePhone").val(json.d.HomeNumber);
			$("#txtBirthYear").val(json.d.BirthYear);
			
			$("#txtAddress1").val(json.d.Address1);
			$("#txtAddress2").val(json.d.Address2);
			$("#txtAddress3").val(json.d.Address3);
			$("#txtTown").val(json.d.Town);
			$("#txtCounty").val(json.d.County);
			$("#txtPostCode").val(json.d.PostCode);
			
			$("#txtOccupation").val(json.d.Occupation);
			$("#txtNOK").val(json.d.NextOfKin);
			$("#txtNOKAddress").val(json.d.NextOfKinAddress);
			$("#txtNOKPhone").val(json.d.NextOfKinPhone);
			
			$("#loading").slideUp();
			$("#entry").slideDown();
		},
		function()
		{
		}
	);
}

function SaveBasicMember(memberId)
{
	$("#loading").slideDown();
	$("#entry").slideUp();
	var json = JsonifyBasicMemberFromForm(memberId);
	callServerSide(
		"Service/Service.asmx/SaveMember", 
		json,
		function(json)
		{
			$("#loading").slideUp();
			$("#success").slideDown();
		},
		function()
		{
			$("#loading").slideUp();
			$("#error").slideDown();
		}
	);
}

function JsonifyBasicMemberFromForm(memberId)
{
	return '{"member":{"MemberID":' + memberId + ',' + 
		'"FirstName":"' + $("#txtFirstName").val() + '","LastName":"' + $("#txtLastName").val() + '",' + 
		'"EmailAddress":"' + $("#txtEmail").val() + '","MobileNumber":"' + $("#txtMobile").val() + '",' + 
		'"HomeNumber":"' + $("#txtHomePhone").val() + '","Occupation":"' + $("#txtOccupation").val() + '",' +
		'"Address1":"' + $("#txtAddress1").val() + '","Address2":"' + $("#txtAddress2").val() + '","Address3":"' + $("#txtAddress3").val() + '",' + 
		'"Town":"' + $("#txtTown").val() + '","County":"' + $("#txtCounty").val() + '","PostCode":"' + $("#txtPostCode").val() + '",' + 
		'"BirthYear":' + $("#txtBirthYear").val() + ',"NextOfKin":"' + $("#txtNOK").val() + '","NextOfKinAddress":"' + $("#txtNOKAddress").val() + '",' +
		'"NextOfKinPhone":"' + $("#txtNOKPhone").val() + '"}}';
}

function callServerSide(url, data, success, error)
{
	$.ajax({
		type: "POST",
		url: url,
		data: data,
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: success,
		error: error
	});
}
