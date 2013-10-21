
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
