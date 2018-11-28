var folder = "images/bilder/";

$.ajax({
    url : folder,
    success: function (data) {
        $(data).find("a").attr("href", function (i, val) {
	        if( val.match(/\.(jpe?g|png|gif)$/) ) { 
	        	var bilder = "";
	        	bilder += '<div class="col-lg-3 col-md-4 col-xs-6 thumb">';
                bilder += '<a class="thumbnail" href="#" data-image-id="" data-toggle="modal" data-title=""';
                bilder += '   data-image="https://images.pexels.com/photos/853168/pexels-photo-853168.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"';
                bilder += '   data-target="#image-gallery">';
                bilder += '    <img class="img-thumbnail"';
                bilder += '         src="'+ folder + val +'"';
                bilder += '         alt="Another alt text">';
                bilder += '</a>';
            	bilder += '</div>';
	            $(".bilderRow").append( bilder );
	        } 
        });
    }
});

//Devexteme File Upload
$(function(){
    var fileUploader = $("#file-uploader").dxFileUploader({
        multiple: true,
        accept: "*",
        value: [],
        uploadMode: "instantly",
        uploadUrl: "https://js.devexpress.com/Content/Services/upload.aspx",
        onValueChanged: function(e) {
            var files = e.value;
            if(files.length > 0) {
                $("#selected-files .selected-item").remove();
                $.each(files, function(i, file) {
                    var $selectedItem = $("<div />").addClass("selected-item");
                    $selectedItem.append(
                        $("<span />").html("Name: " + file.name + "<br/>"),
                        $("<span />").html("Größe " + file.size + " bytes" + "<br/>"),
                        $("<span />").html("Typ " + file.type + "<br/>"),
                        $("<span />").html("Zuletzt verändert: " + file.lastModifiedDate)
                    );
                    $selectedItem.appendTo($("#selected-files"));
                });
                $("#selected-files").show();
            }
            else
                $("#selected-files").hide();
        }
    }).dxFileUploader("instance");
    
    $("#accept-option").dxSelectBox({
        dataSource: [
            {name: "Images", value: "image/*"}, 
            {name: "Audio", value: "audio/*"}, 
            {name: "Videos", value: "video/*"}
        ],
        valueExpr: "value",
        displayExpr: "name",
        value: "*",
        onValueChanged: function(e) {
            fileUploader.option("accept", e.value);
        }
    });
});