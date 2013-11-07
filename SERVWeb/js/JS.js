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
