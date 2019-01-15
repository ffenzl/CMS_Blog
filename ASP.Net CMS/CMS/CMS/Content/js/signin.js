$(document).ready(function() {
	$(document).on('submit','.form-signin',function(){
		var email = $("#inputEmail").val();
		var password = $("#inputPassword").val();
		console.log(email);
		console.log(password);
		if(email == "a@a" && password == "a")
			window.location.href = "file:///C:/Users/is201/Documents/GitHub/CMS_Blog/backend.html#";
		return false;
	});
});